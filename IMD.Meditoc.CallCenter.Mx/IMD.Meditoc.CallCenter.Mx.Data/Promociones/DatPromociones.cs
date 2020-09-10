using IMD.Admin.Conekta.Entities;
using IMD.Admin.Conekta.Entities.Promotions;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Data
{
    public class DatPromociones
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatPromociones));
#if DEBUG
        private Database database;
        IMDCommonData imdCommonData;
        private string spSaveCupon;
        private string spGetCupones;
        private string spUnsubscribeCupon;
        private string spGetNewIdCupon;
        private string spGetCuponUsed;
        private string spGetCuponAutocomplete;
#else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Database database;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IMDCommonData imdCommonData;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spSaveCupon;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spGetCupones;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spUnsubscribeCupon;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spGetNewIdCupon;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spGetCuponUsed;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spGetCuponAutocomplete;
#endif
        public DatPromociones(string appToken, string appKey)
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxConekta";
            database = imdCommonData.DGetDatabase(FsConnectionString, appToken, appKey);

            switch (ConfigurationManager.ConnectionStrings[FsConnectionString].ProviderName)
            {
                case "System.Data.SqlClient":
                    spSaveCupon = "Orders.svaSaveConektaOrder";
                    spGetCupones = "Orders.svaSaveCustomerInfo";
                    spUnsubscribeCupon = "sve_unsubscribe_cupon";
                    spGetNewIdCupon = "";
                    spGetCuponUsed = "";
                    spGetCuponAutocomplete = "";
                    break;
                case "MySql.Data.MySqlClient":
                    spSaveCupon = "sva_save_cupon";
                    spGetCupones = "svc_get_cupones";
                    spUnsubscribeCupon = "sve_unsubscribe_cupon";
                    spGetNewIdCupon = "svc_get_new_id_cupon";
                    spGetCuponUsed = "svc_get_cupon_used";
                    spGetCuponAutocomplete = "svc_get_cupones_autocomplete";
                    break;
                default:
                    throw new Exception("No se ha especificado el motor de base de datos");
            }
        }

        /// <summary>
        /// Función: Guarda el cupón en la base
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="entCupon">Datos de cupón</param>
        /// <param name="piUsuario">Usuario que crea ek cupón</param>
        /// <returns></returns>
        public IMDResponse<bool> DSaveCupon(EntCupon entCupon, int? piUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveCupon);
            logger.Info(IMDSerialize.Serialize(67823458183516, $"Inicia {metodo}(EntCupon entCupon, int? piUsuario = null)", entCupon, piUsuario));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveCupon))
                {
                    database.AddInParameter(dbCommand, "piIdCupon", DbType.Int32, entCupon.fiIdCupon);
                    database.AddInParameter(dbCommand, "piIdCuponCategoria", DbType.Int32, entCupon.fiIdCuponCategoria);
                    database.AddInParameter(dbCommand, "psDescripcion", DbType.String, entCupon.fsDescripcion?.Trim());
                    database.AddInParameter(dbCommand, "psCodigo", DbType.String, entCupon.fsCodigo?.Trim());
                    database.AddInParameter(dbCommand, "pnMontoDescuento", DbType.Decimal, entCupon.fnMontoDescuento);
                    database.AddInParameter(dbCommand, "pnPorcentajeDescuento", DbType.Decimal, entCupon.fnPorcentajeDescuento);
                    database.AddInParameter(dbCommand, "piMesBono", DbType.Int32, entCupon.fiMesBono);
                    database.AddInParameter(dbCommand, "piTotalLanzamiento", DbType.Int32, entCupon.fiTotalLanzamiento);
                    database.AddInParameter(dbCommand, "pdtFechaVencimiento", DbType.DateTime, entCupon.fdtFechaVencimiento);
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, piUsuario);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458184293;
                response.Message = "Ocurrió un error al intentar guardar el cupon";

                logger.Error(IMDSerialize.Serialize(67823458184293, $"Error en {metodo}(EntCupon entCupon, int? piUsuario = null): {ex.Message}", entCupon, piUsuario, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Obtiene los cupones de la base
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piIdCupon">Id de cupón</param>
        /// <param name="piIdCuponCategoria">Categoría del cupón</param>
        /// <param name="psDescripcion">Descripción del cupón</param>
        /// <param name="psCodigo">Código de cupón</param>
        /// <param name="pdtFechaVencimientoInicio">Fecha de vencimiento desde...</param>
        /// <param name="pdtFechaVencimientoFin">..a la Fecha de vencimiento</param>
        /// <param name="pdtFechaCreacionInicio">Fecha de creación desde...</param>
        /// <param name="pdtFechaCreacionFin">...a la fecha de creación</param>
        /// <param name="pbActivo">Cupón activo</param>
        /// <param name="pbBaja">Cupón inactivo</param>
        /// <returns></returns>
        public IMDResponse<DataTable> DGetCupones(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool? pbActivo = true, bool? pbBaja = false)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetCupones);
            logger.Info(IMDSerialize.Serialize(67823458185070, $"Inicia {metodo}(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool pbActivo = true, bool pbBaja = false)", piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetCupones))
                {
                    database.AddInParameter(dbCommand, "piIdCupon", DbType.Int32, piIdCupon);
                    database.AddInParameter(dbCommand, "piIdCuponCategoria", DbType.Int32, piIdCuponCategoria);
                    database.AddInParameter(dbCommand, "psDescripcion", DbType.String, psDescripcion?.Trim());
                    database.AddInParameter(dbCommand, "psCodigo", DbType.String, psCodigo?.Trim());
                    database.AddInParameter(dbCommand, "pdtFechaVencimientoInicio", DbType.DateTime, pdtFechaVencimientoInicio);
                    database.AddInParameter(dbCommand, "pdtFechaVencimientoFin", DbType.DateTime, pdtFechaVencimientoFin);
                    database.AddInParameter(dbCommand, "pdtFechaCreacionInicio", DbType.DateTime, pdtFechaCreacionInicio);
                    database.AddInParameter(dbCommand, "pdtFechaCreacionFin", DbType.DateTime, pdtFechaCreacionFin);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, pbActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, pbBaja);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458185847;
                response.Message = "Ocurrió un error al obtener los cupones";

                logger.Error(IMDSerialize.Serialize(67823458185847, $"Error en {metodo}: {ex.Message}(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool pbActivo = true, bool pbBaja = false)", piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Da de baja un cupón
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piIdCupon">ID de cupón</param>
        /// <param name="piIdUsuario">Usuario que da de baja</param>
        /// <returns></returns>
        public IMDResponse<bool> DUnsuscribeCupon(int piIdCupon, int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DUnsuscribeCupon);
            logger.Info(IMDSerialize.Serialize(67823458186624, $"Inicia {metodo}(int piIdCupon, int? piIdUsuario = null)", piIdCupon, piIdUsuario));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spUnsubscribeCupon))
                {
                    database.AddInParameter(dbCommand, "piIdCupon", DbType.Int32, piIdCupon);
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.Int32, piIdUsuario);

                    response = imdCommonData.DExecute(database, dbCommand);
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458187401;
                response.Message = "Ocurrió un error al desactivar el cupon";

                logger.Error(IMDSerialize.Serialize(67823458187401, $"Error en {metodo}: {ex.Message}(int piIdCupon, int? piIdUsuario = null)", piIdCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Genera un nuevo ID de cupón
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <returns></returns>
        public IMDResponse<DataTable> DGetNewCouponID()
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetNewCouponID);
            logger.Info(IMDSerialize.Serialize(67823458209934, $"Inicia {metodo}()"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetNewIdCupon))
                {
                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458210711;
                response.Message = "Ocurrió un error inesperado al generar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458210711, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Valida si un usuario ya aplico el cupón antes
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piIdCupon">ID de cupón</param>
        /// <param name="psEmail">Correo del cliente que compra</param>
        /// <returns></returns>
        public IMDResponse<DataTable> DGetCuponUsed(int piIdCupon, string psEmail)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetCuponUsed);
            logger.Info(IMDSerialize.Serialize(67823458213042, $"Inicia {metodo}(int piIdCupon, string psEmail)", piIdCupon, psEmail));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetCuponUsed))
                {
                    database.AddInParameter(dbCommand, "piIdCupon", DbType.Int32, piIdCupon);
                    database.AddInParameter(dbCommand, "psEmail", DbType.String, psEmail?.Trim());

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458213819;
                response.Message = "Ocurrió un error inesperado al validar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458213819, $"Error en {metodo}(int piIdCupon, string psEmail): {ex.Message}", piIdCupon, psEmail, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Obtiene solo los nombres de cupones para autocompletado
        /// Creado: Cristopher Noh 14/08/2020
        /// Modificado:
        /// </summary>
        /// <returns></returns>
        public IMDResponse<DataTable> DGetCuponAutocomplete()
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetCuponAutocomplete);
            logger.Info(IMDSerialize.Serialize(67823458234798, $"Inicia {metodo}()"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetCuponAutocomplete))
                {
                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458235575;
                response.Message = "Ocurrió un error inesperado al consultar los códigos";

                logger.Error(IMDSerialize.Serialize(67823458235575, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}