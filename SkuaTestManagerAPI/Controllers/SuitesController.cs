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
    [Route("api/Suites")]
    public class SuitesController : Controller
    {
        private ISuiteRepository _suiteRepository;
        public SuitesController(ISuiteRepository suiteRepository)
        {
           _suiteRepository = suiteRepository;
        }



        //Get Suites
        /// <summary>
        /// Retrieves a list of Suites
        /// </summary>
        [HttpGet]
        [Route("Suite")]
        public async Task<IActionResult> GetSuitesAsync()
        {
            var response = new ListModelResponse<SuiteViewModel>();

            try
            {
                //response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;

                var suitesDataModel = await _suiteRepository.GetSuites();
                response.Model = suitesDataModel.Select(item => item.ToViewModel());
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

        //Update Suite
        /// <summary>
        /// Updates Suite based on id
        /// </summary>
        [HttpPut]
        [Route("UpdateSuite/{suiteId}/")]
        public async Task<string> UpdateSuiteAsync([FromBody]SuiteViewModel request, int suiteId)
        {
            request.UpdatedBy= "Admin";
            //HttpRequest res = null;
            var response = new ListModelResponse<SuiteViewModel>();
            var projectsDataModel = false;
            try
            {
                projectsDataModel = await _suiteRepository.UpdateSuite(request.ToEntity(), suiteId);

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



        //Create Suite
        /// <summary>
        /// Create a new Suite
        /// </summary>
        [HttpPost]
        [Route("CreateSuite")]

        public async Task<string> CreateSuiteAsync([FromBody]SuiteViewModel request)
        {
            // HttpRequest res = null;
            request.CreatedBy = request.UpdatedBy = "Admin";
            var response = new ListModelResponse<SuiteViewModel>();
            var suitesDataModel = false;
            try
            {
                request.Active = 1;
                suitesDataModel = await _suiteRepository.CreateSuite(request.ToEntity());

                if (suitesDataModel)
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

        //Delete Suite
        /// <summary>
        /// Deletes suite based on id
        /// </summary>
        [HttpPost("DeleteSuite/{id}/")]
        public async Task<string> DeleteProjectAsync(int id)
        {

            var response = new ListModelResponse<SuiteViewModel>();

            var suitesDataModel = false;
            try
            {
                suitesDataModel = await _suiteRepository.DeleteSuite(id);

                if (suitesDataModel)
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