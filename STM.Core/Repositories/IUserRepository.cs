using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user, int userId);
        Task<bool> DeleteUser(User user);
        Task<IQueryable<User>> GetUsers();


    }
}
