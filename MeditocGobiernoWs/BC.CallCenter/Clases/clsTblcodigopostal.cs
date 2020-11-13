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
using System.Configuration;

namespace BC.Clases
{
    /// <summary>
    /// Descripción: Clase BF con los métodos públicos y el flujo del proceso.
    /// </summary>
    public class clsTblcodigopostal
    {
        public clsTblcodigopostalBE gbloclsTblcodigopostalBE;


        //Database db = DatabaseFactory.CreateDatabase("--NombreCadenaConexionEnConfig--");
        Database db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");

        public clsTblcodigopostal()
        {
        }

        /// <summary>
        /// Descripcion: Guarda o actualiza los datos contenidos en el objeto publico gbloclsTblcodigopostalBE.
        /// </summary>
        public void m_Save()
        {
            try
            {
                if (gbloclsTblcodigopostalBE != null)
                {
                    gbloclsTblcodigopostalBE.m_Save(db);
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
        /// Descripcion: Guarda o actualiza los datos contenidos en el objeto publico gbloclsTblcodigopostalBE manejando una transaccion.
        /// </summary>
        public void m_Save_Trans()
        {
            try
            {
                if (gbloclsTblcodigopostalBE != null)
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
                            gbloclsTblcodigopostalBE.m_Save(db, oTrans);

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
        /// Descripcion: Obtiene todos los registros relacionados con la clase. Los datos pueden ser accedidos mediante la lista gblListclsTblcodigopostalBE en el objeto gbloclsTblcodigopostalBE;
        /// </summary>
        public void m_Load_All()
        {
            try
            {
                gbloclsTblcodigopostalBE = new clsTblcodigopostalBE();
                gbloclsTblcodigopostalBE.m_Load(db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidarCP(string sCodigoPostal)
        {
            bool bEsValido = false;
            try
            {
                gbloclsTblcodigopostalBE = new clsTblcodigopostalBE();
                gbloclsTblcodigopostalBE.sCodigo = sCodigoPostal;

                bEsValido = gbloclsTblcodigopostalBE.ValidarCP(db);

                if(!bEsValido)
                    throw new ArgumentException(ConfigurationManager.AppSettings["sMensajeErrorCodigoPostal"]);

                return bEsValido;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
