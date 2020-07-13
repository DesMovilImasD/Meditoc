﻿using IMD.Admin.Utilities.Entities;
using IMD.Admin.Utilities.Business.Serialize;
using IMD.Meditoc.Pagos.Entities.Reporte;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IMD.Meditoc.Pagos.Business;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace IMD.Meditoc.Pagos.Web.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ReportesController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ReportesController));
        [DisableCors]
        [HttpGet]
        [Route("Api/Meditoc/Pagos/Reporte/Ordenes")]
        public IMDResponse<List<EntOrderReporte>> CObtenerReporteOrdenes([FromUri]string psStatus = null, [FromUri]string psType = null, [FromUri]DateTime? pdtFechaInicio = null, [FromUri]DateTime? pdtFechaFinal = null)
        {
            IMDResponse<List<EntOrderReporte>> response = new IMDResponse<List<EntOrderReporte>>();

            string metodo = nameof(this.CObtenerReporteOrdenes);
            logger.Info(IMDSerialize.Serialize(67823458164868, $"Inicia {metodo}([FromUri]string psStatus = null, [FromUri]string psType = null, [FromUri]DateTime? pdtFechaInicio = null, [FromUri]DateTime? pdtFechaFinal = null)", psStatus, psType, pdtFechaInicio, pdtFechaFinal));

            try
            {
                BusReportes busReportes = new BusReportes();
                response = busReportes.BObtenerReporteOrdenes(psStatus, psType, pdtFechaInicio, pdtFechaFinal);
            }
            catch (Exception ex)
            {
                response.Code = 67823458165645;
                response.Message = "Ocurrió un error inesperado al consultar las órdenes en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458165645, $"Error en {metodo}([FromUri]string psStatus = null, [FromUri]string psType = null, [FromUri]DateTime? pdtFechaInicio = null, [FromUri]DateTime? pdtFechaFinal = null): {ex.Message}", psStatus, psType, pdtFechaInicio, pdtFechaFinal, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/Meditoc/Pagos/Descargar/Reporte/Ordenes")]
        public HttpResponseMessage CObtenerDocumento([FromUri]string psStatus = null, [FromUri]string psType = null, [FromUri]DateTime? pdtFechaInicio = null, [FromUri]DateTime? pdtFechaFinal = null)
        {
            HttpResponseMessage response;

            string metodo = nameof(this.CObtenerDocumento);
            logger.Info(IMDSerialize.Serialize(67823458078621, $"Inicia {metodo}"));

            try
            {
                BusReportes busReportes = new BusReportes();
                IMDResponse<MemoryStream> resExcel = busReportes.BDescargarReporte(psStatus, psType, pdtFechaInicio, pdtFechaFinal);
                if (resExcel.Code != 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, resExcel.Message);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StreamContent(resExcel.Result);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "ReporteOrdenesConekta.xlsx";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "No se pudo consultar el documento del contratista");

                logger.Error(IMDSerialize.Serialize(67823458079398, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/Meditoc/Check/Server/Status")]
        public IMDResponse<bool> CCheckServer()
        {
            return new IMDResponse<bool>
            {
                Code = 0,
                Message = "SERVER OK",
                Result = true
            };
        }
    }
}
