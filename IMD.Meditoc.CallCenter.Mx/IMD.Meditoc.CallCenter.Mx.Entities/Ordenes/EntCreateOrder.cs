using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Entities
{
    public class EntCreateOrder
    {
        public string currency { get; set; }
        [JsonIgnore]
        public int? coupon { get; set; }
        public EntCreateCustomerInfo customer_info { get; set; }
        public List<EntCreateLineItem> line_items { get; set; }
        public List<EntCreateDiscountLine> discount_lines { get; set; }
        public List<EntCreateCharge> charges { get; set; }
    }
}