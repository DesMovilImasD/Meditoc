using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Models.Info
{

    /// <summary>
    /// Descripción: Clase que contiene la estrutura de los datos pasados a la funcion.
    /// </summary>
    [Serializable()]
    public class clsEnvioMailInfo : MarshalByRefObject
    {
        #region "VARIABLES"
        private string _sServerMail;
        private string _sUserMail;
        private string _sPassMail;
        private bool _bSSLMail;
        private string _sAsuntoMail;
        private string _sMensajeMail;
        private int _iPortMail;
        private bool _bAdjuntarFile;
        private string _sFile;
        #endregion

        #region "CONSTRUCTORES"
        public clsEnvioMailInfo() : base() { }
        #endregion

        #region "PROPIEDADES"
        public string sServerMail
        {
            get { return _sServerMail; }
            set { _sServerMail = value; }
        }
        public string sUserMail
        {
            get { return _sUserMail; }
            set { _sUserMail = value; }
        }
        public string sPassMail
        {
            get { return _sPassMail; }
            set { _sPassMail = value; }
        }
        public bool bSSLMail
        {
            get { return _bSSLMail; }
            set { _bSSLMail = value; }
        }
        public string sAsuntoMail
        {
            get { return _sAsuntoMail; }
            set { _sAsuntoMail = value; }
        }
        public string sMensajeMail
        {
            get { return _sMensajeMail; }
            set { _sMensajeMail = value; }
        }
        public int iPortMail
        {
            get { return _iPortMail; }
            set { _iPortMail = value; }
        }
        public bool bAdjuntarFile
        {
            get { return _bAdjuntarFile; }
            set { _bAdjuntarFile = value; }
        }

        public string sFile
        {
            get { return _sFile; }
            set { _sFile = value; }
        }

        #endregion

    }
}