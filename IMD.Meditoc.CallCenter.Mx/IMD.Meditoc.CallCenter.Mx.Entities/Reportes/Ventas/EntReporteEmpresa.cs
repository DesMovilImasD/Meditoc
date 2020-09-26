using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntReporteEmpresa
    {
        public int iIdEmpresa { get; set; }
        public string sNombre { get; set; }
        public string sFolioEmpresa { get; set; }
        public string sCorreo { get; set; }
        public string sFechaCreacion { get; set; }
        public double dTotalSinIva { get; set; }
        public double dTotalIva { get; set; }
        public double dTotal { get; set; }
        public int iTotalFolios { get; set; }
        public List<EntReporteProductoEmpresa> lstProductos { get; set; }
    }
}
