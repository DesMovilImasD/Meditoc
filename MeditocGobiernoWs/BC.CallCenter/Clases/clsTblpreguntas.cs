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
using BC.Modelos.BE;
using BC.CallCenter.Clases;

namespace BC.Clases
{
	/// <summary>
    /// Descripción: Clase BF con los métodos públicos y el flujo del proceso.
    /// </summary>
    public class clsTblpreguntas
	{
		public clsTblpreguntasBE gbloclsTblpreguntasBE;

        //Database db = DatabaseFactory.CreateDatabase("--NombreCadenaConexionEnConfig--");
        Database db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");

        public clsTblpreguntas()
		{
		}
		
		/// <summary>
        /// Descripcion: Guarda o actualiza los datos contenidos en el objeto publico gbloclsTblpreguntasBE.
        /// </summary>
		public void m_Save()
        {
			try
            {
				if(gbloclsTblpreguntasBE != null)
				{
					gbloclsTblpreguntasBE.m_Save(db);
				}
				else
				{
					throw new Exception("No se puede guardar, faltan datos.");
				}
			}
			catch (Exception ex )
            {
                throw ex;
            }			
		}
		
		/// <summary>
        /// Descripcion: Guarda o actualiza los datos contenidos en el objeto publico gbloclsTblpreguntasBE manejando una transaccion.
        /// </summary>
		public void m_Save_Trans()
        {
			try
            {
				if(gbloclsTblpreguntasBE != null)
				{
					DbTransaction oTrans = null;
					using (DbConnection oCnn = db.CreateConnection())
					{
						oCnn.Open();
						if (!(oCnn.State == System.Data.ConnectionState.Open))
							throw new Exception("No se pudo establecer conexión con la base de datos.");
						else
							oTrans = oCnn.BeginTransaction();
	
						try
						{
							gbloclsTblpreguntasBE.m_Save(db, oTrans);
								
							oTrans.Commit();
						}
						catch (Exception ex)
						{
							oTrans.Rollback();
							throw ex;
						}
					}
				}
				else
				{
					throw new Exception("No se puede guardar, faltan datos.");
				}
			}
			catch (Exception ex )
            {
                throw ex;
            }			
		}
		
		/// <summary>
        /// Descripcion: Obtiene todos los registros relacionados con la clase. Los datos pueden ser accedidos mediante la lista gblListclsTblpreguntasBE en el objeto gbloclsTblpreguntasBE;
        /// </summary>
		public void m_Load_All()
        {
			try
            {
				gbloclsTblpreguntasBE= new clsTblpreguntasBE();
                gbloclsTblpreguntasBE.m_Load(db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}		

	}
}
