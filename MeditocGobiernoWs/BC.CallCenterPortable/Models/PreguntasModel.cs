using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    public class PreguntasModel
    {
        #region "VARIABLES"
        private int _iIdpreguntas;
        private string _sNombre;
        private string _sParam;
        private int _iOrden;
        private DateTime _dtFechacreacion;
        private DateTime _dtFechamodificacion;
        private DateTime _dtFechabaja;
        private bool _bActivo;
        private bool _bBaja;
        internal string _MensajeSistema;
        internal string _MensajePersonalizado;
        internal Boolean _bInsert = true;
        #endregion

        #region "PROPIEDADES"
        public int iIdpreguntas
        {
            get { return _iIdpreguntas; }
            set { _iIdpreguntas = value; }
        }
        public string sNombre
        {
            get { return _sNombre; }
            set { _sNombre = value; }
        }
        public string sParam
        {
            get { return _sParam; }
            set { _sParam = value; }
        }
        public int iOrden
        {
            get { return _iOrden; }
            set { _iOrden = value; }
        }
        public DateTime dtFechacreacion
        {
            get { return _dtFechacreacion; }
            set { _dtFechacreacion = value; }
        }
        public DateTime dtFechamodificacion
        {
            get { return _dtFechamodificacion; }
            set { _dtFechamodificacion = value; }
        }
        public DateTime dtFechabaja
        {
            get { return _dtFechabaja; }
            set { _dtFechabaja = value; }
        }
        public bool bActivo
        {
            get { return _bActivo; }
            set { _bActivo = value; }
        }
        public bool bBaja
        {
            get { return _bBaja; }
            set { _bBaja = value; }
        }
        public string sMensajeSistema { get { return _MensajeSistema; } }
        public string sMensajePersonalizado { get { return _MensajePersonalizado; } }
        public bool bInsert
        {
            get { return _bInsert; }
            set { _bInsert = value; }
        }
        public bool bPrecionado { get; set; }
        #endregion
    }
}
