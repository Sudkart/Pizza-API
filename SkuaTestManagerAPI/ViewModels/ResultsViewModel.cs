using System;

namespace STMAPI.ViewModels
{
    public class ResultsViewModel
    {
        public String PackName { get; set; }

        public string PackId { get; set; }

        public String ProjectName { get; set; }

        public String Status { get; set; }

        public String RunningAt { get; set; }

        public string TestGroupId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Decimal PercentCompleted { get; set; }

        public Decimal PercentFailed { get; set; }

        public Decimal PercentPassed { get; set; }

        public Decimal PercentSkipped { get; set; }

    }
}
