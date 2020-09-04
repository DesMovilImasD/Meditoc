using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class EmpresaController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CGUController));

        [HttpPost]
        [Route("Api/Empresa/Create/Empresa")]
        public IMDResponse<EntEmpresa> CSaveEmpresa([FromBody] EntEmpresa entEmpresa)
        {
            IMDResponse<EntEmpresa> response = new IMDResponse<EntEmpresa>();

            string metodo = nameof(this.CSaveEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458388644, $"Inicia {metodo}"));

            try
            {
                BusEmpresa busEmpresa = new BusEmpresa();

                response = busEmpresa.BSaveEmpresa(entEmpresa);
            }
            catch (Exception ex)
            {
                response.Code = 67823458389421;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458389421, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }


        [HttpGet]
        [Route("Api/Empresa/Get/GetEmpresas")]
        public IMDResponse<List<EntEmpresa>> BGetEmpresas([FromUri] int? iIdEmpresa = null)
        {
            IMDResponse<List<EntEmpresa>> response = new IMDResponse<List<EntEmpresa>>();

            string metodo = nameof(this.BGetEmpresas);
            logger.Info(IMDSerialize.Serialize(67823458411954, $"Inicia {metodo}"));

            try
            {
                BusEmpresa busEmpresa = new BusEmpresa();

                response = busEmpresa.BGetEmpresas(iIdEmpresa);
            }
            catch (Exception ex)
            {
                response.Code = 67823458412731;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458412731, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
