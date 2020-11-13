using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace CallCenter.Views.MedicDirectory
{
    public class medicSpecialityDTO
    {

        public string iIdColaborador { get; set; }
        public string iIdTipoDoctor { get; set; }
        public string sTipoDoctor { get; set; }
        public string iIdEspecialidad { get; set; }
        public string sEspecialidad { get; set; }
        public string iIdUsuarioCGU { get; set; }
        public string iIdTipoCuenta { get; set; }
        public string sTipoCuenta { get; set; }
        public string iNumSala { get; set; }
        public string sNombreDirectorio { get; set; }
        public string sCedulaProfecional { get; set; }
        public string sTelefonoDirectorio { get; set; }
        public string sWhatsApp { get; set; }
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
        public string dtFechaNacimientoDoctor { get; set; }
        public string sFechaNacimientoDoctor { get; set; }
        public string sTelefonoDoctor { get; set; }
        public string sCorreoDoctor { get; set; }
        public string sDomicilioDoctor { get; set; }
        public string bOnline { get; set; }
        public string bOcupado { get; set; }
        public string dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
        public string bActivo { get; set; }
        public string bBaja { get; set; }
        public string sFoto { get; set; }
        public ImageSource isFoto { get; set; }
        public string  sIconWhatsApp { get; set; }
        public string sIconCellPhone { get; set; }
        public string sIconMaps { get; set; }
        public string sNombreCompleto { get { return String.Format("{0} {1} {2}", sNombresDoctor, sApellidoPaternoDoctor, sApellidoMaternoDoctor); } }
        public medicSpecialityDTO()
        {
        }
    }

    public class EntDirectorio
    {
        public int iIdColaborador { get; set; }
        public int iIdEspecialidad { get; set; }
        public string sEspecialidad { get; set; }
        public string sNombre { get; set; }
        public string sCedulaProfecional { get; set; }
        public string sTelefono { get; set; }
        public string sWhatsApp { get; set; }
        public string sCorreo { get; set; }
        public string sFoto { get; set; }
        public string sNombreConsultorio { get; set; }
        public string sDireccionConsultorio { get; set; }
        public string sRFC { get; set; }
        public string sURL { get; set; }
        public string sMaps { get; set; }
        public string sIconWhatsApp { get; set; }
        public string sIconCellPhone { get; set; }
        public string sIconMaps { get; set; }
    }

    public class EspecialistasDTO
    {
        public Int64 iTotalPaginas { get; set; }
        public List<EntDirectorio> lstColaboradores { get;set; }

    }
}
