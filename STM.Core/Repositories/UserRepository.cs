using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using STM.Core.EntityLayer;
using STM.Core;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using System.Reflection;
using Npgsql;
using System.Threading.Tasks;
namespace STM.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public UserRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }


        public async Task<bool> CreateUser(User user)
        {
            // throw new NotImplementedException();
            // using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            // {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();

                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"Update tblUsers set email = @email where userid =@userid";
                string createQuery = @"Insert into tblUsers(Username, ""Password"", Email, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, LastLoginDate, RoleId,Active, salt ) values(@Username, @Password,@Email,@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate, @LastLoginDate,@roleid,@active,@salt)";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                    user.UserName,
                    user.Password,
                    user.Email,
                    user.CreatedBy,
                    user.CreatedDate,
                    user.UpdatedBy,
                    user.UpdatedDate,
                    user.LastLoginDate,
                    user.RoleId,
                    user.Active,
                    user.Salt

                });

                return Convert.ToBoolean(result);
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

        public async Task<bool> DeleteUser(User user)
        {
            //throw new NotImplementedException();
            // using (IDbConnection dbConnection = _connectionFactory.GetConnection())
             //  {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
           
                try
                {
                   // IDbConnection dbConnection = _connectionFactory.GetConnection()
                    dbConnection.Open();
                    string delQuery = @"delete from tblusers where username = @username";
                    var result = await dbConnection.ExecuteAsync(delQuery, new
                    {
                        user.UserName

                    });
                   // if (result >= 1)
                      //  return true;
                    //else
                    //   return false;

                    return Convert.ToBoolean(result);
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

           // return true;

          //  }
        }



        public async Task<IQueryable<User>> GetUsers()
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            //using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            //{
            try { 
            dbConnection.Open();
                var users = await dbConnection.QueryAsync<User>("SELECT * FROM tblusers");

                    return users.AsQueryable();
                //  }
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


        public async Task<bool> UpdateUser(User user, int userId)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            //  using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            // {
            try
                {
                    dbConnection.Open();
                    //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                    string updateQuery = @"Update tblUsers set username = @username, ""Password"" = @Password, email = @email, createdby = @createdby, 
createddate = @createddate, updatedby =@updatedby, updateddate= @updateddate, lastlogindate = @lastlogindate, roleid = @roleid, active = @active,salt = @salt 
where userid =@userid";
                    var result = await dbConnection.ExecuteAsync(updateQuery, new
                    {
                        username = user.UserName,
                         user.Password,
                        email = user.Email,
                        createdby = user.CreatedBy,
                        createddate = user.CreatedDate,
                        updatedby = user.UpdatedBy,
                        updateddate = user.UpdatedDate,
                        lastlogindate = user.LastLoginDate,
                        roleid = user.RoleId,
                        active = user.Active,
                        salt = user.Salt,
                        userid = userId
                    });


                    return Convert.ToBoolean(result);
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
           // }

         }  




    }
}
