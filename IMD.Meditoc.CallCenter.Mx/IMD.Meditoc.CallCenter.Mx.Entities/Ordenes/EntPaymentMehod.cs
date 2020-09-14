using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Admin.Conekta.Entities.Orders
{
    public class EntPaymentMehod
    {
        public string name { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string auth_code { get; set; }
        public string type { get; set; }
        public string last4 { get; set; }
        public string brand { get; set; }
        public string issuer { get; set; }
        public string account_type { get; set; }
        public string country { get; set; }
        public string service_name { get; set; }
        public string barcode_url { get; set; }
        public long expires_at { get; set; }
        public string store_name { get; set; }
        public string reference { get; set; }
    }
}
