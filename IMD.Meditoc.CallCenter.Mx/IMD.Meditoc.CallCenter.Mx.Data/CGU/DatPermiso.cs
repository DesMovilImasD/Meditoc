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
    public class DatPermiso
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatPermiso));
        private Database database;
        IMDCommonData imdCommonData;
        private string spSavePermiso;
        private string spGetPermisosPerfil;

        public DatPermiso()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spSavePermiso = "sva_cgu_save_permiso";
            spGetPermisosPerfil = "svc_cgu_system";
        }
        public IMDResponse<bool> DSavePermiso(EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSavePermiso);
            logger.Info(IMDSerialize.Serialize(67823458345909, $"Inicia {metodo}(EntPermiso entPermiso)", entPermiso));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSavePermiso))
                {
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, entPermiso.iIdPerfil);
                    database.AddInParameter(dbCommand, "piIdModulo", DbType.Int32, entPermiso.iIdModulo);
                    database.AddInParameter(dbCommand, "piIdSubmodulo", DbType.Int32, entPermiso.iIdSubModulo);
                    database.AddInParameter(dbCommand, "piIdButon", DbType.String, entPermiso.iIdBoton);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entPermiso.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entPermiso.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entPermiso.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458346686;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el permiso.";

                logger.Error(IMDSerialize.Serialize(67823458346686, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
            }
            return response;
        }

        public IMDResponse<DataSet> DObtenerPermisosPorPerfil(int? iIdPerfil)
        {
            IMDResponse<DataSet> response = new IMDResponse<DataSet>();

            string metodo = nameof(this.DObtenerPermisosPorPerfil);
            logger.Info(IMDSerialize.Serialize(67823458351348, $"Inicia {metodo}(int iIdPerfil)", iIdPerfil));

            try
            {

                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetPermisosPerfil))
                {
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, iIdPerfil);

                    response = imdCommonData.DExecuteDS(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458352125;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar los permisos.";

                logger.Error(IMDSerialize.Serialize(67823458352125, $"Error en {metodo}: {ex.Message}(int iIdPerfil)", ex, iIdPerfil, response));
            }
            return response;
        }
    }
}
