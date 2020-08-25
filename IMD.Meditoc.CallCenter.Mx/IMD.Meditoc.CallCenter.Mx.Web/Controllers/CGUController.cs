using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class CGUController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CGUController));


        [HttpPost]
        [Route("Api/CGU/Create/Modulo")]
        public IMDResponse<bool> CCreateModulo([FromBody] EntModulo entCreateModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();            

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntCreateModulo entCreateModulo)", entCreateModulo));

            try
            {
                BusModulo busModulo = new BusModulo();
                response = busModulo.BSaveModulo(entCreateModulo);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntCreateModulo entCreateModulo): {ex.Message}", entCreateModulo, ex, response));
            }

            return response;
        }

        [HttpPost]
        [Route("Api/CGU/Create/SubModulo")]
        public IMDResponse<bool> CCreateSubModulo([FromBody] EntSubModulo entCreateSubModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntSubModulo entCreateSubModulo)", entCreateSubModulo));

            try
            {
                BusSubModulo busSubModulo = new BusSubModulo();
                response = busSubModulo.BSaveSubModulo(entCreateSubModulo);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntSubModulo entCreateSubModulo): {ex.Message}", entCreateSubModulo, ex, response));
            }

            return response;
        }


        [HttpPost]
        [Route("Api/CGU/Create/Boton")]
        public IMDResponse<bool> CCreateBoton([FromBody] EntBoton entBoton)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntBoton entBoton)", entBoton));

            try
            {
                BusBoton busBoton = new BusBoton();
                response = busBoton.BSaveBoton(entBoton);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntBoton entBoton): {ex.Message}", entBoton, ex, response));
            }

            return response;
        }
    }
}