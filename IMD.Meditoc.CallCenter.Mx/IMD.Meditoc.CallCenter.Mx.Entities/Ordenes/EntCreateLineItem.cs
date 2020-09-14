using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Admin.Conekta.Entities.Orders
{
    public class EntCreateLineItem
    {
        public string name { get; set; }
        public long unit_price { get; set; }
        public int quantity { get; set; }
    }
}
