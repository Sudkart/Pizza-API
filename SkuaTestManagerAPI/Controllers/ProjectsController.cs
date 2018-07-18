using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STMAPI.Responses;
using STMAPI.ViewModels;
using STM.Core.EntityLayer;
using Npgsql;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STM.Core.Repositories;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STMAPI
{

    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        private IProjectRepository _projectRepository;
        public ProjectsController(IProjectRepository projectRepository)
        {
            
            _projectRepository = projectRepository;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Get Projects
        /// <summary>
        /// Retrieves a list of Projects
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("Project")]
        public async Task<IActionResult> GetProjectsAsync()

        {
            var response = new ListModelResponse<ProjectViewModel>();

            try
            {
                //response.PageSize = (Int32)pageSize;
               // response.PageNumber = (Int32)pageNumber;

                var projectsDataModel = await _projectRepository.GetProjects();
                response.Model = projectsDataModel.Select(item => item.ToViewModel());
                //.Select(item => item.ToViewModel())
                //.ToListAsync();

                response.Message = String.Format("Total of records: {0}", response.Model.Count());


            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        [HttpGet]
        [Route("ActiveProject")]
        public async Task<IActionResult> GetActiveProjectsAsync()

        {
            var response = new ListModelResponse<ProjectViewModel>();

            try
            {
                //response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;

                var projectsDataModel = await _projectRepository.GetActiveProjects();
                response.Model = projectsDataModel.Select(item => item.ToViewModel());
                //.Select(item => item.ToViewModel())
                //.ToListAsync();
                response.Message = String.Format("Total of records: {0}", response.Model.Count());


            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }



        //Create Project
        /// <summary>
        /// Create a new Project
        /// </summary>
        [HttpPost]
        [Route("CreateProject")]

        public async Task<string> CreateProjectAsync([FromBody]ProjectViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<UserViewModel>();
            bool usersDataModel = false; 
            try
            {
             usersDataModel = await _projectRepository.CreateProject(request.ToEntity());

                

                if (usersDataModel)
                    response.Message = Messages.CreateSuccessMsg;
                //response.Message = String.Format("Created Project Successfully");

                else
                    //  response.Message = String.Format("Create Project failed");
                    response.Message = Messages.CreateFailMsg;
            }

            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;


        }
        //Update Projects
        /// <summary>
        /// Updates project based on id
        /// </summary>
        [HttpPost]
        [Route("UpdateProject")]
        public async Task<string> UpdateProjectAsync([FromBody]ProjectViewModel request)
        {
            //HttpRequest res = null;
            var response = new ListModelResponse<ProjectViewModel>();
            var projectsDataModel = false;

            try
            {
                projectsDataModel = await _projectRepository.UpdateProject(request.ToEntity());

                if (projectsDataModel)
                    //   response.Message = String.Format("Record Updated Successfully");
                    response.Message = Messages.SuccessMsg;
                else
                    //  response.Message = String.Format("Record Updation failed");
                    response.Message = Messages.FailMsg;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;

        }



        //Delete Project
        /// <summary>
        /// Deletes project based on id
        /// </summary>
        [HttpPost("DeleteProject")]
        public async Task<string> DeleteProjectAsync([FromBody]ProjectViewModel request)
        {

            var response = new ListModelResponse<ProjectViewModel>();
           
            var projectsDataModel = false;
            try
            {
                projectsDataModel = await _projectRepository.DeleteProject(request.ToEntity());

                if (projectsDataModel)
                    // response.Message = String.Format("Record Deleted Successfully");
                    response.Message = Messages.DeletionSuccessMsg;
                else
                    //response.Message = String.Format("Record Deletion failed");
                    response.Message = Messages.DeletionFailMsg;

            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;
        }
    }
}
