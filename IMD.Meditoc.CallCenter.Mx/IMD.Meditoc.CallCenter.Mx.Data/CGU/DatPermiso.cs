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
    public class DatPermiso
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatPermiso));
        private Database database;
        IMDCommonData imdCommonData;
        private string savePermiso;
        private string ObterPermisosxPerfil;

        public DatPermiso()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            savePermiso = "sva_cgu_save_permiso";
            ObterPermisosxPerfil = "svc_cgu_system";
        }
        public IMDResponse<bool> DSavePermiso(EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSavePermiso);
            logger.Info(IMDSerialize.Serialize(67823458345909, $"Inicia {metodo}(EntPermiso entPermiso)", entPermiso));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(savePermiso))
                {
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, entPermiso.iIdPerfil);
                    database.AddInParameter(dbCommand, "piIdModulo", DbType.Int32, entPermiso.iIdModulo);
                    database.AddInParameter(dbCommand, "piIdSubmodulo", DbType.Int32, entPermiso.iIdSubModulo);
                    database.AddInParameter(dbCommand, "piIdButon", DbType.String, entPermiso.iIdBoton);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entPermiso.sNombre);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entPermiso.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entPermiso.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entPermiso.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458346686;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458346686, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DObtenerPermisosPorPerfil(int? iIdPerfil)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerPermisosPorPerfil);
            logger.Info(IMDSerialize.Serialize(67823458351348, $"Inicia {metodo}(int iIdPerfil)", iIdPerfil));

            try
            {

                using (DbCommand dbCommand = database.GetStoredProcCommand(ObterPermisosxPerfil))
                {
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, iIdPerfil);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458352125;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458352125, $"Error en {metodo}: {ex.Message}(int iIdPerfil)", ex, iIdPerfil, response));
            }
            return response;
        }
    }
}
