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
    public class ObjectRepository: IObjectRepository

    {
        private IConnectionFactory _connectionFactory;

        public ObjectRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<TestObject>> GetAllObjects()
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();

                var objects = await dbConnection.QueryAsync<TestObject>("SELECT * FROM tblObjectRepo");

                return objects.AsQueryable();

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

        public async Task<IQueryable<TestObject>> GetObjects(string objectName)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();

                string query = "SELECT * FROM tblObjectRepo where objectname = @objectname";

                // var objects = await dbConnection.QueryAsync<TestObject>("SELECT * FROM tblObjectRepo  where objectname = @objectname");
                var result = await dbConnection.QueryAsync<TestObject>(query, new
                {
                    objectname = objectName
                });

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


        public async Task<IQueryable<TestObject>> GetObjectsByName(string objectName,string projectName)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();

            
                //string query = "SELECT objectid,objectname FROM tblObjectRepo where projectname = '" + projectName +
                //               "' AND objectname LIKE '%" + objectName + "%'";
                String query = "SELECT objectid,objectname FROM tblObjectRepo where projectname = @projectname AND objectname LIKE @objectname";
                var result = await dbConnection.QueryAsync<TestObject>(query, new
                {
                    projectname=projectName,
                    objectname = "%"+ objectName+ "%"
                });

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


        public async Task<IQueryable<TestObject>> GetObjectsByNameMatch(string objectName)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                //string query = "SELECT objectid,objectname FROM tblObjectRepo where projectname = '" + projectName +
                //               "' AND objectname LIKE '%" + objectName + "%'";
                String query = "SELECT objectid,objectname FROM tblObjectRepo where objectname LIKE @objectname";
                var result = await dbConnection.QueryAsync<TestObject>(query, new
                {
                    objectname = "%" + objectName + "%"
                });

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



        public async Task<IQueryable<TestObject>> GetObjectsByName(string projectName)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();


                //string query = "SELECT objectid,objectname FROM tblObjectRepo where projectname = '" + projectName +
                //               "' AND objectname LIKE '%" + objectName + "%'";
                String query = "SELECT * FROM tblObjectRepo where projectname = @projectname";
                var result = await dbConnection.QueryAsync<TestObject>(query, new
                {
                    projectname = projectName
                });

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


        public async Task<IQueryable<TestObject>> GetObjectsById(int objectId)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();

                string query = "SELECT * FROM tblObjectRepo where objectId = @objectId";

                // var objects = await dbConnection.QueryAsync<TestObject>("SELECT * FROM tblObjectRepo  where objectname = @objectname");
                var result = await dbConnection.QueryAsync<TestObject>(query, new
                {
                    objectId = objectId
                });

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


        public async Task<bool> UpdateObject(TestObject Object, int ObjectId)
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                string updateQuery = @"update tblobjectrepo set objectname = @objectname, objectvalue = @objectvalue, createdby = @createdby, createddate = @createddate, 
updatedby = @updatedby, updateddate = @updateddate where objectId = @objectId";
                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {
                   Object.ObjectName,
                   Object.ObjectValue,
                   Object.CreatedBy,
                  createddate = Convert.ToDateTime(Object.CreatedDate),
                   Object.UpdatedBy,
                   updateddate = Convert.ToDateTime(Object.UpdatedDate),
                   objectId = ObjectId
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


        public async Task<int> CreateObject(TestObject testObject)
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();

                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"Update tblUsers set email = @email where userid =@userid";
                string createQuery = @"insert into tblobjectrepo(objectname, identitytype, objectvalue, createdby, createddate, updatedby, updateddate) 
                         values(@objectname, @identitytype, @objectvalue, @createdby, @createddate, @updatedby, @updateddate); select lastval();";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                    testObject.ObjectName,
                    testObject.IdentityType,
                    testObject.ObjectValue,
                    testObject.CreatedBy,
                   createddate= Convert.ToDateTime(testObject.CreatedDate),
                    testObject.UpdatedBy,
                    updateddate = Convert.ToDateTime(testObject.UpdatedDate)

                });

                return result;
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


        public async Task<int> CreateObjectWithReturnId(TestObject testObject)
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();

                string createQuery = @"insert into tblobjectrepo(objectname, projectname, identitytype, objectvalue, createdby, createddate, updatedby, updateddate) 
                         values(@objectname,@projectname, @identitytype, @objectvalue, @createdby, @createddate, @updatedby, @updateddate) RETURNING objectid;";
                var reader = await dbConnection.ExecuteReaderAsync(createQuery, new
                {
                    testObject.ObjectName,
                    testObject.ProjectName,
                    testObject.IdentityType,
                    testObject.ObjectValue,
                    testObject.CreatedBy,
                    createddate = Convert.ToDateTime(testObject.CreatedDate),
                    testObject.UpdatedBy,
                    updateddate = Convert.ToDateTime(testObject.UpdatedDate)
                });

                int objectId = 0;
                while (reader.Read())
                {
                    objectId = reader.GetInt32(0);
                }
                return objectId;
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
    }
}
