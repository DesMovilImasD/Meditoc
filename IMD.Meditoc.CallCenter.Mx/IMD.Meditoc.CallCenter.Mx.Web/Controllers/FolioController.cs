using IMD.Admin.Conekta.Entities.Orders;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class FolioController : ApiController
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(FolioController));

        [HttpPost]
        [Route("Api/Folio/Create/Folio")]
        public IMDResponse<EntDetalleCompra> CNuevoFolio([FromBody] EntCreateOrder entCreateOrder)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.CNuevoFolio);
            logger.Info(IMDSerialize.Serialize(67823458413508, $"Inicia {metodo}([FromBody]EntConecktaPago entConecktaPago)", entCreateOrder));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BNuevoFolioCompra(entCreateOrder);

            }
            catch (Exception ex)
            {
                response.Code = 67823458414285;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458414285, $"Error en {metodo}([FromBody]EntConecktaPago entConecktaPago): {ex.Message}", entCreateOrder, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Folio/Create/FolioEmpresa")]
        public IMDResponse<bool> CNuevoFolioEmpresa([FromBody] EntFolioxEmpresa entFolioxEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CNuevoFolioEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458425163, $"Inicia {metodo}"));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BNuevosFoliosEmpresa(entFolioxEmpresa);

            }
            catch (Exception ex)
            {
                response.Code = 67823458425940;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458425940, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Folio/Get/LoginApp")]
        public IMDResponse<EntFolio> CLoginApp([FromUri] string sUsuario, string sPassword)
        {
            IMDResponse<EntFolio> response = new IMDResponse<EntFolio>();

            string metodo = nameof(this.CLoginApp);
            logger.Info(IMDSerialize.Serialize(67823458431379, $"Inicia {metodo}([FromUri] string sUsuario, string sPassword)", sUsuario, sPassword));


            try
            {
                BusFolio busFolio = new BusFolio();

                response = busFolio.BLoginApp(sUsuario, sPassword);
            }
            catch (Exception ex)
            {
                response.Code = 67823458432156;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458432156, $"Error en {metodo}([FromUri] string sUsuario, string sPassword): {ex.Message}", sUsuario, sPassword, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Folios/Get/Report")]
        public IMDResponse<List<EntFolioReporte>> CGetFolios(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto = null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false)
        {
            IMDResponse<List<EntFolioReporte>> response = new IMDResponse<List<EntFolioReporte>>();

            string metodo = nameof(this.CGetFolios);
            logger.Info(IMDSerialize.Serialize(67823458436041, $"Inicia {metodo}", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BGetFolios(piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja);
            }
            catch (Exception ex)
            {
                response.Code = 67823458436818;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458436818, $"Error en {metodo}: {ex.Message}", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Folio/Update/FechaVencimiento")]
        public IMDResponse<bool> CUpdFechaVencimiento(EntFolioFV entFolioFV)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CUpdFechaVencimiento);
            logger.Info(IMDSerialize.Serialize(67823458440703, $"Inicia {metodo}"));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BUpdFechaVencimiento(entFolioFV);
            }
            catch (Exception ex)
            {
                response.Code = 67823458441480;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458441480, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Folio/Delete/FoliosEmpresa")]
        public IMDResponse<bool> CEliminarFoliosEmpresa(EntFolioFV entFolioFV)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CEliminarFoliosEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458445365, $"Inicia {metodo}"));


            try
            {
                BusFolio busFolio = new BusFolio();

                response = busFolio.BEliminarFoliosEmpresa(entFolioFV);
            }
            catch (Exception ex)
            {
                response.Code = 67823458446142;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458446142, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Folio/Update/TerminosYCondiciones")]
        public IMDResponse<bool> CTerminosYCondiciones(string sFolio = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CTerminosYCondiciones);
            logger.Info(IMDSerialize.Serialize(67823458464013, $"Inicia {metodo}(string sFolio = null)", sFolio));

            try
            {
                BusFolio busFolio = new BusFolio();

                response = busFolio.BTerminosYCondiciones(sFolio);
            }
            catch (Exception ex)
            {
                response.Code = 67823458464790;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458464790, $"Error en {metodo}(string sFolio = null): {ex.Message}", sFolio, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Folio/Update/FolioPassword")]
        public IMDResponse<bool> CUpdPassword(string sFolio = null, string sPassword = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CUpdPassword);
            logger.Info(IMDSerialize.Serialize(67823458498201, $"Inicia {metodo}(string sFolio = null, string  sPassword = null)", sFolio, sPassword));

            try
            {

                BusFolio busFolio = new BusFolio();
                response = busFolio.BUpdPassword(sFolio, sPassword);


            }
            catch (Exception ex)
            {
                response.Code = 67823458498978;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458498978, $"Error en {metodo}(string sFolio = null, string  sPassword = null): {ex.Message}", sFolio, sPassword, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Folio/Verificar/Folio/VentaCalle")]
        public IMDResponse<List<EntFolioVerificarCarga>> CVerificarFoliosVentaCalle()
        {
            IMDResponse<List<EntFolioVerificarCarga>> response = new IMDResponse<List<EntFolioVerificarCarga>>();

            string metodo = nameof(this.CVerificarFoliosVentaCalle);
            logger.Info(IMDSerialize.Serialize(67823458606981, $"Inicia {metodo}"));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BVerificarFoliosVentaCalle(HttpContext.Current.Request.InputStream);
            }
            catch (Exception ex)
            {
                response.Code = 67823458607758;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458607758, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Folio/Save/Folio/VentaCalle")]
        public IMDResponse<bool> CGenerarFoliosVentaCalle(int piIdUsuarioMod, string sFolioEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CGenerarFoliosVentaCalle);
            logger.Info(IMDSerialize.Serialize(67823458600765, $"Inicia {metodo}"));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BGenerarFoliosVentaCalle(piIdUsuarioMod, sFolioEmpresa, HttpContext.Current.Request.InputStream);
            }
            catch (Exception ex)
            {
                response.Code = 67823458601542;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458601542, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Folio/Get/Folio/VentaCalle/Plantilla")]
        public HttpResponseMessage CGetPlantillaFolioVC()
        {
            HttpResponseMessage response;

            string metodo = nameof(this.CGetPlantillaFolioVC);
            logger.Info(IMDSerialize.Serialize(67823458611643, $"Inicia {metodo}"));

            try
            {
                BusFolio busFolio = new BusFolio();
                IMDResponse<MemoryStream> resGetPlantilla = busFolio.BGetPlantillaFolioVC();
                if (resGetPlantilla.Code != 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, resGetPlantilla.Message);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StreamContent(resGetPlantilla.Result);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = ConfigurationManager.AppSettings["sNombrePlantillaFoliosVC"];
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error al obtener la plantilla de carga de folios");

                logger.Error(IMDSerialize.Serialize(67823458612420, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
