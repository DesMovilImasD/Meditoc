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
        public IMDResponse<bool> CSaveProducto([FromBody] EntProducto entProducto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CSaveProducto);
            logger.Info(IMDSerialize.Serialize(67823458404184, $"Inicia {metodo}([FromBody] EntProducto entProducto)", entProducto));

            try
            {
                BusProducto busProducto = new BusProducto();

                response = busProducto.BSaveProducto(entProducto);
            }
            catch (Exception ex)
            {
                response.Code = 67823458404961;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar el producto.";

                logger.Error(IMDSerialize.Serialize(67823458404961, $"Error en {metodo}([FromBody] EntProducto entProducto): {ex.Message}", entProducto, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Producto/Get/ObtenerProducto")]
        public IMDResponse<List<EntProducto>> CObtenerProductoByID([FromUri] int? iIdProducto = null)
        {
            IMDResponse<List<EntProducto>> response = new IMDResponse<List<EntProducto>>();

            string metodo = nameof(this.CObtenerProductoByID);
            logger.Info(IMDSerialize.Serialize(67823458399522, $"Inicia {metodo}([FromUri] int iIdProducto)", iIdProducto));

            try
            {
                BusProducto busProducto = new BusProducto();

                response = busProducto.BObtenerProductos(iIdProducto);
            }
            catch (Exception ex)
            {
                response.Code = 67823458400299;
                response.Message = "Ocurrió un error inesperado en el servicio al consultar los productos.";

                logger.Error(IMDSerialize.Serialize(67823458400299, $"Error en {metodo}([FromUri] int iIdProducto): {ex.Message}", iIdProducto, ex, response));
            }
            return response;
        }

        //[MeditocAuthentication]
        [HttpGet]
        [Route("Api/Producto/Get/ObtenerMembresia")]
        public IMDResponse<List<EntProducto>> CgetMembership()
        {
            IMDResponse<List<EntProducto>> response = new IMDResponse<List<EntProducto>>();

            string metodo = nameof(this.CgetMembership);
            logger.Info(IMDSerialize.Serialize(67823458471783, $"Inicia {metodo}()"));

            try
            {
                BusProducto busProducto = new BusProducto();

                response = busProducto.BGetMembership();

            }
            catch (Exception ex)
            {
                response.Code = 67823458472560;
                response.Message = "Ocurrió un error inesperado en el servicio al obtener las membresías.";

                logger.Error(IMDSerialize.Serialize(67823458472560, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }

        //[MeditocAuthentication]
        [HttpGet]
        [Route("Api/Producto/Get/ObtenerServicio")]
        public IMDResponse<List<EntProducto>> CgetServices()
        {
            IMDResponse<List<EntProducto>> response = new IMDResponse<List<EntProducto>>();

            string metodo = nameof(this.CgetServices);
            logger.Info(IMDSerialize.Serialize(67823458473337, $"Inicia {metodo}()"));

            try
            {
                BusProducto busProducto = new BusProducto();

                response = busProducto.BGetServices();
            }
            catch (Exception ex)
            {
                response.Code = 67823458474114;
                response.Message = "Ocurrió un error inesperado en el servicio al obtener los servicios de orientación.";

                logger.Error(IMDSerialize.Serialize(67823458474114, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/Producto/Get/Nutricional/Psicologia")]
        public IMDResponse<EntProductosNutricionalPsicologia> CGetProductosNutricionalPsicologia()
        {
            IMDResponse<EntProductosNutricionalPsicologia> response = new IMDResponse<EntProductosNutricionalPsicologia>();

            string metodo = nameof(this.CGetProductosNutricionalPsicologia);
            logger.Info(IMDSerialize.Serialize(67823458636507, $"Inicia {metodo}()"));

            try
            {
                BusProducto busProducto = new BusProducto();
                response = busProducto.BGetProductosNutricionalPsicologia();
            }
            catch (Exception ex)
            {
                response.Code = 67823458637284;
                response.Message = "Ocurrió un error inesperado en el servicio al consultar los productos disponibles.";

                logger.Error(IMDSerialize.Serialize(67823458637284, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
