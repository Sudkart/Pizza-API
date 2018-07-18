using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
    public class TestStep
    {
        public Int32? Step_Id { get; set; }

        public string ProjectId { get; set; }

        public string Scen_Id { get; set; }

        public string Sub_Scen_Id { get; set; }

        public String TestStepId { get; set; }

        public Int32? ActionId { get; set; }

        public Int32? ObjectId { get; set; }

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

    }
}
