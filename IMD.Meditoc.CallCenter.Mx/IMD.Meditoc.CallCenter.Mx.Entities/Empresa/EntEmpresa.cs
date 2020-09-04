using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Empresa
{
    public class EntEmpresa
    {
        public int iIdEmpresa { get; set; }
        public string sNombre { get; set; }
        public string sFolioEmpresa { get; set; }
        public string sCorreo { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public int iIdUsuarioCreacion { get; set; }
        public DateTime dtFechaModificacion { get; set; }
        public int iIdUsuarioModificacion { get; set; }
        public DateTime dtFechaBaja { get; set; }
        public int iIdUsuarioBaja { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
