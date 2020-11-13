//========================================================================
// Este archivo fue generado usando MyGeneration.
//========================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BC.Modelos.Informacion
{
	/// <summary>
    /// Descripción: Clase que contiene la estrutura de la tabla. Tambien cuenta con una variable booleana (bInsert) que identifica si el objeto debe ser guardado o modificado.
    /// </summary>
    [Serializable()]
    public class clsTblcatladaInformacion : MarshalByRefObject
    {
		#region "VARIABLES"
		private int _iIdlada;
		private string _sNombre;
		private string _sDescripcion;
		private string _bActivo;
		private string _bBaja;
		private string _iIdusuariocreacion;
		private string _dTfechacreacion;
		private string _iIdusuariomodificacion;
		private string _dTfechamodificacion;
		private string _iIdusuarioabaja;
		private string _dTfechabaja;
		internal string _MensajeSistema;
        internal string _MensajePersonalizado;
        internal Boolean _bInsert = true;
		#endregion
		
		#region "CONSTRUCTORES"
        public clsTblcatladaInformacion() : base() { }
        #endregion

        #region "PROPIEDADES"
		public int iIdlada
		{
			get { return _iIdlada; }
			set { _iIdlada = value; }
		}
		public string sNombre
		{
			get { return _sNombre; }
			set { _sNombre = value; }
		}
		public string sDescripcion
		{
			get { return _sDescripcion; }
			set { _sDescripcion = value; }
		}
		public string bActivo
		{
			get { return _bActivo; }
			set { _bActivo = value; }
		}
		public string bBaja
		{
			get { return _bBaja; }
			set { _bBaja = value; }
		}
		public string iIdusuariocreacion
		{
			get { return _iIdusuariocreacion; }
			set { _iIdusuariocreacion = value; }
		}
		public string dTfechacreacion
		{
			get { return _dTfechacreacion; }
			set { _dTfechacreacion = value; }
		}
		public string iIdusuariomodificacion
		{
			get { return _iIdusuariomodificacion; }
			set { _iIdusuariomodificacion = value; }
		}
		public string dTfechamodificacion
		{
			get { return _dTfechamodificacion; }
			set { _dTfechamodificacion = value; }
		}
		public string iIdusuarioabaja
		{
			get { return _iIdusuarioabaja; }
			set { _iIdusuarioabaja = value; }
		}
		public string dTfechabaja
		{
			get { return _dTfechabaja; }
			set { _dTfechabaja = value; }
		}
		public string sMensajeSistema { get { return _MensajeSistema; } }
        public string sMensajePersonalizado { get { return _MensajePersonalizado; } }
		public bool bInsert
        {
            get { return _bInsert; }
            set { _bInsert = value; }
        }
		#endregion
	}
}
