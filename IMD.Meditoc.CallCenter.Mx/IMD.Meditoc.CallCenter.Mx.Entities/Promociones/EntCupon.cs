using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Entities
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
    }
}