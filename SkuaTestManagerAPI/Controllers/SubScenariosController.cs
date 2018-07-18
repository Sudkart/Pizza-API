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
using Newtonsoft.Json;

namespace STMAPI
{
    [Produces("application/json")]
    [Route("api/SubScenarios")]
    public class SubScenariosController : Controller
    {
        private ISubScenarioRepository _subscenarioRepository;
        public SubScenariosController(ISubScenarioRepository subscenarioRepository)
        {
            _subscenarioRepository = subscenarioRepository;
        }

        /// <summary>
        /// Retrieves a list of SubScenarios based on Project Id and Scenario Id
        /// </summary>
       [HttpGet]
        [Route("SubScenario/{projectId}/{scenarioId}/")]
        public async Task<IActionResult> GetSubScenariosAsync(int projectId, int scenarioId)
        {
            var response = new ListModelResponse<SubScenarioViewModel>();

            try
            {
                // response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;
                var subscenariosDataModel = await _subscenarioRepository
                        .GetSubScenarios(projectId, scenarioId);
                response.Model = subscenariosDataModel.Select(item => item.ToViewModel());

                response.Message = String.Format("Total of records: {0}", response.Model.Count());


            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

		/// <summary>
		/// Retrieves a list of SubScenarios and scenarios based on Project Id 
		/// </summary>
		[HttpGet]
		[Route("SubScenariosWithScenarios/{projectId}")]
		public async Task<IActionResult> GetSubScenarioswithScenariosAsync(int projectId)
		{
			var response = new ListModelResponse<SubScenarioViewModel>();

			try
			{
				// response.PageSize = (Int32)pageSize;
				// response.PageNumber = (Int32)pageNumber;
				var subscenariosDataModel = await _subscenarioRepository
						.GetSubScenarioswithScenarios(projectId);
				response.Model = subscenariosDataModel.Select(item => item.ToViewModel());
                var json = JsonConvert.SerializeObject(response.Model);
                //var json = new JavaScriptSerializer().Serialize(response.Model);
                response.Message = String.Format("Total of records: {0}", response.Model.Count());


			}
			catch (Exception ex)
			{
				response.DidError = true;
				response.ErrorMessage = ex.Message;
			}

			return response.ToHttpResponse();
		}



		[HttpPost]
        [Route("CreateSubScenario")]

        public async Task<string> CreateSubScenarioAsync([FromBody]SubScenarioViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<SubScenarioViewModel>();
            var scenarioDataModel = false;
            try
            {

                var maxscenId = await _subscenarioRepository.GetMaxSubScenarioId(request.ProjectId.Value, request.Scen_Id.Value);

                request.SubScenarioId = Convert.ToString(Convert.ToInt32(maxscenId.FirstOrDefault()) + 1); //ends here
                scenarioDataModel = await _subscenarioRepository.CreateSubScenario(request.ToEntity());

                if (scenarioDataModel)
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


        //Update SubScenario
        /// <summary>
        /// Updates SubScenario based on id
        /// </summary>
        [HttpPost]
        [Route("UpdateSubScenario/{scenId}/{projId}/")]
        public async Task<string> UpdateSubScenarioAsync([FromBody]SubScenarioViewModel request, int scenId,int projId)
        {

            var response = new ListModelResponse<SubScenarioViewModel>();
            var scenariosDataModel = false;

            try
            {
                scenariosDataModel = await _subscenarioRepository.UpdateSubScenario(request.ToEntity(),  scenId, projId);

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


        /// <summary>
        /// Retrieves a list of SubScenarios based on Project Id and Scenario Id
        /// </summary>
        [HttpGet]
        [Route("SubScenario")]
        public async Task<IActionResult> GetAllSubScenariosAsync()
        {
            var response = new ListModelResponse<SubScenarioViewModel>();

            try
            {
                // response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;
              var subscenariosDataModel = await _subscenarioRepository.GetAllSubScenarios();
                       
                response.Model = subscenariosDataModel.Select(item => item.ToViewModel());

                response.Message = String.Format("Total of records: {0}", response.Model.Count());


            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }


        //Delete SubScenario
        /// <summary>
        /// Deletes subscenario based on id
        /// </summary>
        [HttpPost("DeleteSubScenario")]
        public async Task<string> DeleteSubScenAsync([FromBody]SubScenarioViewModel request)
        {

            var response = new ListModelResponse<SubScenarioViewModel>();

            var subscenariosDataModel = false;
            try
            {
                subscenariosDataModel = await _subscenarioRepository.DeleteSubScenario(request.ToEntity());

                if (subscenariosDataModel)
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