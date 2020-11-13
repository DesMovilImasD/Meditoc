using BC.CallCenter.Clases;
using BC.CallCenter.NuevaImplementacion.DTO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace BC.CallCenter.NuevaImplementacion.Data
{
    public class BitacoraData
    {
        private readonly Database database;

        public BitacoraData()
        {
            this.database = clsBDPersonalizada.CreateDatabase("cnxCallCenter");
        }

        public void save(BitacoraDTO bitacoraDTO)
        {
            try
            {

                string sProceduce = "sva_SaveBitacora";

                using (DbCommand dbCommand = database.GetStoredProcCommand(sProceduce))
                {
                    database.AddInParameter(dbCommand, "piIdAcceso", DbType.Int16, bitacoraDTO.iIdAcceso);
                    database.AddInParameter(dbCommand, "piIdEncuesta", DbType.Int16, bitacoraDTO.iIdEncuesta);
                    database.AddInParameter(dbCommand, "piIdLlamada", DbType.Int16, bitacoraDTO.iIdLlamada);
                    database.AddInParameter(dbCommand, "psEstatus", DbType.String, bitacoraDTO.sEstatus);
                    database.AddInParameter(dbCommand, "psMensaje", DbType.String, bitacoraDTO.sMensaje);
                    database.AddInParameter(dbCommand, "psCoordenadas", DbType.String, bitacoraDTO.sCoordenadas);
                    database.AddInParameter(dbCommand, "pdtFechaCreacion", DbType.DateTime, bitacoraDTO.dtFechaCreacion);

                    int i = database.ExecuteNonQuery(dbCommand);

                    if (i == 0)
                        throw new ArgumentException("Ocurrio un error inesperado al guardar en la bitacora.");
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public int saveTrazado(BitacoraDTO bitacoraDTO)
        {
            int i = 0;
            try
            {
                string sProceduce = "sva_SaveBitacora";

                using (DbCommand dbCommand = database.GetStoredProcCommand(sProceduce))
                {
                    database.AddInParameter(dbCommand, "piIdAcceso", DbType.Int16, bitacoraDTO.iIdAcceso);
                    database.AddInParameter(dbCommand, "piIdEncuesta", DbType.Int16, bitacoraDTO.iIdEncuesta);
                    database.AddInParameter(dbCommand, "piIdLlamada", DbType.Int16, bitacoraDTO.iIdLlamada);
                    database.AddInParameter(dbCommand, "psEstatus", DbType.String, bitacoraDTO.sEstatus);
                    database.AddInParameter(dbCommand, "psMensaje", DbType.String, bitacoraDTO.sMensaje);
                    database.AddInParameter(dbCommand, "psCoordenadas", DbType.String, bitacoraDTO.sCoordenadas);
                    database.AddInParameter(dbCommand, "pdtFechaCreacion", DbType.DateTime, bitacoraDTO.dtFechaCreacion);

                    i = database.ExecuteNonQuery(dbCommand);

                    if (i == 0)
                        throw new ArgumentException("Ocurrio un error inesperado al guardar en la bitacora.");
                }

            }
            catch (System.Exception e)
            {

                throw e;
            }

            return i;
        }
    }
}
