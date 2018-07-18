using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public class TestStepViewModel
    {

        public Int32? Step_Id { get; set; }

        public Int32? ProjectId { get; set; }

        public Int32? Scen_Id { get; set; }

        public Int32? Sub_Scen_Id { get; set; }

        public String TestStepId { get; set; }

        public Int32? ActionId { get; set; }

        public Int32? ObjectId { get; set; }

        public string ObjectName { get; set; }

        public String Xpath { get; set; }

        public String Description { get; set; }

        public String RunMode { get; set; }

        public String Status { get; set; }

        public Int32? Active { get; set; }

        public String CreatedBy { get; set; }

        public String CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public String UpdatedDate { get; set; }

        public String TestData { get; set; }

        public string IdentityType { get; set; }
    }
}
