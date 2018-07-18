using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STM.Core.EntityLayer;
using Action = STM.Core.EntityLayer.Action;

namespace STM.Core.Repositories
{
    public class ActionRepository:IActionRepository
    {

        private IConnectionFactory _connectionFactory;
        public ActionRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }


        public Task<bool> CreateAction(Action subscenario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAction(Action subscenario, int actionid, int actiongroupid)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Action>> GetActions(int actiongroupid)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                // string query = @"SELECT * FROM tblScenarios where projectId = @projectId";
                // string query = @"SELECT ts.scen_id, ts.projectid, tp.projectname, ts.mainscenarioid, ts.mainscenarioname, ts.mainscenariodescription, ts.runmode, ts.status, ts.active, ts.testscenexecdate,ts.testscenexecby,ts.testscenlaststatus, ts.createdby, ts.createddate, ts.updatedby,ts.updateddate FROM tblScenarios ts inner join tblprojects tp on ts.projectid = tp.projectid where ts.projectId = @projectId";
                string query = @"select * from tblcommonaction tca  where tca.actiongroup= '" + actiongroupid + "'";

                //   var result = await dbConnection.QueryAsync<SubScenario>(query, new
                //  {
                //     projectId, ScenarioId
                // });
                var result = await dbConnection.QueryAsync<Action>(query);
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

        public async Task<IQueryable<Action>> GetAllActions()
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                string query = @"select * from tblcommonaction";
                var result = await dbConnection.QueryAsync<Action>(query);
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

        public Task<bool> DeleteAction(int actionid)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Action>> GetactionswithActiongroups()
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                dbConnection.Open();
                string query = @"select tca.actionid, tca.actiongroup, tca.actiontype, tca.actionname,tag.""Action"" as ActionGroupName,tca.actiondescription, tca.actionremarks, tca.createdby,tca.createddate, tca.updatedby, tca.updateddate from tblcommonaction tca inner join tblactiongroup tag on cast(tca.actiongroup as integer) = cast(tag.groupid as integer)";
                var result = await dbConnection.QueryAsync<Action>(query);
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
    }
}
