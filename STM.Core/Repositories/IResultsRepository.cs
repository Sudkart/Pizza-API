using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STM.Core.Repositories
{
    public interface IResultsRepository
    {
      
        Task<IQueryable<Results>> GetLiveResults();

    }
}
