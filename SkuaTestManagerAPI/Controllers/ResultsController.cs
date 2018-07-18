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

// For more information on enabling MVC for empty resultss, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STMAPI
{

    [Produces("application/json")]
    [Route("api/Results")]
    public class ResultsController : Controller
    {
        private IResultsRepository _resultsRepository;
        public ResultsController(IResultsRepository resultsRepository)
        {
            _resultsRepository = resultsRepository;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Get resultss
        /// <summary>
        /// Retrieves a list of resultss
        /// </summary>
        [HttpGet]
        [Route("results")]
        public async Task<IActionResult> GetResultsAsync()

        {
            var response = new ListModelResponse<ResultsViewModel>();
            try
            {

                var resultsDataModel = await _resultsRepository.GetLiveResults();
                foreach(var item in resultsDataModel)
                {
                    item.StartTime = Convert.ToDateTime(item.StartTime.ToString("yyyy-MM-dd hh:mm:ss"));
                    item.EndTime = Convert.ToDateTime(item.EndTime.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                response.Model = resultsDataModel.Select(item => item.ToViewModel());
                response.Message = String.Format("Total of records: {0}", response.Model.Count());
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }
            return response.ToHttpResponse();
        }


        ////Create results
        ///// <summary>
        ///// Create a new results
        ///// </summary>
        //[HttpPost]
        //[Route("Createresults")]

        //public async Task<string> CreateresultsAsync([FromBody]resultsViewModel request)
        //{
        //    // HttpRequest res = null;
        //    var response = new ListModelResponse<UserViewModel>();
        //    var usersDataModel = false;
        //    try
        //    {

        //        usersDataModel = await _resultsRepository.Createresults(request.ToEntity());

        //        if (usersDataModel)
        //            response.Message = Messages.SuccessMsg;
        //        //response.Message = String.Format("Created results Successfully");

        //        else
        //            //  response.Message = String.Format("Create results failed");
        //            response.Message = Messages.FailMsg;
        //    }

        //    catch (Exception ex)
        //    {
        //        response.DidError = true;
        //        response.ErrorMessage = ex.Message;
        //    }

        //    return response.Message;


        //}

        ////Update resultss
        ///// <summary>
        ///// Updates results based on id
        ///// </summary>
        //[HttpPut]
        //[Route("Updateresults/{id}/")]
        //public async Task<string> UpdateresultsAsync([FromBody]resultsViewModel request)
        //{
        //    //HttpRequest res = null;
        //    var response = new ListModelResponse<resultsViewModel>();
        //    var resultssDataModel = false;


        //    try
        //    {
        //        resultssDataModel = await _resultsRepository.Updateresults(request.ToEntity());

        //        if (resultssDataModel)
        //            //   response.Message = String.Format("Record Updated Successfully");
        //            response.Message = Messages.SuccessMsg;
        //        else
        //            //  response.Message = String.Format("Record Updation failed");
        //            response.Message = Messages.FailMsg;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.DidError = true;
        //        response.ErrorMessage = ex.Message;
        //    }

        //    return response.Message;

        //}



        ////Delete results
        ///// <summary>
        ///// Deletes results based on id
        ///// </summary>
        //[HttpPost("Deleteresults/{id}/")]
        //public async Task<string> DeleteresultsAsync(int id)
        //{

        //    var response = new ListModelResponse<resultsViewModel>();

        //    var resultssDataModel = false;
        //    try
        //    {
        //        resultssDataModel = await _resultsRepository.Deleteresults(id);

        //        if (resultssDataModel)
        //            // response.Message = String.Format("Record Deleted Successfully");
        //            response.Message = Messages.SuccessMsg;
        //        else
        //            //response.Message = String.Format("Record Deletion failed");
        //            response.Message = Messages.FailMsg;

        //    }
        //    catch (Exception ex)
        //    {
        //        response.DidError = true;
        //        response.ErrorMessage = ex.Message;
        //    }

        //    return response.Message;
        //}

       

    }
}
