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
using System;

namespace STM.Core.Repositories
{
    public class SubScenarioRepository: ISubScenarioRepository
    {
        private IConnectionFactory _connectionFactory;
        public SubScenarioRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<SubScenario>> GetAllSubScenarios()
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                string query = @"select tss.sub_scen_id, tss.scen_id, tss.projectid, tp.projectname, ts.mainscenarioname, tss.scen_id, tss.sub_scen_id, tss.subscenarioid, tss.subscenarioname, tss.subscenariodescription, tss.runmode, tss.status, tss.active, tss.testsubscenexecdate, tss.testsubscenexecby, tss.createdby,tss.createddate, tss.updatedby, tss.updateddate from tblSubScenarios tss inner join tblprojects tp on cast(tss.projectid as int) = cast(tp.projectid as int) inner join tblscenarios ts on cast(tss.scen_id as int) = cast(ts.scen_id as int) limit 100";
                var result = await dbConnection.QueryAsync<SubScenario>(query);
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

        public async Task<IQueryable<SubScenario>> GetSubScenarios(int projectId, int ScenarioId)
        {

            // using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            //{
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                // string query = @"SELECT * FROM tblScenarios where projectId = @projectId";
                // string query = @"SELECT ts.scen_id, ts.projectid, tp.projectname, ts.mainscenarioid, ts.mainscenarioname, ts.mainscenariodescription, ts.runmode, ts.status, ts.active, ts.testscenexecdate,ts.testscenexecby,ts.testscenlaststatus, ts.createdby, ts.createddate, ts.updatedby,ts.updateddate FROM tblScenarios ts inner join tblprojects tp on ts.projectid = tp.projectid where ts.projectId = @projectId";
                string query = @"select tss.sub_scen_id, tss.scen_id, tss.projectid, tp.projectname, ts.mainscenarioname, tss.scen_id, tss.sub_scen_id, tss.subscenarioid, tss.subscenarioname, tss.subscenariodescription, tss.runmode, tss.status, tss.active, tss.testsubscenexecdate, tss.testsubscenexecby, tss.createdby,tss.createddate, tss.updatedby, tss.updateddate from tblSubScenarios tss inner join tblprojects tp on cast(tss.projectid as integer) = cast(tp.projectid as integer) inner join tblscenarios ts on cast(tss.scen_id as integer) = cast(ts.scen_id as integer) where tss.projectid = '"+ projectId + "' and tss.scen_id= '"+ ScenarioId +"'";

                //   var result = await dbConnection.QueryAsync<SubScenario>(query, new
                //  {
                //     projectId, ScenarioId
                // });
                var result = await dbConnection.QueryAsync<SubScenario>(query);
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

        public async Task<IQueryable<SubScenario>> GetSubScenarioswithScenarios(int projectId)
        {

            // using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            //{
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                // string query = @"SELECT * FROM tblScenarios where projectId = @projectId";
                // string query = @"SELECT ts.scen_id, ts.projectid, tp.projectname, ts.mainscenarioid, ts.mainscenarioname, ts.mainscenariodescription, ts.runmode, ts.status, ts.active, ts.testscenexecdate,ts.testscenexecby,ts.testscenlaststatus, ts.createdby, ts.createddate, ts.updatedby,ts.updateddate FROM tblScenarios ts inner join tblprojects tp on ts.projectid = tp.projectid where ts.projectId = @projectId";
                string query = @"select tss.sub_scen_id, tss.scen_id, tss.projectid, tp.projectname, ts.mainscenarioname as scenarioname, tss.scen_id, tss.sub_scen_id, tss.subscenarioid, tss.subscenarioname, tss.subscenariodescription, tss.runmode, tss.status, tss.active, tss.testsubscenexecdate, tss.testsubscenexecby, tss.createdby,tss.createddate, tss.updatedby, tss.updateddate from tblSubScenarios tss inner join tblprojects tp on cast(tss.projectid as integer) = cast(tp.projectid as integer) inner join tblscenarios ts on cast(tss.scen_id as integer) = cast(ts.scen_id as integer) where tss.projectid = '" + projectId + "'";

                //   var result = await dbConnection.QueryAsync<SubScenario>(query, new
                //  {
                //     projectId, ScenarioId
                // });
                var result = await dbConnection.QueryAsync<SubScenario>(query);
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



        public async Task<bool> CreateSubScenario(SubScenario SubScenario)
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
                string createQuery = @"Insert into tblSubScenarios(projectid, scen_id, sub_scen_id, subscenarioid, subscenarioname, subscenariodescription,runmode, status, active, isautomated, iscompleted, testsubscenexecdate, testsubscenexecby, testsubscenlaststatus, createdby, createddate, updatedby, updateddate) values (@projectId, @scen_id, nextval('tblsubscenarios_sub_scen_id_seq'::regclass), @subscenarioid, @subscenarioname, @subscenariodescription, @runmode, @status, @active, @isautomated, @iscompleted, @testsubscenexecdate, @testsubscenexecby, @testsubscenlaststatus, @createdby, @createdDate, @updatedBy, @updateddate)";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                    SubScenario.ProjectId,
                    SubScenario.Scen_Id,
                    //SubScenario.Sub_Scen_Id,
                    SubScenario.SubScenarioId,
                    SubScenario.SubScenarioName,
                    SubScenario.SubScenarioDescription,
                    SubScenario.RunMode,
                    SubScenario.Status,
                    SubScenario.Active,
                    SubScenario.IsAutomated,
                    SubScenario.IsCompleted,
                    SubScenario.TestSubScenExecDate,
                    SubScenario.TestSubScenExecBy,
                    SubScenario.TestSubScenLastStatus,
                    SubScenario.CreatedBy,
                    SubScenario.CreatedDate,
                    SubScenario.UpdatedBy,
                    SubScenario.UpdatedDate
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

        public async Task<bool> UpdateSubScenario(SubScenario subScenario, int scenId, int projectId)
        {

            //using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            // {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"update tblSubScenarios set subscenarioid  = @subscenarioid, subscenarioname = @subscenarioname, subscenariodescription = @subscenariodescription, runmode = @runmode, status = @status, active = @active, isautomated = @isautomated, iscompleted = @iscompleted, testsubscenexecdate = @testsubscenexecdate, testsubscenexecby = @testsubscenexecby, testsubscenlaststatus =  @testsubscenlaststatus, createdby = @createdby, createddate  = @createddate, updatedby = @updatedby, updateddate = @updateddate  where cast(sub_scen_id as int) = cast(@sub_scen_id as int) and cast(scen_id as int) = cast(@scenId as int) and cast(projectid as int) = cast(@projectId as int)";
                string updateQuery = @"update tblSubScenarios set subscenarioname = @subscenarioname, subscenariodescription = @subscenariodescription where cast(sub_scen_id as int) = cast(@sub_scen_id as int) and cast(scen_id as int) = cast(@scenId as int) and cast(projectid as int) = cast(@projectId as int)";
                // string updateQuery = @"update tblScenarios set projectid = @projectid, mainscenarioid = @mainscenarioid, mainscenarioname = @mainscenarioname, mainscenariodescription = @mainscenariodescription, runmode = @runmode, status = @status, active = @active, testscenexecdate = @testscenexecdate, testscenexecby = @testscenexecby, testscenlaststatus =  @testscenlaststatus, createdby = @createdby, createddate = @createddate, updatedby = @updatedby, updateddate = @updateddate where scen_id = @scen_id and projectId = @projectId";


                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {
                subScenario.SubScenarioId,
                subScenario.SubScenarioName,
                subScenario.SubScenarioDescription,
                subScenario.RunMode,
                subScenario.Status,
                subScenario.Active,
                subScenario.IsAutomated,
                subScenario.IsCompleted,
                subScenario.TestSubScenExecDate,
                subScenario.TestSubScenExecBy,
                subScenario.TestSubScenLastStatus,
                subScenario.CreatedBy,
                subScenario.CreatedDate,
                subScenario.UpdatedBy,
                subScenario.UpdatedDate,
                subScenario.Sub_Scen_Id,
                scenId,
                projectId
            });
                return Convert.ToBoolean(result);
            }
            catch (NpgsqlException ex)
            {
                Console.Write(ex.Message);
                throw ex;

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

        public async Task<bool> DeleteSubScenario(SubScenario subScenario)
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
                string delQuery = @"delete from tblteststeps where cast(sub_scen_id as int) = cast(@sub_scen_id as int) and cast(projectid as int) = cast(@projectid as int)";
                var result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    sub_scen_id = subScenario.Sub_Scen_Id,

                    projectid = subScenario.ProjectId

                });
                delQuery = @"delete from tblsubscenarios where cast(sub_scen_id as int) = cast(@sub_scen_id as int) and cast(projectid as int) = cast(@projectid as int)";
                result = await dbConnection.ExecuteAsync(delQuery, new
                {
                    sub_scen_id = subScenario.Sub_Scen_Id,

                    projectid = subScenario.ProjectId

                });
                // var result = await dbConnection.ExecuteAsync(delQuery);
                var result1 = Convert.ToBoolean(result);
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

        public async Task<IQueryable<string>> GetMaxSubScenarioId(int projectId, int scenId)
        {
            //{
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                //  string query = "select max(a[5]) from( select regexp_split_to_array(subscenarioid, '_') from tblsubscenarios where cast(projectid as int) = @projectId and cast(scen_id as int) = @scenId ) as dt(a)";
                string query = "select b from ( select projectid,subscenarioid,substring(subscenarioid,17) as val, cast(case when strpos(subscenarioid,'_')!=0 then substring(subscenarioid,17) else subscenarioid end as integer) b  from tblsubscenarios where projectid = cast(@projectId as varchar) and scen_id = cast(@scenId as varchar)) temp  order by b desc nulls last limit 1;";

                var result = await dbConnection.QueryAsync<String>(query, new
                {
                   projectId,
                   scenId
                    //Convert.ToString(scenId)

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
