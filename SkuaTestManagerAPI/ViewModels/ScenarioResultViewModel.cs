using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public class ScenarioResultViewModel
    {

        public ScenarioResultViewModel()
        {
            SubScenarios= new List<SubScenarioResultViewModel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public string Status  { get; set; }

        public List<SubScenarioResultViewModel> SubScenarios { get; set; }
    }

    public class SubScenarioResultViewModel
    {
        public SubScenarioResultViewModel()
        {
            Steps = new List<StepResultViewModel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }
        public List<StepResultViewModel> Steps { get; set; }
    }

    public class StepResultViewModel
    {
        public string StepId { get; set; }

        public string Status { get; set; }
    }


}
