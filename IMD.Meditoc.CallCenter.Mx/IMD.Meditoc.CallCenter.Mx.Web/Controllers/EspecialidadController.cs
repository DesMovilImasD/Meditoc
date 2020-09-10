using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
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
    public class EspecialidadController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EspecialidadController));

        [HttpPost]
        [Route("Api/Especialidad/Create/Resgistro")]
        public IMDResponse<bool> CSaveEspecialidad(EntEspecialidad entEspecialidad)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CSaveEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458454689, $"Inicia {metodo}"));

            try
            {
                BusEspecialidad busEspecialidad = new BusEspecialidad();
                response = busEspecialidad.BSaveEspecialidad(entEspecialidad);
            }
            catch (Exception ex)
            {
                response.Code = 67823458455466;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458455466, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/Especialidad/Get/Registros")]
        public IMDResponse<List<EntEspecialidad>> CGetEspecialidad([FromUri]int? piIdEspecialidad = null)
        {
            IMDResponse<List<EntEspecialidad>> response = new IMDResponse<List<EntEspecialidad>>();

            string metodo = nameof(this.CGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458453135, $"Inicia {metodo}"));

            try
            {
                BusEspecialidad busEspecialidad = new BusEspecialidad();
                response = busEspecialidad.BGetEspecialidad(piIdEspecialidad);
            }
            catch (Exception ex)
            {
                response.Code = 67823458453912;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458453912, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
