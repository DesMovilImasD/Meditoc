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
    public class DatBoton
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatModulo));
        private Database database;
        IMDCommonData imdCommonData;
        private string saveBoton;

        public DatBoton()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveBoton = "sva_cgu_save_boton";
        }

        public IMDResponse<bool> DSaveBoton(EntBoton entBoton)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.saveBoton);
            logger.Info(IMDSerialize.Serialize(67823458338916, $"Inicia {metodo}(EntBoton entBoton)", entBoton));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveBoton))
                {
                    database.AddInParameter(dbCommand, "piIdModulo", DbType.Int32, entBoton.iIdModulo);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entBoton.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "piIdButon", DbType.Int32, entBoton.iIdBoton);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entBoton.sNombre);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entBoton.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entBoton.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458184293;
                response.Message = "Ocurrió un error al intentar guardar el botón.";

                logger.Error(IMDSerialize.Serialize(67823458338916, $"Error en {metodo}(EntBoton entBoton): {ex.Message}", entBoton, ex, response));
            }
            return response;
        }
    }
}
