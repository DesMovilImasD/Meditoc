using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class FolioController : ApiController
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(FolioController));

        [HttpPost]
        [Route("Api/Folio/Create/Folio")]
        public IMDResponse<EntDetalleCompra> CNuevoFolio([FromBody] EntConecktaPago entConecktaPago)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.CNuevoFolio);
            logger.Info(IMDSerialize.Serialize(67823458413508, $"Inicia {metodo}([FromBody]EntConecktaPago entConecktaPago)", entConecktaPago));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.CSaveNuevoFolio(entConecktaPago);

            }
            catch (Exception ex)
            {
                response.Code = 67823458414285;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458414285, $"Error en {metodo}([FromBody]EntConecktaPago entConecktaPago): {ex.Message}", entConecktaPago, ex, response));
            }
            return response;
        }

    }
}
