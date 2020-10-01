using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.CallCenter
{
    public class DatCallCenter
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatCallCenter));
        private Database database;
        IMDCommonData imdCommonData;
        string svaCallCenterOnline;

        public DatCallCenter()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            svaCallCenterOnline = "sva_meditoc_callcenter_online";
        }

        public IMDResponse<bool> DCallCenterOnline(int piIdColaborador, bool pbOnline, bool pbOcupado, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DCallCenterOnline);
            logger.Info(IMDSerialize.Serialize(67823458507525, $"Inicia {metodo}(int piIdColaborador, bool pbOnline, bool pbOcupado, int piIdUsuarioMod)", piIdColaborador, pbOnline, pbOcupado, piIdUsuarioMod));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(svaCallCenterOnline))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "pbOnline", DbType.Boolean, pbOnline);
                    database.AddInParameter(dbCommand, "pbOcupado", DbType.Boolean, pbOcupado);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458508302;
                response.Message = "Ocurrió un error inesperado al actualizar el estatus.";

                logger.Error(IMDSerialize.Serialize(67823458508302, $"Error en {metodo}(int piIdColaborador, bool pbOnline, bool pbOcupado, int piIdUsuarioMod): {ex.Message}", piIdColaborador, pbOnline, pbOcupado, piIdUsuarioMod, ex, response));
            }
            return response;
        }
    }
}
