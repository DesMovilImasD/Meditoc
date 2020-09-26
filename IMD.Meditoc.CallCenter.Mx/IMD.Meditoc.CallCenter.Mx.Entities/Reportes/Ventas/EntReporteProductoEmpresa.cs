using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntReporteProductoEmpresa
    {
        public int iIdProducto { get; set; }
        public int iConsecutivo { get; set; }
        public int iIdTipoProducto { get; set; }
        public string sTipoProducto { get; set; }
        public string sItemId { get; set; }
        public int iQuantity { get; set; }
        public string sNombre { get; set; }
        public double nUnitPrice { get; set; }
        public EntReporteFolio entFolio { get; set; }
    }
}
