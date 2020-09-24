using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.Reportes
{
    public class DatReportes
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatReportes));
        private Database database;
        IMDCommonData imdCommonData;
        string spGetReporteVentas;
        string spGetReporteDoctores;

        public DatReportes() {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spGetReporteVentas = "svc_get_ventas_report";
            spGetReporteDoctores = "svc_get_historial_doctores_report";
        }

        #region Ventas

        /// <summary>
        /// Función: Obtiene los datos para reporte de ventas
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 15/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psFolio">Folio de la venta</param>
        /// <param name="psIdEmpresa">ID de la empresa</param>
        /// <param name="psIdProducto">ID del producto</param>
        /// <param name="psIdTipoProducto">ID del tipo de producto</param>
        /// <param name="psIdOrigen">ID del origen</param>
        /// <param name="psOrderId">Código de la orden</param>
        /// <param name="psStatus">Estatus del pago</param>
        /// <param name="psCupon">Cupon que se aplico</param>
        /// <param name="pdtFechaInicio">Fecha de creación de la orden desde...</param>
        /// <param name="pdtFechaFinal">...a la fecha de creación de la orden</param>
        /// <param name="pdtFechaVencimiento">Fecha de vencimiento del folio</param>
        /// <returns></returns>
        public IMDResponse<DataTable> DObtenerReporteVentas(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerReporteVentas);
            logger.Info(IMDSerialize.Serialize(67823458558807, $"Inicia {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetReporteVentas))
                {
                    database.AddInParameter(dbCommand, "psFolio", DbType.String, psFolio?.Trim());
                    database.AddInParameter(dbCommand, "psIdEmpresa", DbType.String, psIdEmpresa);
                    database.AddInParameter(dbCommand, "psIdProducto", DbType.String, psIdProducto);
                    database.AddInParameter(dbCommand, "psIdTipoProducto", DbType.String, psIdTipoProducto);
                    database.AddInParameter(dbCommand, "psIdOrigen", DbType.String, psIdOrigen);
                    database.AddInParameter(dbCommand, "psOrderId", DbType.String, psOrderId?.Trim());
                    database.AddInParameter(dbCommand, "psStatus", DbType.String, psStatus?.Trim());
                    database.AddInParameter(dbCommand, "psCupon", DbType.String, psCupon?.Trim());
                    database.AddInParameter(dbCommand, "pdtFechaInicio", DbType.DateTime, pdtFechaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaFinal", DbType.String, pdtFechaFinal);
                    database.AddInParameter(dbCommand, "pdtFechaVencimiento", DbType.String, pdtFechaVencimiento);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458559584;
                response.Message = "Ocurrió un error inesperado al consultar el detalle de los folios";

                logger.Error(IMDSerialize.Serialize(67823458559584, $"Error en {metodo}: {ex.Message}(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }
        #endregion

        #region Doctores
        /// <summary>
        /// Función: Obtiene los datos para reporte de doctores
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 17/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psIdColaborador">folio del doctor</param>
        /// <param name="psColaborador">nombre del doctor</param>
        /// <param name="psIdTipoDoctor">id tipo de doctor</param>
        /// <param name="psIdEspecialidad">id de especialidad</param>
        /// <param name="psIdConsulta">id de la consulta</param>
        /// <param name="psIdEstatusConsulta">estatus de la consulta</param>
        /// <param name="psRFC">rfc del doctor</param>
        /// <param name="psNumSala">numero de sala de la consulta</param>
        /// <param name="pdtFechaProgramadaInicio">fecha inicial programada de la consulta</param>
        /// <param name="pdtFechaProgramadaFinal">fecha final programada de la consulta</param>
        /// <param name="pdtFechaConsultaInicio">fecha inicial real de la consulta</param>
        /// <param name="pdtFechaConsultaFin">fecha final real de la consulta</param>
        /// <returns></returns>
        public IMDResponse<DataTable> DObtenerReporteDoctores(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,
            string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFinal = null,
            DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerReporteDoctores);
            logger.Info(IMDSerialize.Serialize(67823458560361, $"Inicia {metodo}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null)", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetReporteDoctores))
                {
                    database.AddInParameter(dbCommand, "psIdColaborador", DbType.String, psIdColaborador?.Trim());
                    database.AddInParameter(dbCommand, "psColaborador", DbType.String, psColaborador?.Trim());
                    database.AddInParameter(dbCommand, "psIdTipoDoctor", DbType.String, psIdTipoDoctor?.Trim());
                    database.AddInParameter(dbCommand, "psIdEspecialidad", DbType.String, psIdEspecialidad?.Trim());
                    database.AddInParameter(dbCommand, "psIdConsulta", DbType.String, psIdConsulta?.Trim());
                    database.AddInParameter(dbCommand, "psIdEstatusConsulta", DbType.String, psIdEstatusConsulta?.Trim());
                    database.AddInParameter(dbCommand, "psRFC", DbType.String, psRFC?.Trim());
                    database.AddInParameter(dbCommand, "psNumSala", DbType.String, psNumSala?.Trim());
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaInicio", DbType.DateTime, pdtFechaProgramadaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaFinal", DbType.DateTime, pdtFechaProgramadaFinal);
                    database.AddInParameter(dbCommand, "pdtFechaConsultaInicio", DbType.DateTime, pdtFechaConsultaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaConsultaFin", DbType.DateTime, pdtFechaConsultaFin);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458561138;
                response.Message = "Ocurrió un error inesperado al consultar el detalle de las consultas de doctores";

                logger.Error(IMDSerialize.Serialize(67823458561138, $"Error en {metodo}: {ex.Message}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null)", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }
        #endregion
    }
}
