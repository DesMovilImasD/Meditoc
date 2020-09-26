using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntResumenOrdenes
    {
        public int iTotalOrdenes { get; set; }
        public int iTotalFolios { get; set; }
        public double dTotalVendido { get; set; }
        public double dTotalDescontado { get; set; }
        public int iTotalCuponesAplicados { get; set; }
        public List<EntReporteOrden> lstOrdenes { get; set; }
    }
}
