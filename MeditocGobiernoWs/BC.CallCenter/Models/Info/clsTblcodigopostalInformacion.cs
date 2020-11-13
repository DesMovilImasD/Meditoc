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
    public class clsTblcodigopostalInformacion : MarshalByRefObject
    {
		#region "VARIABLES"
		private int _iIdcodigopostal;
		private string _sCodigo;
		private string _sAsentamiento;
		private string _sTipoasentamiento;
		private string _sMunicipio;
		private string _sEstado;
		private string _sCiudad;
		private DateTime _dtFechacreacion;
		private DateTime _dtFechamodificacion;
		private DateTime _dtFechabaja;
		private bool _bBaja;
		internal string _MensajeSistema;
        internal string _MensajePersonalizado;
        internal Boolean _bInsert = true;
		#endregion
		
		#region "CONSTRUCTORES"
        public clsTblcodigopostalInformacion() : base() { }
        #endregion

        #region "PROPIEDADES"
		public int iIdcodigopostal
		{
			get { return _iIdcodigopostal; }
			set { _iIdcodigopostal = value; }
		}
		public string sCodigo
		{
			get { return _sCodigo; }
			set { _sCodigo = value; }
		}
		public string sAsentamiento
		{
			get { return _sAsentamiento; }
			set { _sAsentamiento = value; }
		}
		public string sTipoasentamiento
		{
			get { return _sTipoasentamiento; }
			set { _sTipoasentamiento = value; }
		}
		public string sMunicipio
		{
			get { return _sMunicipio; }
			set { _sMunicipio = value; }
		}
		public string sEstado
		{
			get { return _sEstado; }
			set { _sEstado = value; }
		}
		public string sCiudad
		{
			get { return _sCiudad; }
			set { _sCiudad = value; }
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
		#endregion
	}
}
