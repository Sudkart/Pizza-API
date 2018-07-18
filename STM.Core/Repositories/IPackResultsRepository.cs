using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;


namespace STM.Core.Repositories
{
    public interface IPackResultsRepository
    {
         
          Task<IQueryable<PackResult>> GetPackResults(int packId, int projectId, DateTime fromDate, DateTime toDate);
        Task<IQueryable<StepDetails>> GetSteps(int packId, string subScenarioId, string testGroupId);

        Task<IQueryable<ScenarioResult>> GetScenarioWithStatusResults(string testGroupId);

        Task<List<PieChartData>> GetPieChartDetails(string packId, List<string> testGroupId);

    }
}
