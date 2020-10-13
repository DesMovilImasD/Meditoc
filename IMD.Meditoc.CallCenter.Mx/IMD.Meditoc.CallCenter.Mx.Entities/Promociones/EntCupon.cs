using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Promociones
{
    public class EntCupon
    {
        public int fiIdCupon { get; set; }
        public int fiIdCuponCategoria { get; set; }
        public string fsDescripcion { get; set; }
        public string fsDescripcionCategoria { get; set; }
        public string fsCodigo { get; set; }
        public double? fnMontoDescuento { get; set; }
        public double? fnPorcentajeDescuento { get; set; }
        public int? fiMesBono { get; set; }
        public int fiTotalLanzamiento { get; set; }
        public int fiTotalCanjeado { get; set; }
        public DateTime? fdtFechaVencimiento { get; set; }
        public bool fbActivo { get; set; }
        public bool fbBaja { get; set; }
        public string sFechaVencimiento { get; set; }
        public DateTime? dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
        public string sMontoDescuento { get; set; }
        public string sPorcentajeDescuento { get; set; }
        public bool fbVigente { get; set; }
    }
}
