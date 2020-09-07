using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.Folio
{
    public class DatFolio
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatFolio));
        private Database database;
        IMDCommonData imdCommonData;
        string saveFolio;

        public DatFolio()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveFolio = "sva_meditoc_save_folo";

        }


        public IMDResponse<DataTable> DSaveFolio(EntFolio entFolio)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSaveFolio);
            logger.Info(IMDSerialize.Serialize(67823458418170, $"Inicia {metodo}"));

            try
            {

                using (DbCommand dbCommand = database.GetStoredProcCommand(saveFolio))
                {
                    database.AddInParameter(dbCommand, "piIdEmpresa", DbType.Int32, entFolio.iIdEmpresa);
                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, entFolio.iIdProducto);
                    database.AddInParameter(dbCommand, "piIdConsecutivo", DbType.Int32, entFolio.iConsecutivo);
                    database.AddInParameter(dbCommand, "piIdOrigen", DbType.Int32, entFolio.iIdOrigen);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, entFolio.sPassword);
                    database.AddInParameter(dbCommand, "pdtFechaVencimiento", DbType.DateTime, entFolio.dtFechaVencimiento);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entFolio.iIdUsuarioMod);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458418947;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458418947, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

    }
}
