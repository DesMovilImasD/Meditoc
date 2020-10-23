using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace IMD.Meditoc.CallCenter.Mx.Data.Folio
{
    public class DatFolio
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatFolio));
        public Database database;
        IMDCommonData imdCommonData;
        string spSaveFolio;
        string spGetloginApp;
        string spGetFolios;
        string spUpdFechaVencimiento;
        string spDelFolioEmpresa;
        string spUpdTerminosYCondiciones;
        string spUpdPassword;
        string spSaveFolioVC;
        string spSaveFolioRequest;
        string spGetFolioRequest;

        public DatFolio()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spSaveFolio = "sva_meditoc_save_folio";
            spGetloginApp = "svc_app_login";
            spGetFolios = "svc_meditoc_folios";
            spUpdFechaVencimiento = "sva_meditoc_upd_foliovigencia";
            spDelFolioEmpresa = "sva_meditoc_del_folioempresa";
            spUpdTerminosYCondiciones = "svc_meditoc_upd_terminosyCondiciones";
            spUpdPassword = "svc_meditoc_upd_updPassword";
            spSaveFolioVC = "sva_meditoc_save_foliovc";
            spSaveFolioRequest = "sva_meditoc_save_folio_request";
            spGetFolioRequest = "svc_meditoc_folio_request";
        }


        public IMDResponse<DataTable> DSaveFolio(EntFolio entFolio)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSaveFolio);
            logger.Info(IMDSerialize.Serialize(67823458418170, $"Inicia {metodo}(EntFolio entFolio)", entFolio));

            try
            {

                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveFolio))
                {
                    database.AddInParameter(dbCommand, "piIdEmpresa", DbType.Int32, entFolio.iIdEmpresa);
                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, entFolio.iIdProducto);
                    database.AddInParameter(dbCommand, "piConsecutivo", DbType.Int32, entFolio.iConsecutivo);
                    database.AddInParameter(dbCommand, "piIdOrigen", DbType.Int32, entFolio.iIdOrigen);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, entFolio.sPassword);
                    database.AddInParameter(dbCommand, "pbConfirmado", DbType.Boolean, entFolio.bConfirmado);
                    database.AddInParameter(dbCommand, "psOrdenConekta", DbType.String, entFolio.sOrdenConekta);
                    database.AddInParameter(dbCommand, "pdtFechaVencimiento", DbType.DateTime, entFolio.dtFechaVencimiento);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entFolio.iIdUsuarioMod);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458418947;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el folio.";

                logger.Error(IMDSerialize.Serialize(67823458418947, $"Error en {metodo}(EntFolio entFolio): {ex.Message}", entFolio, ex, response));
            }
            return response;
        }


        public IMDResponse<DataTable> DLoginApp(string sUsuario, string sPassword)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DLoginApp);
            logger.Info(IMDSerialize.Serialize(67823458428271, $"Inicia {metodo}(string sUsuario, string sPassword)", sUsuario, sPassword));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetloginApp))
                {
                    database.AddInParameter(dbCommand, "psUsuario", DbType.String, sUsuario);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);
                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {

                response.Code = 67823458429048;
                response.Message = "Ocurrió un error inesperado en la base de datos al validar los datos de usuario.";

                logger.Error(IMDSerialize.Serialize(67823458429048, $"Error en {metodo}(string sUsuario, string sPassword): {ex.Message}", sUsuario, sPassword, ex, response));
            }
            return response;
        }


        public IMDResponse<DataTable> DGetFolios(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto = null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false, bool? pbVigente = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetFolios);
            logger.Info(IMDSerialize.Serialize(67823458432933, $"Inicia {metodo}(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto= null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false)", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetFolios))
                {
                    database.AddInParameter(dbCommand, "piIdFolio", DbType.Int32, piIdFolio);
                    database.AddInParameter(dbCommand, "piIdEmpresa", DbType.Int32, piIdEmpresa);
                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, piIdProducto);
                    database.AddInParameter(dbCommand, "piIdOrigen", DbType.Int32, piIdOrigen);
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, psFolio);
                    database.AddInParameter(dbCommand, "psOrdenConekta", DbType.String, psOrdenConekta);
                    database.AddInParameter(dbCommand, "pbTerminosYCondiciones", DbType.Boolean, pbTerminosYCondiciones);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, pbActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, pbBaja);
                    database.AddInParameter(dbCommand, "pbVigente", DbType.Boolean, pbVigente);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {

                response.Code = 67823458433710;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar los folios.";

                logger.Error(IMDSerialize.Serialize(67823458433710, $"Error en {metodo}(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto= null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false): {ex.Message}", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DUpdFechaVencimiento(int piIdEmpresa, int piIdFolio, DateTime pdtFechaVencimiento, int piIdUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DUpdFechaVencimiento);
            logger.Info(IMDSerialize.Serialize(67823458437595, $"Inicia {metodo}(int piIdEmpresa, int piIdFolio, DateTime pdtFechaVencimiento, int piIdUsuario)", piIdEmpresa, piIdFolio, pdtFechaVencimiento, piIdUsuario));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spUpdFechaVencimiento))
                {
                    database.AddInParameter(dbCommand, "piIdFolio", DbType.Int32, piIdFolio);
                    database.AddInParameter(dbCommand, "piIdEmpresa", DbType.Int32, piIdEmpresa);
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, piIdUsuario);
                    database.AddInParameter(dbCommand, "pdtFechaVencimiento", DbType.DateTime, pdtFechaVencimiento);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458438372;
                response.Message = "Ocurrió un error inesperado en la base de datos al actualizar la vigencia de los folios.";

                logger.Error(IMDSerialize.Serialize(67823458438372, $"Error en {metodo}(int piIdEmpresa, int piIdFolio, DateTime pdtFechaVencimiento, int piIdUsuario): {ex.Message}", piIdEmpresa, piIdFolio, pdtFechaVencimiento, piIdUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DEliminarFoliosEmpresa(int piIdEmpresa, int piIdFolio, int piIdUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DEliminarFoliosEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458442257, $"Inicia {metodo}(int piIdEmpresa, int piIdFolio, int piIdUsuario)", piIdEmpresa, piIdFolio, piIdUsuario));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spDelFolioEmpresa))
                {
                    database.AddInParameter(dbCommand, "piIdFolio", DbType.Int32, piIdFolio);
                    database.AddInParameter(dbCommand, "piIdEmpresa", DbType.Int32, piIdEmpresa);
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, piIdUsuario);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458443034;
                response.Message = "Ocurrió un error inesperado en la base de datos al eliminar los folios solicitados.";

                logger.Error(IMDSerialize.Serialize(67823458443034, $"Error en {metodo}(int piIdEmpresa, int piIdFolio, int piIdUsuario): {ex.Message}", piIdEmpresa, piIdFolio, piIdUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DTerminosYCondiciones(string sFolio = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DTerminosYCondiciones);
            logger.Info(IMDSerialize.Serialize(67823458460905, $"Inicia {metodo}(string sFolio = null)", sFolio));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spUpdTerminosYCondiciones))
                {
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, sFolio);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458461682;
                response.Message = "Ocurrió un error inesperado al aceptar los términos y condiciones de la cuenta.";

                logger.Error(IMDSerialize.Serialize(67823458461682, $"Error en {metodo}(string sFolio = null): {ex.Message}", sFolio, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DUpdPassword(string sFolio = null, string sPassword = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DUpdPassword);
            logger.Info(IMDSerialize.Serialize(67823458502863, $"Inicia {metodo}(string sFolio = null, string sPassword = null)", sFolio, sPassword));

            try
            {

                using (DbCommand dbCommand = database.GetStoredProcCommand(spUpdPassword))
                {
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, sFolio);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);

                    response = imdCommonData.DExecute(database, dbCommand);
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458503640;
                response.Message = "Ocurrió un error inesperado en la base de datos al actualizar la contraseña de la cuenta.";

                logger.Error(IMDSerialize.Serialize(67823458503640, $"Error en {metodo}(string sFolio = null, string sPassword = null): {ex.Message}", sFolio, sPassword, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DSaveFolioVC(int piIdEmpresa, int piIdProducto, int piIdOrigen, string psFolio, string psPassword, int piIdUsuarioMod, DateTime? pdtFechaVencimiento = null, string psOrdenConekta = null, bool? pbConfirmado = false)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSaveFolioVC);
            logger.Info(IMDSerialize.Serialize(67823458603873, $"Inicia {metodo}(int piIdEmpresa, int piIdProducto, int piIdOrigen, string psFolio, string psPassword, int piIdUsuarioMod)", piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psPassword, piIdUsuarioMod));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveFolioVC))
                {
                    database.AddInParameter(dbCommand, "piIdEmpresa", DbType.Int32, piIdEmpresa);
                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, piIdProducto);
                    database.AddInParameter(dbCommand, "piIdOrigen", DbType.Int32, piIdOrigen);
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, psFolio);
                    database.AddInParameter(dbCommand, "psOrdenConekta", DbType.String, psOrdenConekta);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, psPassword);
                    database.AddInParameter(dbCommand, "pbConfirmado", DbType.Boolean, pbConfirmado);
                    database.AddInParameter(dbCommand, "pdtFechaVencimiento", DbType.DateTime, pdtFechaVencimiento);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458604650;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el folio de venta calle.";

                logger.Error(IMDSerialize.Serialize(67823458604650, $"Error en {metodo}(int piIdEmpresa, int piIdProducto, int piIdOrigen, string psFolio, string psPassword, int piIdUsuarioMod): {ex.Message}", piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psPassword, piIdUsuarioMod, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DSaveFolioRequest(int piIdRequest, string psNumberPhone = null, string psFolio = null, string psPassword = null, DateTime? pdtFechaVencimiento = null, int? piIdOrigen = null, int? piIdProducto = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveFolioRequest);
            logger.Info(IMDSerialize.Serialize(67823458664479, $"Inicia {metodo}(int piIdRequest, string psNumberPhone = null, string psFolio = null, string psPassword = null, DateTime? pdtFechaVencimiento = null)", piIdRequest, psNumberPhone, psFolio, psPassword, pdtFechaVencimiento));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveFolioRequest))
                {
                    database.AddInParameter(dbCommand, "piIdRequest", DbType.Int32, piIdRequest);
                    database.AddInParameter(dbCommand, "piIdOrigen", DbType.Int32, piIdOrigen);
                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, piIdProducto);
                    database.AddInParameter(dbCommand, "psNumberPhone", DbType.String, psNumberPhone?.Trim());
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, psFolio?.Trim());
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, psPassword?.Trim());
                    database.AddInParameter(dbCommand, "pdtFechaVencimiento", DbType.DateTime, pdtFechaVencimiento);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458665256;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar la petición de folio.";

                logger.Error(IMDSerialize.Serialize(67823458665256, $"Error en {metodo}(int piIdRequest, string psNumberPhone = null, string psFolio = null, string psPassword = null, DateTime? pdtFechaVencimiento = null): {ex.Message}", piIdRequest, psNumberPhone, psFolio, psPassword, pdtFechaVencimiento, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetFolioRequest(string psNumberPhone, int piIdOrigen, int piIdProducto, int piMinutos)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetFolioRequest);
            logger.Info(IMDSerialize.Serialize(67823458666033, $"Inicia {metodo}(string psNumberPhone, string piMinutos)", psNumberPhone, piMinutos));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetFolioRequest))
                {
                    database.AddInParameter(dbCommand, "piMinutos", DbType.Int32, piMinutos);
                    database.AddInParameter(dbCommand, "piIdOrigen", DbType.Int32, piIdOrigen);
                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, piIdProducto);
                    database.AddInParameter(dbCommand, "psNumberPhone", DbType.String, psNumberPhone?.Trim());

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458666810;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar las peticiones de folio.";

                logger.Error(IMDSerialize.Serialize(67823458666810, $"Error en {metodo}(string psNumberPhone, string piMinutos): {ex.Message}", psNumberPhone, piMinutos, ex, response));
            }
            return response;
        }
    }
}
