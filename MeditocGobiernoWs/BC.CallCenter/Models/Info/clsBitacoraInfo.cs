namespace BC.CallCenter.Models.Info
{
    public class clsBitacoraInfo
    {
        #region "CONSTRUCTORES"
        public clsBitacoraInfo() : base() { }
        #endregion

        #region [Variables]
        private string _sUID = string.Empty;
        private string _sMensaje = string.Empty;
        private bool _bError = false;
        private string _sUserID;
        private string _iIdMedico;
        #endregion

        #region [Propiedades]
        public string sUID
        {
            get { return _sUID; }
            set { _sUID = value; }
        }
        public string sMensaje
        {
            get { return _sMensaje; }
            set { _sMensaje = value; }
        }
        public bool bError
        {
            get { return _bError; }
            set { _bError = value; }
        }
        public string iIdMedico
        {
            get { return _iIdMedico; }
            set { _iIdMedico = value; }
        }
        public string sUserID
        {
            get { return _sUserID; }
            set { _sUserID = value; }
        }

        public string sCoordenadas { get; set; }
        public string sFolio { get; set; }
        public string sNumero { get; set; }
        public string sTipoFolio { get; set; }
        public string sCP { get; set; }
        #endregion
    }
}
