using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Entities
{
    public class EntOrder
    {
        public bool livemode { get; set; }
        public long amount { get; set; }
        public string currency { get; set; }
        public string payment_status { get; set; }
        public long amount_refunded { get; set; }
        public long amount_paid { get; set; }
        public EntCustomerInfo customer_info { get; set; }
        public string id { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public EntLineItem line_items { get; set; }
        public EntCharge charges { get; set; }
        public string order_id { get; set; }
        public string status { get; set; }
        public string @object { get; set; }
    }
}