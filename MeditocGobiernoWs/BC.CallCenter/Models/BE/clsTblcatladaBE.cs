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
    public class clsTblcatladaBE : clsTblcatladaInformacion
    {
        [NonSerialized()]
        public List<clsTblcatladaBE> gblListclsTblcatladaBE;


        [NonSerialized]
        public ITblcatladaRepositorio gbloclsTblcatladaRepositorio;

        public clsTblcatladaBE() : this(new clsTblcatladaRepositorio())
        {
        }

        public clsTblcatladaBE(ITblcatladaRepositorio repositorio) : base()
        {
            gbloclsTblcatladaRepositorio = repositorio;
        }

        /// <summary>
        /// Descripción: Metodo para guardar y actualizar los datos de la clase sin el manejo de la transaccion.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        internal void m_Save(Database pdb)
        {
            try
            {
                gbloclsTblcatladaRepositorio.m_Save((clsTblcatladaInformacion)this, pdb);

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
                gbloclsTblcatladaRepositorio.m_Save((clsTblcatladaInformacion)this, pdb, poTrans);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para obtener todos los registros de la base de datos en un DataSet. Generalmente este obtiene solo los registros que no estan dados de baja.
        /// El detalle de los registros obtenidos se encuentra en la colección de objetos gblListclsTblcatladaBE.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Load(Database pdb)
        {
            gblListclsTblcatladaBE = new List<clsTblcatladaBE>();
            clsTblcatladaBE objBE;
            try
            {
                DataSet ds = gbloclsTblcatladaRepositorio.m_Load(pdb);
                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    objBE = new clsTblcatladaBE();
                    objBE.iIdlada = rw["iIdlada"] is DBNull ? 0 : Convert.ToInt32(rw["iIdlada"]);
                    objBE.sNombre = rw["sNombre"] is DBNull ? String.Empty : Convert.ToString(rw["sNombre"]);
                    objBE.sDescripcion = rw["sDescripcion"] is DBNull ? String.Empty : Convert.ToString(rw["sDescripcion"]);
                    objBE.bActivo = rw["bActivo"] is DBNull ? String.Empty : Convert.ToString(rw["bActivo"]);
                    objBE.bBaja = rw["bBaja"] is DBNull ? String.Empty : Convert.ToString(rw["bBaja"]);
                    objBE.iIdusuariocreacion = rw["iIdusuariocreacion"] is DBNull ? String.Empty : Convert.ToString(rw["iIdusuariocreacion"]);
                    objBE.dTfechacreacion = rw["dTfechacreacion"] is DBNull ? String.Empty : Convert.ToString(rw["dTfechacreacion"]);
                    objBE.iIdusuariomodificacion = rw["iIdusuariomodificacion"] is DBNull ? String.Empty : Convert.ToString(rw["iIdusuariomodificacion"]);
                    objBE.dTfechamodificacion = rw["dTfechamodificacion"] is DBNull ? String.Empty : Convert.ToString(rw["dTfechamodificacion"]);
                    objBE.iIdusuarioabaja = rw["iIdusuarioabaja"] is DBNull ? String.Empty : Convert.ToString(rw["iIdusuarioabaja"]);
                    objBE.dTfechabaja = rw["dTfechabaja"] is DBNull ? String.Empty : Convert.ToString(rw["dTfechabaja"]);
                    this.bInsert = false;
                    gblListclsTblcatladaBE.Add(objBE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarLada(Database pdb)
        {
            try
            {
                gbloclsTblcatladaRepositorio.ValidarLada(pdb, this);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
