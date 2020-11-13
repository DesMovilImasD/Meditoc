using BC.CallCenter.Clases;
using BC.CallCenter.NuevaImplementacion.DTO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.Data
{
    public class LlamadaData
    {
        private readonly Database database;

        public LlamadaData()
        {
            this.database = clsBDPersonalizada.CreateDatabase("cnxCallCenter");
        }


        public int save(LlamadaDTO llamadaDTO)
        {
            int i = 0;
            try
            {
                string sProceduce = "sva_SaveLlamada";

                using (DbCommand dbCommand = database.GetStoredProcCommand(sProceduce))
                {
                    database.AddOutParameter(dbCommand, "iIdLlamada", DbType.String, llamadaDTO.iIdLlamada);
                    database.AddInParameter(dbCommand, "piIdAcceso", DbType.String, llamadaDTO.iIdAcceso);
                    database.AddInParameter(dbCommand, "piIdDoctor", DbType.String, llamadaDTO.iIdDoctor);
                    database.AddInParameter(dbCommand, "psNombreDoctor", DbType.String, llamadaDTO.sNombreDoctor);
                    database.AddInParameter(dbCommand, "pdtFechaCreacion", DbType.DateTime, llamadaDTO.dtFechaCreacion);

                    database.ExecuteNonQuery(dbCommand);

                    i = Convert.ToInt32(dbCommand.Parameters["iIdLlamada"].Value);

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
