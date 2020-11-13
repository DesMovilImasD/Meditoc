using System;
using System.Data;
using BC.CallCenter.Models.BE;
using BC.CallCenter.Models.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace BC.CallCenter.Models.Repositorios
{
    internal class clsBitacoraRepository : IBitacoraRepository
    {
        /// <summary>
        /// Descripción: Método para guardar la bitacora.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objBitacoraBE">Instancia del objBitacora.</param>
        public void m_Save(Database pdb, clsBitacoraBE objBitacoraBE)
        {
            Int32 i = 0;
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("sva_Bitacora_Ins");
                pdb.AddInParameter(oCmd, "psUID", DbType.String, objBitacoraBE.sUID);
                pdb.AddInParameter(oCmd, "psMensaje", DbType.String, objBitacoraBE.sMensaje);
                pdb.AddInParameter(oCmd, "pbError", DbType.Boolean, objBitacoraBE.bError);
                pdb.AddInParameter(oCmd, "psUserId", DbType.String, objBitacoraBE.sUserID);
                pdb.AddInParameter(oCmd, "piIdMedico", DbType.String, objBitacoraBE.iIdMedico);
                pdb.AddInParameter(oCmd, "psCoordenadas", DbType.String, objBitacoraBE.sCoordenadas);
                pdb.AddInParameter(oCmd, "psFolio", DbType.String, objBitacoraBE.sFolio);
                pdb.AddInParameter(oCmd, "psNumero", DbType.String, objBitacoraBE.sNumero);
                pdb.AddInParameter(oCmd, "psTipoFolio", DbType.String, objBitacoraBE.sTipoFolio);
                pdb.AddInParameter(oCmd, "psCP", DbType.String, objBitacoraBE.sCP);

                i = pdb.ExecuteNonQuery(oCmd);

                if ((i == 0))
                {
                    throw new Exception("No se Guardo el Registro. Intente de Nuevo");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
