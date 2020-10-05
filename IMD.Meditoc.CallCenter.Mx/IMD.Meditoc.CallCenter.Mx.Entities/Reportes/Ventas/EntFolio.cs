using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntFolio
    {
        public int iIdFolio { get; set; }
        public string sFolio { get; set; }
        public int iConsecutivo { get; set; }
        public bool bTerminosYCondiciones { get; set; }
        public bool bConfirmado { get; set; }
        public int iIdOrigen { get; set; }
        public string sOrigen { get; set; }
        public DateTime? dtFechaVencimiento { get; set; }
        public string sFechaVencimiento { get; set; }
        public EntEmpresa entEmpresa { get; set; }
        public EntProducto entProducto { get; set; }
        public EntOrden entOrden { get; set; }
    }
}
