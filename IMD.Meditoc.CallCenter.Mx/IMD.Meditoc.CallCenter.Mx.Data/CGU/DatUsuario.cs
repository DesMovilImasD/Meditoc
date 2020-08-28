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
    public class DatUsuario
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatModulo));
        private Database database;
        IMDCommonData imdCommonData;
        private string saveUsuario;
        private string getUsuario;
        private string cambioContrasenia;
        private string getLogin;

        public DatUsuario()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveUsuario = "sva_cgu_save_usuario";
            getUsuario = "svc_cgu_usuarios";
            cambioContrasenia = "sva_cgu_CambiarContrasenia";
            getLogin = "svc_cgu_login";
        }

        public IMDResponse<bool> DSaveUsuario(EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveUsuario);
            logger.Info(IMDSerialize.Serialize(67823458342801, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveUsuario))
                {
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, entUsuario.iIdUsuario);
                    database.AddInParameter(dbCommand, "piIdTipoCuenta", DbType.Int32, entUsuario.iIdTipoCuenta);
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, entUsuario.iIdPerfil);
                    database.AddInParameter(dbCommand, "psUsuario", DbType.String, entUsuario.sUsuario);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, entUsuario.sPassword);
                    database.AddInParameter(dbCommand, "psNombres", DbType.String, entUsuario.sNombres);
                    database.AddInParameter(dbCommand, "psApellidoPaterno", DbType.String, entUsuario.sApellidoPaterno);
                    database.AddInParameter(dbCommand, "psApellidoMaterno", DbType.String, entUsuario.sApellidoMaterno);
                    database.AddInParameter(dbCommand, "pdtFechaNacimiento", DbType.DateTime, entUsuario.dtFechaNacimiento);
                    database.AddInParameter(dbCommand, "psTelefono", DbType.String, entUsuario.sTelefono);
                    database.AddInParameter(dbCommand, "psCorreo", DbType.String, entUsuario.sCorreo);
                    database.AddInParameter(dbCommand, "psDomicilio", DbType.String, entUsuario.sDomicilio);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entUsuario.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entUsuario.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entUsuario.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458343578;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458343578, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DObtenerUsuario(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool? bActivo, bool? bBaja)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerUsuario);
            logger.Info(IMDSerialize.Serialize(67823458360672, $"Inicia {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja)", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getUsuario))
                {
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, iIdUsuario);
                    database.AddInParameter(dbCommand, "piIdTipoCuenta", DbType.Int32, iIdTipoCuenta);
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, iIdPerfil);
                    database.AddInParameter(dbCommand, "psUsuario", DbType.String, sUsuario);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, bBaja);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458361449;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458361449, $"Error en {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja): {ex.Message}", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DCambiarContrasenia(int iIdUsuario, string sPassword, int iIdUsuarioUltMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DCambiarContrasenia);
            logger.Info(IMDSerialize.Serialize(67823458365334, $"Inicia {metodo}(int iIdUsuario, string sPassword)", iIdUsuario, sPassword));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(cambioContrasenia))
                {
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, iIdUsuario);
                    database.AddInParameter(dbCommand, "piIdUsuarioUltMod", DbType.Int32, iIdUsuarioUltMod);
                    database.AddInParameter(dbCommand, "psPassword", DbType.Int32, sPassword);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458366111;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458366111, $"Error en {metodo}(int iIdUsuario, string sPassword): {ex.Message}", iIdUsuario, sPassword, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DLogin(string sUsuario, string sPassword)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DLogin);
            logger.Info(IMDSerialize.Serialize(67823458373104, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getLogin))
                {
                    database.AddInParameter(dbCommand, "psUsuario", DbType.String, sUsuario);                    
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458373881;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458373881, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
