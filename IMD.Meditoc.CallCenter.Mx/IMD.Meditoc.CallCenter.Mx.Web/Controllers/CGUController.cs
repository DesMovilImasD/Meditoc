using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class CGUController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CGUController));

        #region Modulo
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
        #endregion

        #region SubModulo
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
        #endregion

        #region Boton
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
        #endregion

        #region Perfil

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


        #endregion

        #region Usuario

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

        [HttpGet]
        [Route("Api/CGU/Get/Usuarios")]
        public IMDResponse<List<EntUsuario>> CObtenerUsuario([FromUri] int? iIdUsuario = null, int? iIdTipoCuenta = null, int? iIdPerfil = null, string sUsuario = null, string sPassword = null, bool? bActivo = null, bool? bBaja = null)
        {
            IMDResponse<List<EntUsuario>> response = new IMDResponse<List<EntUsuario>>();

            string metodo = nameof(this.CObtenerUsuario);
            logger.Info(IMDSerialize.Serialize(67823458363780, $"Inicia {metodo}([FromUri] int? iIdUsuario = null, int? iIdTipoCuenta = null, int? iIdPerfil = null, string sUsuario = null, string sPassword = null, bool? bActivo = null, bool? bBaja = null)", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja));

            try
            {
                BusUsuario busUsuario = new BusUsuario();

                response = busUsuario.BObtenerUsuario(iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja);

            }
            catch (Exception ex)
            {
                response.Code = 67823458364557;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458364557, $"Error en {metodo}([FromUri] int? iIdUsuario = null, int? iIdTipoCuenta = null, int? iIdPerfil = null, string sUsuario = null, string sPassword = null, bool? bActivo = null, bool? bBaja = null): {ex.Message}", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CGU/Create/CambiarContrasenia")]
        public IMDResponse<bool> CCambiarContrasenia([FromBody] int iIdUsuario, string sPassword, int iIdUsuarioUltMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCambiarContrasenia);
            logger.Info(IMDSerialize.Serialize(67823458371550, $"Inicia {metodo}"));

            try
            {
                BusUsuario busUsuario = new BusUsuario();
                response = busUsuario.BCambiarContrasenia(iIdUsuario, sPassword, iIdUsuario);

            }
            catch (Exception ex)
            {
                response.Code = 67823458372327;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458372327, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
        #endregion

        #region Permiso        

        [HttpGet]
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
        public IMDResponse<bool> CCreatePermiso([FromBody] List<EntPermiso> entPermisos)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntPermiso entPermiso)", entPermisos));

            try
            {
                BusPermiso busPermiso = new BusPermiso();
                response = busPermiso.DSavePermiso(entPermisos);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntPermiso entPermiso): {ex.Message}", entPermisos, ex, response));
            }

            return response;
        }

        [HttpGet]
        [Route("Api/CGU/Get/Perfiles")]
        public IMDResponse<List<EntPerfil>> CObtenerPerfil([FromUri] int? iIdPerfil, bool bActivo, bool bBaja)
        {
            IMDResponse<List<EntPerfil>> response = new IMDResponse<List<EntPerfil>>();

            string metodo = nameof(this.CObtenerPerfil);
            logger.Info(IMDSerialize.Serialize(67823458359118, $"Inicia {metodo}([FromBody] int? iIdPerfil, bool bActivo, bool bBaja)", iIdPerfil, bActivo, bBaja));

            try
            {
                BusPerfil busPerfil = new BusPerfil();

                response = busPerfil.BObtenerPerfil(iIdPerfil, bActivo, bBaja);

            }
            catch (Exception ex)
            {
                response.Code = 67823458359895;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458359895, $"Error en {metodo}([FromBody] int? iIdPerfil, bool bActivo, bool bBaja): {ex.Message}", iIdPerfil, bActivo, bBaja, ex, response));
            }
            return response;
        }
        #endregion

    }
}