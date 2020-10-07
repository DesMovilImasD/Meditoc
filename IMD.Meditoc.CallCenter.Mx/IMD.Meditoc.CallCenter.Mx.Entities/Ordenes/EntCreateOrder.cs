using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Ordenes
{
    public class EntCreateOrder
    {
        public int iIdOrigen { get; set; }
        public string currency { get; set; }
        public int? coupon { get; set; }
        public bool tax { get; set; }
        public EntCreateCustomerInfo customer_info { get; set; }
        public List<EntCreateLineItem> line_items { get; set; }
        public List<EntCreateDiscountLine> discount_lines { get; set; }
        public List<EntCreateTaxLine> tax_lines { get; set; }
        public List<EntCreateCharge> charges { get; set; }
    }
}
