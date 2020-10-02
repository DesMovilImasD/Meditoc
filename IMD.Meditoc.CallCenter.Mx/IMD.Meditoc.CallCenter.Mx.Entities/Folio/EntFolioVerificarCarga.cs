using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioVerificarCarga
    {
        public int totalFolios { get; set; }
        public EntProducto entProducto { get; set; }
        public EntEmpresa entEmpresa { get; set; }
        public List<EntFolioUser> lstFolios { get; set; }

        public EntFolioVerificarCarga()
        {
            lstFolios = new List<EntFolioUser>();
            entProducto = new EntProducto();
            entEmpresa = new EntEmpresa();
        }
    }
}
