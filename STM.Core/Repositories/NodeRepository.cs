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
   public class NodeRepository: INodeRepository
    {

        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public NodeRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }

        public async Task<IQueryable<Node>> GetNodes()
        {

            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var nodes = await dbConnection.QueryAsync<Node>("SELECT * FROM tblnode");

                return nodes.AsQueryable();
            }

        }
        public async Task<bool> UpdateNode(Node node, int nodeId)
        {

            //using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            // {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                string updateQuery = @"update tblnode set nodeip = @nodeip, nodename = @nodename, nodeportno = @nodeportno, hostname = @hostname, 
active = @active, createdby = @createdby, createddate = @createddate, updatedby = @updatedby, updateddate = @updateddate where nodeid = @nodeid";
                var result = await dbConnection.ExecuteAsync(updateQuery, new
                {
                    node.NodeIp,
                    node.NodeName,
                    node.NodePortNo,
                    node.HostName,
                    node.Active,
                    node.CreatedBy,
                    node.CreatedDate,
                    node.UpdatedBy,
                    node.UpdatedDate,
                    nodeid = nodeId

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

        public async Task<bool> CreateNode(Node node)
        {
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();

                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"Update tblUsers set email = @email where userid =@userid";
                string createQuery = @"insert into tblnode(nodeid, nodeip, nodename, nodeportno, hostname, hostip, hostportno, active, createdby, createddate, updatedby, updateddate) 
                         values(@nodeid, @nodeip, @nodename, @nodeportno, @hostname, @hostip, @hostportno, @active, @createdby, @createddate, @updatedby, @updateddate);";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                    node.NodeId,
                    node.NodeIp,
                    node.NodeName,
                    node.NodePortNo,
                    node.HostName,
                    node.HostIp,
                    node.HostPortNo,
                    node.Active,
                    node.CreatedBy,
                    node.CreatedDate,
                    node.UpdatedBy,
                    node.UpdatedDate
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

        public async Task<bool> DeleteNode(int nodeId)
        {
            
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {
                // IDbConnection dbConnection = _connectionFactory.GetConnection()
                dbConnection.Open();

                string delQuery = @"delete from tblnode where nodeid = @nodeid";
                var result = await dbConnection.ExecuteAsync(delQuery, new
                {
                   nodeid = nodeId

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
