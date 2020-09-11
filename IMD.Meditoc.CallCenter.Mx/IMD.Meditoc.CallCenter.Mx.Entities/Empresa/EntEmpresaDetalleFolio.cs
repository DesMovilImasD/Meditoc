using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using System.Collections.Generic;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Empresa
{
    public class EntEmpresaDetalleFolio
    {
        public EntEmpresa entEmpresa;
        public List<EntDetalleCompraArticulo> lstArticulos { get; set; }

        public EntEmpresaDetalleFolio()
        {
            lstArticulos = new List<EntDetalleCompraArticulo>();
            entEmpresa = new EntEmpresa();
        }
    }
}
