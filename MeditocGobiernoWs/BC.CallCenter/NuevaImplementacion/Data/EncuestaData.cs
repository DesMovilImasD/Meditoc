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
    class EncuestaData
    {
        private readonly Database database;

        public EncuestaData()
        {
            this.database = clsBDPersonalizada.CreateDatabase("cnxCallCenter");
        }

        /// <summary>
        /// Método que guarda la encuesta
        /// </summary>
        /// <param name="encuestaDTO">EL objeto de la encuesta</param>
        /// <returns>El id del que se inserto.</returns>
        public int save(EncuestaDTO encuestaDTO)
        {
            int i = 0;
            try
            {
                string sProceduce = "sva_SaveEncuesta";

                using (DbCommand dbCommand = database.GetStoredProcCommand(sProceduce))
                {
                    database.AddOutParameter(dbCommand, "iIdEncuesta", DbType.Int16, encuestaDTO.iIdEncuesta);
                    database.AddInParameter(dbCommand, "piIdAcceso", DbType.Int16, encuestaDTO.iIdAcceso);
                    database.AddInParameter(dbCommand, "psTipoFolio", DbType.String, encuestaDTO.sTipoFolio);
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, encuestaDTO.sFolio == null ? "" : encuestaDTO.sFolio);
                    database.AddInParameter(dbCommand, "psCP", DbType.String, encuestaDTO.sCP);
                    database.AddInParameter(dbCommand, "pdtFechaCreacion", DbType.DateTime, encuestaDTO.dtFechaCreacion);

                    database.ExecuteNonQuery(dbCommand);

                    i = Convert.ToInt32(dbCommand.Parameters["iIdEncuesta"].Value);

                    if (i == 0)
                        throw new ArgumentException("Ocurrio un error inesperado al guardar su encuesta.");
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
