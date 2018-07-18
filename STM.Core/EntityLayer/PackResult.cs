using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
   public class PackResult
    {
        public Int32? PackId { get; set; }
        public Int32? ProjectId { get; set; }

        public Int32? TypeOfPackId { get; set; }
         

        public String PackName { get; set; }
        public Int32? EntityId { get; set; }

        public String Status { get; set; }

        public DateTime RunDate { get; set; }

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

       // public String CreatedBy { get; set; }

        // public DateTime UpdatedDate { get; set; }

        // public String UpdatedBy { get; set; }

    }
}
