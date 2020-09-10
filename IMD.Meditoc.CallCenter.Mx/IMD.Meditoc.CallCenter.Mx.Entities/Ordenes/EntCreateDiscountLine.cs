using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Admin.Conekta.Entities.Orders
{
    public class EntCreateDiscountLine
    {
        public string code { get; set; }
        public string type { get; set; }
        public long amount { get; set; }
    }
}
