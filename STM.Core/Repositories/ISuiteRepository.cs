using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;
namespace STM.Core.Repositories
{
    public interface ISuiteRepository
    {
         Task<bool> UpdateSuite(Suite Suite, int SuiteId);
         Task<bool> CreateSuite(Suite Suite);
         Task<IQueryable<Suite>> GetSuites();
         Task<bool> DeleteSuite(int SuiteId);

    }



}
