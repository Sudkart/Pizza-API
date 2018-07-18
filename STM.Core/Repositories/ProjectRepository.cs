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
    public  class ProjectRepository: IProjectRepository
    {
        private string connectionstring;
        private IConnectionFactory _connectionFactory;
        public ProjectRepository(IConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;
        }

         public async Task<IQueryable<Project>> GetProjects()
        {

            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                // var projects = await dbConnection.QueryAsync<Project>("SELECT * FROM tblprojects");
                var projects = await dbConnection.QueryAsync<Project>("select projectid, projectname, projectdesc, projectowner, isincluded, case(active) when 1 then True else False end as active, createdby, createddate, updatedby, updateddate, projectcode,countryid from tblprojects ");
                return projects.AsQueryable();
            }

        }

        public async Task<IQueryable<Project>> GetActiveProjects()
        {

            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                 var projects = await dbConnection.QueryAsync<Project>("SELECT * FROM tblprojects where active = 1");
                 return projects.AsQueryable();
            }

        }



        public async Task<bool> CreateProject(Project project)
        {
      
            IDbConnection dbConnection = _connectionFactory.GetConnection();
            try
            {

                dbConnection.Open();

                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                // string updateQuery = @"Update tblUsers set email = @email where userid =@userid";
                string createQuery = @"insert into tblprojects(projectname, projectdesc, projectowner, active, createdby, createddate,updatedby, updateddate, projectcode, countryid) values(@projectname, @projectdesc, @projectowner, @active, @createdby, @createddate, @updatedby, @updateddate, @projectcode, @countryid); select lastval()";
                var result = await dbConnection.ExecuteAsync(createQuery, new
                {
                      project.ProjectName,
                      project.ProjectDesc,
                      project.ProjectOwner,
                      project.Active,
                      project.CreatedBy,
                      project.CreatedDate,
                      project.UpdatedBy,
                      project.UpdatedDate,
                      project.ProjectCode,
                      project.CountryId
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

          public async Task<bool> UpdateProject(Project project)
        {

            //using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            // {
                  IDbConnection dbConnection = _connectionFactory.GetConnection();
             try
                {
                
                    dbConnection.Open();
                //  var users = await dbConnection.QueryAsync<User>("Update tblUsers set email = '" + user.Email + "' where userid = " + user.UserId);
                //  string updateQuery = @"update tblprojects set projectname = @projectname, projectdesc = @projectdesc, projectowner = @projectowner, active = @active, createdby = @createdby, createddate = @createddate, updatedby = @updatedby, updateddate = @updateddate, projectcode = @projectcode, countryid = @countryid where projectid = @projectid";
                string updateQuery = @"update tblprojects set projectname = @projectname, projectdesc = @projectdesc, projectowner = @projectowner, projectcode = @projectcode where projectid = @projectid";
                    var result = await dbConnection.ExecuteAsync(updateQuery, new
                    {
                        project.ProjectName,
                        project.ProjectDesc,
                        project.ProjectOwner,
                        project.Active,
                        project.CreatedBy,
                        project.CreatedDate,
                        project.UpdatedBy,
                        project.UpdatedDate,
                        project.ProjectCode,
                        project.CountryId,
                        project.ProjectId
                       
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

        public async Task<bool> DeleteProject(Project project)
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
                //string delQuery = @"delete from tblprojects where projectid = @projectId";
                string delQuery = @"update tblProjects set active = 0 where projectid = @projectId";
                    var result = await dbConnection.ExecuteAsync(delQuery, new
                     {
                         project.ProjectId

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

        public async Task<Project> GetProjectById(int projid)
        {
            using (IDbConnection dbConnection = _connectionFactory.GetConnection())
            {
                dbConnection.Open();
                var projects = await dbConnection.QueryAsync<Project>("SELECT * FROM tblprojects");

                return projects.SingleOrDefault(item => item.ProjectId == projid);
            }
        }
    }
}
