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
using System.Configuration;

namespace BC.Modelos.Repositorios
{
    /// <summary>
    /// Descripción: Clase Repositorio con la implementacion de los metodos usados para el acceso a datos.
    /// </summary>
    internal class clsTblcatladaRepositorio : BC.Modelos.Interfaces.ITblcatladaRepositorio
    {
        /// <summary>
        /// Descripción: Metodo para guardar y actualizar un registro con los datos de la clase Informacion sin el manejo de la transaccion.
        /// </summary>
        /// <param name="oclsTblcatladaInformacion">Instancia de la clase que se guardara.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Save(BC.Modelos.Informacion.clsTblcatladaInformacion oclsTblcatladaInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb)
        {
            string vstrSP = string.Empty;
            try
            {
                if (oclsTblcatladaInformacion.bInsert)
                {
                    vstrSP = "sva_Tblcatlada_Ins";
                }
                else
                {
                    vstrSP = "sva_Tblcatlada_Upd";
                }
                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);
                pdb.AddInParameter(oCmd, "piIdlada", DbType.Int32, oclsTblcatladaInformacion.iIdlada);
                pdb.AddInParameter(oCmd, "psNombre", DbType.String, oclsTblcatladaInformacion.sNombre);
                pdb.AddInParameter(oCmd, "psDescripcion", DbType.String, oclsTblcatladaInformacion.sDescripcion);
                pdb.AddInParameter(oCmd, "pbActivo", DbType.String, oclsTblcatladaInformacion.bActivo);
                pdb.AddInParameter(oCmd, "pbBaja", DbType.String, oclsTblcatladaInformacion.bBaja);
                pdb.AddInParameter(oCmd, "piIdusuariocreacion", DbType.String, oclsTblcatladaInformacion.iIdusuariocreacion);
                pdb.AddInParameter(oCmd, "pdTfechacreacion", DbType.String, oclsTblcatladaInformacion.dTfechacreacion);
                pdb.AddInParameter(oCmd, "piIdusuariomodificacion", DbType.String, oclsTblcatladaInformacion.iIdusuariomodificacion);
                pdb.AddInParameter(oCmd, "pdTfechamodificacion", DbType.String, oclsTblcatladaInformacion.dTfechamodificacion);
                pdb.AddInParameter(oCmd, "piIdusuarioabaja", DbType.String, oclsTblcatladaInformacion.iIdusuarioabaja);
                pdb.AddInParameter(oCmd, "pdTfechabaja", DbType.String, oclsTblcatladaInformacion.dTfechabaja);


                pdb.ExecuteNonQuery(oCmd);

                oclsTblcatladaInformacion.bInsert = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para guardar y actualizar un registro con los datos de la clase Informacion con el manejo de la transacción.
        /// </summary>
        /// <param name="oclsTblcatladaInformacion">Instancia de la clase que se guardara.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="poTrans">Instancia de la Transacción.</param>
        public void m_Save(BC.Modelos.Informacion.clsTblcatladaInformacion oclsTblcatladaInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, System.Data.Common.DbTransaction poTrans)
        {
            string vstrSP = string.Empty;
            try
            {
                if (oclsTblcatladaInformacion.bInsert)
                {
                    vstrSP = "sva_Tblcatlada_Ins";
                }
                else
                {
                    vstrSP = "sva_Tblcatlada_Upd";
                }

                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);
                pdb.AddInParameter(oCmd, "piIdlada", DbType.Int32, oclsTblcatladaInformacion.iIdlada);
                pdb.AddInParameter(oCmd, "psNombre", DbType.String, oclsTblcatladaInformacion.sNombre);
                pdb.AddInParameter(oCmd, "psDescripcion", DbType.String, oclsTblcatladaInformacion.sDescripcion);
                pdb.AddInParameter(oCmd, "pbActivo", DbType.String, oclsTblcatladaInformacion.bActivo);
                pdb.AddInParameter(oCmd, "pbBaja", DbType.String, oclsTblcatladaInformacion.bBaja);
                pdb.AddInParameter(oCmd, "piIdusuariocreacion", DbType.String, oclsTblcatladaInformacion.iIdusuariocreacion);
                pdb.AddInParameter(oCmd, "pdTfechacreacion", DbType.String, oclsTblcatladaInformacion.dTfechacreacion);
                pdb.AddInParameter(oCmd, "piIdusuariomodificacion", DbType.String, oclsTblcatladaInformacion.iIdusuariomodificacion);
                pdb.AddInParameter(oCmd, "pdTfechamodificacion", DbType.String, oclsTblcatladaInformacion.dTfechamodificacion);
                pdb.AddInParameter(oCmd, "piIdusuarioabaja", DbType.String, oclsTblcatladaInformacion.iIdusuarioabaja);
                pdb.AddInParameter(oCmd, "pdTfechabaja", DbType.String, oclsTblcatladaInformacion.dTfechabaja);


                pdb.ExecuteNonQuery(oCmd, poTrans);


                oclsTblcatladaInformacion.bInsert = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para obtener todos los registros de la base de datos en un DataSet. Generalmente este obtiene solo los registros que no estan dados de baja.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos</param>
        /// <returns>Devuelve un objeto DataSet con la coleccion de los registros obtenidos en la consulta.</returns>
        public System.Data.DataSet m_Load(Microsoft.Practices.EnterpriseLibrary.Data.Database pdb)
        {
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("svc_Tblcatlada");
                DataSet ds = pdb.ExecuteDataSet(oCmd);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarLada(Database pdb, clsTblcatladaInformacion oclsTblcatladaInformacion)
        {
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("svc_ValidaLada");
                pdb.AddInParameter(oCmd, "psDescripcion", DbType.String, oclsTblcatladaInformacion.sDescripcion);
                DataSet ds = pdb.ExecuteDataSet(oCmd);

                if (ds.Tables["Table"].Rows.Count == 0)
                    throw new ArgumentException(ConfigurationManager.AppSettings["sMensajeErrorLada"]);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
