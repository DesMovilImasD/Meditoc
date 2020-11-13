using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Models.Info
{
    [Serializable()]
    public class clsCometChatInfo
    {
        #region "CONSTRUCTORES"
        public clsCometChatInfo() : base() { }
        #endregion

        #region [Variables]
        private string _sUID;
        private string _sName;
        private string _sFriends;
        private int _iStatus=0;
        private bool _bResult=false;
        #endregion

        #region [Propiedades]
        public string sUID
        {
            get { return _sUID; }
            set { _sUID = value; }
        }
        public string sName
        {
            get { return _sName; }
            set { _sName = value; }
        }
        public string sFriends
        {
            get { return _sFriends; }
            set { _sFriends = value; }
        }

        public int iStatus
        {
            get { return _iStatus; }
            set { _iStatus = value; }
        }
        public bool bResult
        {
            get { return _bResult; }
            set { _bResult = value; }
        }
        #endregion
    }

    public class clsMensagesCCModel
    {
        #region "CONSTRUCTORES"
        public clsMensagesCCModel() : base() { }
        #endregion

        #region [Variables]
        private int _iMessage_id;
        private string _sSender_uid;
        private string _sReciever_uid;
        private string _sMessage;
        private string _sTimestamp;
        private string _sRead;
        private string _sVisibility;

        #endregion
        #region [Propiedades]
        public int iMessage_id
        {
            get { return _iMessage_id; }
            set { _iMessage_id = value; }
        }
        public string sSender_uid
        {
            get { return _sSender_uid; }
            set { _sSender_uid = value; }
        }
        public string sReciever_uid
        {
            get { return _sReciever_uid; }
            set { _sReciever_uid = value; }
        }

        public string sMessage
        {
            get { return _sMessage; }
            set { _sMessage = value; }
        }
        public string sTimestamp
        {
            get { return _sTimestamp; }
            set { _sTimestamp = value; }
        }
        public string sRead
        {
            get { return _sRead; }
            set { _sRead = value; }
        }
        public string sVisibility
        {
            get { return _sVisibility; }
            set { _sVisibility = value; }
        }
        #endregion
    }

    public class clsMsgGroupCCModel
    {
        #region "CONSTRUCTORES"
        public clsMsgGroupCCModel() : base() { }
        #endregion

        #region [Variables]
        private int _iMessage_id;
        private string _sGuid;
        private string _sSender_uid;
        private string _sMessage;
        private string _sTimestamp;
        private bool _bGrupo;
        #endregion
        #region [Propiedades]
        public int iMessage_id
        {
            get { return _iMessage_id; }
            set { _iMessage_id = value; }
        }
        public string sGuid
        {
            get { return _sGuid; }
            set { _sGuid = value; }
        }
        public string sSender_uid
        {
            get { return _sSender_uid; }
            set { _sSender_uid = value; }
        }
        public string sMessage
        {
            get { return _sMessage; }
            set { _sMessage = value; }
        }
        public string sTimestamp
        {
            get { return _sTimestamp; }
            set { _sTimestamp = value; }
        }
        public bool bGrupo
        {
            get { return _bGrupo; }
            set { _bGrupo = value; }
        }
        #endregion
    }
}
