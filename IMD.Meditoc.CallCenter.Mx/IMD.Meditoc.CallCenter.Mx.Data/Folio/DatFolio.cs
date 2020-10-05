using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.Folio
{
    public class DatFolio
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatFolio));
        public Database database;
        IMDCommonData imdCommonData;
        string saveFolio;
        string loginApp;
        string getFolios;
        string updFechaVencimiento;
        string delFolioEmpresa;
        string updTerminosYCondiciones;
        string updPassword;
        string saveFolioVC;

        public DatFolio()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveFolio = "sva_meditoc_save_folio";
            loginApp = "svc_app_login";
            getFolios = "svc_meditoc_folios";
            updFechaVencimiento = "sva_meditoc_upd_foliovigencia";
            delFolioEmpresa = "sva_meditoc_del_folioempresa";
            updTerminosYCondiciones = "svc_meditoc_upd_terminosyCondiciones";
            updPassword = "svc_meditoc_upd_updPassword";
            saveFolioVC = "sva_meditoc_save_foliovc";

        }


        public IMDResponse<DataTable> DSaveFolio(EntFolio entFolio)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSaveFolio);
            logger.Info(IMDSerialize.Serialize(67823458418170, $"Inicia {metodo}(EntFolio entFolio)", entFolio));

            try
            {

                using (DbCommand dbCommand = database.GetStoredProcCommand(saveFolio))
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
                using (DbCommand dbCommand = database.GetStoredProcCommand(loginApp))
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


        public IMDResponse<DataTable> DGetFolios(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto = null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetFolios);
            logger.Info(IMDSerialize.Serialize(67823458432933, $"Inicia {metodo}(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto= null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false)", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getFolios))
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
                using (DbCommand dbCommand = database.GetStoredProcCommand(updFechaVencimiento))
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
                using (DbCommand dbCommand = database.GetStoredProcCommand(delFolioEmpresa))
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
                using (DbCommand dbCommand = database.GetStoredProcCommand(updTerminosYCondiciones))
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

                using (DbCommand dbCommand = database.GetStoredProcCommand(updPassword))
                {
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, sFolio);
                    database.AddInParameter(dbCommand, "psPassword", DbType.String, sPassword);

                    response = imdCommonData.DExecute(database, dbCommand);
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458503640;
                response.Message = "Ocurrió un error inesperado en la base de datos al actualizar la contraseña de la cuenta";

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
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveFolioVC))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el folio de venta calle";

                logger.Error(IMDSerialize.Serialize(67823458604650, $"Error en {metodo}(int piIdEmpresa, int piIdProducto, int piIdOrigen, string psFolio, string psPassword, int piIdUsuarioMod): {ex.Message}", piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psPassword, piIdUsuarioMod, ex, response));
            }
            return response;
        }
    }
}
