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
    public class TestStepRepository : ITestStepRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public TestStepRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }
        public async Task<IQueryable<TestStep>> GetTestStepsAsync(int projectId, int scenId, int subscenId)
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                //using (IDbConnection dbConnection = _connectionFactory.GetConnection())
                //{
                dbConnection.Open();
                // To do - change it ot parameterised query
                var query = @"SELECT * FROM tblTestSteps where projectId=@projectId and scen_Id = @scen_Id and sub_scen_Id = @sub_scen_Id order by teststepid";
                // string query = @"SELECT * FROM tblTestSteps where projectId ='" + projectId + "'  and scen_id = '" + scenId + "' and sub_scen_id = '" + subscenId + "'";
                // return testSteps.AsQueryable();
                var result = await dbConnection.QueryAsync<TestStep>(query, new
                {
                    projectId = Convert.ToString(projectId),
                    scen_Id = Convert.ToString(scenId),
                    sub_scen_Id = Convert.ToString(subscenId)
                });

                // var result = await dbConnection.QueryAsync<TestStep>(query);
                return result.AsQueryable();


                //  return result.AsQueryable();
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

        public async Task<bool> CreateTestStep(TestStep testStep)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                testStep.CreatedDate = testStep.UpdatedDate = Convert.ToString(DateTime.Now);
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"Update tblUsers set email = @email where userid =@userid";
                string createQuery = "insert into tblTestSteps(projectid, scen_id, sub_scen_id, teststepid, actionid, objectid, xpath, description, runmode, " +
                    "status, active, createdby,createddate, updatedby, updateddate, testdata) values(@projectid, @scen_id, @sub_scen_id, @teststepid, @actionid, @objectid,@xpath, @description,@runmode, @status, @active, @createdby, @createddate, @updatedby, @updateddate, @testdata)";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                    testStep.ProjectId,
                    testStep.Scen_Id,
                    testStep.Sub_Scen_Id,
                    testStep.TestStepId,
                    testStep.ActionId,
                    testStep.ObjectId,
                    testStep.Xpath,
                    testStep.Description,
                    testStep.RunMode,
                    testStep.Status,
                    testStep.Active,
                    testStep.CreatedBy,
                    testStep.CreatedDate,
                    testStep.UpdatedBy,
                    testStep.UpdatedDate,
                    testStep.TestData
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

        public async Task<bool> UpdateTestStep(TestStep testStep, int testStepId)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                string updateQuery = @"update tblTestSteps set projectid=@projectid, scen_id = @scen_id, sub_scen_id= @sub_scen_id, 
teststepid = @teststepid, actionid = @actionid, objectid = @objectid, xpath = @xpath, description = @description, runmode = @runmode, 
status = @status, active = @active,  createdby = @createdby, createddate = @createddate, updatedby = @updatedby, updateddate = @updateddate, 
testdata = @testdata where step_id = @step_id";
                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {
                    testStep.ProjectId,
                    testStep.Scen_Id,
                    testStep.Sub_Scen_Id,
                    testStep.TestStepId,
                    testStep.ActionId,
                    testStep.ObjectId,
                    testStep.Xpath,
                    testStep.Description,
                    testStep.RunMode,
                    testStep.Status,
                    testStep.Active,
                    testStep.CreatedBy,
                    testStep.CreatedDate,
                    testStep.UpdatedBy,
                    testStep.UpdatedDate,
                    testStep.TestData,
                    testStepId
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

        public async Task<bool> DeleteTestStep(int testStepId, int projectId)
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
                string delQuery = @"delete from tblteststeps where step_id = @step_id and projectid = @projId";
                var result = await dbConnection.QueryAsync(delQuery, new
                {
                    step_id = testStepId,
                    projId = projectId

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
        }


        public async Task<int> DeleteAllTestStepsfromSubscenerio(int subsceneId, int projectId)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                string delQuery = @"delete from tblteststeps where sub_scen_id = @subscene_Id and projectid = @projId";
                var result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    subscene_Id = Convert.ToString(subsceneId),
                    projId = Convert.ToString(projectId)

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
                dbConnection.Dispose();
            }
        }

    }
}
