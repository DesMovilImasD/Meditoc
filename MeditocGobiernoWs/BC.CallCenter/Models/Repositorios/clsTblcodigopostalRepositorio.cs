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

namespace BC.Modelos.Repositorios
{
    /// <summary>
    /// Descripción: Clase Repositorio con la implementacion de los metodos usados para el acceso a datos.
    /// </summary>
    internal class clsTblcodigopostalRepositorio : BC.Modelos.Interfaces.ITblcodigopostalRepositorio
    {
        /// <summary>
        /// Descripción: Metodo para guardar y actualizar un registro con los datos de la clase Informacion sin el manejo de la transaccion.
        /// </summary>
        /// <param name="oclsTblcodigopostalInformacion">Instancia de la clase que se guardara.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Save(BC.Modelos.Informacion.clsTblcodigopostalInformacion oclsTblcodigopostalInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb)
        {
            string vstrSP = string.Empty;
            try
            {
                if (oclsTblcodigopostalInformacion.bInsert)
                {
                    vstrSP = "sva_Tblcodigopostal_Ins";
                }
                else
                {
                    vstrSP = "sva_Tblcodigopostal_Upd";
                }
                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);
                pdb.AddInParameter(oCmd, "piIdcodigopostal", DbType.Int32, oclsTblcodigopostalInformacion.iIdcodigopostal);
                pdb.AddInParameter(oCmd, "psCodigo", DbType.String, oclsTblcodigopostalInformacion.sCodigo);
                pdb.AddInParameter(oCmd, "psAsentamiento", DbType.String, oclsTblcodigopostalInformacion.sAsentamiento);
                pdb.AddInParameter(oCmd, "psTipoasentamiento", DbType.String, oclsTblcodigopostalInformacion.sTipoasentamiento);
                pdb.AddInParameter(oCmd, "psMunicipio", DbType.String, oclsTblcodigopostalInformacion.sMunicipio);
                pdb.AddInParameter(oCmd, "psEstado", DbType.String, oclsTblcodigopostalInformacion.sEstado);
                pdb.AddInParameter(oCmd, "psCiudad", DbType.String, oclsTblcodigopostalInformacion.sCiudad);
                pdb.AddInParameter(oCmd, "pdtFechacreacion", DbType.DateTime, oclsTblcodigopostalInformacion.dtFechacreacion);
                pdb.AddInParameter(oCmd, "pdtFechamodificacion", DbType.DateTime, oclsTblcodigopostalInformacion.dtFechamodificacion);
                pdb.AddInParameter(oCmd, "pdtFechabaja", DbType.DateTime, oclsTblcodigopostalInformacion.dtFechabaja);
                pdb.AddInParameter(oCmd, "pbBaja", DbType.Boolean, oclsTblcodigopostalInformacion.bBaja);


                pdb.ExecuteNonQuery(oCmd);

                oclsTblcodigopostalInformacion.bInsert = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para guardar y actualizar un registro con los datos de la clase Informacion con el manejo de la transacción.
        /// </summary>
        /// <param name="oclsTblcodigopostalInformacion">Instancia de la clase que se guardara.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="poTrans">Instancia de la Transacción.</param>
        public void m_Save(BC.Modelos.Informacion.clsTblcodigopostalInformacion oclsTblcodigopostalInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, System.Data.Common.DbTransaction poTrans)
        {
            string vstrSP = string.Empty;
            try
            {
                if (oclsTblcodigopostalInformacion.bInsert)
                {
                    vstrSP = "sva_Tblcodigopostal_Ins";
                }
                else
                {
                    vstrSP = "sva_Tblcodigopostal_Upd";
                }

                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);
                pdb.AddInParameter(oCmd, "piIdcodigopostal", DbType.Int32, oclsTblcodigopostalInformacion.iIdcodigopostal);
                pdb.AddInParameter(oCmd, "psCodigo", DbType.String, oclsTblcodigopostalInformacion.sCodigo);
                pdb.AddInParameter(oCmd, "psAsentamiento", DbType.String, oclsTblcodigopostalInformacion.sAsentamiento);
                pdb.AddInParameter(oCmd, "psTipoasentamiento", DbType.String, oclsTblcodigopostalInformacion.sTipoasentamiento);
                pdb.AddInParameter(oCmd, "psMunicipio", DbType.String, oclsTblcodigopostalInformacion.sMunicipio);
                pdb.AddInParameter(oCmd, "psEstado", DbType.String, oclsTblcodigopostalInformacion.sEstado);
                pdb.AddInParameter(oCmd, "psCiudad", DbType.String, oclsTblcodigopostalInformacion.sCiudad);
                pdb.AddInParameter(oCmd, "pdtFechacreacion", DbType.DateTime, oclsTblcodigopostalInformacion.dtFechacreacion);
                pdb.AddInParameter(oCmd, "pdtFechamodificacion", DbType.DateTime, oclsTblcodigopostalInformacion.dtFechamodificacion);
                pdb.AddInParameter(oCmd, "pdtFechabaja", DbType.DateTime, oclsTblcodigopostalInformacion.dtFechabaja);
                pdb.AddInParameter(oCmd, "pbBaja", DbType.Boolean, oclsTblcodigopostalInformacion.bBaja);


                pdb.ExecuteNonQuery(oCmd, poTrans);


                oclsTblcodigopostalInformacion.bInsert = false;
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
                DbCommand oCmd = pdb.GetStoredProcCommand("svc_Tblcodigopostal");
                DataSet ds = pdb.ExecuteDataSet(oCmd);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para validar si existe el Código postal ingresado
        /// </summary>
        /// <param name="pdb"></param>
        /// <param name="oclsTblcodigopostalInformacion"></param>
        /// <returns></returns>
        public int ValidarCP(Database pdb, clsTblcodigopostalInformacion oclsTblcodigopostalInformacion)
        {
            try
            {
                int i = 0;
                DbCommand oCmd = pdb.GetStoredProcCommand("svc_ValidaCodigoPostal");
                pdb.AddInParameter(oCmd, "psCodigo", DbType.String, oclsTblcodigopostalInformacion.sCodigo);
                DataSet ds = pdb.ExecuteDataSet(oCmd);

                if (ds.Tables["Table"].Rows.Count >= 1)
                    i = 1;

                return i;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
