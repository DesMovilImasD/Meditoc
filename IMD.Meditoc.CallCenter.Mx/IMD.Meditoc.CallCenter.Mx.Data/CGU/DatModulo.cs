using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace IMD.Meditoc.CallCenter.Mx.Data.CGU
{
    public class DatModulo
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(DatModulo));
        private Database database;
        IMDCommonData imdCommonData;
        private string saveModulo;

        public DatModulo()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveModulo = "sva_cgu_save_modulo";
        }

        public IMDResponse<bool> DSaveModulo(EntModulo entModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.saveModulo);
            logger.Info(IMDSerialize.Serialize(67823458332700, $"Inicia {metodo}(EntModulo entModulo)", entModulo));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveModulo))
                {
                    database.AddInParameter(dbCommand, "piIdModulo", DbType.Int32, entModulo.iIdModulo);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entModulo.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entModulo.sNombre);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entModulo.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entModulo.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458184293;
                response.Message = "Ocurrió un error al intentar guardar el módulo.";

                logger.Error(IMDSerialize.Serialize(67823458332700, $"Error en {metodo}(EntModulo entModulo): {ex.Message}", entModulo, ex, response));
            }
            return response;
        }
    }
}
