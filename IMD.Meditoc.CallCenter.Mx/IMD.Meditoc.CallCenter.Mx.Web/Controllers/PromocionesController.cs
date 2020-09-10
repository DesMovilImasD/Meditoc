using IMD.Admin.Conekta.Business;
using IMD.Admin.Conekta.Entities;
using IMD.Admin.Conekta.Entities.Promotions;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class PromocionesController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PromocionesController));

        private readonly BusPromociones busPromociones = new BusPromociones("hdiu4soi3IHD334F", "SKlru3nc");

        [HttpPost]
        [Route("api/promociones/guardar/cupon")]
        public IMDResponse<bool> CActivarCupon([FromBody]EntCreateCupon entCreateCupon, [FromUri]int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CActivarCupon);
            logger.Info(IMDSerialize.Serialize(67823458200610, $"Inicia {metodo}([FromBody]EntCreateCupon entCreateCupon, [FromUri]int? piIdUsuario = null)", entCreateCupon, piIdUsuario));

            try
            {
                response = busPromociones.BActivarCupon(entCreateCupon, piIdUsuario);
            }
            catch (Exception ex)
            {
                response.Code = 67823458201387;
                response.Message = "Ocurrió un error al intentar activar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458201387, $"Error en {metodo}([FromBody]EntCreateCupon entCreateCupon, [FromUri]int? piIdUsuario = null): {ex.Message}", entCreateCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("api/promociones/aplicar/cupon")]
        public IMDResponse<bool> CAplicarCupon([FromUri]int piIdCupon, [FromUri]int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CAplicarCupon);
            logger.Info(IMDSerialize.Serialize(67823458202164, $"Inicia {metodo}([FromUri]int piIdCupon, [FromUri]int? piIdUsuario = null)", piIdCupon, piIdUsuario));

            try
            {
                response = busPromociones.BAplicarCupon(piIdCupon, piIdUsuario);
            }
            catch (Exception ex)
            {
                response.Code = 67823458202941;
                response.Message = "Ocurrió un error al intentar aplicar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458202941, $"Error en {metodo}([FromUri]int piIdCupon, [FromUri]int? piIdUsuario = null): {ex.Message}", piIdCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("api/promociones/validar/cupon")]
        public IMDResponse<EntCupon> CValidarCupon([FromUri]string psCodigo = null, [FromUri]int? piIdCupon = null)
        {
            IMDResponse<EntCupon> response = new IMDResponse<EntCupon>();

            string metodo = nameof(this.CValidarCupon);
            logger.Info(IMDSerialize.Serialize(67823458203718, $"Inicia {metodo}([FromUri]string psCodigo = null, [FromUri]int? piIdCupon = null)", psCodigo, piIdCupon));

            try
            {
                response = busPromociones.BValidarCupon(psCodigo, piIdCupon);
            }
            catch (Exception ex)
            {
                response.Code = 67823458204495;
                response.Message = "Ocurrió un error al intentar validar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458204495, $"Error en {metodo}([FromUri]string psCodigo = null, [FromUri]int? piIdCupon = null): {ex.Message}", psCodigo, piIdCupon, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("api/promociones/desactivar/cupon")]
        public IMDResponse<bool> CDesactivarCupon([FromUri]int piIdCupon, [FromUri]int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CDesactivarCupon);
            logger.Info(IMDSerialize.Serialize(67823458205272, $"Inicia {metodo}([FromUri]int piIdCupon, [FromUri]int? piIdUsuario = null)", piIdCupon, piIdUsuario));

            try
            {
                response = busPromociones.BDeshabilitarCupon(piIdCupon, piIdUsuario);
            }
            catch (Exception ex)
            {
                response.Code = 67823458206049;
                response.Message = "Ocurrió un error al intentar desactivar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458206049, $"Error en {metodo}([FromUri]int piIdCupon, [FromUri]int? piIdUsuario = null): {ex.Message}", piIdCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("api/promociones/obtener/cupones")]
        public IMDResponse<List<EntCupon>> CObtenerCupones([FromUri]int? piIdCupon = null, [FromUri]int? piIdCuponCategoria = null, [FromUri]string psDescripcion = null, [FromUri]string psCodigo = null, [FromUri]DateTime? pdtFechaVencimientoInicio = null, [FromUri]DateTime? pdtFechaVencimientoFin = null, [FromUri]DateTime? pdtFechaCreacionInicio = null, [FromUri]DateTime? pdtFechaCreacionFin = null, [FromUri]bool? pbActivo = true, [FromUri]bool? pbBaja = false)
        {
            IMDResponse<List<EntCupon>> response = new IMDResponse<List<EntCupon>>();

            string metodo = nameof(this.CObtenerCupones);
            logger.Info(IMDSerialize.Serialize(67823458206826, $"Inicia {metodo}([FromUri]int? piIdCupon = null, [FromUri]int? piIdCuponCategoria = null, [FromUri]string psDescripcion = null, [FromUri]string psCodigo = null, [FromUri]DateTime? pdtFechaVencimientoInicio = null, [FromUri]DateTime? pdtFechaVencimientoFin = null, [FromUri]DateTime? pdtFechaCreacionInicio = null, [FromUri]DateTime? pdtFechaCreacionFin = null, [FromUri]bool pbActivo = true, [FromUri]bool pbBaja = false)", piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja));

            try
            {
                response = busPromociones.BObtenerCupones(piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja);
            }
            catch (Exception ex)
            {
                response.Code = 67823458207603;
                response.Message = "Ocurrió un error al intentar consultar los cupones";

                logger.Error(IMDSerialize.Serialize(67823458207603, $"Error en {metodo}([FromUri]int? piIdCupon = null, [FromUri]int? piIdCuponCategoria = null, [FromUri]string psDescripcion = null, [FromUri]string psCodigo = null, [FromUri]DateTime? pdtFechaVencimientoInicio = null, [FromUri]DateTime? pdtFechaVencimientoFin = null, [FromUri]DateTime? pdtFechaCreacionInicio = null, [FromUri]DateTime? pdtFechaCreacionFin = null, [FromUri]bool pbActivo = true, [FromUri]bool pbBaja = false): {ex.Message}", piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja, ex, response));
            }
            return response;
        }

        //https://localhost:44344/api/promociones/validar/cupon/email?piIdCupon=10005&psEmail=g098@live.com.mx
        [HttpGet]
        [Route("api/promociones/validar/cupon/email")]
        public IMDResponse<bool> CGetCuponUsed([FromUri]int piIdCupon, [FromUri]string psEmail)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CGetCuponUsed);
            logger.Info(IMDSerialize.Serialize(67823458216150, $"Inicia {metodo}"));

            try
            {
                response = busPromociones.BGetCuponUsed(piIdCupon, psEmail);
            }
            catch (Exception ex)
            {
                response.Code = 67823458216927;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458216927, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("api/promociones/obtener/cupon/autocompletar")]
        public IMDResponse<List<string>> CGetCuponAutocomplete()
        {
            IMDResponse<List<string>> response = new IMDResponse<List<string>>();

            string metodo = nameof(this.CGetCuponAutocomplete);
            logger.Info(IMDSerialize.Serialize(67823458237906, $"Inicia {metodo}()"));

            try
            {
                response = busPromociones.BGetCuponAutocomplete();
            }
            catch (Exception ex)
            {
                response.Code = 67823458238683;
                response.Message = "Ocurrió un error inesperado al consultar los códigos";

                logger.Error(IMDSerialize.Serialize(67823458238683, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
