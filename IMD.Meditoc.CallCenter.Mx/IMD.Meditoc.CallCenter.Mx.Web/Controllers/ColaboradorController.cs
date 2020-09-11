using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class ColaboradorController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EspecialidadController));

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Colaborador/Save/CallCenter/Especialista")]
        public IMDResponse<bool> CSaveColaborador(EntCreateColaborador entCreateColaborador)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CSaveColaborador);
            logger.Info(IMDSerialize.Serialize(67823458459351, $"Inicia {metodo}"));

            try
            {
                BusColaborador busColaborador = new BusColaborador();
                response = busColaborador.BSaveColaborador(entCreateColaborador);
            }
            catch (Exception ex)
            {
                response.Code = 67823458460128;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458460128, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Get/Colaboradores/CallCenter/Especialistas")]
        public IMDResponse<List<EntColaborador>> CGetColaborador(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null)
        {
            IMDResponse<List<EntColaborador>> response = new IMDResponse<List<EntColaborador>>();

            string metodo = nameof(this.CGetColaborador);
            logger.Info(IMDSerialize.Serialize(67823458476445, $"Inicia {metodo}"));

            try
            {
                BusColaborador busColaborador = new BusColaborador();
                response = busColaborador.BGetColaborador(piIdColaborador, piIdTipoDoctor, piIdEspecialidad, piIdUsuarioCGU);
            }
            catch (Exception ex)
            {
                response.Code = 67823458477222;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458477222, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Colaborador/Save/Foto")]
        public IMDResponse<bool> CSaveColaboradorFoto(int piIdColaborador, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CSaveColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458481107, $"Inicia {metodo}"));

            try
            {
                BusColaborador busColaborador = new BusColaborador();
                response = busColaborador.BSaveColaboradorFoto(piIdColaborador, piIdUsuarioMod, HttpContext.Current.Request.InputStream);
            }
            catch (Exception ex)
            {
                response.Code = 67823458481884;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458481884, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Colaborador/Get/Foto")]
        public IMDResponse<string> CGetColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.CGetColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458488877, $"Inicia {metodo}"));

            try
            {
                BusColaborador busColaborador = new BusColaborador();
                response = busColaborador.BGetColaboradorFoto(piIdColaborador);
            }
            catch (Exception ex)
            {
                response.Code = 67823458489654;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458489654, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/Colaborador/Descargar/Foto")]
        public HttpResponseMessage CDescargarColaboradorFoto(int piIdColaborador)
        {
            HttpResponseMessage response;

            string metodo = nameof(this.CDescargarColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458490431, $"Inicia {metodo}"));

            try
            {
                BusColaborador busColaborador = new BusColaborador();
                IMDResponse<MemoryStream> resGetFoto = busColaborador.BDescargarColaboradorFoto(piIdColaborador);
                if(resGetFoto.Code != 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, resGetFoto.Message);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StreamContent(resGetFoto.Result);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = $"Foto-Colaborador-{piIdColaborador}.jpg";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error al obtener la foto del colaborador");

                logger.Error(IMDSerialize.Serialize(67823458491208, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Colaborador/Eliminar/Foto")]
        public IMDResponse<bool> CEliminarColaborador(int piIdColaborador, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CEliminarColaborador);
            logger.Info(IMDSerialize.Serialize(67823458495093, $"Inicia {metodo}"));

            try
            {
                BusColaborador busColaborador = new BusColaborador();
                response = busColaborador.BEliminarColaboradorFoto(piIdColaborador, piIdUsuarioMod);
            }
            catch (Exception ex)
            {
                response.Code = 67823458495870;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458495870, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/Colaborador/Get/Directorio/Especialistas")]
        public IMDResponse<List<EntColaboradorDirectorio>> CGetDirectorio(int? piIdEspecialidad = null, string psBuscador = null)
        {
            IMDResponse<List<EntColaboradorDirectorio>> response = new IMDResponse<List<EntColaboradorDirectorio>>();

            string metodo = nameof(this.CGetDirectorio);
            logger.Info(IMDSerialize.Serialize(67823458505971, $"Inicia {metodo}"));

            try
            {
                BusColaborador busColaborador = new BusColaborador();
                response = busColaborador.BGetDirectorio(piIdEspecialidad, psBuscador);
            }
            catch (Exception ex)
            {
                response.Code = 67823458506748;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458506748, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
