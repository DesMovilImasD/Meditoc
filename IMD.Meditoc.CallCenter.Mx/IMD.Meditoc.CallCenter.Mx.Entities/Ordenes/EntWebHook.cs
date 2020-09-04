using IMD.Admin.Conekta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Entities
{
    public class EntWebHook
    {
        public EntWebHookData data { get; set; }
        public bool livemode { get; set; }
        public string webhook_status { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public int created_at { get; set; }
        public List<EntWebHookLog> webhook_logs { get; set; }
        public EntOrder @object { get; set; }
    }
}