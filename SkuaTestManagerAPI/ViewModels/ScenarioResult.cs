using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
   public class ScenarioResultModel
    {
        public string TestStepId { get; set; }
        public string Status { get; set; }

        public string SubSenarioId { get; set; } 

        public string ScenarioId { get; set; }
       
        public string TestGroupId { get; set; }
        

    }
}
