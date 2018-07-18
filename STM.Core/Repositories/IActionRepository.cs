using System.Linq;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STM.Core.Repositories
{
    public interface IActionRepository
    {
        Task<bool> CreateAction(Action subscenario);
        Task<bool> UpdateAction(Action subscenario, int actionid, int actiongroupid);
        Task<IQueryable<Action>> GetActions(int actiongroupid);
        Task<IQueryable<Action>> GetAllActions();
        Task<bool> DeleteAction(int actionid);
        Task<IQueryable<Action>> GetactionswithActiongroups();
    }
}