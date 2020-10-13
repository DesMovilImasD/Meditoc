using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Catalogos
{
    public class EntCatalogos
    {
        public List<EntCatalogo> catCuponCategoria { get; set; }
        public List<EntCatalogo> catEspecialidad { get; set; }
        public List<EntCatalogo> catEstatusConsulta { get; set; }
        public List<EntCatalogo> catGrupoProducto { get; set; }
        public List<EntCatalogo> catOrigen { get; set; }
        public List<EntCatalogo> catSexo { get; set; }
        public List<EntCatalogo> catTipoDoctor { get; set; }
        public List<EntCatalogo> catTipoProducto { get; set; }
        public List<EntCatalogo> catUsuarioAccion { get; set; }
        public List<EntCatalogo> catTipoCuenta { get; set; }
        public List<EntCatalogo> catPerfil { get; set; }
        public List<EntCatalogo> catIcon { get; set; }
    }
}
