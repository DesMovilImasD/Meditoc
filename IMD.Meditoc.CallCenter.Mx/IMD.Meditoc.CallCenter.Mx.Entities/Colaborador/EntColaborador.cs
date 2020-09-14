using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Colaborador
{
    public class EntColaborador
    {
        public int iIdColaborador { get; set; }
        public int iIdTipoDoctor { get; set; }
        public string sTipoDoctor { get; set; }
        public int iIdEspecialidad { get; set; }
        public string sEspecialidad { get; set; }
        public int iIdUsuarioCGU { get; set; }
        public int iIdTipoCuenta { get; set; }
        public string sTipoCuenta { get; set; }
        public int iNumSala { get; set; }
        public string sNombreDirectorio { get; set; }
        public string sCedulaProfecional { get; set; }
        public string sTelefonoDirectorio { get; set; }
        public string sCorreoDirectorio { get; set; }
        public string sDireccionConsultorio { get; set; }
        public string sRFC { get; set; }
        public string sURL { get; set; }
        public string sMaps { get; set; }
        public string sUsuarioTitular { get; set; }
        public string sPasswordTitular { get; set; }
        public string sUsuarioAdministrativo { get; set; }
        public string sPasswordAdministrativo { get; set; }
        public string sNombresDoctor { get; set; }
        public string sApellidoPaternoDoctor { get; set; }
        public string sApellidoMaternoDoctor { get; set; }
        public DateTime dtFechaNacimientoDoctor { get; set; }
        public string sFechaNacimientoDoctor { get; set; }
        public string sTelefonoDoctor { get; set; }
        public string sCorreoDoctor { get; set; }
        public string sDomicilioDoctor { get; set; }
        public bool bOnline { get; set; }
        public bool bOcupado { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
