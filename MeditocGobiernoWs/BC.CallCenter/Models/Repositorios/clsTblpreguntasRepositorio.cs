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
    internal class clsTblpreguntasRepositorio : BC.Modelos.Interfaces.ITblpreguntasRepositorio
    {
		/// <summary>
        /// Descripción: Metodo para guardar y actualizar un registro con los datos de la clase Informacion sin el manejo de la transaccion.
        /// </summary>
        /// <param name="oclsTblpreguntasInformacion">Instancia de la clase que se guardara.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
		public void m_Save(BC.Modelos.Informacion.clsTblpreguntasInformacion oclsTblpreguntasInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb)
		{
			string vstrSP = string.Empty;
            try
            {
				if (oclsTblpreguntasInformacion.bInsert)
                { 
					vstrSP = "sva_Tblpreguntas_Ins"; 
				}
                else
                { 
					vstrSP = "sva_Tblpreguntas_Upd"; 
				}
                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);				
				pdb.AddInParameter(oCmd, "piIdpreguntas", DbType.Int32, oclsTblpreguntasInformacion.iIdpreguntas);
				pdb.AddInParameter(oCmd, "psNombre", DbType.String, oclsTblpreguntasInformacion.sNombre);
				pdb.AddInParameter(oCmd, "psParam", DbType.String, oclsTblpreguntasInformacion.sParam);
				pdb.AddInParameter(oCmd, "piOrden", DbType.Int32, oclsTblpreguntasInformacion.iOrden);
				pdb.AddInParameter(oCmd, "pdtFechacreacion", DbType.DateTime, oclsTblpreguntasInformacion.dtFechacreacion);
				pdb.AddInParameter(oCmd, "pdtFechamodificacion", DbType.DateTime, oclsTblpreguntasInformacion.dtFechamodificacion);
				pdb.AddInParameter(oCmd, "pdtFechabaja", DbType.DateTime, oclsTblpreguntasInformacion.dtFechabaja);
				pdb.AddInParameter(oCmd, "pbActivo", DbType.Boolean, oclsTblpreguntasInformacion.bActivo);
				pdb.AddInParameter(oCmd, "pbBaja", DbType.Boolean, oclsTblpreguntasInformacion.bBaja);

				
				pdb.ExecuteNonQuery(oCmd);

				oclsTblpreguntasInformacion.bInsert=false;
			}
            catch (Exception ex)
            {
                throw ex;
            }
		}
		
		/// <summary>
        /// Descripción: Metodo para guardar y actualizar un registro con los datos de la clase Informacion con el manejo de la transacción.
        /// </summary>
        /// <param name="oclsTblpreguntasInformacion">Instancia de la clase que se guardara.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="poTrans">Instancia de la Transacción.</param>
		public void m_Save(BC.Modelos.Informacion.clsTblpreguntasInformacion oclsTblpreguntasInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, System.Data.Common.DbTransaction poTrans)
		{
			string vstrSP = string.Empty;
            try
            {
				if (oclsTblpreguntasInformacion.bInsert)
                { 
					vstrSP = "sva_Tblpreguntas_Ins"; 
				}
                else
                { 
					vstrSP = "sva_Tblpreguntas_Upd"; 
				}
               
                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);				
				pdb.AddInParameter(oCmd, "piIdpreguntas", DbType.Int32, oclsTblpreguntasInformacion.iIdpreguntas);
				pdb.AddInParameter(oCmd, "psNombre", DbType.String, oclsTblpreguntasInformacion.sNombre);
				pdb.AddInParameter(oCmd, "psParam", DbType.String, oclsTblpreguntasInformacion.sParam);
				pdb.AddInParameter(oCmd, "piOrden", DbType.Int32, oclsTblpreguntasInformacion.iOrden);
				pdb.AddInParameter(oCmd, "pdtFechacreacion", DbType.DateTime, oclsTblpreguntasInformacion.dtFechacreacion);
				pdb.AddInParameter(oCmd, "pdtFechamodificacion", DbType.DateTime, oclsTblpreguntasInformacion.dtFechamodificacion);
				pdb.AddInParameter(oCmd, "pdtFechabaja", DbType.DateTime, oclsTblpreguntasInformacion.dtFechabaja);
				pdb.AddInParameter(oCmd, "pbActivo", DbType.Boolean, oclsTblpreguntasInformacion.bActivo);
				pdb.AddInParameter(oCmd, "pbBaja", DbType.Boolean, oclsTblpreguntasInformacion.bBaja);

				
				pdb.ExecuteNonQuery(oCmd, poTrans);

				
				oclsTblpreguntasInformacion.bInsert=false;
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
				DbCommand oCmd = pdb.GetStoredProcCommand("svc_Tblpreguntas");
                DataSet ds = pdb.ExecuteDataSet(oCmd);
                return ds;
			}
            catch (Exception ex)
            {
                throw ex;
            }
		}

	}
}
