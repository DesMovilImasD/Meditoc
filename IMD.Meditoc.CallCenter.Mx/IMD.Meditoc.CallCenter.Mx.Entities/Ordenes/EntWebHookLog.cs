using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Admin.Conekta.Entities.WebHooks
{
    public class EntWebHookLog
    {
        public string id { get; set; }
        public string url { get; set; }
        public int failed_attempts { get; set; }
        public int last_http_response_status { get; set; }
        public long last_attempted_at { get; set; }
    }
}
