using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Correo;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusUsuario
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusUsuario));
        DatUsuario datUsuario;

        public BusUsuario()
        {
            datUsuario = new DatUsuario();
        }

        /// <summary>
        /// Agrega/actualiza/elimina un usuario de la base
        /// </summary>
        /// <param name="entUsuario"></param>
        /// <param name="bEnviarCredenciales"></param>
        /// <returns></returns>
        public IMDResponse<EntUsuario> BSaveUsuario(EntUsuario entUsuario, bool bEnviarCredenciales = false)
        {
            IMDResponse<EntUsuario> response = new IMDResponse<EntUsuario>();

            string metodo = nameof(this.BSaveUsuario);
            logger.Info(IMDSerialize.Serialize(67823458344355, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));

            try
            {
                if (entUsuario == null)
                {
                    response.Code = 67823458345132;
                    response.Message = "No se ingresó información del usuario.";
                    return response;
                }

                if (entUsuario.bActivo == true && entUsuario.bBaja == false)
                {
                    //Validar datos y encriptar contraseña sólo si se trata de un guardado o actualización
                    IMDResponse<bool> resValidaDatos = BValidaDatos(entUsuario);
                    if (!resValidaDatos.Result)
                    {
                        return resValidaDatos.GetResponse<EntUsuario>();
                    }
                    if (!string.IsNullOrWhiteSpace(entUsuario.sPassword))
                    {
                        entUsuario.sPassword = BEncodePassword(entUsuario.sPassword);
                    }
                }

                IMDResponse<DataTable> resSaveUsuario = datUsuario.DSaveUsuario(entUsuario);
                if (resSaveUsuario.Code != 0)
                {
                    return resSaveUsuario.GetResponse<EntUsuario>();
                }

                //Enviar credenciales por correo sólo si es un nuevo usuario, la contraseña ha sido actualizada, o se ha pasado de un estado SIN ACCESO a CON ACCESO
                if (bEnviarCredenciales && (entUsuario.iIdUsuario == 0 || !string.IsNullOrWhiteSpace(entUsuario.sPassword)))
                {
                    List<string> users = new List<string> { entUsuario.sUsuario };
                    IMDResponse<bool> resEnviarCredenciales = this.BEnviarCredenciales(entUsuario.sCorreo, entUsuario.iIdUsuario == 0 ? EnumEmailActionPass.Crear : EnumEmailActionPass.Modificar, users);
                }

                IMDDataRow dr = new IMDDataRow(resSaveUsuario.Result.Rows[0]);
                entUsuario.iIdUsuario = dr.ConvertTo<int>("iIdUsuario");

                response.Code = 0;
                response.Message = entUsuario.iIdUsuario == 0 ? "El usuario ha sido guardado correctamente." : !entUsuario.bActivo ? "El usuario ha sido eliminado correctamente." : "El usuario ha sido actualizado correctamente.";
                response.Result = entUsuario;
            }
            catch (Exception ex)
            {
                response.Code = 67823458345132;
                response.Message = "Ocurrió un error inesperado al guardar los datos del usuario.";

                logger.Error(IMDSerialize.Serialize(67823458345132, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene la lista de usuarios del sistema
        /// </summary>
        /// <param name="iIdUsuario"></param>
        /// <param name="iIdTipoCuenta"></param>
        /// <param name="iIdPerfil"></param>
        /// <param name="sUsuario"></param>
        /// <param name="sPassword"></param>
        /// <param name="bActivo"></param>
        /// <param name="bBaja"></param>
        /// <param name="psCorreo"></param>
        /// <returns></returns>
        public IMDResponse<List<EntUsuario>> BObtenerUsuario(int? iIdUsuario = null, int? iIdTipoCuenta = null, int? iIdPerfil = null, string sUsuario = null, string sPassword = null, bool? bActivo = null, bool? bBaja = null, string psCorreo = null)
        {
            IMDResponse<List<EntUsuario>> response = new IMDResponse<List<EntUsuario>>();

            string metodo = nameof(this.BObtenerUsuario);
            logger.Info(IMDSerialize.Serialize(67823458362226, $"Inicia {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja)", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja));

            try
            {
                //Consultar usuarios
                IMDResponse<DataTable> dtUsuario = datUsuario.DObtenerUsuario(iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja, psCorreo);
                if (dtUsuario.Code != 0)
                {
                    return dtUsuario.GetResponse<List<EntUsuario>>();
                }

                List<EntUsuario> lstUsuario = new List<EntUsuario>();
                foreach (DataRow item in dtUsuario.Result.Rows)
                {

                    IMDDataRow dr = new IMDDataRow(item);
                    EntUsuario entUsuario = new EntUsuario();

                    entUsuario.iIdUsuario = dr.ConvertTo<int>("iIdUsuario");
                    entUsuario.iIdTipoCuenta = dr.ConvertTo<int>("iIdTipoCuenta");
                    entUsuario.sTipoCuenta = dr.ConvertTo<string>("sTipoCuenta");
                    entUsuario.iIdPerfil = dr.ConvertTo<int>("iIdPerfil");
                    entUsuario.sPerfil = dr.ConvertTo<string>("sPerfil");
                    entUsuario.sUsuario = dr.ConvertTo<string>("sUsuario");
                    entUsuario.sPassword = dr.ConvertTo<string>("sPassword");
                    entUsuario.sNombres = dr.ConvertTo<string>("sNombres");
                    entUsuario.sApellidoPaterno = dr.ConvertTo<string>("sApellidoPaterno");
                    entUsuario.sApellidoMaterno = dr.ConvertTo<string>("sApellidoMaterno");
                    entUsuario.dtFechaNacimiento = dr.ConvertTo<DateTime?>("dtFechaNacimiento");
                    entUsuario.sFechaNacimiento = entUsuario.dtFechaNacimiento?.ToString("dd/MM/yyyy");
                    entUsuario.dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion");
                    entUsuario.sFechaCreacion = entUsuario.dtFechaCreacion.ToString("dd/MM/yyyy HH:mm");
                    entUsuario.sTelefono = dr.ConvertTo<string>("sTelefono");
                    entUsuario.sCorreo = dr.ConvertTo<string>("sCorreo");
                    entUsuario.sDomicilio = dr.ConvertTo<string>("sDomicilio");
                    entUsuario.iIdUsuarioMod = dr.ConvertTo<int>("iIdUsuarioMod");
                    entUsuario.bAcceso = Convert.ToBoolean(dr.ConvertTo<int>("bAcceso"));
                    entUsuario.bActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo"));
                    entUsuario.bBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja"));

                    lstUsuario.Add(entUsuario);
                }


                response.Message = "Los usuarios del sistema han sido obtenidos.";
                response.Result = lstUsuario;
            }
            catch (Exception ex)
            {
                response.Code = 67823458363003;
                response.Message = "Ocurrió un error inesperado al consultar los usuarios del sistema.";

                logger.Error(IMDSerialize.Serialize(67823458363003, $"Error en {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja): {ex.Message}", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Valida los datos antes de guardar un usuario.
        /// </summary>
        /// <param name="entUsuario"></param>
        /// <returns></returns>
        public IMDResponse<bool> BValidaDatos(EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458345132, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));
            try
            {

                if (string.IsNullOrWhiteSpace(entUsuario.sNombres))
                {
                    response.Code = 67823458345132;
                    response.Message = "El nombre del usuario no puede ser vacío";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.iIdPerfil == 0)
                {
                    response.Code = 67823458345132;
                    response.Message = "No se ha especificado un perfil para el usuario.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.bAcceso == true)
                {
                    //Validar usuario sólo si se dan permisos de acceso al sistema
                    if (string.IsNullOrWhiteSpace(entUsuario.sUsuario))
                    {
                        response.Code = 67823458345132;
                        response.Message = "El nombre de usuario de acceso no puede ser vacío.";
                        response.Result = false;

                        return response;
                    }
                }


                if (entUsuario.iIdUsuario == 0 && entUsuario.bAcceso == true)
                {
                    //Validar usuario sólo si se trata de un nuevo registro con acceso al sistema
                    response = datUsuario.DValidaUsuarioYCorreo(entUsuario.sUsuario, "", true);
                    if (!response.Result)
                    {
                        response.Code = 67823458345132;
                        response.Message = "Ya existe un usuario registrado con el nombre de usuario de acceso proporcionado.";
                        response.Result = false;

                        return response;
                    }
                }

                if (string.IsNullOrWhiteSpace(entUsuario.sPassword))
                {
                    if (entUsuario.iIdUsuario == 0 && entUsuario.bAcceso == true)
                    {
                        //Validar la contraseña sólo si es nuevo ingreso y la contraseña contiene valor
                        response.Code = 67823458345132;
                        response.Message = "La contraseña del usuario no puede ser vacío.";
                        response.Result = false;

                        return response;
                    }
                }

                if (string.IsNullOrWhiteSpace(entUsuario.sApellidoPaterno))
                {
                    response.Code = 67823458345132;
                    response.Message = "El apellido paterno del usuario no puede ser vacío.";
                    response.Result = false;

                    return response;
                }

                //if (entUsuario.sApellidoMaterno == "")
                //{
                //    response.Code = 67823458345132;
                //    response.Message = "El apellido materno del usuario no puede ser vacio.";
                //    response.Result = false;

                //    return response;
                //}

                if (string.IsNullOrWhiteSpace(entUsuario.sCorreo))
                {
                    response.Code = 67823458345132;
                    response.Message = "El correo del usuario no puede ser vacío.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.iIdTipoCuenta == (int)EnumTipoCuenta.Titular)
                {
                    if (entUsuario.iIdUsuario == 0 && entUsuario.bAcceso == true)
                    {
                        //Validar correo sólo a usuarios titulares nuevos con acceso al sistema
                        response = datUsuario.DValidaUsuarioYCorreo("", entUsuario.sCorreo, false);
                        if (!response.Result)
                        {
                            response.Code = 67823458345132;
                            response.Message = "Ya existe un usuario registrado con el correo electrónico proporcionado.";
                            response.Result = false;

                            return response;
                        }
                    }
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458342024;
                response.Message = "Ocurrió un error al intentar validar los datos del usuario.";

                logger.Error(IMDSerialize.Serialize(67823458341247, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Cambiar la contraseña desde el portal CallCenter
        /// </summary>
        /// <param name="iIdUsuario"></param>
        /// <param name="sPassword"></param>
        /// <param name="iIdUsuarioUltMod"></param>
        /// <returns></returns>
        public IMDResponse<bool> BCambiarContrasenia(int iIdUsuario, string sPassword, int iIdUsuarioUltMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BCambiarContrasenia);
            logger.Info(IMDSerialize.Serialize(67823458369996, $"Inicia {metodo}(int iIdUsuario, string sPassword, int iIdUsuarioUltMod)", iIdUsuario, sPassword, iIdUsuarioUltMod));

            try
            {

                sPassword = BEncodePassword(sPassword);

                //Actualizar la contraseña
                response = datUsuario.DCambiarContrasenia(iIdUsuario, sPassword, iIdUsuarioUltMod);

                if (response.Code != 0)
                {
                    return response;
                }

                response.Result = true;
                response.Message = "La contraseña ha sido actualizada correctamente.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458370773;
                response.Message = "Ocurrió un error inesperado al intentar actualizar la contraseña.";

                logger.Error(IMDSerialize.Serialize(67823458370773, $"Error en {metodo}(int iIdUsuario, string sPassword, int iIdUsuarioUltMod): {ex.Message}", iIdUsuario, sPassword, iIdUsuarioUltMod, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Iniciar sesión en el portal administrativo/Callcenter
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public IMDResponse<EntUsuario> BLogin(string sUsuario, string sPassword)
        {
            IMDResponse<EntUsuario> response = new IMDResponse<EntUsuario>();

            string metodo = nameof(this.BLogin);
            logger.Info(IMDSerialize.Serialize(67823458374658, $"Inicia {metodo}(string sUsuario, string sPassword)", sUsuario, sPassword));

            try
            {
                sPassword = BEncodePassword(sPassword);

                IMDResponse<DataTable> dtUsuario = datUsuario.DLogin(sUsuario, sPassword);

                if (dtUsuario.Code != 0)
                {
                    return dtUsuario.GetResponse<EntUsuario>();
                }

                if (dtUsuario.Result.Rows.Count != 1)
                {
                    response.Code = 78772637586;
                    response.Message = "Usuario o contraseña incorrecta.";
                    return response;
                }

                EntUsuario entUsuario = new EntUsuario();

                foreach (DataRow item in dtUsuario.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);

                    entUsuario.iIdUsuario = dr.ConvertTo<int>("iIdUsuario");
                    entUsuario.iIdTipoCuenta = dr.ConvertTo<int>("iIdTipoCuenta");
                    entUsuario.sTipoCuenta = dr.ConvertTo<string>("sTipoCuenta");
                    entUsuario.iIdPerfil = dr.ConvertTo<int>("iIdPerfil");
                    entUsuario.sPerfil = dr.ConvertTo<string>("sPerfil");
                    entUsuario.sUsuario = dr.ConvertTo<string>("sUsuario");
                    //entUsuario.sPassword = dr.ConvertTo<string>("sPassword");
                    entUsuario.sNombres = dr.ConvertTo<string>("sNombres");
                    entUsuario.sApellidoPaterno = dr.ConvertTo<string>("sApellidoPaterno");
                    entUsuario.sApellidoMaterno = dr.ConvertTo<string>("sApellidoMaterno");
                    entUsuario.dtFechaNacimiento = dr.ConvertTo<DateTime>("dtFechaNacimiento");
                    entUsuario.sTelefono = dr.ConvertTo<string>("sTelefono");
                    entUsuario.sCorreo = dr.ConvertTo<string>("sCorreo");
                    entUsuario.sDomicilio = dr.ConvertTo<string>("sDomicilio");
                    entUsuario.iIdUsuarioMod = dr.ConvertTo<int>("iIdUsuarioMod");
                    entUsuario.bActivo = dr.ConvertTo<bool>("bActivo");
                    entUsuario.bBaja = dr.ConvertTo<bool>("bBaja");
                }

                response.Result = entUsuario;
                response.Message = "Inicio de sesión existoso.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458375435;
                response.Message = "Ocurrió un error inesperado al iniciar sesión";

                logger.Error(IMDSerialize.Serialize(67823458375435, $"Error en {metodo}(string sUsuario, string sPassword): {ex.Message}", sUsuario, sPassword, ex, response));
            }
            return response;
        }

        public string BEncodePassword(string sPassWord)
        {
            IMDResponse<string> response;
            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BEncrypt(sPassWord, "M3diT0cPassword1", "Evector1");
                if (string.IsNullOrWhiteSpace(response.Result))
                {
                    throw new Exception("Ocurrió un error al intentar verificar la información de seguridad");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return response.Result;
        }

        public string BEncodePassword(string sCadena, string sKey, string sVector)
        {
            IMDResponse<string> response = new IMDResponse<string>();
            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BEncrypt(sCadena, sKey, sVector);

            }
            catch (Exception)
            {

                throw;
            }
            return response.Result;
        }

        public string BDeCodePassWord(string sPassWord)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BDeCodePassWord);
            //logger.Info(IMDSerialize.Serialize(67823458366888, $"Inicia {metodo}(string sPassWord)", sPassWord));

            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BDecrypt(sPassWord, "M3diT0cPassword1", "Evector1");

                sPassWord = response.Result;
            }
            catch (Exception ex)
            {
                response.Code = 67823458367665;
                response.Message = "Ocurrió un error al intentar verificar la información de seguridad";

                logger.Error(IMDSerialize.Serialize(67823458367665, $"Error en {metodo}(string sPassWord): {ex.Message}", sPassWord, ex, response));
            }
            return sPassWord;
        }

        public string BDeCodePassWord(string sCadena, string sKey, string sVector)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BDeCodePassWord);
            //logger.Info(IMDSerialize.Serialize(67823458366888, $"Inicia {metodo}(string sCadena, string sKey, string sVector)", sCadena, sKey, sVector));

            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BDecrypt(sCadena, sKey, sVector);

                sCadena = response.Result;
            }
            catch (Exception ex)
            {
                response.Code = 67823458367665;
                response.Message = "Ocurrió un error al intentar verificar la información de seguridad";

                logger.Error(IMDSerialize.Serialize(67823458367665, $"Error en {metodo}(string sCadena, string sKey, string sVector): {ex.Message}", sCadena, sKey, sVector, ex, response));
            }
            return sCadena;
        }

        /// <summary>
        /// Envía las credenciales al correo del usuario
        /// </summary>
        /// <param name="psCorreo"></param>
        /// <param name="enumEmail"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public IMDResponse<bool> BEnviarCredenciales(string psCorreo, EnumEmailActionPass enumEmail, List<string> users = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEnviarCredenciales);
            logger.Info(IMDSerialize.Serialize(67823458631845, $"Inicia {metodo}(string psCorreo)", psCorreo));

            try
            {
                if (string.IsNullOrWhiteSpace(psCorreo))
                {
                    response.Code = -23746876326;
                    response.Message = "El correo electrónico es requerido.";
                    return response;
                }

                //Obtener datos del usuario
                IMDResponse<List<EntUsuario>> resGetUser = this.BObtenerUsuario(psCorreo: psCorreo);
                if (resGetUser.Code != 0)
                {
                    return resGetUser.GetResponse<bool>();
                }

                if (resGetUser.Result.Count < 1)
                {
                    response.Code = -8767263467;
                    response.Message = "El correo electrónico no se encuentra registrado en el sistema";
                    return response;
                }

                string cuenta = string.Empty;
                List<EntUsuario> currentUsers = resGetUser.Result;
                if (users != null)
                {
                    currentUsers = currentUsers.Where(x => users.Contains(x.sUsuario)).ToList();
                }

                //Armar la lista de usuarios enlazados al correo electrónico
                foreach (EntUsuario user in currentUsers)
                {
                    if (user.bAcceso == null || user.bAcceso == false || !user.bActivo || user.bBaja)
                    {
                        continue;
                    }
                    string pass = this.BDeCodePassWord(user.sPassword);
                    cuenta += $"<tr class=\"font-table bold small center table-border-b\"><td>{user.sTipoCuenta}</td><td>{user.sUsuario}</td><td>{pass}</td></tr>";
                }

                if (string.IsNullOrWhiteSpace(cuenta))
                {
                    response.Code = -834787687623;
                    response.Message = "No se encontraron cuentas activas para el usuario.";
                    return response;
                }

                EntUsuario entUsuario = resGetUser.Result.First();

                //Armar asunto y cupero del correo
                string asunto = string.Empty;
                string titulo = string.Empty;
                string header = string.Empty;
                string footer = string.Empty;
                switch (enumEmail)
                {
                    case EnumEmailActionPass.Crear:
                        asunto = "Meditoc - Credenciales de acceso";
                        titulo = "Bienvenido a Meditoc Call Center";
                        header = "Se han creado las credenciales de acceso al portal de MeditocCallCenter:";
                        footer = "Le sugerimos cambiar la contraseña en su próximo ingreso al portal de MeditocCallCenter.";
                        break;
                    case EnumEmailActionPass.Modificar:
                        asunto = "Meditoc - Cambio en las credenciales";
                        titulo = "Modificación en las credenciales de acceso";
                        header = "Se han modificado las credenciales de acceso al portal de MeditocCallCenter:";
                        footer = "Si no realizó esta acción, cambie sus credenciales en su próximo ingreso o contacte a su administrador.";
                        break;
                    case EnumEmailActionPass.Recuperar:
                        asunto = "Meditoc - Recuperación de cuenta";
                        titulo = "Recuperación de la cuenta";
                        header = "Se ha solicitado la recuperación de las credenciales de acceso al portal de MeditocCallCenter:";
                        footer = "Le sugerimos cambiar la contraseña en su próximo ingreso al portal de MeditocCallCenter.";
                        break;
                    default:
                        break;
                }

                //Preparar correo
                string plantillaBody = "<!DOCTYPE html><html><head><meta charset=\"utf-8;\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><link href=\"https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap\" rel=\"stylesheet\" /><style>body {font-family: Roboto, \"Segoe UI\", Tahoma, Geneva, Verdana, sans-serif;margin: 0;}.center {text-align: center !important;}.light {font-weight: 300;}.normal {font-weight: normal;}.bold {font-weight: 500;}.small {font-size: 12px;}.large {font-size: 15px;}.font-default {color: #707070;}.font-secondary {color: #115c8a;}.font-unset {color: #ffffff;}.font-table {color: #878787;}.table {margin: auto;width: 100%;max-width: 800px;border: 1px solid #dddddd;border-spacing: 0px;border-collapse: 0px;}.table td {padding: 6px 0px;}.logo-head {background-color: #11b6ca;padding: 5px 0px;}.table-content {margin: auto;width: 90%;border-collapse: collapse;}.table-detail {margin: auto;width: 100%;border-collapse: collapse;}.table-detail td {padding: 8px;}.head-detail {background-color: #115c8a;}.divider {height: 1px;border: 0;background-color: #989898;}.link {text-decoration: none;}.link:hover {text-decoration: underline;}.link-none {text-decoration: none;}.table-border-b td {border-bottom: 1px solid #ccc;}</style></head><body><table class=\"table\"><tr><td class=\"logo-head center\"><img alt=\"logo-meditoc\" src=\"sLogoMeditoc\" height=\"50px\" /></td></tr><tr><td><table class=\"table-content\"><tr><td class=\"center\"><span class=\"font-default bold large\">data.titulo</span></td></tr><tr class=\"center\"><td><span class=\"font-default normal large\">data.header</span></td></tr><tr><td><table class=\"table-detail\"><tr class=\"head-detail font-unset bold small center\"><td colspan=\"3\">ACCESO</td></tr><tr><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Tipo de cuenta</th><th>Usuario</th><th>Contraseña</th></tr></thead><tbody>data.cuenta</tbody></table></td></tr></table></td></tr><tr class=\"center\"><td><p><span class=\"font-default normal large\">data.footer</span></p></td></tr><tr><td><hr class=\"divider\" /></td></tr><tr><td><span class=\"font-default light small\">De conformidad con la ley federal de protección de datos personales en posesión delos particulares, ponemos a su disposición nuestro&nbsp;<a href=\"sAvisoPrivacidad\" class=\"link font-secondary normal\"> Aviso de Privacidad </a>&nbsp;y&nbsp;<a href=\"sTerminosCondiciones\" class=\"link font-secondary normal\"> Términos y Condiciones. </a></span></td></tr></table></td></tr></table></body></html>";

                plantillaBody = plantillaBody.Replace("data.cuenta", cuenta);
                plantillaBody = plantillaBody.Replace("data.titulo", titulo);
                plantillaBody = plantillaBody.Replace("data.header", header);
                plantillaBody = plantillaBody.Replace("data.footer", footer);
                plantillaBody = plantillaBody.Replace("sLogoMeditoc", ConfigurationManager.AppSettings["sLogoMeditoc"]);
                plantillaBody = plantillaBody.Replace("sAvisoPrivacidad", ConfigurationManager.AppSettings["sAvisoDePrivacidad"]);
                plantillaBody = plantillaBody.Replace("sTerminosCondiciones", ConfigurationManager.AppSettings["sTerminosYCondiciones"]);

                //Enviar correo
                BusCorreo busCorreo = new BusCorreo();
                busCorreo.BEnviarEmail("", "", "", asunto, plantillaBody, entUsuario.sCorreo, "", "");

                response.Code = 0;
                response.Result = true;
                response.Message = "Las credenciales se han enviado al correo proporcionado.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458632622;
                response.Message = "Ocurrió un error inesperado al recuperar la contraseña. Intenta más tarde.";

                logger.Error(IMDSerialize.Serialize(67823458632622, $"Error en {metodo}(string psCorreo): {ex.Message}", psCorreo, ex, response));
            }
            return response;
        }
    }
}
