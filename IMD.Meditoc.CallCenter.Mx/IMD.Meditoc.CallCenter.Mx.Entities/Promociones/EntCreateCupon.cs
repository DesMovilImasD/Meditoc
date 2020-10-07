using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Promociones
{
    public class EntCreateCupon
    {
        public int fiIdCuponCategoria { get; set; }
        public string fsDescripcion { get; set; }
        public string fsCodigo { get; set; }
        public int? fiLongitudCodigo { get; set; }
        public double? fnMontoDescuento { get; set; }
        public double? fnPorcentajeDescuento { get; set; }
        public int? fiMesBono { get; set; }
        public int fiTotalLanzamiento { get; set; }
        public int? fiDiasActivo { get; set; }
    }
}
