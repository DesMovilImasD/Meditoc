using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.Pagos.Entities.Reporte
{
    public class EntOrderReporte
    {
        public Guid uId { get; set; }
        public string sOrderId { get; set; }
        public double nAmount { get; set; }
        public string sPaymentStatus { get; set; }
        public string sEmail { get; set; }
        public string sPhone { get; set; }
        public string sName { get; set; }
        public string sChargeId { get; set; }
        public string sAuthCode { get; set; }
        public string sType { get; set; }
        public List<EntItemReporte> lstItems { get; set; }
        public DateTime dtRegisterDate { get; set; }
        public string sRegisterDate { get; set; }
    }
}
