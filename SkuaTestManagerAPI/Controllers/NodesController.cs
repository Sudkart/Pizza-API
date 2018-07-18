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
using System.Net.Http;
using STM.Core.Repositories;

namespace STMAPI
{
    [Produces("application/json")]
    [Route("api/Nodes")]
    public class NodesController : Controller
    {

        private INodeRepository _nodeRepository;
        public NodesController(INodeRepository nodeRepository)
        {

            _nodeRepository = nodeRepository;

        }
         
         //Get Nodes
        /// <summary>
        /// Retrieves a list of Nodes
        /// </summary>
        [HttpGet]
        [Route("Node")]
        public async Task<IActionResult> GetNodesAsync()

        {
            var response = new ListModelResponse<NodeViewModel>();

            try
            {
                //response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;

                var nodesDataModel = await _nodeRepository.GetNodes();
                response.Model = nodesDataModel.Select(item => item.ToViewModel());
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

        //Update Nodes
        /// <summary>
        /// Updates node based on nodeid
        /// </summary>
        [HttpPut]
        [Route("UpdateNode/{nodeId}/")]
        public async Task<string> UpdateNodeAsync([FromBody]NodeViewModel request, int nodeId)
        {
            //HttpRequest res = null;
            var response = new ListModelResponse<ProjectViewModel>();
            var nodeDataModel = false;


            try
            {
                nodeDataModel = await _nodeRepository.UpdateNode(request.ToEntity(), nodeId);

                if (nodeDataModel)
                   
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



        /// </summary>
        /// Create Node
        /// <summary>
        /// Create a new Node
        /// </summary>
        [HttpPost]
        [Route("CreateNode")]

        public async Task<string> CreateNodeAsync([FromBody]NodeViewModel request)
        {
            // HttpRequest res = null;
            var response = new ListModelResponse<NodeViewModel>();
            var objectsDataModel = false ;
            try
            {

                objectsDataModel = await _nodeRepository.CreateNode(request.ToEntity());

                if (objectsDataModel)
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




        //Delete Node
        /// <summary>
        /// Deletes Node based on id
        /// </summary>
        [HttpPost("DeleteNode/{id}/")]
        public async Task<string> DeleteNodeAsync(int id)
        {

            var response = new ListModelResponse<NodeViewModel>();

            var nodeDataModel = false;
            try
            {
                nodeDataModel = await _nodeRepository.DeleteNode(id);

                if (nodeDataModel)
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