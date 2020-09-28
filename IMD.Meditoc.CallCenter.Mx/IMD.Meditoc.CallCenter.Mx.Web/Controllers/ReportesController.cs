using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Reportes;
using IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Doctores;
using IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    //[MeditocAuthentication]
    public class ReportesController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ReportesController));

        #region Ventas
        [HttpGet]
        [Route("api/reportes/ventas")]
        public IMDResponse<EntVentasReporte> CObtenerReporteVentas([FromUri]string psFolio = null,
            [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null,
            [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null,
            [FromUri]string psOrderId = null, [FromUri]string psStatus = null, [FromUri]string psCupon = null, [FromUri]string psTipoPago = null,
            [FromUri]DateTime? pdtFechaInicio = null, [FromUri]DateTime? pdtFechaFinal = null,
            [FromUri]DateTime? pdtFechaVencimiento = null)
        {
            IMDResponse<EntVentasReporte> response = new IMDResponse<EntVentasReporte>();

            string metodo = nameof(this.CObtenerReporteVentas);
            logger.Info(IMDSerialize.Serialize(67823458571239, $"Inicia {metodo}([FromUri]string psFolio = null, [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null, [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null, [FromUri]string psOrderId = null, [FromUri]string psStatus = null,[FromUri]string psCupon = null, [FromUri]DateTime ? pdtFechaInicio = null, [FromUri]DateTime ? pdtFechaFinal = null, [FromUri]DateTime ? pdtFechaVencimiento = null)", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {
                BusReportes busReportes = new BusReportes();
                response = busReportes.BObtenerReporteVentas(psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento);
            }
            catch (Exception ex)
            {
                response.Code = 67823458572016;
                response.Message = "Ocurrió un error inesperado al consultar los folios del reporte en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458572016, $"Error en {metodo}([FromUri]string psFolio = null, [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null, [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null, [FromUri]string psOrderId = null, [FromUri]string psStatus = null,[FromUri]string psCupon = null, [FromUri]DateTime ? pdtFechaInicio = null, [FromUri]DateTime ? pdtFechaFinal = null, [FromUri]DateTime ? pdtFechaVencimiento = null): {ex.Message}", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("api/reportes/ventas/meditoc")]
        public IMDResponse<EntReporteVenta> CReporteGlobalVentas([FromUri]string psFolio = null,
            [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null,
            [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null,
            [FromUri]string psOrderId = null, [FromUri]string psStatus = null, [FromUri]string psCupon = null, [FromUri]string psTipoPago = null,
            [FromUri]DateTime? pdtFechaInicio = null, [FromUri]DateTime? pdtFechaFinal = null,
            [FromUri]DateTime? pdtFechaVencimiento = null)
        {
            IMDResponse<EntReporteVenta> response = new IMDResponse<EntReporteVenta>();

            string metodo = nameof(this.CReporteGlobalVentas);
            logger.Info(IMDSerialize.Serialize(67823458599211, $"Inicia {metodo}([FromUri]string psFolio = null, [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null, [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null, [FromUri]string psOrderId = null, [FromUri]string psStatus = null,[FromUri]string psCupon = null, [FromUri]DateTime ? pdtFechaInicio = null, [FromUri]DateTime ? pdtFechaFinal = null, [FromUri]DateTime ? pdtFechaVencimiento = null)", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {
                BusReportes busReportes = new BusReportes();
                response = busReportes.BReporteGlobalVentas(psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento);
            }
            catch (Exception ex)
            {
                response.Code = 67823458599988;
                response.Message = "Ocurrió un error inesperado al consultar los folios del reporte en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458599988, $"Error en {metodo}([FromUri]string psFolio = null, [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null, [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null, [FromUri]string psOrderId = null, [FromUri]string psStatus = null,[FromUri]string psCupon = null, [FromUri]DateTime ? pdtFechaInicio = null, [FromUri]DateTime ? pdtFechaFinal = null, [FromUri]DateTime ? pdtFechaVencimiento = null): {ex.Message}", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("api/reportes/ventas/descargar")]
        public HttpResponseMessage CDescargarReporteVentas([FromUri]string psFolio = null,
            [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null,
            [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null,
            [FromUri]string psOrderId = null, [FromUri]string psStatus = null, [FromUri]string psCupon = null, [FromUri]string psTipoPago = null,
            [FromUri]DateTime? pdtFechaInicio = null, [FromUri]DateTime? pdtFechaFinal = null,
            [FromUri]DateTime? pdtFechaVencimiento = null)
        {
            HttpResponseMessage response;

            string metodo = nameof(this.CDescargarReporteVentas);
            logger.Info(IMDSerialize.Serialize(67823458572793, $"Inicia {metodo}([FromUri]string psFolio = null, [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null, [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null, [FromUri]string psOrderId = null, [FromUri]string psStatus = null,[FromUri]string psCupon = null, [FromUri]DateTime ? pdtFechaInicio = null, [FromUri]DateTime ? pdtFechaFinal = null, [FromUri]DateTime ? pdtFechaVencimiento = null)", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {
                BusReportes busReportes = new BusReportes();
                IMDResponse<MemoryStream> resExcel = busReportes.BDescargarReporteVentas(psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento);
                if (resExcel.Code != 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, resExcel.Message);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StreamContent(resExcel.Result);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "ReporteVentas.xlsx";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "No se pudo generar el reporte de ventas");

                logger.Error(IMDSerialize.Serialize(67823458573570, $"Error en {metodo}([FromUri]string psFolio = null, [FromUri]string psIdEmpresa = null, [FromUri]string psIdProducto = null, [FromUri]string psIdTipoProducto = null, [FromUri]string psIdOrigen = null, [FromUri]string psOrderId = null, [FromUri]string psStatus = null,[FromUri]string psCupon = null, [FromUri]DateTime ? pdtFechaInicio = null, [FromUri]DateTime ? pdtFechaFinal = null, [FromUri]DateTime ? pdtFechaVencimiento = null): {ex.Message}", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }
        #endregion

        #region Doctores
        [HttpGet]
        [Route("api/reportes/doctores")]
        public IMDResponse<EntDoctoresReporte> CObtenerReporteDoctores(
            [FromUri]string psIdColaborador = null, [FromUri]string psColaborador = null, [FromUri]string psIdTipoDoctor = null, [FromUri]string psIdEspecialidad = null,
            [FromUri]string psIdConsulta = null, [FromUri]string psIdEstatusConsulta = null, [FromUri]string psRFC = null, [FromUri]string psNumSala = null,
            [FromUri]DateTime? pdtFechaProgramadaInicio = null, [FromUri]DateTime? pdtFechaProgramadaFinal = null, [FromUri]DateTime? pdtFechaConsultaInicio = null, [FromUri]DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<EntDoctoresReporte> response = new IMDResponse<EntDoctoresReporte>();

            string metodo = nameof(this.CObtenerReporteDoctores);
            logger.Info(IMDSerialize.Serialize(67823458574347, $"Inicia {metodo}([FromUri]string psIdColaborador = null, [FromUri]string psColaborador = null, [FromUri]string psIdTipoDoctor = null, [FromUri]string psIdEspecialidad = null, [FromUri]string psIdConsulta = null, [FromUri]string psIdEstatusConsulta = null, [FromUri]string psRFC = null, [FromUri]string psNumSala = null, [FromUri]DateTime ? pdtFechaProgramadaInicio = null, [FromUri]DateTime ? pdtFechaProgramadaFinal = null, [FromUri]DateTime ? pdtFechaConsultaInicio = null, [FromUri]DateTime ? pdtFechaConsultaFin = null)", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                BusReportes busReportes = new BusReportes();
                response = busReportes.BObtenerReporteDoctores(psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin);
            }
            catch (Exception ex)
            {
                response.Code = 67823458575124;
                response.Message = "Ocurrió un error inesperado al consultar la información de doctores para el reporte en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458575124, $"Error en {metodo}([FromUri]string psIdColaborador = null, [FromUri]string psColaborador = null, [FromUri]string psIdTipoDoctor = null, [FromUri]string psIdEspecialidad = null, [FromUri]string psIdConsulta = null, [FromUri]string psIdEstatusConsulta = null, [FromUri]string psRFC = null, [FromUri]string psNumSala = null, [FromUri]DateTime ? pdtFechaProgramadaInicio = null, [FromUri]DateTime ? pdtFechaProgramadaFinal = null, [FromUri]DateTime ? pdtFechaConsultaInicio = null, [FromUri]DateTime ? pdtFechaConsultaFin = null): {ex.Message}", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("api/reportes/doctores/descargar")]
        public HttpResponseMessage CDescargarReporteDoctores([FromUri]string psIdColaborador = null, [FromUri]string psColaborador = null, [FromUri]string psIdTipoDoctor = null, [FromUri]string psIdEspecialidad = null,
            [FromUri]string psIdConsulta = null, [FromUri]string psIdEstatusConsulta = null, [FromUri]string psRFC = null, [FromUri]string psNumSala = null,
            [FromUri]DateTime? pdtFechaProgramadaInicio = null, [FromUri]DateTime? pdtFechaProgramadaFinal = null, [FromUri]DateTime? pdtFechaConsultaInicio = null, [FromUri]DateTime? pdtFechaConsultaFin = null)
        {
            HttpResponseMessage response;

            string metodo = nameof(this.CDescargarReporteDoctores);
            logger.Info(IMDSerialize.Serialize(67823458575901, $"Inicia {metodo}([FromUri]string psIdColaborador = null, [FromUri]string psColaborador = null, [FromUri]string psIdTipoDoctor = null, [FromUri]string psIdEspecialidad = null, [FromUri]string psIdConsulta = null, [FromUri]string psIdEstatusConsulta = null, [FromUri]string psRFC = null, [FromUri]string psNumSala = null, [FromUri]DateTime ? pdtFechaProgramadaInicio = null, [FromUri]DateTime ? pdtFechaProgramadaFinal = null, [FromUri]DateTime ? pdtFechaConsultaInicio = null, [FromUri]DateTime ? pdtFechaConsultaFin = null)", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                BusReportes busReportes = new BusReportes();
                IMDResponse<MemoryStream> resExcel = busReportes.BDescargarReporteDoctores(psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin);
                if (resExcel.Code != 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, resExcel.Message);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StreamContent(resExcel.Result);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "ReporteDoctores.xlsx";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "No se pudo generar el reporte de doctores");

                logger.Error(IMDSerialize.Serialize(67823458576678, $"Error en {metodo}([FromUri]string psIdColaborador = null, [FromUri]string psColaborador = null, [FromUri]string psIdTipoDoctor = null, [FromUri]string psIdEspecialidad = null, [FromUri]string psIdConsulta = null, [FromUri]string psIdEstatusConsulta = null, [FromUri]string psRFC = null, [FromUri]string psNumSala = null, [FromUri]DateTime ? pdtFechaProgramadaInicio = null, [FromUri]DateTime ? pdtFechaProgramadaFinal = null, [FromUri]DateTime ? pdtFechaConsultaInicio = null, [FromUri]DateTime ? pdtFechaConsultaFin = null): {ex.Message}", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        #endregion
    }
}
