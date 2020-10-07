using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class EspecialidadController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EspecialidadController));

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/Especialidad/Create/Resgistro")]
        public IMDResponse<bool> CSaveEspecialidad([FromBody]EntEspecialidad entEspecialidad)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CSaveEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458454689, $"Inicia {metodo}([FromBody]EntEspecialidad entEspecialidad)", entEspecialidad));

            try
            {
                BusEspecialidad busEspecialidad = new BusEspecialidad();
                response = busEspecialidad.BSaveEspecialidad(entEspecialidad);
            }
            catch (Exception ex)
            {
                response.Code = 67823458455466;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar la especialidad.";

                logger.Error(IMDSerialize.Serialize(67823458455466, $"Error en {metodo}([FromBody]EntEspecialidad entEspecialidad): {ex.Message}", entEspecialidad, ex, response));
            }
            return response;
        }

        [HttpGet]
        [Route("Api/Especialidad/Get/Registros")]
        public IMDResponse<List<EntEspecialidad>> CGetEspecialidad([FromUri]int? piIdEspecialidad = null)
        {
            IMDResponse<List<EntEspecialidad>> response = new IMDResponse<List<EntEspecialidad>>();

            string metodo = nameof(this.CGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458453135, $"Inicia {metodo}([FromUri]int? piIdEspecialidad = null)", piIdEspecialidad));

            try
            {
                BusEspecialidad busEspecialidad = new BusEspecialidad();
                response = busEspecialidad.BGetEspecialidad(piIdEspecialidad);
            }
            catch (Exception ex)
            {
                response.Code = 67823458453912;
                response.Message = "Ocurrió un error inesperado en el servicio al consultar las especialidades médicas";

                logger.Error(IMDSerialize.Serialize(67823458453912, $"Error en {metodo}([FromUri]int? piIdEspecialidad = null): {ex.Message}", piIdEspecialidad, ex, response));
            }
            return response;
        }
    }
}
