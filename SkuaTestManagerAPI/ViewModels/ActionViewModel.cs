using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public class ActionViewModel
    {
        public Int32? ActionId { get; set; }
        public Int32? ActionGroup { get; set; }

        public String ActionType { get; set; }

        public String ActionGroupName { get; set; }

        public String ActionName { get; set; }

        public String ActionDescription { get; set; }

        public String ActionRemarks { get; set; }

        public String CreatedBy { get; set; }

        public String CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public String UpdatedDate { get; set; }
    }
}

