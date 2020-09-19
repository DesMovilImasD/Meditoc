using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Business.Consulta;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    [MeditocAuthentication]
    public class CallCenterController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CallCenterController));

        [HttpPost]
        [Route("Api/CallCenter/Set/Colaborador/Online")]
        public IMDResponse<bool> CCallCenterOnline(EntOnlineMod entOnlineMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCallCenterOnline);
            logger.Info(IMDSerialize.Serialize(67823458510633, $"Inicia {metodo}"));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BCallCenterOnline(entOnlineMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458511410;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458511410, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Start/Service/WithFolio")]
        public IMDResponse<EntCallCenter> CCallCenterStartWithFolio(int iIdColaborador, string sFolio, int iIdUsuarioMod)
        {
            IMDResponse<EntCallCenter> response = new IMDResponse<EntCallCenter>();

            string metodo = nameof(this.CCallCenterStartWithFolio);
            logger.Info(IMDSerialize.Serialize(67823458512187, $"Inicia {metodo}"));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BCallCenterStartWithFolio(iIdColaborador, sFolio, iIdUsuarioMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458512964;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458512964, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Save/Consulta")]
        public IMDResponse<EntConsulta> CSaveConsulta(EntConsulta entConsulta)
        {
            IMDResponse<EntConsulta> response = new IMDResponse<EntConsulta>();

            string metodo = nameof(this.CSaveConsulta);
            logger.Info(IMDSerialize.Serialize(67823458521511, $"Inicia {metodo}"));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BSaveConsulta(entConsulta);
            }
            catch (Exception ex)
            {
                response.Code = 67823458522288;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458522288, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/CallCenter/Get/Consulta/Detalle")]
        public IMDResponse<List<EntDetalleConsulta>> CGetDetalleConsulta(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<List<EntDetalleConsulta>> response = new IMDResponse<List<EntDetalleConsulta>>();

            string metodo = nameof(this.CGetDetalleConsulta);
            logger.Info(IMDSerialize.Serialize(67823458532389, $"Inicia {metodo}"));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BGetDetalleConsulta(piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin);
            }
            catch (Exception ex)
            {
                response.Code = 67823458533166;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458533166, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Save/Folio/Especialista/Consulta")]
        public IMDResponse<EntDetalleCompra> CNuevoFolioEspecialista(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.CNuevoFolioEspecialista);
            logger.Info(IMDSerialize.Serialize(67823458538605, $"Inicia {metodo}"));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BNuevaConsulta(entNuevaConsulta);
            }
            catch (Exception ex)
            {
                response.Code = 67823458539382;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458539382, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Cancelar/Folio/Especialista/Consulta")]
        public IMDResponse<bool> CCancelarConsulta(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCancelarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458554145, $"Inicia {metodo}"));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BCancelarConsulta(entNuevaConsulta);
            }
            catch (Exception ex)
            {
                response.Code = 67823458554922;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458554922, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Iniciar/Consulta")]
        public IMDResponse<bool> CIniciarConsulta(int iIdConsulta, int iIdColaborador, int iIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CIniciarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458555699, $"Inicia {metodo}"));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BIniciarConsulta(iIdConsulta, iIdColaborador, iIdUsuarioMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458556476;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458556476, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Finalizar/Consulta")]
        public IMDResponse<bool> CFinalizarConsulta(int iIdConsulta, int iIdColaborador, int iIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CFinalizarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458557253, $"Inicia {metodo}"));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BFinalizarConsulta(iIdConsulta, iIdColaborador, iIdUsuarioMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458558030;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458558030, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
