using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using STM.Core;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using System.Reflection;
using Npgsql;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public  class PackResultRepository : IPackResultsRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public PackResultRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory; 
        }

         public async Task<IQueryable<PackResult>> GetPackResults(int packId, int projectId, DateTime fromDate, DateTime toDate)
        {

            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open(); 
                
                var packResults = await dbConnection.QueryAsync<PackResult>("with A as (select tr.testgroupid,case when string_agg(distinct status,',') like '%FAIL%' then 'FAIL' when string_agg(distinct status,',') like '%SKIP%' then 'FAIL' else 'PASS' end as status,packid from tblresults tr where packid = '"+packId + "' group by tr.testgroupid,packid), B as (select testgroupid, round(coalesce(Cast(coalesce(sum(stepspassed), 0) as numeric) / (Cast(nullif(sum(totalSteps) , 0) as numeric)) * 100, 0), 2)  PassPercentage, round(coalesce(Cast(coalesce(sum(stepsfailed), 0) as numeric) / (Cast(nullif(sum(totalSteps), 0) as numeric)) * 100, 0), 2)  FailPercentage, round(coalesce(Cast(coalesce(sum(stepsskipped), 0) as numeric) / Cast(nullif(sum(totalSteps), 0) as numeric) * 100, 0), 2)  SkipPercentage, sum(stepspassed) as PassedSteps, sum(stepsfailed) as FailedSteps, sum(stepsskipped) as SkippedSteps from tblcurrprojs   group by testgroupid), C as (select tcp.testgroupid,ut.username,ut.runat,ut.rundate,tcp.status as ProgressStatus,tcp.error, ROW_NUMBER() OVER(PARTITION BY ut.TestGroupId ORDER BY RunDate DESC) AS rn from tblcurrprojs tcp left outer join  tblusertestrun ut on tcp.testgroupid = ut.testgroupid where(ut.RunDate, ut.RunDate) OVERLAPS('" + fromDate+"':: DATE, '"+toDate+ "'::DATE + 1) and tcp.status not in ('STARTED', 'IN PROGRESS')) select a.*,b.passPercentage,b.failPercentage,b.skipPercentage,b.PassedSteps,b.FailedSteps,b.SkippedSteps,username,a.packid,c.runat,rundate,rn,c.ProgressStatus,c.error,st.packname, tp.projectid, tp.projectname,tp.countryid as entityId,st.type_of_pack as typeOfPackId from a left outer join B on  a.testgroupid = b.testgroupid left outer join c on a.testgroupid = c.testgroupid inner join tblpacks st on a.packid = st.packid inner join tblprojects tp on st.projectid = tp.projectid where rn = 1 order by rundate desc; ");
                //SELECT tblpak.packname,  * FROM tblcurrprojs tblprojts inner join tblpacks tblpak on tblprojts.packid = tblpak.packid where cast(tblprojts.curprojid as integer)=" + projectId + " and cast(tblprojts.packid as integer) =" + packId + " and tblprojts.starttime::timestamp::date>=" + " cast( '"+fromDate.ToString("yyyy/MM/dd")+ "' as date) " + " and tblprojts.starttime::timestamp::date<=" + " cast('" +DateTime.Now.ToString("yyyy/MM/dd") + "' as date) 
                return packResults.AsQueryable();
            }

        }
        public async Task<IQueryable<StepDetails>> GetSteps(int packId, string subScenarioId, string testGroupId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();

                var steps = await dbConnection.QueryAsync<StepDetails>("select distinct tr.TestStepId, tr.Description, tr.Keyword, tr.ObjectName, tr.TestData, tr.ObjectValue, tr.Status, 'Screenshot' as Screenshot, tr.\"Exception\" from tblResults tr left outer join tblTestSteps tss on tr.TestStepID = tss.TestStepId where tr.Sub_Scen_Id = '" + subScenarioId + "' and tr.PackID = '" + packId + "' and tr.TestGroupId = '" + testGroupId + "'");
                //select distinct tr.TestStepID, tr.Description, tr.Keyword, tr.ObjectName, tr.TestData, tr.ObjectValue, tr.Status, 'Screenshot' as Screenshot, tr.\"Exception\" from tblResults tr left outer join tblTestSteps tss on tr.TestStepID = tss.TestStepId where tr.Sub_Scen_Id = '" + ssId + "' and tr.PackID = '" + pkId + "' and tr.TestGroupId = '" + trId + "'"
                return steps.AsQueryable();
            }

        }
        public async Task<IQueryable<ScenarioResult>> GetScenarioWithStatusResults(string testGroupId)
        {

            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();

                var scenarioResults = await dbConnection.QueryAsync<ScenarioResult>("select distinct tr.status,tr.teststepid,tr.sub_scen_id as subscenarioId,tr.scen_id as scenarioId,ts.mainscenarioname,tss.subscenarioname from tblresults tr inner join tblusertestrun tusr on tr.testgroupid=tusr.testgroupid inner join tblscenarios ts on ts.scen_id=tr.scen_id left join tblsubscenarios tss on tr.sub_scen_id =tss.sub_scen_id where tr.testgroupid= '" + testGroupId + "' order by tr.scen_id,tr.sub_scen_id");
                //SELECT tblpak.packname,  * FROM tblcurrprojs tblprojts inner join tblpacks tblpak on tblprojts.packid = tblpak.packid where cast(tblprojts.curprojid as integer)=" + projectId + " and cast(tblprojts.packid as integer) =" + packId + " and tblprojts.starttime::timestamp::date>=" + " cast( '"+fromDate.ToString("yyyy/MM/dd")+ "' as date) " + " and tblprojts.starttime::timestamp::date<=" + " cast('" +DateTime.Now.ToString("yyyy/MM/dd") + "' as date) 
                return scenarioResults.AsQueryable();
            }

        }

        public async Task<List<PieChartData>> GetPieChartDetails(string packId,List<string> testGroupIds)
        {
            List<PieChartData> pieChartlst = new List<PieChartData>();
            using(IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                foreach(var testGroupId in testGroupIds)
                {
                    var pieChart = await dbConnection.QueryFirstOrDefaultAsync<PieChartData>("select ttr.PackName as SuiteName,ttr.testgroupid, ttr.Browser, tr.CreatedDate, cc.Country,ttt.TestType, te.Environmentname,tcp.startTime as StartTime, tcp.endTime as EndTime from tblTestRun ttr left outer join tblResults tr on ttr.TestGroupId = tr.TestGroupId left outer join tblPacks tp on cast(ttr.PackId as integer) = cast(tp.PackId as integer) left outer join tblProjects tpj on cast(ttr.ProjectId as integer) = cast(tpj.ProjectId as integer) left outer join tblTestTypes ttt on tp.type_of_pack = ttt.\"Id\"  left outer join (select min(starttime) as starttime,max(endtime) as endtime,testgroupid from tblcurrprojs group by testgroupid) as  tcp on ttr.testgroupid = tcp.testgroupid left outer join tblTestRunTemp1 ttr1 on ttr.TestGroupId = ttr1.TestGroupId left outer join tblEnvironment te on ttr1.Environment = te.EnvironmentId left outer join Countries cc on tpj.CountryId = cc.\"Id\"  where ttr.PackId = '" + packId + "' and ttr.TestGroupId = '" + testGroupId + "' limit 1");
                    pieChartlst.Add(pieChart);
                }
                return pieChartlst;
            }
        }








    }
}
