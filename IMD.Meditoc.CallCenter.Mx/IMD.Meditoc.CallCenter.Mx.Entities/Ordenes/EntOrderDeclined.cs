using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Ordenes
{
    public class EntOrderDeclined
    {
        public string @object { get; set; }
        public string type { get; set; }
        public string log_id { get; set; }
        public EntOrder data { get; set; }
        public List<EntOrderDeclinedDetail> details { get; set; }
    }

    public class EntOrderDeclinedDetail
    {
        public string debug_message { get; set; }
        public string message { get; set; }
        public string code { get; set; }
    }
}
