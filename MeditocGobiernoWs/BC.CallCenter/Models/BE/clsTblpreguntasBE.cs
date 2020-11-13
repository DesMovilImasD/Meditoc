//========================================================================
// Este archivo fue generado usando MyGeneration.
//========================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using BC.Modelos.Informacion;
using BC.Modelos.Interfaces;
using BC.Modelos.Repositorios;

namespace BC.Modelos.BE
{
	/// <summary>
    /// Descripción: Clase BE con los metodos usados para la reglamentacion de las entidades de negocios.
    /// </summary>
	 [Serializable()]
    public class clsTblpreguntasBE : clsTblpreguntasInformacion
    {
		[NonSerialized()]
        public  List<clsTblpreguntasBE> gblListclsTblpreguntasBE;

	
		[NonSerialized]
        public ITblpreguntasRepositorio gbloclsTblpreguntasRepositorio;
		
        public clsTblpreguntasBE() : this(new clsTblpreguntasRepositorio()) 
        {
        }
		
        public clsTblpreguntasBE(ITblpreguntasRepositorio repositorio): base()
        {
            gbloclsTblpreguntasRepositorio = repositorio;
        }
		
		/// <summary>
        /// Descripción: Metodo para guardar y actualizar los datos de la clase sin el manejo de la transaccion.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        internal void m_Save(Database pdb)
        {
            try
            {
                gbloclsTblpreguntasRepositorio.m_Save((clsTblpreguntasInformacion)this, pdb);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
		
		/// <summary>
        /// Descripción: Metodo para guardar y actualizar los datos de la clase con el manejo de la transaccion.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="poTrans">Instancia de la Transacción.</param>
        internal void m_Save(Database pdb, DbTransaction poTrans)
        {
            try
            {
                gbloclsTblpreguntasRepositorio.m_Save((clsTblpreguntasInformacion)this, pdb, poTrans);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
		
		/// <summary>
        /// Descripción: Método para obtener todos los registros de la base de datos en un DataSet. Generalmente este obtiene solo los registros que no estan dados de baja.
		/// El detalle de los registros obtenidos se encuentra en la colección de objetos gblListclsTblpreguntasBE.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
		public void m_Load(Database pdb)
        {
			gblListclsTblpreguntasBE= new List<clsTblpreguntasBE>();
			clsTblpreguntasBE objBE;
            try
            {
                DataSet ds = gbloclsTblpreguntasRepositorio.m_Load(pdb);
                foreach (DataRow rw in ds.Tables[0].Rows)
                {
					objBE= new clsTblpreguntasBE();
					objBE.iIdpreguntas= rw["iIdpreguntas"] is DBNull ? 0 : Convert.ToInt32(rw["iIdpreguntas"]);
					objBE.sNombre= rw["sNombre"] is DBNull ? String.Empty : Convert.ToString(rw["sNombre"]);
					objBE.sParam= rw["sParam"] is DBNull ? String.Empty : Convert.ToString(rw["sParam"]);
					objBE.iOrden= rw["iOrden"] is DBNull ? 0 : Convert.ToInt32(rw["iOrden"]);
					objBE.dtFechacreacion= rw["dtFechacreacion"] is DBNull ? new DateTime(1900, 01, 01) : Convert.ToDateTime(rw["dtFechacreacion"]);
					objBE.dtFechamodificacion= rw["dtFechamodificacion"] is DBNull ? new DateTime(1900, 01, 01) : Convert.ToDateTime(rw["dtFechamodificacion"]);
					objBE.dtFechabaja= rw["dtFechabaja"] is DBNull ? new DateTime(1900, 01, 01) : Convert.ToDateTime(rw["dtFechabaja"]);
					objBE.bActivo= rw["bActivo"] is DBNull ? false : Convert.ToBoolean(rw["bActivo"]);
					objBE.bBaja= rw["bBaja"] is DBNull ? false : Convert.ToBoolean(rw["bBaja"]);
					this.bInsert = false;
					gblListclsTblpreguntasBE.Add(objBE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


	}
}
