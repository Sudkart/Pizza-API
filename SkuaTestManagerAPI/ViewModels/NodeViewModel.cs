using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STMAPI.ViewModels
{
    public class NodeViewModel
    {
        public Int32? NodeId { get; set; }
        public string NodeIp { get; set; }

        public string NodeName { get; set; }
        public Int32? NodePortNo { get; set; }
        public String HostName { get; set; }
        public String HostIp { get; set; }
        public int HostPortNo { get; set; }

        public int Active { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public String UpdatedBy { get; set; }

        public DateTime  UpdatedDate {get; set;}

    }
}
