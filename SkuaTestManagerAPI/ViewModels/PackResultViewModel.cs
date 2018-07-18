using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public class PackResultViewModel
    {
        public Int32? PackId { get; set; }

        public Int32? ProjectId { get; set; }
        public Int32? EntityId { get; set; }
        public Int32? TypeOfPackId { get; set; }
        public String PackName { get; set; }

        public String Status { get; set; }

        public string RunDate { get; set; }

        public string TestGroupId { get; set; }

        public String PassPercentage { get; set; }

        public String FailPercentage { get; set; }

        public String SkipPercentage { get; set; }

        public string ProgresStatus { get; set; }

        public string RanAt { get; set; }

        public string RunAt { get; set; }

        public string PassedSteps { get; set; }

        public string FailedSteps { get; set; }

        public string SkippedSteps { get; set; }

        public String SuiteName { get; set; }

        public String Browser { get; set; }

        public String CreatedDate { get; set; }

        public String Country { get; set; }

        public String TestType { get; set; }

        public String EnvironmentName { get; set; }

        public String StartTime { get; set; }

        public String EndTime { get; set; }

    }
    
}
