using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
    public class Scenario
    {
        public Int32? Scen_Id { get; set; }

        public Int32? ProjectId { get; set; }
        public String ProjectName { get; set; }

        public String MainScenarioId { get; set; }

        public String MainScenarioName { get; set; }

        public String MainScenarioDescription { get; set; }

        public String RunMode { get; set; }

        public String Status { get; set; }

        public Int32? Active { get; set; }

        public DateTime TestScenExecDate { get; set; }

        public String TestScenExecBy { get; set; }

        public String TestScenLastStatus { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }




    }
}
