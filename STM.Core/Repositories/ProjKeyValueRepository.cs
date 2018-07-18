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
   public class ProjKeyValueRepository: IProjKeyValueRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public ProjKeyValueRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<ProjKeyValue>> GetProjKeyValuePairs()
        {

            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var projkeyvalues = await dbConnection.QueryAsync<ProjKeyValue>("SELECT * FROM tblprojkeyvalues");

                return projkeyvalues.AsQueryable();
            }

        }

        public async Task<bool> CreateKeyValuePair(ProjKeyValue ProjKeyValue)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();

                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"Update tblUsers set email = @email where userid =@userid";
                string createQuery = @"insert into tblprojkeyvalues(varid, entityid, projectid, environmentid, varname, varvalue, createdby, createddate,updatedby, updateddate) 
              values(@varid, @entityid, @projectid, @environmentid,@varname, @varvalue, @createdby, @createddate, @updatedby, @updateddate)";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                   ProjKeyValue.VarId,
                   ProjKeyValue.EntityId,
                   ProjKeyValue.ProjectId,
                   ProjKeyValue.EnvironmentId,
                   ProjKeyValue.VarName,
                   ProjKeyValue.VarValue,
                   ProjKeyValue.CreatedBy,
                   ProjKeyValue.CreatedDate,
                   ProjKeyValue.UpdatedBy,
                   ProjKeyValue.UpdatedDate

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

            }
        }

        public async Task<bool> UpdateProjKeyValuePair(ProjKeyValue ProjKeyValue, int varId)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                string updateQuery = @"update tblprojkeyvalues set entityid = @entityid, projectid = @projectid, environmentid = @environmentid, 
varname = @varname,varvalue = @varvalue, createdby = @createdby, createddate = @createddate, updatedby = @updatedby, updateddate = @updateddate
             where varid = @varid";
                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {
                 ProjKeyValue.EntityId,
                 ProjKeyValue.ProjectId,
                 ProjKeyValue.EnvironmentId,
                 ProjKeyValue.VarName,
                 ProjKeyValue.VarValue,
                 ProjKeyValue.CreatedBy,
                 ProjKeyValue.CreatedDate,
                 ProjKeyValue.UpdatedBy,
                 ProjKeyValue.UpdatedDate,
                 varid = varId
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
            //return true;
            //}



        }

        public async Task<bool> DeleteProjKeyValuePair(int varId)
        {
            //throw new NotImplementedException();
            //using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            //{
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                // IDbConnection dbConnection = _connectionFactory.GetConnection()
                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                //  string delQuery = @"delete from tblprojects where projectid = '" + projId + "'";
                string delQuery = @"delete from tblprojkeyvalues where varid = @varId";
                var result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    varId = varId

                });
                // var result = await dbConnection.ExecuteAsync(delQuery);
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

            //}
        }








    }
}
