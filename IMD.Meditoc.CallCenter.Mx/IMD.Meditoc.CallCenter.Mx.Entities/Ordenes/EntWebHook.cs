using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Ordenes
{
    public class EntWebHook
    {
        public EntWebHookData data { get; set; }
        public bool livemode { get; set; }
        public string webhook_status { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public long created_at { get; set; }
        public List<EntWebHookLog> webhook_logs { get; set; }
        public EntOrder @object { get; set; }
    }
}
