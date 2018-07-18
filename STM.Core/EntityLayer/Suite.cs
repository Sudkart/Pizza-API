using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
    public class Suite
    {
        public Int32? PackId { get; set; }
        public String PackName { get; set; }

        public Int32? ProjectId { get; set; }

        public String Scenarios { get; set; }

        public String SubScenarios { get; set; }

        public String RunAt { get; set; }

        public String TestGroup { get; set; }

        public String ScheduleDate { get; set; }

        public int Active { get; set; }
        public String CreatedBy { get; set; }

        public String CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public String UpdatedDate { get; set; }

        public Int32? No_of_Steps { get; set; }

        public Int32? Type_Of_Pack { get; set; }

        public string scenario_subscenario { get; set; }
    }
}
