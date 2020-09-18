using IMD.Admin.Conekta.Entities.Orders;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.CallCenter
{
    public class EntNuevaConsulta
    {
        public string sFolio { get; set; }
        public EntCustomerInfo customerInfo { get; set; }
        public EntConsulta consulta { get; set; }
    }
}
