using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Ordenes
{
    public class EntRequestOrder
    {
        public string id { get; set; }
        public long amount { get; set; }
        public long amount_paid { get; set; }
        public long amount_discount { get; set; }
        public long amount_tax { get; set; }
        public string coupon_code { get; set; }
    }
}
