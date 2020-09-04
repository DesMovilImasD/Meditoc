using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Entities
{
    public class EntCreateLineItem
    {
        public string name { get; set; }
        public long unit_price { get; set; }
        public int quantity { get; set; }
    }
}