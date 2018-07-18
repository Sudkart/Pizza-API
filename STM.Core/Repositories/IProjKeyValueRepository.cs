using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public interface IProjKeyValueRepository
    {
        Task<IQueryable<ProjKeyValue>> GetProjKeyValuePairs();

        Task<bool> CreateKeyValuePair(ProjKeyValue projKeyValue);

        Task<bool> UpdateProjKeyValuePair(ProjKeyValue projKeyValue, int varid);

        Task<bool> DeleteProjKeyValuePair(int varid);
    }
}
