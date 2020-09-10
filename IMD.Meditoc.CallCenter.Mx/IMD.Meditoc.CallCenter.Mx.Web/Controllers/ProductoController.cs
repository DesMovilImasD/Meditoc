using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Producto;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class ProductoController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ProductoController));

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Producto/Create/Producto")]
        public IMDResponse<bool> BSaveProducto([FromBody] EntProducto entProducto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveProducto);
            logger.Info(IMDSerialize.Serialize(67823458404184, $"Inicia {metodo}([FromBody] EntProducto entProducto)", entProducto));

            try
            {
                BusProducto busProducto = new BusProducto();

                response = busProducto.BSaveProducto(entProducto);
            }
            catch (Exception ex)
            {
                response.Code = 67823458404961;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458404961, $"Error en {metodo}([FromBody] EntProducto entProducto): {ex.Message}", entProducto, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Producto/Get/ObtenerProducto")]
        public IMDResponse<List<EntProducto>> BObtenerProductoByID([FromUri] int? iIdProducto = null)
        {
            IMDResponse<List<EntProducto>> response = new IMDResponse<List<EntProducto>>();

            string metodo = nameof(this.BObtenerProductoByID);
            logger.Info(IMDSerialize.Serialize(67823458399522, $"Inicia {metodo}([FromUri] int iIdProducto)", iIdProducto));

            try
            {
                BusProducto busProducto = new BusProducto();

                response = busProducto.BObtenerProductos(iIdProducto);
            }
            catch (Exception ex)
            {
                response.Code = 67823458400299;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458400299, $"Error en {metodo}([FromUri] int iIdProducto): {ex.Message}", iIdProducto, ex, response));
            }
            return response;
        }
    }
}
