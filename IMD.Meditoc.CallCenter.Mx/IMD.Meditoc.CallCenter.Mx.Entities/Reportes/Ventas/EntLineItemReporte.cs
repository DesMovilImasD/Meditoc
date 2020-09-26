using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntLineItemReporte
    {
        public int iConsecutive { get; set; }
        public string sItemId { get; set; }
        public string sItemName { get; set; }
        public double nUnitPrice { get; set; }
        public int iQuantity { get; set; }
        public string sFolio { get; set; }
        public int iIdTitular { get; set; }
    }
}
