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
    public class clsTblcatlada
    {
        public clsTblcatladaBE gbloclsTblcatladaBE;

        //Database db = DatabaseFactory.CreateDatabase("--NombreCadenaConexionEnConfig--");
        Database db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");

        public clsTblcatlada()
        {
        }

        /// <summary>
        /// Descripcion: Guarda o actualiza los datos contenidos en el objeto publico gbloclsTblcatladaBE.
        /// </summary>
        public void m_Save()
        {
            try
            {
                if (gbloclsTblcatladaBE != null)
                {
                    gbloclsTblcatladaBE.m_Save(db);
                }
                else
                {
                    throw new Exception("No se puede guardar, faltan datos.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripcion: Guarda o actualiza los datos contenidos en el objeto publico gbloclsTblcatladaBE manejando una transaccion.
        /// </summary>
        public void m_Save_Trans()
        {
            try
            {
                if (gbloclsTblcatladaBE != null)
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
                            gbloclsTblcatladaBE.m_Save(db, oTrans);

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripcion: Obtiene todos los registros relacionados con la clase. Los datos pueden ser accedidos mediante la lista gblListclsTblcatladaBE en el objeto gbloclsTblcatladaBE;
        /// </summary>
        public void m_Load_All()
        {
            try
            {
                gbloclsTblcatladaBE = new clsTblcatladaBE();
                gbloclsTblcatladaBE.m_Load(db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarLada(string sNumero)
        {
            try
            {
                gbloclsTblcatladaBE = new clsTblcatladaBE();
                gbloclsTblcatladaBE.sDescripcion = sNumero.Substring(0, 3);

                gbloclsTblcatladaBE.ValidarLada(db);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
