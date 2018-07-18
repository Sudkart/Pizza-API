using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
    public class Url
    {
        public Int32? UrlId { get; set; }

        public Int32? EntityId { get; set; }

        public Int32? ProjectId { get; set; }

        public Int32? EnvironmentId { get; set; }

        public String BaseUrl { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
