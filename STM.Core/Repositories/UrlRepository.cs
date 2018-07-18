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
    public class UrlRepository : IUrlRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public UrlRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }
        public async Task<IQueryable<Url>> GetUrls()
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var urls = await dbConnection.QueryAsync<Url>("SELECT * FROM tblurl");

                return urls.AsQueryable();
            }

        }

        public async Task<bool> CreateUrl(Url Url)
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                string createQuery = @"insert into tblurl(urlid, entityid, projectId, environmentId, baseUrl, createdby, createddate,updatedby, updateddate) values(@urlid, @entityid, @projectid, @environmentid, @baseurl, @createdby, @createddate, @updatedby, @updateddate)";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                    Url.UrlId,
                    Url.EntityId,
                    Url.ProjectId,
                    Url.EnvironmentId,
                    Url.BaseUrl,
                    Url.CreatedBy,
                    Url.CreatedDate,
                    Url.UpdatedBy,
                    Url.UpdatedDate
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

        public async Task<bool> UpdateUrl(Url url, int urlId)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                string updateQuery = @"update tblurl set entityid=@entityid, projectid = @projectid, environmentid= @environmentid, 
baseurl = @baseurl, createdby = @createdby, createddate = @createddate, updatedby = @updatedby, updateddate = @updateddate where urlid = @urlid";
                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {
                    url.EntityId,
                    url.ProjectId,
                    url.EnvironmentId,
                    url.BaseUrl,
                    url.CreatedBy,
                    url.CreatedDate,
                    url.UpdatedBy,
                    url.UpdatedDate
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

        public async Task<bool> DeleteUrl(int urlId)
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
                string delQuery = @"delete from tblUrl where urlid = @urlid";
                var result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    urlid = urlId

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
