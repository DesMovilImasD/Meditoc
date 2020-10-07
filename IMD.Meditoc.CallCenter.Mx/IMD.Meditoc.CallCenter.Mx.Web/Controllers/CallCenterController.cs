using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Business.Consulta;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Business.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    [MeditocAuthentication]
    public class CallCenterController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CallCenterController));

        [HttpPost]
        [Route("Api/CallCenter/Set/Colaborador/Online")]
        public IMDResponse<bool> CCallCenterOnline([FromBody]EntOnlineMod entOnlineMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCallCenterOnline);
            logger.Info(IMDSerialize.Serialize(67823458510633, $"Inicia {metodo}([FromBody]EntOnlineMod entOnlineMod)", entOnlineMod));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BCallCenterOnline(entOnlineMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458511410;
                response.Message = "Ocurrió un error inesperado en el servicio al cambiar el estatus.";

                logger.Error(IMDSerialize.Serialize(67823458511410, $"Error en {metodo}([FromBody]EntOnlineMod entOnlineMod): {ex.Message}", entOnlineMod, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Start/Service/WithFolio")]
        public IMDResponse<EntCallCenter> CCallCenterStartWithFolio([FromUri]int iIdColaborador, [FromUri]string sFolio, [FromUri]int iIdUsuarioMod)
        {
            IMDResponse<EntCallCenter> response = new IMDResponse<EntCallCenter>();

            string metodo = nameof(this.CCallCenterStartWithFolio);
            logger.Info(IMDSerialize.Serialize(67823458512187, $"Inicia {metodo}([FromUri]int iIdColaborador, [FromUri]string sFolio, [FromUri]int iIdUsuarioMod)", iIdColaborador, sFolio, iIdUsuarioMod));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BCallCenterStartWithFolio(iIdColaborador, sFolio, iIdUsuarioMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458512964;
                response.Message = "Ocurrió un error inesperado en el servicio al iniciar la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458512964, $"Error en {metodo}([FromUri]int iIdColaborador, [FromUri]string sFolio, [FromUri]int iIdUsuarioMod): {ex.Message}", iIdColaborador, sFolio, iIdUsuarioMod, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Save/Consulta")]
        public IMDResponse<EntConsulta> CSaveConsulta([FromBody]EntConsulta entConsulta)
        {
            IMDResponse<EntConsulta> response = new IMDResponse<EntConsulta>();

            string metodo = nameof(this.CSaveConsulta);
            logger.Info(IMDSerialize.Serialize(67823458521511, $"Inicia {metodo}([FromBody]EntConsulta entConsulta)", entConsulta));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BSaveConsulta(entConsulta);
            }
            catch (Exception ex)
            {
                response.Code = 67823458522288;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458522288, $"Error en {metodo}([FromBody]EntConsulta entConsulta): {ex.Message}", entConsulta, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/CallCenter/Get/Consulta/Detalle")]
        public IMDResponse<List<EntDetalleConsulta>> CGetDetalleConsulta([FromUri]int? piIdConsulta = null, [FromUri]int? piIdPaciente = null, [FromUri]int? piIdColaborador = null, [FromUri]int? piIdEstatusConsulta = null, [FromUri]DateTime? pdtFechaProgramadaInicio = null, [FromUri]DateTime? pdtFechaProgramadaFin = null, [FromUri]DateTime? pdtFechaConsultaInicio = null, [FromUri]DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<List<EntDetalleConsulta>> response = new IMDResponse<List<EntDetalleConsulta>>();

            string metodo = nameof(this.CGetDetalleConsulta);
            logger.Info(IMDSerialize.Serialize(67823458532389, $"Inicia {metodo}([FromUri]int? piIdConsulta = null, [FromUri]int? piIdPaciente = null, [FromUri]int? piIdColaborador = null, [FromUri]int? piIdEstatusConsulta = null, [FromUri]DateTime? pdtFechaProgramadaInicio = null, [FromUri]DateTime? pdtFechaProgramadaFin = null, [FromUri]DateTime? pdtFechaConsultaInicio = null, [FromUri]DateTime? pdtFechaConsultaFin = null)", piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BGetDetalleConsulta(piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin);
            }
            catch (Exception ex)
            {
                response.Code = 67823458533166;
                response.Message = "Ocurrió un error inesperado en el servicio al obtener el detalle de la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458533166, $"Error en {metodo}([FromUri]int? piIdConsulta = null, [FromUri]int? piIdPaciente = null, [FromUri]int? piIdColaborador = null, [FromUri]int? piIdEstatusConsulta = null, [FromUri]DateTime? pdtFechaProgramadaInicio = null, [FromUri]DateTime? pdtFechaProgramadaFin = null, [FromUri]DateTime? pdtFechaConsultaInicio = null, [FromUri]DateTime? pdtFechaConsultaFin = null): {ex.Message}", piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Save/Folio/Especialista/Consulta")]
        public IMDResponse<EntDetalleCompra> CNuevoFolioEspecialista([FromBody]EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.CNuevoFolioEspecialista);
            logger.Info(IMDSerialize.Serialize(67823458538605, $"Inicia {metodo}([FromBody]EntNuevaConsulta entNuevaConsulta)", entNuevaConsulta));

            try
            {
                BusFolio busFolio = new BusFolio();
                response = busFolio.BNuevaConsulta(entNuevaConsulta);
            }
            catch (Exception ex)
            {
                response.Code = 67823458539382;
                response.Message = "Ocurrió un error inesperado en el servicio al generar un nuevo folio de consulta.";

                logger.Error(IMDSerialize.Serialize(67823458539382, $"Error en {metodo}([FromBody]EntNuevaConsulta entNuevaConsulta): {ex.Message}", entNuevaConsulta, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Cancelar/Folio/Especialista/Consulta")]
        public IMDResponse<bool> CCancelarConsulta([FromBody]EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCancelarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458554145, $"Inicia {metodo}([FromBody]EntNuevaConsulta entNuevaConsulta)", entNuevaConsulta));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BCancelarConsulta(entNuevaConsulta);
            }
            catch (Exception ex)
            {
                response.Code = 67823458554922;
                response.Message = "Ocurrió un error inesperado en el servicio al cancelar la consulta del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458554922, $"Error en {metodo}([FromBody]EntNuevaConsulta entNuevaConsulta): {ex.Message}", entNuevaConsulta, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Iniciar/Consulta")]
        public IMDResponse<bool> CIniciarConsulta([FromUri]int iIdConsulta, [FromUri]int iIdColaborador, [FromUri]int iIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CIniciarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458555699, $"Inicia {metodo}([FromUri]int iIdConsulta, [FromUri]int iIdColaborador, [FromUri]int iIdUsuarioMod)", iIdConsulta, iIdColaborador, iIdUsuarioMod));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BIniciarConsulta(iIdConsulta, iIdColaborador, iIdUsuarioMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458556476;
                response.Message = "Ocurrió un error inesperado en el servicio al iniciar la consulta del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458556476, $"Error en {metodo}([FromUri]int iIdConsulta, [FromUri]int iIdColaborador, [FromUri]int iIdUsuarioMod): {ex.Message}", iIdConsulta, iIdColaborador, iIdUsuarioMod, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Finalizar/Consulta")]
        public IMDResponse<bool> CFinalizarConsulta([FromUri]int iIdConsulta, [FromUri]int iIdColaborador, [FromUri]int iIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CFinalizarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458557253, $"Inicia {metodo}([FromUri]int iIdConsulta, [FromUri]int iIdColaborador, [FromUri]int iIdUsuarioMod)", iIdConsulta, iIdColaborador, iIdUsuarioMod));

            try
            {
                BusCallCenter busCallCenter = new BusCallCenter();
                response = busCallCenter.BFinalizarConsulta(iIdConsulta, iIdColaborador, iIdUsuarioMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458558030;
                response.Message = "Ocurrió un error inesperado en el servicio al finalizar la consulta del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458558030, $"Error en {metodo}([FromUri]int iIdConsulta, [FromUri]int iIdColaborador, [FromUri]int iIdUsuarioMod): {ex.Message}", iIdConsulta, iIdColaborador, iIdUsuarioMod, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Guardar/Datos/Paciente")]
        public IMDResponse<bool> CGuardarDatosPaciente([FromBody]EntUpdPaciente entUpdPaciente)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CGuardarDatosPaciente);
            logger.Info(IMDSerialize.Serialize(67823458580563, $"Inicia {metodo}([FromBody]EntUpdPaciente entUpdPaciente)", entUpdPaciente));

            try
            {
                BusPaciente busPaciente = new BusPaciente();
                response = busPaciente.BUpdPaciente(entUpdPaciente);
            }
            catch (Exception ex)
            {
                response.Code = 67823458581340;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar los datos del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458581340, $"Error en {metodo}([FromBody]EntUpdPaciente entUpdPaciente): {ex.Message}", entUpdPaciente, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CallCenter/Guardar/Historial/Clinico")]
        public IMDResponse<bool> CSaveHistorialClinico([FromBody]EntHistorialClinico entHistorialClinico)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CSaveHistorialClinico);
            logger.Info(IMDSerialize.Serialize(67823458585225, $"Inicia {metodo}([FromBody]EntHistorialClinico entHistorialClinico)", entHistorialClinico));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BSaveHistorialClinico(entHistorialClinico);
            }
            catch (Exception ex)
            {
                response.Code = 67823458586002;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar el historial clínico del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458586002, $"Error en {metodo}([FromBody]EntHistorialClinico entHistorialClinico): {ex.Message}", entHistorialClinico, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/CallCenter/Get/Historial/Clinico")]
        public IMDResponse<List<EntHistorialClinico>> CGetHistorialMedico([FromUri]int? piIdHistorialClinico = null, [FromUri]int? piIdConsulta = null, [FromUri]int? piIdPaciente = null, [FromUri]int? piIdColaborador = null, [FromUri]int? piIdFolio = null)
        {
            IMDResponse<List<EntHistorialClinico>> response = new IMDResponse<List<EntHistorialClinico>>();

            string metodo = nameof(this.CGetHistorialMedico);
            logger.Info(IMDSerialize.Serialize(67823458620967, $"Inicia {metodo}([FromUri]int? piIdHistorialClinico = null, [FromUri]int? piIdConsulta = null, [FromUri]int? piIdPaciente = null, [FromUri]int? piIdColaborador = null, [FromUri]int? piIdFolio = null)", piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio));

            try
            {
                BusConsulta busConsulta = new BusConsulta();
                response = busConsulta.BGetHistorialMedico(piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio);
            }
            catch (Exception ex)
            {
                response.Code = 67823458621744;
                response.Message = "Ocurrió un error inesperado en el servicio al consultar el historial clínico.";

                logger.Error(IMDSerialize.Serialize(67823458621744, $"Error en {metodo}([FromUri]int? piIdHistorialClinico = null, [FromUri]int? piIdConsulta = null, [FromUri]int? piIdPaciente = null, [FromUri]int? piIdColaborador = null, [FromUri]int? piIdFolio = null): {ex.Message}", piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio, ex, response));
            }
            return response;
        }
    }
}
