using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using STM.Core.EntityLayer;

namespace STM.Core.Repositories
{
    public class RunSuiteRepository : IRunSuiteRepository
    {
        //private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public RunSuiteRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<Countries>> GetEntities()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<Countries>("select \"Id\", Country from Countries order by country asc;");

                return urls.AsQueryable();
            }
        }

        public async Task<IQueryable<RunSuite>> GetRunSuitesByEntity(string entityId,string packId,string username)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                IEnumerable<RunSuite> runSuite = Enumerable.Empty<RunSuite>();
                string query = string.Empty;

                if (Convert.ToInt32(entityId) > 0)
                {

                    if (Convert.ToInt32(packId) > 0)
                    {
                        query = "select * from (select st.PackId,st.scenario_subscenario, st.PackName as PackName,pr.ProjectId, pr.ProjectName as ProjectName,st.RunAt, ROW_NUMBER() OVER(PARTITION BY tr.PackId ORDER BY tr.PackId DESC)  from tblPacks st inner join tblProjects pr on pr.ProjectId = st.ProjectId inner join tblresults tr on st.packid =tr.packid   where (pr.ProjectOwner = '" + username + "' or pr.ProjectId in (select ProjectId from tblUserProjects where UserId in (select UserId from tblUsers where UserName = '"+username+"'))) and pr.CountryId = '" + entityId + "' and st.type_of_pack = '" + packId + "') a where row_number = 1";
                    }
                    else
                    {
                        query = "select * from (select st.PackId,st.scenario_subscenario, st.PackName as PackName,pr.ProjectId, pr.ProjectName as ProjectName,st.RunAt, ROW_NUMBER() OVER(PARTITION BY PackId ORDER BY PackId DESC)  from tblPacks st inner join tblProjects pr on pr.ProjectId = st.ProjectId where (pr.ProjectOwner = '" + username + "' or pr.ProjectId in (select ProjectId from tblUserProjects where UserId in (select UserId from tblUsers where UserName = '" + username + "'))) and pr.CountryId = '" + entityId + "') a where row_number = 1";
                    }
                }
                else
                {

                    if (Convert.ToInt32(packId) > 0)
                    {
                        query = "select * from (select st.PackId,st.scenario_subscenario, st.PackName as PackName,pr.ProjectId, pr.ProjectName as ProjectName,st.RunAt, ROW_NUMBER() OVER(PARTITION BY PackId ORDER BY PackId DESC)  from tblPacks st inner join tblProjects pr on pr.ProjectId = st.ProjectId where (pr.ProjectOwner = '" + username + "' or pr.ProjectId in (select ProjectId from tblUserProjects where UserId in (select UserId from tblUsers where UserName = '" + username + "'))) and st.type_of_pack = '" + packId + "') a where row_number = 1";
                    }

                    else
                    {
                        query = "select * from (select st.PackId,st.scenario_subscenario, st.PackName as PackName,pr.ProjectId, pr.ProjectName as ProjectName,st.RunAt, ROW_NUMBER() OVER(PARTITION BY PackId ORDER BY PackId DESC) from tblPacks st inner join tblProjects pr on pr.ProjectId = st.ProjectId where pr.ProjectOwner = '" + username + "' or pr.ProjectId in (select ProjectId from tblUserProjects where UserId in (select UserId from tblUsers where UserName = '" + username + "'))) a where row_number = 1";
                    }

                }

                runSuite = await dbConnection.QueryAsync<RunSuite>(query);
                //if (Convert.ToInt32(packId) > -1)
                //{
                //    runSuite = await dbConnection.QueryAsync<RunSuite>("select * from (select st.PackId, st.PackName as PackName,pr.ProjectId, concat(substring(sc.MainScenarioName,1,20), '...') as MainScenarioName, concat(substring(ss.SubScenarioName,1,20), '...') as SubScenarioName,pr.ProjectName as ProjectName,st.RunAt, ROW_NUMBER() OVER(PARTITION BY PackId ORDER BY PackId DESC)  from tblPacks st inner join tblProjects pr on pr.ProjectId = st.ProjectId inner join tblScenarios sc on cast(split_part(st.Scenarios, ',', '1') as varchar) = cast(sc.scen_id as varchar) inner join tblSubScenarios ss on cast(split_part(st.SubScenarios, ',', 1) as varchar)= cast(ss.SubScenarioId as varchar)  where (pr.ProjectOwner = 'admin' or pr.ProjectId in (select ProjectId from tblUserProjects where UserId in (select UserId from tblUsers where UserName = 'admin'))) and pr.CountryId = '" + entityId + "') a where row_number = 1");
                //}
                //else
                //{
                //    runSuite = await dbConnection.QueryAsync<RunSuite>("select * from (select st.PackId, st.PackName as PackName,pr.ProjectId, concat(substring(sc.MainScenarioName,1,20), '...') as MainScenarioName, concat(substring(ss.SubScenarioName,1,20), '...')  as SubScenarioName,pr.ProjectName as ProjectName,st.RunAt, ROW_NUMBER() OVER(PARTITION BY PackId ORDER BY PackId DESC)  from tblPacks st inner join tblProjects pr on pr.ProjectId = st.ProjectId inner join tblScenarios sc on cast(split_part(st.Scenarios, ',', '1') as varchar) = cast(sc.scen_id as varchar) inner join tblSubScenarios ss on cast(split_part(st.SubScenarios, ',', 1) as varchar)= cast(ss.SubScenarioId as varchar)   where (pr.ProjectOwner = 'admin' or pr.ProjectId in (select ProjectId from tblUserProjects where UserId in (select UserId from tblUsers where UserName = 'admin'))) and pr.CountryId = '" + entityId + "' and st.type_of_pack = '" + packId + "') a where row_number = 1");
                //}
                return runSuite.AsQueryable();
            }
        }

        public async Task<IQueryable<RunSuite>> GetRunSuites(string username)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<RunSuite>("select * from (select st.PackId,st.scenario_subscenario, st.PackName as PackName,pr.ProjectId, pr.ProjectName as ProjectName,st.RunAt, ROW_NUMBER() OVER(PARTITION BY PackId ORDER BY PackId DESC) from tblPacks st inner join tblProjects pr on pr.ProjectId = st.ProjectId where pr.ProjectOwner = '" + username + "' or pr.ProjectId in (select ProjectId from tblUserProjects where UserId in (select UserId from tblUsers where UserName = '" + username + "'))) a where row_number = 1");

                return urls.AsQueryable();
            }
        }

        public async Task<IQueryable<String>> GeActiveRunSuites()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<String>("SELECT DISTINCT(packId) FROM tblCurrProjs WHERE status IN('IN PROGRESS', 'STARTED') AND active = 1");

                return urls.AsQueryable();
            }
        }


        public async Task<IQueryable<Common>> GetBrowser()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<Common>("select BrowserId as Id, BrowserName as Name from tblBrowser where BrowserName in ('chrome', 'firefox', 'internet explorer') ");

                return urls.AsQueryable();
            }
        }

        public  async Task<IQueryable<Common>> GetEnvironemnts()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<Common>("select EnvironmentId as Id, Environmentname as Name from tblEnvironment");

                return urls.AsQueryable();
            }
        }
        
        public async Task<IQueryable<Common>> GetNodes()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<Common>("select nodeId as Id,nodeName as Name from tblNode order by nodeId asc");

                return urls.AsQueryable();
            }
        }

        public async void PostUserTestRun(TblUserTestRun tblUserTestRun)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.ExecuteAsync("Insert into tblUserTestRun(TestGroupId, TestRunId, Username, PackId, RunDate, RunAt) values('" + tblUserTestRun.testgroupid + "', '" + tblUserTestRun.testrunid + "','" + tblUserTestRun.username + "','" + tblUserTestRun.packid + "', now(), '" + tblUserTestRun.runat + "')");
            }
        }

        public async Task<string> PostTestRunTemp(List<TblTestRunTemp> tblTestRunTemp)
        {
            try
            {
                using (IDbConnection dbConnection = _connectionFactory.GetConnection())
                {
                    dbConnection.Open();
                    foreach (var item in tblTestRunTemp)
                    {
                        await dbConnection.ExecuteAsync("Insert into tblTestRunTemp(PackId, PackName, ProjectId, ProjectName, Environment, StepNo, TestStepID, Keyword,ObjectName,TestData,ObjectValue, Description, RunMode,Status,RunAt,PortNo,Browser,TestRunId, Scen_Id, Sub_Scen_Id,TestGroupId )values ('" + item.packid + "', '" + item.packname + "', '" + item.projectid + "', '" + item.projectname + "', '" + item.environment + "','" + item.stepno + "', '" + item.teststepid + "', '" + item.keyword + "', '" + item.objectname + "', '" + item.testdata + "', '" + item.objectvalue + "', '" + item.decription + "','" + item.runmode + "', '" + item.status + "', '" + item.runat + "', '" + item.portno + "','" + item.browser + "','" + item.testrunid + "','" + item.scan_id + "', '" + item.sub_scan_id + "', '" + item.testgroupid + "') ;");
                        await dbConnection.ExecuteAsync("Insert into tblTestRunTemp1(PackId, PackName, ProjectId, ProjectName, Environment, StepNo, TestStepID, Keyword,ObjectName,TestData,ObjectValue, Description, RunMode,Status,RunAt,PortNo,Browser,TestRunId, Scen_Id, Sub_Scen_Id,TestGroupId )values ('" + item.packid + "', '" + item.packname + "', '" + item.projectid + "', '" + item.projectname + "', '" + item.environment + "','" + item.stepno + "', '" + item.teststepid + "', '" + item.keyword + "', '" + item.objectname + "', '" + item.testdata + "', '" + item.objectvalue + "', '" + item.decription + "','" + item.runmode + "', '" + item.status + "', '" + item.runat + "', '" + item.portno + "','" + item.browser + "','" + item.testrunid + "','" + item.scan_id + "', '" + item.sub_scan_id + "', '" + item.testgroupid + "') ;");
                    }
                    return "success";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task<IQueryable<String>> GetSubScenariosByPackId(string packId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryAsync<String>("select SubScenarios from tblPacks where PackId = '" + packId + "'");
                return data.AsQueryable();
            }
        }

        public async Task<IQueryable<String>> GetScenariosByPackId(string packId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryAsync<String>("select Scenarios from tblPacks where PackId =  '" + packId + "'");
                return data.AsQueryable();
            }
        }

        public async Task<String> GetProjectNameByProjectId(string projectId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryFirstOrDefaultAsync<String>("select ProjectName from tblProjects where ProjectId = '" + projectId + "'");
                return data;
            }
        }

        public async Task<String> GetPackNameByPackId(string packId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryFirstOrDefaultAsync<String>("select PackName from tblPacks where PackId = '" + packId + "'");
                return data;
            }
        }

        public async Task<TblPacks> GetPackByPackId(string packId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryFirstOrDefaultAsync<TblPacks>("select * from tblPacks where PackId = '" + packId + "'");
                return data;
            }
        }

        public async Task<TblNode> GetNodesData(string nodeName)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryFirstOrDefaultAsync<TblNode>("select nodeIp, nodePortNo, hostIp, hostPortNo from tblNode where nodeName = '" + nodeName + "'");
                return data;
            }
        }

        public async Task<IQueryable<String>> GetSubScenIdBySubScenarioIdAndProjectId(string subscenarioId,string projectId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryAsync<String>("select Sub_Scen_Id from tblSubScenarios where SubScenarioId in (" + subscenarioId + ") and ProjectId = '" + projectId + "'");
                return data.AsQueryable();
            }
        }

        public async Task<IQueryable<TblTestSteps>> GetTestStepsBySceneIdAndSubSceneId(string sceneId, string subsceneId,string nodeIp,string nodePortNo)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryAsync<TblTestSteps>("select tt.TestStepID,tp.projectcode,ts.mainscenarioid,tss.subscenarioid, tca.ActionName, tor.ObjectName, tt.TestData, tor.ObjectValue,tt.Description, tt.RunMode,tt.Status,'" + nodeIp + "','" + nodePortNo + "', tt.Scen_Id,'" + subsceneId + "' as Sub_Scen_Id  from tblTestSteps tt left outer join tblCommonAction tca on cast(tt.ActionId as integer) = cast(tca.ActionID as integer)  inner join tblSubScenarios tss on cast(tt.Sub_Scen_Id as varchar) = cast(tss.Sub_Scen_Id as varchar) inner join tblScenarios ts on cast(tt.Scen_Id as varchar) = cast(ts.Scen_Id as varchar) inner join tblprojects tp on  cast(tt.projectid as varchar)= cast(tp.projectid as varchar) left outer join tblObjectRepo tor on cast(tt.ObjectID as integer) = cast(tor.ObjectID as integer) where tss.Sub_Scen_Id = '" + subsceneId + "' and tt.Scen_Id in (" + sceneId + ") and tt.active='1' and tt.RunMode='Y' order by tt.TestStepID asc");
                return data.AsQueryable();
            }
        }

        public async Task<String> GetProjectCodeByProjectId(string projectId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var data = await dbConnection.QueryFirstOrDefaultAsync<String>("select ProjectCode from tblProjects where ProjectId = '" + projectId + "'");
                return data;
            }
        }

        public async void  DeleteTableTestStepsByProjectCode(string projectCode,string projectId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync("delete from tblTestSteps where ProjectId = '" + projectId + "' and LEFT(TestStepId, 3) != '" + projectCode + "'");
            }
        }

        public async void DeleteTableTestRunTempByTestGroupId(string testGroupId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync("delete from  tblTestRunTemp where TestGroupId = '" + testGroupId + "'");
            }
        }

        public async Task<String> GetPackIdByPackName(string packId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
               return await dbConnection.QueryFirstOrDefaultAsync<string>("select PackId from tblPacks where PackName = '" + packId+ "'");
            }
        }

        public async void UpdateTblCurrProjsByGroupId(string testGroupId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync("Update tblCurrProjs set Status = 'COMPLETED', active = '0' where TestGroupId = '" + testGroupId + "'");
            }
        }

        public async void UpdateTblCurrProjsByPackId(string packId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync("Update tblCurrProjs set Status = 'COMPLETED', active='0' where packid = '" + packId + "'");
            }
        }

        public async Task<TestGroupandTestRunId> GetTestRunIdAndGroupIdByPackId(string packId)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                return await dbConnection.QueryFirstOrDefaultAsync<TestGroupandTestRunId>("select testRunId,testGroupId from tblCurrProjs where packId = '" + packId + "' and status in ('STARTED', 'IN PROGRESS') limit 1");
            }
        }

    }
}
