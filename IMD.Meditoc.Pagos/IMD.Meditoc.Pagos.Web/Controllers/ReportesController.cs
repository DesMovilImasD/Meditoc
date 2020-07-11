using IMD.Admin.Utilities.Entities;
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

namespace IMD.Meditoc.Pagos.Web.Controllers
{
    public class ReportesController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ReportesController));

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
