using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Models.Info
{
    public class clsPacientesInfo
    {
        #region "CONSTRUCTORES"
        public clsPacientesInfo() : base() { }
        #endregion

        #region [Variables]
        private string _sUIDPaciente = string.Empty;
        private string _sUIDDR = string.Empty;
        private int _iIdCGUDR = 0;
        private bool _bOcupado = true;
        private string _sMensajeRespuesta = string.Empty;
        private bool _bResult = false;

        //Variables para la creación de nuevos usuarios CometChat
        private string _sNameDisplay = "";
        private string _sURLAvatar = "";
        private string _sURLPerfil = "";
        private string _sRol = "";

        //Variables de usuario DB
        private string _sNombre = "";
        private string _sApePaterno = "";
        private string _sApeMaterno = "";
        private string _sEmail = "";
        private string _sPassword = "";
        private string _sCodigoValidacion = "";
        private int _iIdUsuario = 0;
        private int _iNoMensaje = 0;
        private string _sFolio  = "";
        private bool _bGrupo = false;
        private string _sChat = "";
        private bool _bEnServicio = false;
        private bool _bBaja = false;
        private bool _bTerminosyCondiciones = false;

        #endregion

        #region [Propiedades]
        public string sUIDPaciente
        {
            get { return _sUIDPaciente; }
            set { _sUIDPaciente = value; }
        }
        public string sUIDDR
        {
            get { return _sUIDDR; }
            set { _sUIDDR = value; }
        }
        public int iIdCGUDR
        {
            get { return _iIdCGUDR; }
            set { _iIdCGUDR = value; }
        }
        public bool bOcupado
        {
            get { return _bOcupado; }
            set { _bOcupado = value; }
        }
        public string sMensajeRespuesta
        {
            get { return _sMensajeRespuesta; }
            set { _sMensajeRespuesta = value; }
        }
        public bool bResult
        {
            get { return _bResult; }
            set { _bResult = value; }
        }
        
        public string sNameDisplay
        {
            get { return _sNameDisplay; }
            set { _sNameDisplay = value; }
        }

        public string sURLAvatar
        {
            get { return _sURLAvatar; }
            set { _sURLAvatar = value; }
        }

        public string sURLPerfil
        {
            get { return _sURLPerfil; }
            set { _sURLPerfil = value; }
        }

        public string sRol
        {
            get { return _sRol; }
            set { _sRol = value; }
        }

        public string sNombre
        {
            get { return _sNombre; }
            set { _sNombre = value; }
        }

        public string sApePaterno
        {
            get { return _sApePaterno; }
            set { _sApePaterno = value; }
        }

        public string sApeMaterno
        {
            get { return _sApeMaterno; }
            set { _sApeMaterno = value; }
        }

        public string sEmail
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public string sPassword
        {
            get { return _sPassword; }
            set { _sPassword = value; }
        }

        public string sCodigoValidacion
        {
            get { return _sCodigoValidacion; }
            set { _sCodigoValidacion = value; }
        }

        public int iIdUsuario
        {
            get { return _iIdUsuario; }
            set { _iIdUsuario = value; }
        }

        public int iNoMensaje
        {
            get { return _iNoMensaje; }
            set { _iNoMensaje = value; }
        }

        public string sFolio
        {
            get { return _sFolio; }
            set { _sFolio = value; }
        }

        public bool bGrupo
        {
            get { return _bGrupo; }
            set { _bGrupo = value; }
        }

        public string sChat
        {
            get { return _sChat; }
            set { _sChat = value; }
        }
        public bool bEnServicio
        {
            get { return _bEnServicio; }
            set { _bEnServicio = value; }
        }
        public bool bBaja
        {
            get { return _bBaja; }
            set { _bBaja = value; }
        }
        public bool bTerminosyCondiciones
        {
            get { return _bTerminosyCondiciones; }
            set { _bTerminosyCondiciones = value; }
        }
        #endregion
    }
}
