using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;
namespace STM.Core.Repositories
{
   public interface IUrlRepository
    {

        Task<IQueryable<Url>> GetUrls();
        Task<bool> CreateUrl(Url url);
        Task<bool> UpdateUrl(Url url, int urlId);
        Task<bool> DeleteUrl(int urlId);

    }
}
