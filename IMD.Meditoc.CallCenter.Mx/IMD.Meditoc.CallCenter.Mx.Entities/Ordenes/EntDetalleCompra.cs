using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Ordenes
{
    public class EntDetalleCompra
    {
        public string sEmail { get; set; }
        public string sNombre { get; set; }
        public string sTelefono { get; set; }
        public string sOrden { get; set; }
        public double nTotal { get; set; }
        public double nTotalDescuento { get; set; }
        public double nTotalIVA { get; set; }
        public double nTotalPagado { get; set; }
        public string sCodigoCupon { get; set; }
        public bool bAplicaIVA { get; set; }
        public List<EntDetalleCompraArticulo> lstArticulos { get; set; }
        public EntDetalleCompra()
        {
            lstArticulos = new List<EntDetalleCompraArticulo>();
        }
    }
    public class EntDetalleCompraArticulo
    {
        public string sFolio { get; set; }
        public string sPass { get; set; }
        public string sDescripcion { get; set; }
        public double nPrecio { get; set; }
        public double nCantidad { get; set; }
        public DateTime? dtFechaVencimiento { get; set; }
        public int iIdProducto { get; set; }
        public int iIndex { get; set; }
    }
}
