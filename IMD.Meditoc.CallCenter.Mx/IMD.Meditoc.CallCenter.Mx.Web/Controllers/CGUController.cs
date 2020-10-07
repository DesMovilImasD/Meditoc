using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using IMD.Meditoc.CallCenter.Mx.Web.Tokens;
using log4net;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class CGUController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CGUController));

        #region Modulo
        [MeditocAuthentication]
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
                response.Message = "Ocurrió un error inesperado en el servicio al guardar el módulo.";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntCreateModulo entCreateModulo): {ex.Message}", entCreateModulo, ex, response));
            }

            return response;
        }
        #endregion

        #region SubModulo
        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/CGU/Create/SubModulo")]
        public IMDResponse<bool> CCreateSubModulo([FromBody] EntSubModulo entCreateSubModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntSubModulo entCreateSubModulo)", entCreateSubModulo));

            try
            {
                BusSubmodulo busSubModulo = new BusSubmodulo();
                response = busSubModulo.BSaveSubModulo(entCreateSubModulo);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar el submódulo.";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntSubModulo entCreateSubModulo): {ex.Message}", entCreateSubModulo, ex, response));
            }

            return response;
        }
        #endregion

        #region Boton
        [MeditocAuthentication]
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
                response.Message = "Ocurrió un error inesperado en el servicio al guardar el botón.";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntBoton entBoton): {ex.Message}", entBoton, ex, response));
            }

            return response;
        }
        #endregion

        #region Perfil

        [MeditocAuthentication]
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
                response.Message = "Ocurrió un error inesperado en el servicio al guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntPerfil entPerfil): {ex.Message}", entPerfil, ex, response));
            }

            return response;
        }


        #endregion

        #region Usuario

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/CGU/Create/Usuario")]
        public IMDResponse<EntUsuario> CCreateUsuario([FromBody] EntUsuario entUsuario)
        {
            IMDResponse<EntUsuario> response = new IMDResponse<EntUsuario>();

            string metodo = nameof(this.CCreateModulo);
            logger.Info(IMDSerialize.Serialize(67823458338139, $"Inicia {metodo}([FromBody]EntUsuario entUsuario)", entUsuario));

            try
            {
                BusUsuario busUsuario = new BusUsuario();
                response = busUsuario.BSaveUsuario(entUsuario);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar el usuario.";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }

            return response;
        }

        [MeditocAuthentication]
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
                response.Message = "Ocurrió un error inesperado en el servicio al consultar los usuarios del sistema.";

                logger.Error(IMDSerialize.Serialize(67823458364557, $"Error en {metodo}([FromUri] int? iIdUsuario = null, int? iIdTipoCuenta = null, int? iIdPerfil = null, string sUsuario = null, string sPassword = null, bool? bActivo = null, bool? bBaja = null): {ex.Message}", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja, ex, response));
            }
            return response;
        }

        [MeditocAuthentication]
        [HttpPost]
        [Route("Api/CGU/Create/CambiarContrasenia")]
        public IMDResponse<bool> CCambiarContrasenia([FromUri] int iIdUsuario, [FromUri]string sPassword, [FromUri]int iIdUsuarioUltMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CCambiarContrasenia);
            logger.Info(IMDSerialize.Serialize(67823458371550, $"Inicia {metodo}([FromUri] int iIdUsuario, [FromUri]string sPassword, [FromUri]int iIdUsuarioUltMod)", iIdUsuario, iIdUsuarioUltMod));

            try
            {
                BusUsuario busUsuario = new BusUsuario();
                response = busUsuario.BCambiarContrasenia(iIdUsuario, sPassword, iIdUsuario);

            }
            catch (Exception ex)
            {
                response.Code = 67823458372327;
                response.Message = "Ocurrió un error inesperado en el servicio al cambiar la contraseña.";

                logger.Error(IMDSerialize.Serialize(67823458372327, $"Error en {metodo}([FromUri] int iIdUsuario, [FromUri]string sPassword, [FromUri]int iIdUsuarioUltMod): {ex.Message}", iIdUsuario, iIdUsuarioUltMod, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CGU/User/Login")]
        public IMDResponse<EntUsuario> CLogin([FromBody] EntUsuario entUsuario)
        {
            IMDResponse<EntUsuario> response = new IMDResponse<EntUsuario>();

            string metodo = nameof(this.CLogin);
            logger.Info(IMDSerialize.Serialize(67823458376212, $"Inicia {metodo}([FromUri] string sUsuario, string sPassword)", entUsuario));

            try
            {
                BusUsuario busUsuario = new BusUsuario();
                response = busUsuario.BLogin(entUsuario.sUsuario, entUsuario.sPassword);

            }
            catch (Exception ex)
            {
                response.Code = 67823458376989;
                response.Message = "Ocurrió un error inesperado en el servicio al validar los datos de la cuenta.";

                logger.Error(IMDSerialize.Serialize(67823458376989, $"Error en {metodo}([FromUri] string sUsuario, string sPassword): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }

        [HttpPost]
        [Route("Api/CGU/Recuperar/Password")]
        public IMDResponse<bool> CRecuperarPassword([FromBody]EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.CRecuperarPassword);
            logger.Info(IMDSerialize.Serialize(67823458633399, $"Inicia {metodo}"));

            try
            {
                BusUsuario busUsuario = new BusUsuario();
                response = busUsuario.BRecuperarPassword(entUsuario.sCorreo);
            }
            catch (Exception ex)
            {
                response.Code = 67823458634176;
                response.Message = "Ocurrió un error inesperado al recuperar la contraseña del usuario.";

                logger.Error(IMDSerialize.Serialize(67823458634176, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        #endregion

        #region Permiso        

        [MeditocAuthentication]
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
                response.Message = "Ocurrió un error inesperado en el servicio al consultar los perfiles del sistema.";

                logger.Error(IMDSerialize.Serialize(67823458352902, $"Error en {metodo}([FromBody] int? iIdPerfil): {ex.Message}", iIdPerfil, ex, response));
            }

            return response;
        }

        [MeditocAuthentication]
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
                response = busPermiso.BSavePermiso(entPermisos);
            }
            catch (Exception ex)
            {
                response.Code = 67823458122133;
                response.Message = "Ocurrió un error inesperado en el servicio al guardar los permisos solicitados.";

                logger.Error(IMDSerialize.Serialize(67823458338139, $"Error en {metodo}([FromBody]EntPermiso entPermiso): {ex.Message}", entPermisos, ex, response));
            }

            return response;
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("Api/CGU/Get/Perfiles")]
        public IMDResponse<List<EntPerfil>> CObtenerPerfil([FromUri] int? iIdPerfil, [FromUri]bool bActivo, [FromUri]bool bBaja)
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
                response.Message = "Ocurrió un error inesperado en el servicio al obtener los perfiles del sistema.";

                logger.Error(IMDSerialize.Serialize(67823458359895, $"Error en {metodo}([FromBody] int? iIdPerfil, bool bActivo, bool bBaja): {ex.Message}", iIdPerfil, bActivo, bBaja, ex, response));
            }
            return response;
        }
        #endregion

        [HttpGet]
        [Route("status")]
        public string CStatus()
        {
            return "SERVER OK";
        }

        [MeditocAuthentication]
        [HttpGet]
        [Route("status/auth")]
        public string CStatusAuth()
        {
            return "SERVER AUTH OK";
        }
    }
}