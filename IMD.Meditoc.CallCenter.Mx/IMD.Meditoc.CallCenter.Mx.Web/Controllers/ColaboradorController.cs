using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
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
    public class ColaboradorController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(EspecialidadController));

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
    }
}
