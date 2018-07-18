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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STMAPI
{

    [Produces("application/json")]
    [Route("api/PackResults")]
    public class PackResultsController : Controller
    {
        private IPackResultsRepository _packResultRepository;
        public PackResultsController(IPackResultsRepository packResultRepository)
        {

            _packResultRepository = packResultRepository;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
         
        //Get Projects
        /// <summary>
        /// Retrieves a list of Projects
        /// </summary>
        [HttpGet]
        [Route("PackResults")]
        public async Task<IActionResult> GetPackResultsAsync(int packId, int projectId, DateTime fromDate, DateTime toDate)
        {
            var response = new ListModelResponse<PackResultViewModel>();
            List<PackResultViewModel> viewModel = new List<PackResultViewModel>();
            try
            {
                var packResultsDataModel = await _packResultRepository.GetPackResults(packId,projectId,fromDate,toDate);
                List<String> tstGroupIds = packResultsDataModel.Select(x => x.TestGroupId).ToList();
                var lstPieChartData = await _packResultRepository.GetPieChartDetails(packId.ToString(), tstGroupIds);
                foreach(var  pckresults in packResultsDataModel)
                {
                    var pieChartData = lstPieChartData.FirstOrDefault(x => x.TestGroupId == pckresults.TestGroupId);

                    viewModel.Add(pckresults.ToViewModel(pieChartData)); 
                }
                response.Model = viewModel;
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
        [Route("ScenarioResults")]
        public async Task<IActionResult> GetScenarioWithStatusResultsAsync(string testgroupid)
        {
            var response = new ListModelResponse<ScenarioResultViewModel>();

            try
            {
                var packResultsDataModel = await _packResultRepository.GetScenarioWithStatusResults(testgroupid);
                response.Model =  GetFilteredList(packResultsDataModel.ToList());

                response.Message = String.Format("Total of records: {0}", response.Model.Count());
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = ex.Message;
            }

            return response.ToHttpResponse();
        }

        private List<ScenarioResultViewModel> GetFilteredList(List<ScenarioResult> rawList)
        {
            List<ScenarioResultViewModel> list = new List<ScenarioResultViewModel>();
            List<ScenarioResultViewModel> listFiltered = null;


            foreach (var item in rawList)
            {
                list.Add(new ScenarioResultViewModel()
                    {
                        Id = item.ScenarioId,
                       Name = item.MainScenarioName,
                     SubScenarios = rawList.Where(x => x.ScenarioId == item.ScenarioId).Select(
                         y=> new SubScenarioResultViewModel()
                         {
                             Id =y.SubScenarioId,
                             Name = y.SubScenarioName,
                             Status = rawList.Exists(x => x.SubScenarioId == y.SubScenarioId && x.Status.ToUpper() == "FAIL") ? "FAIL" : "PASS",
                             //Steps = rawList.Where(m => m.SubScenarioId == y.SubScenarioId).ToList().Select(k=> new StepResultViewModel(){ StepId = k.TestStepId} ).ToList()

                         }).Distinct().ToList(),
                    Status = rawList.Exists(x=>x.ScenarioId==item.ScenarioId && x.Status.ToUpper()=="FAIL")? "FAIL": "PASS",
                     
                    } 
                );
            }
            listFiltered = list.GroupBy(x => x.Id).Select(y => y.FirstOrDefault()).ToList();

            foreach (var item in listFiltered)
            {
                item.SubScenarios = (from it in item.SubScenarios
                    group it by it.Id
                    into grp
                    select new SubScenarioResultViewModel()
                    {
                        Id = grp.Key,
                        Name = grp.FirstOrDefault().Name,
                        Status = grp.FirstOrDefault().Status
                    }).ToList();
            }


            return listFiltered;
        }



        [HttpGet]
        [Route("Steps")]
        public async Task<IActionResult> GetStepsAsync(int packId, string subScenarioId, string testGroupId)
        {
            var response = new ListModelResponse<StepDetailsViewModel>();
            try
            {
                var stepsDataModel = await _packResultRepository.GetSteps(packId,subScenarioId, testGroupId);
                response.Model = stepsDataModel.Select(item => item.ToViewModel());

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
