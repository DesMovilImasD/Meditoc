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
        private string spSaveUsuario;
        private string spGetUsuario;
        private string spChangePassword;
        private string spGetLogin;
        private string spValidaUsuarioCorreo;

        public DatUsuario()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spSaveUsuario = "sva_cgu_save_usuario";
            spGetUsuario = "svc_cgu_usuarios";
            spChangePassword = "sva_cgu_CambiarContrasenia";
            spGetLogin = "svc_cgu_login";
            spValidaUsuarioCorreo = "svc_cgu_ValidaUsuarioCorreo";
        }

        public IMDResponse<DataTable> DSaveUsuario(EntUsuario entUsuario)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSaveUsuario);
            logger.Info(IMDSerialize.Serialize(67823458342801, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveUsuario))
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

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458343578;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el usuario.";

                logger.Error(IMDSerialize.Serialize(67823458343578, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DObtenerUsuario(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool? bActivo, bool? bBaja, string psCorreo = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerUsuario);
            logger.Info(IMDSerialize.Serialize(67823458360672, $"Inicia {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja)", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetUsuario))
                {
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, iIdUsuario);
                    database.AddInParameter(dbCommand, "piIdTipoCuenta", DbType.Int32, iIdTipoCuenta);
                    database.AddInParameter(dbCommand, "piIdPerfil", DbType.Int32, iIdPerfil);
                    database.AddInParameter(dbCommand, "psUsuario", DbType.String, sUsuario);
                    database.AddInParameter(dbCommand, "psCorreo", DbType.String, psCorreo);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, bBaja);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458361449;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar los usuarios.";

                logger.Error(IMDSerialize.Serialize(67823458361449, $"Error en {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja): {ex.Message}", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DCambiarContrasenia(int iIdUsuario, string sPassword, int iIdUsuarioUltMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DCambiarContrasenia);
            logger.Info(IMDSerialize.Serialize(67823458365334, $"Inicia {metodo}(int iIdUsuario, string sPassword, int iIdUsuarioUltMod)", iIdUsuario, sPassword, iIdUsuarioUltMod));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spChangePassword))
                {
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, iIdUsuario);
                    database.AddInParameter(dbCommand, "piIdUsuarioUltMod", DbType.Int32, iIdUsuarioUltMod);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458366111;
                response.Message = "Ocurrió un error inesperado en la base de datos al cambiar la contraseña.";

                logger.Error(IMDSerialize.Serialize(67823458366111, $"Error en {metodo}(int iIdUsuario, string sPassword, int iIdUsuarioUltMod): {ex.Message}", iIdUsuario, sPassword, iIdUsuarioUltMod, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DLogin(string sUsuario, string sPassword)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DLogin);
            logger.Info(IMDSerialize.Serialize(67823458373104, $"Inicia {metodo}(string sUsuario, string sPassword)", sUsuario, sPassword));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetLogin))
                {
                    database.AddInParameter(dbCommand, "psUsuario", DbType.String, sUsuario);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);

                    response = imdCommonData.DExecuteDT(database, dbCommand);

                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458373881;
                response.Message = "Ocurrió un error inesperado en la base de datos al iniciar la sesión.";

                logger.Error(IMDSerialize.Serialize(67823458373881, $"Error en {metodo}(string sUsuario, string sPassword): {ex.Message}", sUsuario, sPassword, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DValidaUsuarioYCorreo(string sUsuario, string sCorreo, bool bValida)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DValidaUsuarioYCorreo);
            logger.Info(IMDSerialize.Serialize(67823458377766, $"Inicia {metodo}(string sUsuario, string sCorreo, bool bValida)", sUsuario, sCorreo, bValida));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spValidaUsuarioCorreo))
                {
                    database.AddInParameter(dbCommand, "psUsuario", DbType.String, sUsuario);
                    database.AddInParameter(dbCommand, "psCorreo", DbType.String, sCorreo);
                    database.AddInParameter(dbCommand, "pbValida", DbType.Boolean, bValida);

                    IMDResponse<DataTable> dt = imdCommonData.DExecuteDT(database, dbCommand);

                    if (dt.Result.Rows.Count > 0)
                    {
                        response.Result = false;
                    }
                    else
                    {
                        response.Result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458378543;
                response.Message = "Ocurrió un error inesperado en la base de datos al validar los datos de usuario.";

                logger.Error(IMDSerialize.Serialize(67823458378543, $"Error en {metodo}(string sUsuario, string sCorreo, bool bValida): {ex.Message}", sUsuario, sCorreo, bValida, ex, response));
            }
            return response;
        }
    }
}
