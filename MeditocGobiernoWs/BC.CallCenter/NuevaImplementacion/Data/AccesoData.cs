using BC.CallCenter.Clases;
using BC.CallCenter.NuevaImplementacion.DTO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace BC.CallCenter.NuevaImplementacion.Data
{
    public class AccesoData
    {
        private readonly Database database;

        public AccesoData()
        {
            this.database = clsBDPersonalizada.CreateDatabase("cnxCallCenter");
        }

        public DataSet getAccesos(string sPhoneNumber)
        {
            DataSet oResultado = new DataSet();
            try
            {
                string sProceduce = "svc_GetAccesos";

                using (DbCommand dbCommand = database.GetStoredProcCommand(sProceduce))
                {
                    database.AddInParameter(dbCommand, "pPhoneNumber", DbType.String, sPhoneNumber);
                    oResultado = database.ExecuteDataSet(dbCommand);

                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return oResultado;
        }

        public int save(AccesoDTO oAcceso)
        {
            int i = 0;
            try
            {
                string sProceduce = "sva_SaveAcceso";

                using (DbCommand dbCommand = database.GetStoredProcCommand(sProceduce))
                {                    
                    database.AddOutParameter(dbCommand, "iIdAcceso", DbType.String, oAcceso.iIdAcceso);
                    database.AddInParameter(dbCommand, "psTelefono", DbType.String, oAcceso.sTelefono);
                    database.AddInParameter(dbCommand, "pdtFechaCreacion", DbType.DateTime, oAcceso.dtFechaCreacion);
                    database.AddInParameter(dbCommand, "psNombreDispositivo", DbType.String, oAcceso.sNombreDispositivo);

                    database.ExecuteNonQuery(dbCommand);

                    i = Convert.ToInt32(dbCommand.Parameters["iIdAcceso"].Value);

                    if (i == 0)
                        throw new ArgumentException("Ocurrio un error inesperado al guardar su número.");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return i;
        }
    }
}
