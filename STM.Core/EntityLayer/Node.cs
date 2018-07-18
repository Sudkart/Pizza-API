using System;
using System.Collections.Generic;
using System.Text;

namespace STM.Core.EntityLayer
{
    public class Node
    {
        public Int32? NodeId { get; set; }

        public String NodeIp { get; set; }

        public String NodeName { get; set; }

        public Int32? NodePortNo { get; set; }

        public String HostName { get; set; }

        public String HostIp { get; set; }


        public int HostPortNo { get; set; }


        public Int32 Active { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }


        public String UpdatedBy { get; set; }


        public DateTime UpdatedDate { get; set; }



























     
    }
}
