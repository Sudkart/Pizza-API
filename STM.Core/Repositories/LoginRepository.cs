using Dapper;
using Microsoft.AspNetCore.Http;
using STM.Core.EntityLayer;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace STM.Core.Repositories
{
    public class LoginRepository :ILoginRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private IConnectionFactory _connectionFactory;
        public LoginRepository(IConnectionFactory connectionFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<User>> GetUserFromDB(string username)
        {
            var context = _httpContextAccessor.HttpContext;
            // using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            //{
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();

                string query = @"SELECT * FROM tblusers where username = '"+username+"'";
                var result = await dbConnection.QueryAsync<User>(query);

                //Todo - string query = @"SELECT * FROM tblusers where username = @username";
                //var result = await dbConnection.QueryAsync<User>(query,new {
                //    username
                //});

                return result.AsQueryable();
            }                        
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }

        }

        public int UpdateUserPassword(string username, string password)
        {
            var context = _httpContextAccessor.HttpContext;
            
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                string updateQuery = "UPDATE tblusers set \"Password\" ='"+password+"' where \"username\"='"+username
                    +"'";
                int data = dbConnection.Execute(updateQuery);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }
    }
}
