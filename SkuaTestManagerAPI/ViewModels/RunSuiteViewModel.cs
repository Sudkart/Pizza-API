using System;
using System.Collections.Generic;

namespace STMAPI.ViewModels
{
    public class RunSuiteViewModel
    {
        private string defnode = "Local Node";
        private string defbrowser = "chrome";
        private string defenvironment = "Test";
        private string defgender = "female";

        public String Suite { get; set; }

        public String Project { get; set; }

        public String Scenarios { get; set; }

        public String SubScenarios { get; set; }

        public List<CommonViewModel> RunOn { get; set; }

        public List<CommonViewModel> Environment { get; set; }

        public List<CommonViewModel> RunAt { get; set; }

        public Boolean ParallelExecution { get; set; }

        public string Scenario_SubScenario { get; set; }

        public string PackId { get; set; }

        public string ProjectId { get; set; }

        public List<RunAtModel> RunAtJson { get; set; }

        public bool IsActive { get; set; }

        public string DefaultNode
        {
            get
            {
                return defnode;
            }
            set
            {
                defnode = value;
            }
        }

        public string DefaultBrowser
        {
            get
            {
                return defbrowser;
            }
            set
            {
                defbrowser = value;
            }
        }

        public string DefaultEnvironment
        {
            get
            {
                return defenvironment;
            }
            set
            {
                defenvironment = value;
            }
        }

        public string IpAddress { get; set; }
        

        public List<RunSuiteScenariosSubScenarios> RunSuiteScenariosSubScenarios { get; set; }

    }

    public class RunSuiteScenarioViewModel
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public List<RunSuiteSubScenarioViewModel> SubScenerio { get; set; }

    }

    public class RunSuiteSubScenarioViewModel
    {
        public string Name { get; set; }

        public string Id { get; set; }

    }

    public class RunSuiteScenariosSubScenarios
    {
        public string Scenarios { get; set; }

        public string SubScenarios { get; set; }
    }

    public class RunAtModel
    {
        public string id { get; set; }

        public string name { get; set; }
    }

}
