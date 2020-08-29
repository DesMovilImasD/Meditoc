using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.CGU
{
    public class EntUsuario
    {
        public int? iIdUsuario { get; set; }
        public int? iIdTipoCuenta { get; set; }
        public string sTipoCuenta { get; set; }
        public int? iIdPerfil { get; set; }
        public string sPerfil { get; set; }
        public string sUsuario { get; set; }
        public string sPassword { get; set; }
        public string sNombres { get; set; }
        public string sApellidoPaterno { get; set; }
        public string sApellidoMaterno { get; set; }
        public DateTime? dtFechaNacimiento { get; set; }
        public string sTelefono { get; set; }
        public string sCorreo { get; set; }
        public string sDomicilio { get; set; }
        public int? iIdUsuarioMod { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
