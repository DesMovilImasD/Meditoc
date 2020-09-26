using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Admin.Conekta.Entities.Orders
{
    public class EntLineItemDetail
    {
        public string name { get; set; }
        public long unit_price { get; set; }
        public int quantity { get; set; }
        public string id { get; set; }
        public string parent_id { get; set; }
        public int consecutive { get; set; }
        public int product_id { get; set; }
        public int months_expiration { get; set; }
    }
}
