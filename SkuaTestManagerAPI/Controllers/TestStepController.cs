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

namespace STMAPI
{
    [Produces("application/json")]
    [Route("api/TestStep")]
    public class TestStepController : Controller
    {
        private ITestStepRepository _teststepRepository;
        private IObjectRepository _objectRepository;
        private IProjectRepository _projectRepository;
        public TestStepController(ITestStepRepository teststepRepository, IObjectRepository objectRepository, IProjectRepository projectRepository)
        {
            _teststepRepository = teststepRepository;
            _objectRepository = objectRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        [Route("TestSteps/{projectId}/{scenarioId}/{subscenarioId}")]
        public async Task<IActionResult> GetTestStepsAsync(int projectId, int scenarioId, int subscenarioId)
        {
            var response = new ListModelResponse<TestStepViewModel>();

            try
            {
                // response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;
                // var subscenariosDataModel = await _subscenarioRepository
                //   .GetSubScenarios(projectId, scenarioId);

                var teststepsDataModel = await _teststepRepository
                     .GetTestStepsAsync(projectId, scenarioId, subscenarioId);
                response.Model = teststepsDataModel.Select(item => item.ToViewModel());

                response.Message = String.Format("Total of records: {0}", response.Model.Count());


            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }


        //Create a TestStep
        /// <summary>
        /// Create a new TestStep
        /// </summary>
        [HttpPost]
        [Route("CreateTestStep")]

        public async Task<string> CreateTestStepAsync([FromBody]TestStepViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<TestStepViewModel>();
            var testStepsDataModel = false;
            try
            {

                testStepsDataModel = await _teststepRepository.CreateTestStep(request.ToEntity());

                if (testStepsDataModel)
                    // response.Message = String.Format("Created User Successfully");
                    response.Message = Messages.SuccessMsg;
                else
                    //response.Message = String.Format("Create User failed");
                    response.Message = Messages.FailMsg;
            }

            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;
        }


        [HttpPost]
        [Route("CreateBulkTestStep")]

        public async Task<string> CreateBulkTestStepAsync([FromBody]TestStepViewModel[] requests)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<TestStepViewModel>();
            var testStepsDataModel = false;
            bool teststepsdeletion = false;
            try
            {

                string projectname = string.Empty;
                var projects = await _projectRepository.GetProjects();
                List<TestStepViewModel> failedteststeps = new List<TestStepViewModel>();
                int teststepid = 0;
                foreach (var request in requests)
                {
                    if (string.IsNullOrEmpty(projectname))
                    {
                        var projectinfo = projects.SingleOrDefault(item => item.ProjectId == request.ProjectId);
                        projectname = projectinfo.ProjectName;
                    }

                    if (request.ObjectId == 0)
                    {
                        int objectId = await _objectRepository.CreateObjectWithReturnId(new TestObject()
                        {
                            ObjectName = request.ObjectName,
                            ObjectValue = request.Xpath,
                            IdentityType = request.IdentityType,
                            ProjectName = projectname
                        });
                        request.ObjectId = objectId;
                    }

                    int deletedrecords = 0;
                    if (!teststepsdeletion)
                        deletedrecords = await _teststepRepository.DeleteAllTestStepsfromSubscenerio(
                            request.Sub_Scen_Id.GetValueOrDefault(), request.ProjectId.GetValueOrDefault());

                    if (deletedrecords >= 0)
                    {
                        teststepsdeletion = true;
                        try
                        {
                            request.TestStepId =Convert.ToString(++teststepid);
                            request.CreatedBy = request.UpdatedBy = "Admin";
                            testStepsDataModel = await _teststepRepository.CreateTestStep(request.ToEntity());
                            if (testStepsDataModel)
                            {
                                response.Message = Messages.CreateSuccessMsg;
                            }
                            else
                            {
                                response.Message = Messages.CreateFailMsg;
                                //failedteststeps.Add(request);
                                //continue;
                            }
                        }
                        catch (Exception e)
                        {
                            //failedteststeps.Add(request);
                            //continue;
                        }
                       
                    }
                    else
                    {
                        response.Message = Messages.FailMsg;
                    }
                }

            }

            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;


        }

        //Update TestStep
        /// <summary>
        /// Updates teststep based on id
        /// </summary>
        [HttpPost]
        [Route("UpdateTestStep/{teststepId}")]
        public async Task<string> UpdateUserAsync([FromBody]TestStepViewModel request, int teststepId)
        {
            //HttpRequest res = null;
            var response = new ListModelResponse<TestStepViewModel>();
            var usersDataModel = false;
            try
            {
                usersDataModel = await _teststepRepository.UpdateTestStep(request.ToEntity(), teststepId);

                if (usersDataModel)
                    //  response.Message = String.Format("Record Updated Successfully");
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



        //Delete TestStep
        /// <summary>
        /// Deletes TestStep based on teststepid and projectId
        /// </summary>
        [HttpPost("DeleteProject/{teststepId}/{projectId}")]
        public async Task<string> DeleteProjectAsync(int teststepId, int projectId)
        {

            var response = new ListModelResponse<ProjectViewModel>();

            var projectsDataModel = false;
            try
            {
                projectsDataModel = await _teststepRepository.DeleteTestStep(teststepId, projectId);

                if (projectsDataModel)
                    // response.Message = String.Format("Record Deleted Successfully");
                    response.Message = Messages.SuccessMsg;
                else
                    //response.Message = String.Format("Record Deletion failed");
                    response.Message = Messages.FailMsg;

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