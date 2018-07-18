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
    [Route("api/Scenarios")]
    public class ScenariosController : Controller
    {
        private IScenarioRepository _scenarioRepository;
        public ScenariosController(IScenarioRepository scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;
        }


        //Get all Scenarios
        /// <summary>
        /// Retrieves a list of all Scenarios 
        /// </summary>
        [HttpGet]
        [Route("AllScenarios")]
        public async Task<IActionResult> GetAllScenariosAsync()
        {
            var response = new ListModelResponse<ScenarioViewModel>();

            try
            {
                var scenariosDataModel = await _scenarioRepository.GetAllScenarios();

                response.Model = scenariosDataModel.Select(item => item.ToViewModel());

                response.Message = String.Format("Total of records: {0}", response.Model.Count());


                ///return response.ToHttpResponse();
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();

        }
        //Get Scenarios
        /// <summary>
        /// Retrieves a list of Scenarios based on Project Id
        /// </summary>
        [HttpGet]
        [Route("Scenario/{projectId}/")]
        public async Task<IActionResult> GetScenariosAsync(int projectId)
        {
            var response = new ListModelResponse<ScenarioViewModel>();

            try
            {
                // response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;
                var scenariosDataModel = await _scenarioRepository
                        .GetScenarios(projectId);
                response.Model = scenariosDataModel.Select(item => item.ToViewModel());

                response.Message = String.Format("Total of records: {0}", response.Model.Count());


            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        //Create Scenarios
        /// <summary>
        /// Create a new Scenario  based on Project Id
        /// </summary>

        [HttpPost]
        [Route("CreateScenario")]

        public async Task<string> CreateScenarioAsync([FromBody]ScenarioViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<ScenarioViewModel>();
            var scenarioDataModel = 0;
            try
            {
                //Logic to add new scenario comes here
                var maxscenId = await _scenarioRepository.GetMaxScenarioId(request.ProjectId.Value);

                request.MainScenarioId = Convert.ToString(Convert.ToInt32(maxscenId.FirstOrDefault()) + 1); //ends here
                scenarioDataModel = await _scenarioRepository.CreateScenario(request.ToEntity());


                if (scenarioDataModel > 0)

                    // response.Message = String.Format("Created User Successfully");
                    response.Message = Messages.CreateSuccessMsg;
                else
                    //response.Message = String.Format("Create User failed");
                    response.Message = Messages.CreateFailMsg;
            }

            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.Message;


        }


        //Update Scenario
        /// <summary>
        /// Updates Scenario based on id
        /// </summary>
        [HttpPost]
        [Route("UpdateScenario/{projectId}/")]
        public async Task<string> UpdateScenarioAsync([FromBody]ScenarioViewModel request, int projectId)
        {
            
            var response = new ListModelResponse<ScenarioViewModel>();
            var scenariosDataModel = false;
            

            try
            {
                scenariosDataModel = await _scenarioRepository.UpdateScenario(request.ToEntity(), projectId);

                if (scenariosDataModel)
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

        
        //Delete Scenario
        /// <summary>
        /// Deletes scenario based on id
        /// </summary>
        [HttpPost("DeleteScenario")]
        public async Task<string> DeleteScenAsync([FromBody]ScenarioViewModel request)
        {

            var response = new ListModelResponse<ScenarioViewModel>();

            var scenariosDataModel = false;
            try
            {
                scenariosDataModel = await _scenarioRepository.DeleteScenario(request.ToEntity());

                if (scenariosDataModel)
                    response.Message = Messages.DeletionSuccessMsg;
                else
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