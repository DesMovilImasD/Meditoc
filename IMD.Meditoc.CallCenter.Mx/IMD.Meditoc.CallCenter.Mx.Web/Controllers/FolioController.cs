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

        public IMDResponse<EntDetalleCompra> CNuevoFolio(EntConecktaPago entConecktaPago)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.CNuevoFolio);
            logger.Info(IMDSerialize.Serialize(67823458413508, $"Inicia {metodo}"));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.CSaveNuevoFolio(entConecktaPago);

            }
            catch (Exception ex)
            {
                response.Code = 67823458414285;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458414285, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

    }
}
