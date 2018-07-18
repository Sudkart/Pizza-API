using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STM.Core.Repositories;
using STMAPI.ViewModels;
using Newtonsoft.Json;
using STMAPI.Responses;

namespace STMAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ActionGroup")]
    public class ActionGroupController : Controller
    {

        private IActionGroupRepository _actiongroupRepository;
        private IActionRepository _actionRepository;
        public ActionGroupController(IActionGroupRepository actiongroupRepository,IActionRepository actionRepository)
        {
            _actiongroupRepository = actiongroupRepository;
            _actionRepository = actionRepository;
        }


        // GET: api/ActionGroup
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ActionGroup/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ActionGroup
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ActionGroup/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("GetActionswithActionGroups")]
        public async Task<IActionResult> GetActionswithActionGroupsAsync()
        {
            var response = new ListModelResponse<ActionViewModel>();

            try
            {
                // response.PageSize = (Int32)pageSize;
                // response.PageNumber = (Int32)pageNumber;
                var subscenariosDataModel = await _actionRepository.GetactionswithActiongroups();
                    
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


    }
}
