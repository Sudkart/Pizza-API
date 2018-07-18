using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public interface IScenarioRepository
    {

        Task<int> CreateScenario(Scenario scenario);
        Task<bool> UpdateScenario( Scenario scenario, int projectId);
        Task<bool> DeleteScenario(Scenario scenario);
        Task<IQueryable<Scenario>> GetScenarios(int projectId);
        Task<IQueryable<Scenario>> GetAllScenarios();
        Task<IQueryable<string>> GetMaxScenarioId(int projectId);





     }
}
