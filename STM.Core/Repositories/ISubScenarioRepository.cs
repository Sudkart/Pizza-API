using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public interface ISubScenarioRepository
    {
         Task<bool> CreateSubScenario(SubScenario subscenario);
         Task<bool> UpdateSubScenario(SubScenario subscenario, int scenId, int projectId);
         Task<IQueryable<SubScenario>> GetSubScenarios(int projectId, int ScenId);
         Task<IQueryable<SubScenario>> GetAllSubScenarios();
         Task<bool> DeleteSubScenario(SubScenario subscenario);
         Task<IQueryable<SubScenario>> GetSubScenarioswithScenarios(int projectId);
         Task<IQueryable<string>> GetMaxSubScenarioId(int projectId, int scenId);


    }
}
