using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
    public class ActionGroup
    {
        public Int32? GroupId { get; set; }
        public String Action { get; set; }
        public String ActionDescription { get; set; }
        public Int32? Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public String CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public String UpdatedBy { get; set; }
    }
}
