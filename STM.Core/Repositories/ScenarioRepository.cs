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
  public class ScenarioRepository: IScenarioRepository
    {

        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public ScenarioRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<Scenario>> GetScenarios(int projectId)
        {

            // using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            //{
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                // string query = @"SELECT * FROM tblScenarios where projectId = @projectId";
                string query = @"SELECT ts.scen_id, ts.projectid, tp.projectname, ts.mainscenarioid, ts.mainscenarioname, ts.mainscenariodescription, ts.runmode, ts.status, ts.active, ts.testscenexecdate,ts.testscenexecby,ts.testscenlaststatus, ts.createdby, ts.createddate, ts.updatedby,ts.updateddate FROM tblScenarios ts inner join tblprojects tp on ts.projectid = tp.projectid where ts.projectId = @projectId";

                var result = await dbConnection.QueryAsync<Scenario>(query, new
                {
                    projectId
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


        public async Task<IQueryable<Scenario>> GetAllScenarios()

        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                // string query = @"SELECT * FROM tblScenarios where projectId = @projectId";
                string query = @"SELECT ts.scen_id, ts.projectid, tp.projectname, ts.mainscenarioid, ts.mainscenarioname, ts.mainscenariodescription, ts.runmode, ts.status, ts.active, ts.testscenexecdate,ts.testscenexecby,ts.testscenlaststatus, ts.createdby, ts.createddate, ts.updatedby,ts.updateddate FROM tblScenarios ts inner join tblprojects tp on ts.projectid = tp.projectid";

                var result = await dbConnection.QueryAsync<Scenario>(query);
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

        public async Task<int> CreateScenario(Scenario Scenario)
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
                string createQuery = @"Insert into tblScenarios(projectid, mainscenarioid, mainscenarioname, mainscenariodescription, runmode, status, active, testscenexecdate, testscenexecby, testscenlaststatus, createdby, createddate, updatedby, updateddate) values (@projectId, @mainScenarioId, @mainScenarioName, @mainScenarioDescription, @runmode, @status, @active, @testscenexecdate, @testscenexecby, @testscenlaststatus, @createdBy, @createdDate, @updatedBy, @updateddate); select lastval();";
                var result = await dbConnection.ExecuteScalarAsync(createQuery, new
                {
                    Scenario.ProjectId,
                    Scenario.MainScenarioId,
                    Scenario.MainScenarioName,
                    Scenario.MainScenarioDescription,
                    Scenario.RunMode,
                    Scenario.Status,
                    Scenario.Active,
                    Scenario.TestScenExecDate,
                    Scenario.TestScenExecBy,
                    Scenario.TestScenLastStatus,
                    Scenario.CreatedBy,
                    Scenario.CreatedDate,
                    Scenario.UpdatedBy,
                    Scenario.UpdatedDate
                 });

                //int res = Convert.ToInt32(result);
                return Convert.ToInt32(result);
                
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


        public async Task<bool> UpdateScenario(Scenario scenario, int projectId)
        {

            //using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            // {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"update tblScenarios set projectid = @projectid, mainscenarioid = @mainscenarioid, mainscenarioname = @mainscenarioname, mainscenariodescription = @mainscenariodescription, runmode = @runmode, status = @status, active = @active, testscenexecdate = @testscenexecdate, testscenexecby = @testscenexecby, testscenlaststatus =  @testscenlaststatus, createdby = @createdby, createddate = @createddate, updatedby = @updatedby, updateddate = @updateddate where scen_id = @scen_id and projectId = @projectId";
                string updateQuery = @"update tblScenarios set mainscenarioname = @mainscenarioname, mainscenariodescription = @mainscenariodescription where scen_id = @scen_id and projectId = @projectId";
                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {

                    projectId,
                    scenario.MainScenarioId,
                    scenario.MainScenarioName,
                    scenario.MainScenarioDescription,
                    scenario.RunMode,
                    scenario.Status,
                    scenario.Active,
                    scenario.TestScenExecDate,
                    scenario.TestScenExecBy,
                    scenario.TestScenLastStatus,
                    scenario.CreatedBy,
                    scenario.CreatedDate,
                    scenario.UpdatedBy,
                    scenario. UpdatedDate,
                    scenario.Scen_Id
                    

                });
                return Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw ex;
                

            }
            finally
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            



        }

        public async Task<bool> DeleteScenario(Scenario scen)
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
                string delQuery = @"delete from tblTestSteps where cast(scen_id as integer) = @scen_id and cast(projectid as integer) = @ProjectId";
                var result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    scen.Scen_Id,
                    scen.ProjectId

                });
                delQuery = @"delete from tblSubScenarios where cast(scen_id as integer) = @scen_id and cast(projectid as integer) = @projectId";
                 result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    scen.Scen_Id,
                    scen.ProjectId

                });

                delQuery = @"delete from tblscenarios where cast(scen_id as integer) = @scen_id and cast(projectid as integer) = @ProjectId";


                result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    scen.Scen_Id,
                    scen.ProjectId

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

        public async Task<IQueryable<string>> GetMaxScenarioId(int projectId)
        {
            //{
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                //string query = "select max(a[3]) from( select regexp_split_to_array(mainscenarioid, '_') from tblscenarios where projectid = @projectId ) as dt(a)";
                string query = "select b from ( select projectid, mainscenarioid, substring(mainscenarioid, 10) as val, cast(case when strpos(mainscenarioid, '_') != 0 then substring(mainscenarioid, 10) else mainscenarioid end as integer) b from tblscenarios where projectid = @projectId) temp order by b desc nulls last limit 1; ";

                var result = await dbConnection.QueryAsync<String>(query, new
                {
                    projectId

                });
                // var result = await dbConnection.ExecuteAsync(delQuery);
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
            // return true;
        }


    }
}
