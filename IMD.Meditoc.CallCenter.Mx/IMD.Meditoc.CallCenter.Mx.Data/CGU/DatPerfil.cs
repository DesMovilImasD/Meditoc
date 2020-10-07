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
    public class DatPerfil
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatPerfil));
        private Database database;
        IMDCommonData imdCommonData;
        private string spSavePerfil;
        private string spGetPerfil;

        public DatPerfil()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spSavePerfil = "sva_cgu_save_perfil";
            spGetPerfil = "svc_cgu_perfiles";
        }

        public IMDResponse<bool> DSavePerfil(EntPerfil entPerfil)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.spSavePerfil);
            logger.Info(IMDSerialize.Serialize(67823458340470, $"Inicia {metodo}(EntPerfil entPerfil)", entPerfil));
            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSavePerfil))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al intentar guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458332700, $"Error en {metodo}(EntPerfil entPerfil): {ex.Message}", entPerfil, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DObtenerPerfil(int? iIdPerfil, bool bActivo, bool bBaja)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerPerfil);
            logger.Info(IMDSerialize.Serialize(67823458356010, $"Inicia {metodo}(int? iIdPerfil, bool bActivo, bool bBaja)", iIdPerfil, bActivo, bBaja));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetPerfil))
                {
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, iIdPerfil);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, bBaja);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458356787;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar los perfiles.";

                logger.Error(IMDSerialize.Serialize(67823458356787, $"Error en {metodo}(int? iIdPerfil, bool bActivo, bool bBaja): {ex.Message}", iIdPerfil, bActivo, bBaja, ex, response));
            }
            return response;
        }
    }
}
