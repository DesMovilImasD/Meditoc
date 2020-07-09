using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.Pagos.Entities.Reporte
{
    public class EntItemReporte
    {
        public int iConsecutive { get; set; }
        public string sItemId { get; set; }
        public string sItemName { get; set; }
        public double nUnitPrice { get; set; }
        public int iQuantity { get; set; }
    }
}
