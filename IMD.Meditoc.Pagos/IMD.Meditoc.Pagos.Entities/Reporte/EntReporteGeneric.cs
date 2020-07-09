using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.Pagos.Entities.Reporte
{
    public class EntReporteGeneric
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
        public int iConsecutive { get; set; }
        public string sItemId { get; set; }
        public string sItemName { get; set; }
        public double nUnitPrice { get; set; }
        public int iQuantity { get; set; }
        public DateTime dtRegisterDate { get; set; }
    }
}
