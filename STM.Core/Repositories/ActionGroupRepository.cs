using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STM.Core.EntityLayer;

namespace STM.Core.Repositories
{
    public class ActionGroupRepository : IActionGroupRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;

        public ActionGroupRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task<int> CreateActionGroup(ActionGroup project)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteActionGroup(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ActionGroup>> GetActionGroups()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var projects = await dbConnection.QueryAsync<ActionGroup>("SELECT * FROM tblactiongroup");

                return projects.AsQueryable();
            }
        }

        public Task<bool> UpdateActionGroup(ActionGroup project)
        {
            throw new NotImplementedException();
        }
    }
}
