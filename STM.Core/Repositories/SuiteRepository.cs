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
    public class SuiteRepository : ISuiteRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public SuiteRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<Suite>> GetSuites()
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();

            // using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            // {
            try
            {

                dbConnection.Open();
                var suites = await dbConnection.QueryAsync<Suite>("SELECT * FROM tblpacks");

                return suites.AsQueryable();
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

        public async Task<bool> UpdateSuite(Suite Suite, int SuiteId)
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                string updateQuery = @"update tblPacks set packname = @packname, projectId = @projectId, scenarios = @scenarios, subscenarios = @subscenarios,
runat = @runat, testgroup = @testgroup, scheduledate = @scheduledate, active = @active, updatedby = @updatedby,updateddate = @updateddate, 
type_of_pack = @type_of_pack, scenario_subscenario = @scenario_subscenario where packid = @packid";
                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {
                    Suite.PackName,
                    Suite.ProjectId,
                    Suite.Scenarios,
                    Suite.SubScenarios,
                    Suite.RunAt,
                    Suite.TestGroup,
                    scheduledate = Convert.ToDateTime(Suite.ScheduleDate),
                    Suite.Active,
                    //Suite.CreatedBy,
                    //createddate = Convert.ToDateTime(Suite.CreatedDate),
                    //createddate = DateTime.Now,
                    Suite.UpdatedBy,
                    //updateddate = Convert.ToDateTime(Suite.UpdatedDate),
                    updateddate = DateTime.Now,
                    //Suite.No_of_Steps,
                    Suite.Type_Of_Pack,
                    packid = SuiteId,
                    Suite.scenario_subscenario
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

        public async Task<bool> CreateSuite(Suite Suite)
        {

            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();

                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"Update tblUsers set email = @email where userid =@userid";
                string createQuery = @"insert into tblPacks(packname, projectid, scenarios, subscenarios, 
runat, testgroup, scheduledate, active, createdby, createddate, updatedby, updateddate, no_of_steps, type_of_pack, scenario_subscenario) 
values(@packname, @projectid, @scenarios, @subscenarios, @runat, @testgroup, @scheduledate, @active, @createdby, @createddate, @updatedby, @updateddate, @no_of_steps, @type_of_pack, @scenario_subscenario)";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                    Suite.PackName,
                    Suite.ProjectId,
                    Suite.Scenarios,
                    Suite.SubScenarios,
                    Suite.RunAt,
                    Suite.TestGroup,
                    scheduledate = Convert.ToDateTime(Suite.ScheduleDate),
                    //scheduledate = DateTime.Now,
                    Suite.Active,
                    Suite.CreatedBy,
                    //createddate = Convert.ToDateTime(Suite.CreatedDate),
                    createddate = DateTime.Now,
                    Suite.UpdatedBy,
                    //updateddate = Convert.ToDateTime(Suite.UpdatedDate),
                    updateddate = DateTime.Now,
                    Suite.No_of_Steps,
                    Suite.Type_Of_Pack,
                    Suite.scenario_subscenario
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

        //Throwing no columns were selected exception after deletion
        public async Task<bool> DeleteSuite(int suiteId)
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
                string delQuery = @"delete from tblpacks where packid = @packId";
                var result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    packId = suiteId

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
