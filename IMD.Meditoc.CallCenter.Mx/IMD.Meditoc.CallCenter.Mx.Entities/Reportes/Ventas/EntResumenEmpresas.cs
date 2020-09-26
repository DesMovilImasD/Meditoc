using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntResumenEmpresas
    {
        public int iTotalEmpresas { get; set; }
        public int iTotalFolios { get; set; }
        public double dTotalVendido { get; set; }
        public List<EntReporteEmpresa> lstEmpresas { get; set; }
    }
}
