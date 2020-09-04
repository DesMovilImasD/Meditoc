using IMD.Admin.Conekta.Entities;
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
#else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Database database;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IMDCommonData imdCommonData;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spSaveCupon;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spGetCupones;||
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spUnsubscribeCupon;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string spGetNewIdCupon;
#endif
        public DatPromociones()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxConekta";
            database = imdCommonData.DGetDatabase(FsConnectionString, "hdiu4soi3IHD334F", "SKlru3nc");

            switch (ConfigurationManager.ConnectionStrings[FsConnectionString].ProviderName)
            {
                case "System.Data.SqlClient":
                    spSaveCupon = "Orders.svaSaveConektaOrder";
                    spGetCupones = "Orders.svaSaveCustomerInfo";
                    spUnsubscribeCupon = "sve_unsubscribe_cupon";
                    spGetNewIdCupon = "";
                    break;
                case "MySql.Data.MySqlClient":
                    spSaveCupon = "sva_save_cupon";
                    spGetCupones = "svc_get_cupones";
                    spUnsubscribeCupon = "sve_unsubscribe_cupon";
                    spGetNewIdCupon = "svc_get_new_id_cupon";
                    break;
                default:
                    throw new Exception("No se ha especificado el motor de base de datos");
            }
        }
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
                    database.AddInParameter(dbCommand, "psDescripcion", DbType.String, entCupon.fsDescripcion);
                    database.AddInParameter(dbCommand, "psCodigo", DbType.String, entCupon.fsCodigo);
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

        public IMDResponse<DataTable> DGetCupones(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool pbActivo = true, bool pbBaja = false)
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
                    database.AddInParameter(dbCommand, "psDescripcion", DbType.String, psDescripcion);
                    database.AddInParameter(dbCommand, "psCodigo", DbType.String, psCodigo);
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

        public IMDResponse<DataTable> CGetNewCouponID()
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.CGetNewCouponID);
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458210711, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}