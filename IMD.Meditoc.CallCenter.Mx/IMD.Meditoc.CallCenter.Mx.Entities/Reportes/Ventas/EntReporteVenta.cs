using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntReporteVenta
    {
        public EntResumenOrdenes ResumenOrdenes { get; set; }
        public EntResumenEmpresas ResumenEmpresas { get; set; }

        public EntReporteVenta()
        {
            ResumenOrdenes = new EntResumenOrdenes();
            ResumenEmpresas = new EntResumenEmpresas();
        }
    }
}
