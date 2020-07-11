using IMD.Admin.Utilities.Business.Serialize;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.Pagos.Data
{
    public class DatReportes
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatReportes));

#if DEBUG
        private readonly Database database;
        readonly IMDCommonData imdCommonData;
#else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Database database;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IMDCommonData imdCommonData;
#endif
        public DatReportes()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxConekta";
            database = imdCommonData.DGetDatabase(FsConnectionString, "hdiu4soi3IHD334F", "SKlru3nc");
        }

        public IMDResponse<DataTable> DObtenerReporteOrdenes(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerReporteOrdenes);
            logger.Info(IMDSerialize.Serialize(67823458161760, $"Inicia {metodo}(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null)", psStatus, psType, pdtFechaInicio, pdtFechaFinal));

            try
            {
                string procedure = "conekta.svcObtenerReporteOrdenes";

                using (DbCommand dbCommand = database.GetStoredProcCommand(procedure))
                {
                    database.AddInParameter(dbCommand, "psStatus", DbType.String, psStatus);
                    database.AddInParameter(dbCommand, "psType", DbType.String, psType);
                    database.AddInParameter(dbCommand, "pdtFechaInicio", DbType.DateTime, pdtFechaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaFinal", DbType.DateTime, pdtFechaFinal);

                    response = imdCommonData.DExecuteDT(database, dbCommand, true);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458162537;
                response.Message = "Ocurrió un error inesperado al consultar las órdenes en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458162537, $"Error en {metodo}(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null): {ex.Message}", psStatus, psType, pdtFechaInicio, pdtFechaFinal, ex, response));
            }
            return response;
        }
    }
}
