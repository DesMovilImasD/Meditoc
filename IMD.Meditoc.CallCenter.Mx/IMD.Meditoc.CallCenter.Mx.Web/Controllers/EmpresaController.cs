using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using log4net;
using System;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class EmpresaController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CGUController));

        [HttpPost]
        [Route("Api/Empresa/Create/Empresa")]
        public IMDResponse<bool> CSaveEmpresa([FromBody] EntEmpresa entEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CSaveEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458388644, $"Inicia {metodo}"));

            try
            {
                BusEmpresa busEmpresa = new BusEmpresa();

                busEmpresa.BSaveEmpresa(entEmpresa);
            }
            catch (Exception ex)
            {
                response.Code = 67823458389421;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458389421, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
