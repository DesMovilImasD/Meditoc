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
    public class DatSubModulo
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(DatModulo));
        private Database database;
        IMDCommonData imdCommonData;
        private string saveModulo;

        public DatSubModulo()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveModulo = "sva_cgu_save_submodulo";
        }

        public IMDResponse<bool> DSaveSubModulo(EntSubModulo entSubModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.saveModulo);
            logger.Info(IMDSerialize.Serialize(67823458332700, $"Inicia {metodo}(EntSubModulo entSubModulo)", entSubModulo));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveModulo))
                {
                    database.AddInParameter(dbCommand, "piIdModulo", DbType.Int32, entSubModulo.iIdModulo);
                    database.AddInParameter(dbCommand, "piIdSubmodulo", DbType.Int32, entSubModulo.iIdSubModulo);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entSubModulo.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entSubModulo.sNombre);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entSubModulo.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entSubModulo.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458184293;
                response.Message = "Ocurrió un error inesperado en la base de datos al intentar guardar el submódulo.";

                logger.Error(IMDSerialize.Serialize(67823458332700, $"Error en {metodo}(EntSubModulo entSubModulo): {ex.Message}", entSubModulo, ex, response));
            }
            return response;
        }

    }
}
