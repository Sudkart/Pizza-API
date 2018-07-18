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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STMAPI
{
    [Produces("application/json")]
    [Route("api/Urls")]
    public class UrlController : Controller
    {
        private IUrlRepository _urlRepository;
        public UrlController(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        
        //Get Urls
        /// <summary>
        /// Retrieves a list of Urls
        /// </summary>
        [HttpGet]
        [Route("Url")]
        public async Task<IActionResult> GetUrlsAsync()
        {
            var response = new ListModelResponse<UrlViewModel>();

            try
            {
                var urlDataModel = await _urlRepository.GetUrls();
                response.Model = urlDataModel.Select(item => item.ToViewModel());
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

        //Create Url
        /// <summary>
        /// Create a new Url 
        /// </summary>

        [HttpPost]
        [Route("CreateUrl")]

        public async Task<string> CreateUrlAsync([FromBody]UrlViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<UrlViewModel>();
            var urlDataModel = false;
            try
            {

                urlDataModel = await _urlRepository.CreateUrl(request.ToEntity());

                if (urlDataModel)
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


        //Update Url
        /// <summary>
        /// Updates url based on id
        /// </summary>
        [HttpPut]
        [Route("UpdateUrl/{urlId}")]
        public async Task<string> UpdateUrlAsync([FromBody]UrlViewModel request, int urlId)
        {
            //HttpRequest res = null;
            var response = new ListModelResponse<UrlViewModel>();
            var urlsDataModel = false;
            try
            {
                urlsDataModel = await _urlRepository.UpdateUrl(request.ToEntity(), urlId);

                if (urlsDataModel)
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

        //Delete Url
        /// <summary>
        /// Deletes url based on id
        /// </summary>
        [HttpPost("DeleteUrl/{id}/")]
        public async Task<string> DeleteUrlAsync(int id)
        {

            var response = new ListModelResponse<UrlViewModel>();

            var urlsDataModel = false;
            try
            {
                urlsDataModel = await _urlRepository.DeleteUrl(id);

                if (urlsDataModel)
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
