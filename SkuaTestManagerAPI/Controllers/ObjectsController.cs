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
    [Route("api/Objects")]
    public class ObjectsController : Controller
    {
        private IObjectRepository _objectRepository;

        public ObjectsController(IObjectRepository objectRepository)
        {
            _objectRepository = objectRepository;


        }
        
        //Get Objects
        /// <summary>
        /// Retrieves a list of Objects
        /// </summary>
        [HttpGet]
        [Route("Object")]
        public async Task<IActionResult> GetObjectsAsync()
        {
            var response = new ListModelResponse<ObjectViewModel>();

            try
            {
                //response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;

                var objectsDataModel = await _objectRepository.GetAllObjects();
                response.Model = objectsDataModel.Select(item => item.ToViewModel());
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

        //Get Objects
        /// <summary>
        /// Retrieves a list of Objects based on ObjectName
        /// </summary>
        [HttpGet]
        [Route("GetObjects/{objectName}")]
        public async Task<IActionResult> GetObjectsAsync(string objectName)
        {
            var response = new ListModelResponse<ObjectViewModel>();
            //var objectsDataModel = false;
            try
            {
              var  objectsDataModel = await _objectRepository.GetObjects(objectName);

             
                response.Model = objectsDataModel.Select(item => item.ToViewModel());
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
        [Route("GetObjects/{objectName}/{projectName}")]
        public async Task<IActionResult> GetObjectsbyNameMatchAsync(string objectName, string projectName)
        {
            var response = new ListModelResponse<ObjectViewModel>();
            //var objectsDataModel = false;
            try
            {
                var objectsDataModel = await _objectRepository.GetObjectsByName(objectName, projectName);


                response.Model = objectsDataModel.Select(item => item.ToViewModel());
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
        [Route("GetObjectsbyNameMatch/{objectName}")]
        public async Task<IActionResult> GetObjectsbyNameMatchAsync(string objectName)
        {
            var response = new ListModelResponse<ObjectViewModel>();
            //var objectsDataModel = false;
            try
            {
                var objectsDataModel = await _objectRepository.GetObjectsByNameMatch(objectName);


                response.Model = objectsDataModel.Select(item => item.ToViewModel());
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
        [Route("GetObjectsByProject/{projectName}")]
        public async Task<IActionResult> GetObjectsbyProjectName(string projectName)
        {
            var response = new ListModelResponse<ObjectViewModel>();
            //var objectsDataModel = false;
            try
            {
                var objectsDataModel = await _objectRepository.GetObjectsByName(projectName);


                response.Model = objectsDataModel.Select(item => item.ToViewModel());
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


        //Get Objects
        /// <summary>
        /// Retrieves a list of Objects based on ObjectId
        /// </summary>
        [HttpGet]
        [Route("GetObjects/{objectId}")]
        public async Task<IActionResult> GetObjectsById(int objectId)
        {
            var response = new ListModelResponse<ObjectViewModel>();
            //var objectsDataModel = false;
            try
            {
                var objectsDataModel = await _objectRepository.GetObjectsById(objectId);


                response.Model = objectsDataModel.Select(item => item.ToViewModel());
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

        //Update Object
        /// <summary>
        /// Updates Object based on id
        /// </summary>
        [HttpPut]
        [Route("UpdateObject/{objectId}/")]
        public async Task<string> UpdateObjectAsync([FromBody]ObjectViewModel request, int objectId)
        {
            //HttpRequest res = null;
            var response = new ListModelResponse<ObjectViewModel>();
            var projectsDataModel = false;

            try
            {
                projectsDataModel = await _objectRepository.UpdateObject(request.ToEntity(), objectId);

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

    
        /// Create Object 
        /// </summary>
        //Create Object
        /// <summary>
        /// Create a new Object
        /// </summary>
        [HttpPost]
        [Route("CreateObject")]

        public async Task<string> CreateObjectAsync([FromBody]ObjectViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<ObjectViewModel>();
            var objectsDataModel = 0;
            try
            {

                objectsDataModel = await _objectRepository.CreateObject(request.ToEntity());

                if (objectsDataModel > 0)
                {
                    response.Message = Messages.SuccessMsg;
                }
                //response.Message = String.Format("Created Project Successfully");

                else
                    //  response.Message = String.Format("Create Project failed");
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