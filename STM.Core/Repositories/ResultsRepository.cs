using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using STM.Core.EntityLayer;

namespace STM.Core.Repositories
{
    public class ResultsRepository : IResultsRepository
    {
        //private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public ResultsRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<Results>> GetLiveResults()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<Results>("select distinct tp.PackName,tp.packid, tpj.ProjectName, tt.Status, coalesce(tn.nodeName, tc.runningAt) as RunningAt , tc.testGroupId, tt.startTime, tt.endTime, round(coalesce(Cast(coalesce(tt.completedSteps, 0) as numeric) / Cast(coalesce(tt.totalSteps, 0) as numeric) * 100, 0), 2) as PercentCompleted, round(coalesce(Cast(coalesce(tt.stepsfailed, 0) as numeric) / (Cast(coalesce(tt.totalSteps, 0) as numeric) - Cast(coalesce(tt.stepsskipped, 0) as numeric)) * 100, 0), 2) as PercentFailed, round(coalesce(Cast(coalesce(tt.stepspassed, 0) as numeric) / (Cast(coalesce(tt.totalSteps, 0) as numeric) - Cast(coalesce(tt.stepsskipped, 0) as numeric)) * 100, 0), 2) as PercentPassed, round(coalesce(Cast(coalesce(tt.stepsskipped, 0) as numeric) / Cast(coalesce(tt.totalSteps, 0) as numeric) * 100, 0), 2) as PercentSkipped from tblPacks tp inner join tblCurrProjs tc on tp.PackId = tc.packId inner join (select tc.TestGroupId, Sum(totalSteps) as TotalSteps, Sum(completedSteps) as CompletedSteps, Sum(stepspassed) as stepspassed, Sum(stepsfailed) as stepsfailed, Sum(stepsskipped) as stepsskipped, MIN(startTime) as starttime, MAX(endTime) as endtime, case when exists (Select Status from tblCurrProjs tc1 where tc1.status = 'In Progress' and tc1.testGroupId = tc.testGroupId limit 1) then 'In Progress' else 'In Progress' end as Status from tblCurrProjs tc group by testGroupId)  tt on tc.testGroupId = tt.TestGroupId inner join tblUserProjects tu on tp.ProjectId = tu.ProjectId left outer join tblNode tn on tc.runningAt = tn.hostIp left outer join tblProjects tpj on tp.ProjectId = tpj.ProjectId where tc.active = '1' and tc.status not in ('ABORTED_BY_USER', 'COMPLETED') order by startTime desc; ");
                return urls.AsQueryable();
            }
        }
    }
}
