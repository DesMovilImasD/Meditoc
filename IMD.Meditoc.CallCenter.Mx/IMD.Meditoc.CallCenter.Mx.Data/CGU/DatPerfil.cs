using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.CGU
{
    public class DatPerfil
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatPerfil));
        private Database database;
        IMDCommonData imdCommonData;
        private string savePerfil;

        public DatPerfil()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            savePerfil = "sva_cgu_save_perfil";
        }

        public IMDResponse<bool> DSavePerfil(EntPerfil entPerfil)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.savePerfil);
            logger.Info(IMDSerialize.Serialize(67823458340470, $"Inicia {metodo}(EntPerfil entPerfil)", entPerfil));
            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(savePerfil))
                {
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, entPerfil.iIdPerfil);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entPerfil.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entPerfil.sNombre);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entPerfil.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entPerfil.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458184293;
                response.Message = "Ocurrió un error al intentar guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458332700, $"Error en {metodo}(EntPerfil entPerfil): {ex.Message}", entPerfil, ex, response));
            }
            return response;
        }
    }
}
