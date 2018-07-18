using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public interface ILoginRepository
    {
        Task<IQueryable<User>> GetUserFromDB(String username);

        int UpdateUserPassword(string username, string password);
    }
}
