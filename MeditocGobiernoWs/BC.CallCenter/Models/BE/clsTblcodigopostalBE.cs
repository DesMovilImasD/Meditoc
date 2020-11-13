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
    public class clsTblcodigopostalBE : clsTblcodigopostalInformacion
    {
        [NonSerialized()]
        public List<clsTblcodigopostalBE> gblListclsTblcodigopostalBE;


        [NonSerialized]
        public ITblcodigopostalRepositorio gbloclsTblcodigopostalRepositorio;

        public clsTblcodigopostalBE() : this(new clsTblcodigopostalRepositorio())
        {
        }

        public clsTblcodigopostalBE(ITblcodigopostalRepositorio repositorio) : base()
        {
            gbloclsTblcodigopostalRepositorio = repositorio;
        }

        /// <summary>
        /// Descripción: Metodo para guardar y actualizar los datos de la clase sin el manejo de la transaccion.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        internal void m_Save(Database pdb)
        {
            try
            {
                gbloclsTblcodigopostalRepositorio.m_Save((clsTblcodigopostalInformacion)this, pdb);

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
                gbloclsTblcodigopostalRepositorio.m_Save((clsTblcodigopostalInformacion)this, pdb, poTrans);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para obtener todos los registros de la base de datos en un DataSet. Generalmente este obtiene solo los registros que no estan dados de baja.
        /// El detalle de los registros obtenidos se encuentra en la colección de objetos gblListclsTblcodigopostalBE.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Load(Database pdb)
        {
            gblListclsTblcodigopostalBE = new List<clsTblcodigopostalBE>();
            clsTblcodigopostalBE objBE;
            try
            {
                DataSet ds = gbloclsTblcodigopostalRepositorio.m_Load(pdb);
                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    objBE = new clsTblcodigopostalBE();
                    objBE.iIdcodigopostal = rw["iIdcodigopostal"] is DBNull ? 0 : Convert.ToInt32(rw["iIdcodigopostal"]);
                    objBE.sCodigo = rw["sCodigo"] is DBNull ? String.Empty : Convert.ToString(rw["sCodigo"]);
                    objBE.sAsentamiento = rw["sAsentamiento"] is DBNull ? String.Empty : Convert.ToString(rw["sAsentamiento"]);
                    objBE.sTipoasentamiento = rw["sTipoasentamiento"] is DBNull ? String.Empty : Convert.ToString(rw["sTipoasentamiento"]);
                    objBE.sMunicipio = rw["sMunicipio"] is DBNull ? String.Empty : Convert.ToString(rw["sMunicipio"]);
                    objBE.sEstado = rw["sEstado"] is DBNull ? String.Empty : Convert.ToString(rw["sEstado"]);
                    objBE.sCiudad = rw["sCiudad"] is DBNull ? String.Empty : Convert.ToString(rw["sCiudad"]);
                    objBE.dtFechacreacion = rw["dtFechacreacion"] is DBNull ? new DateTime(1900, 01, 01) : Convert.ToDateTime(rw["dtFechacreacion"]);
                    objBE.dtFechamodificacion = rw["dtFechamodificacion"] is DBNull ? new DateTime(1900, 01, 01) : Convert.ToDateTime(rw["dtFechamodificacion"]);
                    objBE.dtFechabaja = rw["dtFechabaja"] is DBNull ? new DateTime(1900, 01, 01) : Convert.ToDateTime(rw["dtFechabaja"]);
                    objBE.bBaja = rw["bBaja"] is DBNull ? false : Convert.ToBoolean(rw["bBaja"]);
                    this.bInsert = false;
                    gblListclsTblcodigopostalBE.Add(objBE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidarCP(Database pdb)
        {
            bool esValido = false;
            try
            {
                int i = gbloclsTblcodigopostalRepositorio.ValidarCP(pdb, this);

                if (i == 1)
                    esValido = true;

                return esValido;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
