using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Admin.Conekta.Entities.Orders
{
    public class EntChargeDetail
    {
        public string id { get; set; }
        public bool livemode { get; set; }
        public long created_at { get; set; }
        public string currency { get; set; }
        public int? monthly_installments { get; set; }
        public string device_fingerprint { get; set; }
        public EntPaymentMehod payment_method { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public long amount { get; set; }
        public long paid_at { get; set; }
        public string fee { get; set; }
        public string customer_id { get; set; }
        public string order_id { get; set; }
    }
}
