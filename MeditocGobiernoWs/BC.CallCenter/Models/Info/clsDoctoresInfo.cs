using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Models.Info
{
    public class clsDoctoresInfo
    {
        #region "CONSTRUCTORES"
        public clsDoctoresInfo() : base() { }
        #endregion

        #region [Variables]
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
        #endregion

        #region [Propiedades]

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

        #endregion
    }
}
