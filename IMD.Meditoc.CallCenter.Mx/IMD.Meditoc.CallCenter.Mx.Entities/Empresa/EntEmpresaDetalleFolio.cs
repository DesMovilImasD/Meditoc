using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
