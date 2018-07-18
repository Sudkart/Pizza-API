using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public interface IActionGroupRepository
    {
        Task<int> CreateActionGroup(ActionGroup project);
        Task<bool> UpdateActionGroup(ActionGroup project);
        Task<bool> DeleteActionGroup(int id);
        Task<IQueryable<ActionGroup>> GetActionGroups();
    }
}
