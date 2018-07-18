using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public class SubScenarioViewModel
    {
        public Int32? Sub_Scen_Id { get; set; }
        public Int32? Scen_Id { get; set; }
        public Int32? ProjectId { get; set; }
        public String SubScenarioId { get; set; }
        public String SubScenarioName { get; set; }
		public String ScenarioName { get; set; }
		public String SubScenarioDesc { get; set; }
        public String RunMode { get; set; }
        public String Status { get; set; }
        public int Active { get; set; }
        public string IsAutomated { get; set; }
        public int IsCompleted { get; set; }
        public String TestSubScenExecDate { get; set; }

        public String TestSubScenExecBy { get; set; }

        public int TestSubScenLastStatus { get; set; }

        public String CreatedBy { get; set; }

        public String CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public String UpdatedDate { get; set; }



    }
}
