using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntReporteVentas
    {
        public int iTotalOrdenes { get; set; }
        public int iTotalFolios { get; set; }
        public double dTotalVendido { get; set; }
        public double dTotalDescontado { get; set; }
        public int iTotalCuponesAplicados { get; set; }
        public List<EntFolioReporte> lstFolios { get; set; }
    }
}
