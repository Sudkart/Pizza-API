using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
   public class Project
    {
        public Int32? ProjectId { get; set; }
        
        public String ProjectName { get; set; }

        public String ProjectDesc { get; set; }

        public String ProjectOwner { get; set; }

        public Boolean Active { get; set; }

        public String ProjectCode { get; set; }

        public Int32? CountryId { get; set; }

        public DateTime CreatedDate { get; set; }

        public String CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public String UpdatedBy { get; set; }

    }
}
