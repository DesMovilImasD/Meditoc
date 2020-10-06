using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Correo
{
    public class EntOrderEmail
    {
        public string sOrderId { get; set; }
        public string sBody { get; set; }
        public string sTo { get; set; }
        public string sSubject { get; set; }
    }
}
