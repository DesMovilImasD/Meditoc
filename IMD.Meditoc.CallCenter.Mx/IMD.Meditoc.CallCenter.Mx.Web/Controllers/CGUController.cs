using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        [Route("Api/CGU/Create/Perfil")]
        public IMDResponse<bool> CCreatePerfil([FromBody] EntPerfil entPerfil)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntPerfil entPerfil)", entPerfil));

            try
            {
                BusPerfil busPerfil = new BusPerfil();
                response = busPerfil.BSavePerfil(entPerfil);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntPerfil entPerfil): {ex.Message}", entPerfil, ex, response));
            }

            return response;
        }

        [HttpPost]
        [Route("Api/CGU/Create/Usuario")]
        public IMDResponse<bool> CCreateUsuario([FromBody] EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntUsuario entUsuario)", entUsuario));

            try
            {
                BusUsuario busUsuario = new BusUsuario();
                response = busUsuario.DSaveUsuario(entUsuario);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }

            return response;
        }

        [HttpPost]
        [Route("Api/CGU/GET/PermisoXPerfil")]
        public IMDResponse<List<EntPermisoSistema>> CObtenerPermisoxPerfil([FromUri] int? iIdPerfil = null)
        {
            IMDResponse<List<EntPermisoSistema>> response = new IMDResponse<List<EntPermisoSistema>>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458352902, $"Inicia {metodo}([FromBody] int? iIdPerfil)", iIdPerfil));

            try
            {
                BusPermiso busPermiso = new BusPermiso();
                response = busPermiso.BObtenerPermisoxPerfil(iIdPerfil);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458352902, $"Error en {metodo}([FromBody] int? iIdPerfil): {ex.Message}", iIdPerfil, ex, response));
            }

            return response;
        }

        [HttpPost]
        [Route("Api/CGU/Create/Permiso")]
        public IMDResponse<bool> CCreatePermiso([FromBody] EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntPermiso entPermiso)", entPermiso));

            try
            {
                BusPermiso busPermiso = new BusPermiso();
                response = busPermiso.DSavePermiso(entPermiso);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
            }

            return response;
        }
    }
}