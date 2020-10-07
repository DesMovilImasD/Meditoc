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

namespace IMD.Meditoc.CallCenter.Mx.Data.Correo
{
    public class DatCorreo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatCorreo));

        private Database database;
        IMDCommonData imdCommonData;
        private string spInsCorreo;
        private string spGetCorreo;

        public DatCorreo()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");
            spInsCorreo = "sva_meditoc_save_correo";
            spGetCorreo = "svc_meditoc_correos";
        }

        public IMDResponse<bool> DSaveCorreo(string psOrderId, string psBody, string psTo, string psSubject)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveCorreo);
            logger.Info(IMDSerialize.Serialize(67823458624075, $"Inicia {metodo}(string psOrderId, string psBody, string psTo, string psSubject)", psOrderId, psTo, psSubject));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spInsCorreo))
                {
                    database.AddInParameter(dbCommand, "psOrderId", DbType.String, psOrderId);
                    database.AddInParameter(dbCommand, "psBody", DbType.String, psBody);
                    database.AddInParameter(dbCommand, "psTo", DbType.String, psTo);
                    database.AddInParameter(dbCommand, "psSubject", DbType.String, psSubject);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458624852;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el correo.";

                logger.Error(IMDSerialize.Serialize(67823458624852, $"Error en {metodo}(string psOrderId, string psBody, string psTo, string psSubject): {ex.Message}", ex, psOrderId, psTo, psSubject, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetCorreo(string psOrderId)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetCorreo);
            logger.Info(IMDSerialize.Serialize(67823458625629, $"Inicia {metodo}(string psOrderId)", psOrderId));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetCorreo))
                {
                    database.AddInParameter(dbCommand, "psOrderId", DbType.String, psOrderId);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458626406;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar el correo.";

                logger.Error(IMDSerialize.Serialize(67823458626406, $"Error en {metodo}(string psOrderId): {ex.Message}", psOrderId, ex, response));
            }
            return response;
        }
    }
}
