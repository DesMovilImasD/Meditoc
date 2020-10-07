using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Ordenes
{
    public class EntCreatePaymentMethod
    {
        public int monthly_installments { get; set; }
        public string type { get; set; }
        public string token_id { get; set; }
        public long expires_at { get; set; }
    }
}
