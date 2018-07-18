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
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using STMAPI.Helper;

// For more information on enabling MVC for empty resultss, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STMAPI
{

    [Produces("application/json")]
    [Route("api/runsuites")]
    [Authorize]
    public class RunSuitesController : Controller
    {
        private IRunSuiteRepository _runsuiteRepository;
        private IHttpContextAccessor _accessor;
        IConfiguration _iconfiguration;
        private UserService _userService ;
        public User getUser;
        public RunSuitesController(IRunSuiteRepository runsuiteRepository, IHttpContextAccessor httpContextAccessor,IConfiguration iconfiguration,IUserService userService)
        {
            _runsuiteRepository = runsuiteRepository;
            _accessor = httpContextAccessor;
            _userService = new UserService(httpContextAccessor);
            _iconfiguration = iconfiguration;
            getUser = _userService.GetUser();
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
        [Route("entities")]
        public async Task<IActionResult> GetResultsAsync()

        {
            var response = new ListModelResponse<CountryViewModel>();
            try
            {
                var resultsDataModel = await _runsuiteRepository.GetEntities();
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getrunsuites/{entityId}/{packId}")]
        public async Task<IActionResult> GetRunSuitesByEntity(string entityId, string packId)

        {
            var response = new ListModelResponse<RunSuiteViewModel>();
            List<RunSuiteViewModel> resultsModel = new List<RunSuiteViewModel>();
            try
            {
                var resultsDataModel = await _runsuiteRepository.GetRunSuitesByEntity(entityId, packId,getUser.UserName.Replace(" ", String.Empty));
                List<CommonViewModel> browsers = _runsuiteRepository.GetBrowser().Result.Select(x => x.ToViewModel()).ToList();
                List<CommonViewModel> environments = _runsuiteRepository.GetEnvironemnts().Result.Select(x => x.ToViewModel()).ToList();
                //List<CommonViewModel> nodes = _runsuiteRepository.GetNodes().Result.OrderBy(x => x.Name).Select(x => x.ToViewModel()).ToList();
                var node = _runsuiteRepository.GetNodes().Result.OrderBy(x => x.Name);
                List<CommonViewModel> nodes = node.Select(x => x.ToViewModel()).ToList();
                List<RunAtModel> nodesAt = node.Select(x => x.ToViewModelRunAt()).ToList();
                //resultsModel = resultsDataModel.Select(item => item.ToViewModel()).ToList();
                //foreach (var item in resultsModel)
                //{
                //    List<RunSuiteScenariosSubScenarios> dictionary = new List<RunSuiteScenariosSubScenarios>();
                //    item.RunOn = browsers;
                //    item.Environment = environments;
                //    item.RunAt = nodes;
                //    //var list = JsonConvert.DeserializeObject<List<RunSuiteScenarioViewModel>>(item.Scenario_SubScenario);
                //    //foreach (var scenes in list)
                //    //{
                //    //    List<String> subscens = scenes.SubScenerio.Select(x => x.Name).ToList();
                //    //    string subsc = String.Join(",", subscens);
                //    //    dictionary.Add(new RunSuiteScenariosSubScenarios
                //    //    {
                //    //        Scenarios = scenes.Name,
                //    //        SubScenarios = subsc
                //    //    });
                //    //}
                //    //item.RunSuiteScenariosSubScenarios = dictionary;
                //}
                resultsModel = resultsDataModel.Select(item => item.ToViewModel()).ToList();
                List<String> activeRunSuites = _runsuiteRepository.GeActiveRunSuites().Result.ToList();
                foreach (var item in resultsModel)
                {
                    var activeItm = activeRunSuites.Where(x => x.Contains(item.PackId));
                    if (activeItm.Count() > 0)
                    {
                        item.IsActive = true;
                    }
                    List<RunSuiteScenariosSubScenarios> listsubscenes = new List<RunSuiteScenariosSubScenarios>();
                    //var stringJson = JsonConvert.SerializeObject(nodesAt);
                    item.RunOn = browsers;
                    item.Environment = environments;
                    item.RunAt = nodes.Where(x => x.Name == "Local Node").ToList(); 
                    item.RunAtJson = nodesAt;
                    if (!string.IsNullOrEmpty(item.Scenario_SubScenario))
                    {
                        var list = JsonConvert.DeserializeObject<List<RunSuiteScenarioViewModel>>(item.Scenario_SubScenario);
                        foreach (var scenes in list)
                        {
                            string subsc = string.Empty;
                            if (scenes.SubScenerio.Count() > 0)
                            {
                                List<String> subscens = scenes.SubScenerio.Select(x => x.Name).ToList();
                                subsc = String.Join(",", subscens);
                            }
                            listsubscenes.Add(new RunSuiteScenariosSubScenarios
                            {
                                Scenarios = scenes.Name,
                                SubScenarios = subsc
                            });
                        }
                    }
                    item.RunSuiteScenariosSubScenarios = listsubscenes;
                }
                response.Model = resultsModel;
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
        [Route("getrunsuites")]
        public async Task<IActionResult> GetRunSuites()

        {
            ListModelResponse<RunSuiteViewModel> response = new ListModelResponse<RunSuiteViewModel>();
            List<RunSuiteViewModel> resultsModel = new List<RunSuiteViewModel>();
            try
            {
                List<RunSuite> resultsDataModel = _runsuiteRepository.GetRunSuites(getUser.UserName.Replace(" ", String.Empty)).Result.ToList();
                List<CommonViewModel> browsers = _runsuiteRepository.GetBrowser().Result.Select(x => x.ToViewModel()).ToList();
                List<CommonViewModel> environments = _runsuiteRepository.GetEnvironemnts().Result.Select(x => x.ToViewModel()).ToList();
                var node = _runsuiteRepository.GetNodes().Result.OrderBy(x=>x.Name);
                List<CommonViewModel> nodes = node.Select(x => x.ToViewModel()).ToList();
                List<RunAtModel> nodesAt = node.Select(x => x.ToViewModelRunAt()).ToList();
                resultsModel = resultsDataModel.Select(item => item.ToViewModel()).ToList();
                List<String> activeRunSuites = _runsuiteRepository.GeActiveRunSuites().Result.ToList();
                foreach(var item in resultsModel)
                {
                    var activeItm = activeRunSuites.Where(x =>x.Contains(item.PackId));
                    if(activeItm.Count()>0)
                    {
                        item.IsActive = true;
                    }
                    List<RunSuiteScenariosSubScenarios> listsubscenes = new List<RunSuiteScenariosSubScenarios>();
                    //var stringJson = JsonConvert.SerializeObject(nodesAt);
                    item.RunOn = browsers;
                    item.Environment = environments;
                    item.RunAt = nodes.Where(x=>x.Name == "Local Node").ToList();
                    item.RunAtJson = nodesAt;
                    if (!string.IsNullOrEmpty(item.Scenario_SubScenario))
                    {
                        var list = JsonConvert.DeserializeObject<List<RunSuiteScenarioViewModel>>(item.Scenario_SubScenario);
                        foreach (var scenes in list)
                        {
                            string subsc = string.Empty;
                            if(scenes.SubScenerio.Count()>0)
                            {
                                List<String> subscens = scenes.SubScenerio.Select(x => x.Name).ToList();
                                subsc = String.Join(",", subscens);
                            }
                            listsubscenes.Add(new RunSuiteScenariosSubScenarios
                            {
                                Scenarios = scenes.Name,
                                SubScenarios = subsc
                            });
                        }
                    }

                    item.RunSuiteScenariosSubScenarios = listsubscenes;
                }

                response.Model = resultsModel;
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
        [Route("runsuite")]
        [Authorize]
        public async Task<string>  RunSuite([FromBody] List<RunSuiteViewModel> runsuites)
        {
            try
            {
                string IPAddress = GetIP();
                IPAddress = GetLocalIPAddress();
                string IPAddress1 = GetIPAddress();
                string ipAddressforRunSuite = runsuites.FirstOrDefault().IpAddress; //_accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                List<CommonViewModel> environments = _runsuiteRepository.GetEnvironemnts().Result.Select(x => x.ToViewModel()).ToList();
              
                foreach (var item in runsuites)
                {
                    TblPacks tblPacks =await _runsuiteRepository.GetPackByPackId(item.PackId);
                    string selectedEnvironmentId = environments.Where(x => x.Name == item.DefaultEnvironment).FirstOrDefault().Id;
                    List<string> subscenariosIds = new List<string>();
                    string packName = string.Empty;
                    string projectName = _runsuiteRepository.GetProjectNameByProjectId(item.ProjectId).Result;
                    string scenarios = string.Empty;
                    if (string.IsNullOrEmpty(tblPacks.scenario_subscenario))
                    {
                        CleanTestSteps(item.ProjectId);
                        List<String> subScenariosResults = _runsuiteRepository.GetSubScenariosByPackId(item.PackId).Result.ToList();
                        string subScenariosIds = String.Join(',', subScenariosResults);
                        string subScenarios = string.Join(",", subScenariosIds.Split(',').Select(x => string.Format("'{0}'", x)).ToList());
                        var scenarioResults = _runsuiteRepository.GetScenariosByPackId(item.PackId).Result;
                        string scenarioIds = string.Join(',', scenarioResults);
                        scenarios = string.Join(",", scenarioIds.Split(',').Select(x => string.Format("'{0}'", x)).ToList());
                        packName = _runsuiteRepository.GetPackNameByPackId(item.PackId).Result;
                        subscenariosIds = _runsuiteRepository.GetSubScenIdBySubScenarioIdAndProjectId(subScenarios, item.ProjectId).Result.ToList();
                    }
                    else
                    {
                        packName = tblPacks.packname;
                        List<RunSuiteScenarioViewModel> scenario_subscenario = JsonConvert.DeserializeObject<List<RunSuiteScenarioViewModel>>(tblPacks.scenario_subscenario);
                        List<String> lstscnIds = scenario_subscenario.Select(x => x.Id).ToList();
                        List<RunSuiteSubScenarioViewModel> lstsubsceneIds = scenario_subscenario.SelectMany(x => x.SubScenerio).ToList();
                        subscenariosIds = lstsubsceneIds.Select(x => x.Id).ToList();
                        String scnIds = string.Join(",", lstscnIds);
                        scenarios = string.Join(",", scnIds.Split(',').Select(x => string.Format("'{0}'", x)).ToList());
                    }
                    if (item.ParallelExecution)
                    {
                        List<String>[] lst = Partition(subscenariosIds, 4);
                        string grpTestRunId = "Grp" + DateTime.Now.ToString("yyyyMMddHHmmssfff",
                                                CultureInfo.InvariantCulture);
                        for (int z = 0;z<4;z++)
                        {
                            foreach (var nodes in item.RunAt)
                            {
                             
                                string testRunId = DateTime.Now.ToString("yyyyMMddHHmmssfff",
                                                                    CultureInfo.InvariantCulture);
                                TblNode tblNodeData = _runsuiteRepository.GetNodesData(nodes.Name).Result;
                                TblUserTestRun tblUserTestRun = new TblUserTestRun
                                {
                                    testgroupid = grpTestRunId,
                                    testrunid = testRunId,
                                    username = getUser.UserName.Replace(" ", String.Empty),
                                    packid = item.PackId,
                                    runat = nodes.Name
                                };
                                _runsuiteRepository.PostUserTestRun(tblUserTestRun);
                                List<TblTestSteps> testSteps = new List<TblTestSteps>();
                                string subsceneswithParallelExecution = string.Join(",", lst[z]);
                                List<TblTestRunTemp> tblTestRunTmp = new List<TblTestRunTemp>();
                                testSteps = new List<TblTestSteps>();
                                List<TblTestSteps> tblTestSteps = new List<TblTestSteps>();
                                foreach (var subscneIds in lst[z])
                                {
                                   var tblSteps = _runsuiteRepository.GetTestStepsBySceneIdAndSubSceneId(scenarios, subscneIds, tblNodeData.nodeIp, tblNodeData.nodePortNo).Result.ToList();
                                    foreach(var zt in tblSteps)
                                    {
                                        tblTestSteps.Add(zt);
                                    }
                                }
                                    foreach (var i in tblTestSteps)
                                    {
                                        var count = testSteps.Where(x => x.teststepid == i.teststepid).Count();
                                        if (count == 0)
                                        {
                                            i.testdata = string.IsNullOrEmpty(i.testdata) ? string.Empty : i.testdata.Replace("'", "''");
                                            i.objectvalue = string.IsNullOrEmpty(i.objectvalue) ? string.Empty : i.objectvalue.Replace("'", "''");
                                            i.description = string.IsNullOrEmpty(i.description) ? string.Empty : i.description.Replace("'", "''");
                                           
                                        }
                                        string stpId = string.Empty;
                                        if (int.TryParse(i.teststepid, out int n))
                                        {
                                            //"CDE_SCEN_02_SUB_09_STEP_08"
                                            if (int.TryParse(i.mainscenarioid, out int c))
                                            {
                                                stpId += i.projectcode + "_" + "SCEN" + "_" + i.mainscenarioid;
                                            }
                                          
                                            if (int.TryParse(i.subscenarioid, out int d))
                                            {
                                                if (!int.TryParse(i.mainscenarioid, out int f))
                                                {
                                                    stpId +=  i.mainscenarioid;
                                                }
                                                stpId = stpId + "_" + "SUB" + "_" + i.subscenarioid;
                                            }
                                            else
                                            {
                                                    stpId = i.subscenarioid;
                                            }
                                            stpId = stpId + "_" + "STEP" + "_" + i.teststepid;
                                            i.teststepid = stpId;

                                        }
                                        testSteps.Add(i);
                                    }

                                    
                                    if (nodes.Name == "Local Node")
                                    {
                                        int count = 1;
                                        foreach (var rows in testSteps)
                                        {
                                            TblTestRunTemp tblTestRunTemp = new TblTestRunTemp
                                            {
                                                packid = item.PackId,
                                                packname = packName,
                                                projectid = item.ProjectId,
                                                projectname = projectName,
                                                environment = selectedEnvironmentId,
                                                stepno = count.ToString(),
                                                teststepid = rows.teststepid,
                                                keyword = rows.actionname,
                                                objectname = rows.objectname,
                                                testdata = rows.testdata,
                                                objectvalue = rows.objectvalue,
                                                decription = rows.description,
                                                runmode = rows.runmode,
                                                status = rows.status,
                                                runat = ipAddressforRunSuite,
                                                portno = "5555",
                                                browser = item.DefaultBrowser,
                                                testrunid = testRunId,
                                                scan_id = rows.scen_id,
                                                sub_scan_id = rows.sub_scen_id,
                                                testgroupid = grpTestRunId

                                            };
                                            tblTestRunTmp.Add(tblTestRunTemp);
                                            count = count + 1;
                                        }

                                    }
                                    else
                                    {
                                        int count = 1;
                                        foreach (var rows in testSteps)
                                        {
                                            TblTestRunTemp tblTestRunTemp = new TblTestRunTemp
                                            {
                                                packid = item.PackId,
                                                packname = packName,
                                                projectid = item.ProjectId,
                                                projectname = projectName,
                                                environment = selectedEnvironmentId,
                                                stepno = count.ToString(),
                                                teststepid = rows.teststepid,
                                                keyword = rows.actionname,
                                                objectname = rows.objectname,
                                                testdata = rows.testdata,
                                                objectvalue = rows.objectvalue,
                                                decription = rows.description,
                                                runmode = rows.runmode,
                                                status = rows.status,
                                                runat = tblNodeData.hostIp,
                                                portno = tblNodeData.hostPortNo,
                                                browser = item.DefaultBrowser,
                                                testrunid = testRunId,
                                                scan_id = rows.scen_id,
                                                sub_scan_id = rows.sub_scen_id,
                                                testgroupid = grpTestRunId

                                            };
                                            tblTestRunTmp.Add(tblTestRunTemp);
                                            count = count + 1;
                                        }
                                    }
                                  
                                
                                var returndata = await _runsuiteRepository.PostTestRunTemp(tblTestRunTmp);

                                NameValueCollection nvc = new NameValueCollection();
                                nvc.Add("userId", getUser.UserName.Replace(" ", String.Empty));
                                nvc.Add("packId", item.PackId);
                                nvc.Add("testRunId", testRunId);
                                nvc.Add("testGroupId", grpTestRunId);
                                string url = _iconfiguration.GetValue<string>("AppSettings:RunSuite");//"http://10.70.10.147:8080/SKUAWeb/TestEngine/FrameWork/RunPackWithIds";
                                url += ToQueryString(nvc);
                                GetMethodCall(url);

                            }
                        }

                    }else
                    {
                       
                        foreach(var nodes in item.RunAt )
                        {
                            string grpTestRunId = "Grp" + DateTime.Now.ToString("yyyyMMddHHmmssfff",
                                                   CultureInfo.InvariantCulture);
                            string testRunId = DateTime.Now.ToString("yyyyMMddHHmmssfff",
                                                                CultureInfo.InvariantCulture);
                            TblNode tblNodeData = _runsuiteRepository.GetNodesData(nodes.Name).Result;
                            TblUserTestRun tblUserTestRun = new TblUserTestRun
                            {
                                testgroupid = grpTestRunId,
                                testrunid = testRunId,
                                username = getUser.UserName.Replace(" ", String.Empty),
                                packid = item.PackId,
                                runat = nodes.Name
                            };
                            _runsuiteRepository.PostUserTestRun(tblUserTestRun);
                            List<TblTestSteps> testSteps = new List<TblTestSteps>();
                            List<TblTestRunTemp> tblTestRunTmp = new List<TblTestRunTemp>();
                            testSteps = new List<TblTestSteps>();
                            foreach (var subscneIds in subscenariosIds)
                            {
                                List<TblTestSteps> tblTestSteps = _runsuiteRepository.GetTestStepsBySceneIdAndSubSceneId(scenarios, subscneIds, tblNodeData.nodeIp, tblNodeData.nodePortNo).Result.ToList();
                                foreach(var i in tblTestSteps)
                                {
                                    var count = testSteps.Where(x => x.teststepid == i.teststepid).Count();
                                    if(count==0)
                                    {
                                        i.testdata = string.IsNullOrEmpty(i.testdata)?string.Empty: i.testdata.Replace("'", "''");
                                        i.objectvalue = string.IsNullOrEmpty(i.objectvalue)?string.Empty: i.objectvalue.Replace("'", "''");
                                        i.description =string.IsNullOrEmpty(i.description)?string.Empty: i.description.Replace("'", "''");
                                        
                                    }
                                    string stpId = string.Empty;
                                    if (int.TryParse(i.teststepid, out int n))
                                    {
                                        //"CDE_SCEN_02_SUB_09_STEP_08"
                                        if (int.TryParse(i.mainscenarioid, out int c))
                                        {
                                            stpId += i.projectcode + "_" + "SCEN" + "_" + i.mainscenarioid;
                                        }

                                        if (int.TryParse(i.subscenarioid, out int d))
                                        {
                                            if (!int.TryParse(i.mainscenarioid, out int f))
                                            {
                                                stpId += i.mainscenarioid;
                                            }
                                            stpId = stpId + "_" + "SUB" + "_" + i.subscenarioid;
                                        }
                                        else
                                        {
                                            stpId = i.subscenarioid;
                                        }
                                        stpId = stpId + "_" + "STEP" + "_" + i.teststepid;
                                        i.teststepid = stpId;

                                    }
                                    testSteps.Add(i);
                                }

                            }
                            if(nodes.Name== "Local Node")
                            {
                                int count = 1;
                                foreach(var rows in testSteps)
                                {
                                    TblTestRunTemp tblTestRunTemp = new TblTestRunTemp
                                    {
                                        packid=item.PackId,
                                        packname = packName,
                                        projectid = item.ProjectId,
                                        projectname = projectName,
                                        environment = selectedEnvironmentId,
                                        stepno = count.ToString(),
                                        teststepid = rows.teststepid,
                                        keyword = rows.actionname,
                                        objectname = rows.objectname,
                                        testdata = rows.testdata,
                                        objectvalue = rows.objectvalue,
                                        decription = rows.description,
                                        runmode= rows.runmode,
                                        status= rows.status,
                                        runat = ipAddressforRunSuite,
                                        portno = "5555",
                                        browser = item.DefaultBrowser,
                                        testrunid = testRunId,
                                        scan_id = rows.scen_id,
                                        sub_scan_id = rows.sub_scen_id,
                                        testgroupid = grpTestRunId

                                    };
                                   tblTestRunTmp.Add(tblTestRunTemp);
                                    count = count + 1;
                                }

                            }else
                            {
                                int count = 1;
                                foreach (var rows in testSteps)
                                {
                                    TblTestRunTemp tblTestRunTemp = new TblTestRunTemp
                                    {
                                        packid = item.PackId,
                                        packname = packName,
                                        projectid = item.ProjectId,
                                        projectname = projectName,
                                        environment = selectedEnvironmentId,
                                        stepno = count.ToString(),
                                        teststepid = rows.teststepid,
                                        keyword = rows.actionname,
                                        objectname = rows.objectname,
                                        testdata = rows.testdata,
                                        objectvalue = rows.objectvalue,
                                        decription = rows.description,
                                        runmode = rows.runmode,
                                        status = rows.status,
                                        runat = tblNodeData.hostIp,
                                        portno = tblNodeData.hostPortNo,
                                        browser = item.DefaultBrowser,
                                        testrunid = testRunId,
                                        scan_id = rows.scen_id,
                                        sub_scan_id = rows.sub_scen_id,
                                        testgroupid = grpTestRunId

                                    };
                                    tblTestRunTmp.Add(tblTestRunTemp);
                                    count = count + 1;
                                }
                            }

                            var returndata =   await   _runsuiteRepository.PostTestRunTemp(tblTestRunTmp);

                            NameValueCollection nvc = new NameValueCollection();
                            nvc.Add("userId", getUser.UserName.Replace(" ", String.Empty));
                            nvc.Add("packId", item.PackId);
                            nvc.Add("testRunId", testRunId);
                            nvc.Add("testGroupId", grpTestRunId);
                            string url = _iconfiguration.GetValue<string>("AppSettings:RunSuite");// System.Configuration.ConfigurationManager.AppSettings["APIQAURL"];// "http://10.70.10.147:8080/SKUAWeb/TestEngine/FrameWork/RunPackWithIds";
                            url += ToQueryString(nvc);
                            GetMethodCall(url);
                        }
                      
                    }
                }
                return "success";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
          
        }


        [HttpPost]
        [Route("stopsuite")]
        public async Task<string> StopSuite([FromBody] List<RunSuiteViewModel> runsuite)
        {
            foreach(var item in runsuite)
            {
                TestGroupandTestRunId testGroupandTestRunId = await _runsuiteRepository.GetTestRunIdAndGroupIdByPackId(item.PackId);
                if (testGroupandTestRunId != null)
                {
                    NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("userId", getUser.UserName.Replace(" ", String.Empty));
                    nvc.Add("packId", item.PackId);
                    nvc.Add("testRunId", testGroupandTestRunId.TestRunId);
                    nvc.Add("testGroupId", testGroupandTestRunId.TestGroupId);
                    string url = _iconfiguration.GetValue<string>("AppSettings:KillSuite");//"http://10.70.10.147:8080/SKUAWeb/TestEngine/FrameWork/KillRun";
                    url += ToQueryString(nvc);
                    GetMethodCall(url);

                    _runsuiteRepository.DeleteTableTestRunTempByTestGroupId(testGroupandTestRunId.TestGroupId);
                    //string packId = _runsuiteRepository.GetPackIdByPackName(item.PackName);
                    _runsuiteRepository.UpdateTblCurrProjsByPackId(item.PackId);
                    _runsuiteRepository.UpdateTblCurrProjsByGroupId(testGroupandTestRunId.TestGroupId);
                }
               
            }
            return "success";
        }

        protected static void GetMethodCall(string URL)
        {
            //Create new HttpWebRequest object for the passed URL using create method
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);//BeginGetResponse is used to call asynchronously URL
            IAsyncResult result = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
            bool result1 = result.IsCompleted;
            if (!result.IsCompleted)
            {

            }

        }

        protected static void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            // Request state is asynchronous.
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            request.Method = "GET";
            try
            {
                //Read the response for any future needs.
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);// Release the HttpWebResponse
                string status = response.StatusCode.ToString();
                response.Close();
            }
            catch (Exception ex)
            {
                //Log the error

                string errormessage = ex.Message;


                // return;
            }

        }

        protected string ToQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        private string GetIP()
        {
            string Str = string.Empty;
            IPAddress IP = null;
            Str = System.Net.Dns.GetHostName();
            return Str;

        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private string GetIPAddress()
        {
            string IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;

        }

        public async void CleanTestSteps(string projectId)
        {
            string projectCode = await _runsuiteRepository.GetProjectCodeByProjectId(projectId);
            _runsuiteRepository.DeleteTableTestStepsByProjectCode(projectCode, projectId);
        }

        public static List<T>[] Partition<T>(List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            List<T>[] partitions = new List<T>[totalPartitions];

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }

    }
}
