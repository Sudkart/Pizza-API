using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STMAPI.Responses;
using STMAPI.ViewModels;
//using STM.Core.EntityLayer;
using Npgsql;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using STM.Core.Repositories;
//using System.Net.Http;
using STM.Core.Repositories;
namespace STMAPI
{
    [Produces("application/json")]
    [Route("api/ProjKeyValues")]
    public class ProjKeyValuesController : Controller
    {
        private IProjKeyValueRepository _projkeyvalueRepository;
        public ProjKeyValuesController(IProjKeyValueRepository projkeyvalueRepository)
        {

            _projkeyvalueRepository = projkeyvalueRepository;
        }

       
        //Get Project Key Value Pairs
        /// <summary>
        /// Retrieves a list of Proj Key Value Pairs
        /// </summary>
        [HttpGet]
        [Route("ProjKeyValue")]
        public async Task<IActionResult> GetProjKeyValuesAsync()
        {
            var response = new ListModelResponse<ProjKeyValueViewModel>();

            try
            {
                //response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;

                var projkeyvaluesDataModel = await _projkeyvalueRepository.GetProjKeyValuePairs();
                response.Model = projkeyvaluesDataModel.Select(item => item.ToViewModel());
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


        //Create Project Key Value Pair
        /// <summary>
        /// Create a new Project Key Value Pair
        /// </summary>

        [HttpPost]
        [Route("CreateProjKeyValue")]

        public async Task<string> CreateProjKeyValueAsync([FromBody]ProjKeyValueViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<ProjKeyValueViewModel>();
            var projkeyvalueDataModel = false;
            try
            {

                projkeyvalueDataModel = await _projkeyvalueRepository.CreateKeyValuePair(request.ToEntity());

                if (projkeyvalueDataModel)
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

        //Update Project Key Value Pair
        /// <summary>
        /// Upate a new Project Key Value Pair based on id
        /// </summary>

        [HttpPut]
        [Route("UpdateProjKeyValuePair/{varId}/")]
        public async Task<string> UpdateProjectAsync([FromBody]ProjKeyValueViewModel request, int varId)
        {
            
            var response = new ListModelResponse<ProjKeyValueViewModel>();
            var projectsDataModel = false;


            try
            {
                projectsDataModel = await _projkeyvalueRepository.UpdateProjKeyValuePair(request.ToEntity(), varId);

                if (projectsDataModel)
                   
                    response.Message = Messages.SuccessMsg;
                else
                   
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
        [HttpPost("DeleteProjKeyValuePair/{varId}/")]
        public async Task<string> DeleteProjectAsync(int varId)
        {
             var response = new ListModelResponse<ProjKeyValueViewModel>();

            var projKeyValueDataModel = false;
            try
            {
                projKeyValueDataModel = await _projkeyvalueRepository.DeleteProjKeyValuePair(varId);

                if (projKeyValueDataModel)
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