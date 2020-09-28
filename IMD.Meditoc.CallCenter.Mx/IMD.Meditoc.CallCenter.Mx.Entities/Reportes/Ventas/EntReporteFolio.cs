using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntReporteFolio
    {
        public int iIdFolio { get; set; }
        public string sFolio { get; set; }
        public bool bTerminosYCondiciones { get; set; }
        public string sTerminosYCondiciones { get; set; }
        public int iIdOrigen { get; set; }
        public string sOrigen { get; set; }
        public string sFechaVencimiento { get; set; }
    }
}
