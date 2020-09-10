using IMD.Admin.Conekta.Business;
using IMD.Admin.Conekta.Entities;
using IMD.Admin.Conekta.Entities.Orders;
using IMD.Admin.Conekta.Entities.WebHooks;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IMD.Admin.Conekta.Web.Controllers
{
    public class ConektaController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ConektaController));

        private readonly BusOrder busOrder = new BusOrder("MeditocComercial", "Meditoc1");
        private readonly BusWebHook busWebHook = new BusWebHook("MeditocComercial", "Meditoc1");

        [HttpPost]
        [Route("Api/Conekta/Create/Order")]
        public IMDResponse<EntOrder> CCreateOrder([FromBody]EntCreateOrder entCreateOrder)
        {
            IMDResponse<EntOrder> response = new IMDResponse<EntOrder>();

            string metodo = nameof(this.CCreateOrder);
            logger.Info(IMDSerialize.Serialize(67823458121356, $"Inicia {metodo}([FromBody]EntCreateOrder entCreateOrder)", entCreateOrder));

            try
            {
                response = busOrder.BCreateOrder(entCreateOrder);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "No pudimos procesar el pago de tu pedido, revisa nuevamente los datos ingresados o intenta con otra tarjeta.";

                logger.Error(IMDSerialize.Serialize(67823458122133, $"Error en {metodo}([FromBody]EntCreateOrder entCreateOrder): {ex.Message}", entCreateOrder, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Conekta/Get/Order")]
        public IMDResponse<EntOrder> CGetOrder([FromUri]string orderId)
        {
            IMDResponse<EntOrder> response = new IMDResponse<EntOrder>();

            string metodo = nameof(this.CGetOrder);
            logger.Info(IMDSerialize.Serialize(67823458122910, $"Inicia {metodo}([FromUri]string orderId)", orderId));

            try
            {
                response = busOrder.BGetOrder(orderId);
            }
            catch (Exception ex)
            {
                response.Code = 67823458123687;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458123687, $"Error en {metodo}([FromUri]string orderId): {ex.Message}", orderId, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/Conekta/WebHook/Client/Server/Main")]
        public IMDResponse<bool> CWebHookMain([FromBody]EntWebHook entWebHook)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CWebHookMain);
            logger.Info(IMDSerialize.Serialize(67823458155544, $"Inicia {metodo}([FromBody]EntWebHook entWebHook)", entWebHook));

            try
            {
                response = busWebHook.BUpdateState(entWebHook);
            }
            catch (Exception ex)
            {
                response.Code = 67823458156321;
                response.Message = "Ocurrió un error al procesar la información de la orden";

                logger.Error(IMDSerialize.Serialize(67823458156321, $"Error en {metodo}([FromBody]EntWebHook entWebHook): {ex.Message}", entWebHook, ex, response));
            }
            return response;
        }
    }
}
