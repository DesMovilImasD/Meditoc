using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Entities
{
    public class EntCreateCharge
    {
        public long amount { get; set; }
        public EntCreatePaymentMethod payment_method { get; set; }
    }
}