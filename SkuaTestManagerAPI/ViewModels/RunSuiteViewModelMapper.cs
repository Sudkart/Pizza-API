using System.Collections.Generic;
using STM.Core.EntityLayer;


namespace STMAPI.ViewModels
{
    public static class RunSuiteViewModelMapper
    {
        public static RunSuiteViewModel ToViewModel(this RunSuite entity)
        {

            return new RunSuiteViewModel
            {
                Suite = entity.packname,
                PackId = entity.packid,
                Project = entity.projectname,
                Scenarios = entity.mainscenarioname,
                SubScenarios = entity.subscenarioname,
                RunOn = new List<CommonViewModel>(),
                Environment = new List<CommonViewModel>(),
                RunAt = new List<CommonViewModel>(),
                Scenario_SubScenario = entity.scenario_subscenario,
                ProjectId = entity.projectid
            };
        }


    }
}
