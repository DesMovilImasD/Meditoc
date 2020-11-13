using System;
using BC.CallCenterPortable.Models;
using BC.CallCenter.Models.BE;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.Common;
using BC.Clases;

namespace BC.CallCenter.Clases
{
    public class clsLogin
    {
        clsLoginBE oclsLoginBE;
        Database db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");
        //private clsCometChat oclsCometChat;
        private clsBitacora oclsBitacora = new clsBitacora();
        public clsLogin()
        {
        }

        /// <summary>
        /// Descripción: Método para realizar la autentificación del usuario solicitado.
        /// </summary>
        /// <param name="pobjLoginModel">Instancia del modelo de objeto.</param>
        public LoginModel m_Login(LoginModel pobjLoginModel)
        {
            LoginModel objclsLoginModel = new LoginModel();
            clsTblcatlada oLada = new clsTblcatlada();

            objclsLoginModel = pobjLoginModel;

            objclsLoginModel.bResult = false;

            try
            {
                oclsBitacora.m_Save(objclsLoginModel.sUsuarioLogin, "", objclsLoginModel.sUsuarioLogin, "Inicia Operación de Login.", false, objclsLoginModel.sLatitud + "," + objclsLoginModel.sLongitud);

                oclsLoginBE = new clsLoginBE();
                oclsLoginBE.sUsuarioLogin = objclsLoginModel.sUsuarioLogin;
                oclsLoginBE.sPasswordLogin = objclsLoginModel.sPasswordLogin;
                oclsLoginBE.sLongitud = objclsLoginModel.sLongitud;
                oclsLoginBE.sLatitud = objclsLoginModel.sLatitud;

                var sNumero = oclsLoginBE.sUsuarioLogin.Split('_');

                if (!oclsLoginBE.ObtenerGeometriaValida(db) && Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarGeolocalizacion"]))
                    throw new ArgumentException(ConfigurationManager.AppSettings["sMensajeGeometria"]);

                //if (!Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarGeolocalizacion"]))
                //    oLada.ValidarLada(sNumero[1].Substring(0, 3));

                if (oclsLoginBE.m_Login(db))
                {
                    objclsLoginModel.sUIDCliente = oclsLoginBE.sUIDCliente;
                    objclsLoginModel.sNombre = oclsLoginBE.sNombre;
                    objclsLoginModel.bDoctor = oclsLoginBE.bDoctor;
                    objclsLoginModel.sSexo = oclsLoginBE.sSexo;
                    objclsLoginModel.bResult = true;
                    objclsLoginModel.sTelefonoDRs = string.IsNullOrEmpty(ConfigurationManager.AppSettings["sTelefono"]) ? "" : ConfigurationManager.AppSettings["sTelefono"].ToString();
                    objclsLoginModel.iIdUsuario = oclsLoginBE.iIdUsuario;
                    objclsLoginModel.sFolio = oclsLoginBE.sFolio;
                    objclsLoginModel.sInstitucion = oclsLoginBE.sInstitucion;
                    objclsLoginModel.bAceptoTerminoCondicion = oclsLoginBE.bAceptoTerminoCondicion;

                }
                else
                {
                    if (!string.IsNullOrEmpty(oclsLoginBE.sMensajeRespuesta))
                        objclsLoginModel.sMensajeRespuesta = oclsLoginBE.sMensajeRespuesta;
                    else
                        objclsLoginModel.sMensajeRespuesta = "La contraseña ingresada no es válida.";
                }

            }
            catch (Exception ex)
            {
                objclsLoginModel.sMensajeRespuesta = ex.Message;
            }
            finally
            {
                if (!objclsLoginModel.bResult)
                    oclsBitacora.m_Save(objclsLoginModel.sUsuarioLogin, "", objclsLoginModel.sUsuarioLogin, objclsLoginModel.sMensajeRespuesta, true);
                else
                    oclsBitacora.m_Save(objclsLoginModel.sUsuarioLogin, "", objclsLoginModel.sUsuarioLogin, "Acceso concedido usuario: " + objclsLoginModel.sUIDCliente);

            }
            return objclsLoginModel;
        }

        /// <summary>
        /// Descripción: Método para realizar la recuperacion de la contraseña del usuario solicitado.
        /// </summary>
        public ResponseModel m_RecoveryPassword(RenewPass pobjRenewPass)
        {
            oclsLoginBE = new clsLoginBE();
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                oclsBitacora.m_Save(pobjRenewPass.sUsuarioLogin, "", pobjRenewPass.sUsuarioLogin, "Inicia Proceso de Recuperación de Password");

                SistemaSeguridad.SistemaSeguridad DES = new SistemaSeguridad.SistemaSeguridad();
                string sSemilla = clsEnums.sDescripcionEnum(clsEnums.enumSemilla.sSemilla);

                oclsBitacora.m_Save(pobjRenewPass.sUsuarioLogin, "", pobjRenewPass.sUsuarioLogin, "Recuperando información del Usuario.");

                clsPacientes objclsPacientes = new clsPacientes();
                objclsPacientes.oclsPacientesBE = new clsPacientesBE();
                objclsPacientes.oclsPacientesBE.sUIDPaciente = pobjRenewPass.sUsuarioLogin;

                if (objclsPacientes.oclsPacientesBE.m_GetUserInfo(db))
                {
                    string sEmail = objclsPacientes.oclsPacientesBE.sEmail;

                    oclsBitacora.m_Save(pobjRenewPass.sUsuarioLogin, "", pobjRenewPass.sUsuarioLogin, "Validando método de aplicación.");

                    string sCodigoValidacion = "";

                    //METODO QUE ENVIA EMAIL ANTES DE REMPLAZAR LA CONTRASEÑA
                    switch (pobjRenewPass.iPaso)
                    {
                        case 1:
                            oclsBitacora.m_Save(pobjRenewPass.sUsuarioLogin, "", pobjRenewPass.sUsuarioLogin, "Paso 1.");
                            if (sEmail != "")
                            {
                                sCodigoValidacion = DES.Encriptar(objclsPacientes.oclsPacientesBE.sCodigoValidacion.ToUpper(), sSemilla);

                                clsEnvioMail oclsEnvioMail = new clsEnvioMail();
                                string sResult = oclsEnvioMail.m_EnviarEmail("", "", "", "Recuperación de contraseña...", "Se ha solicitado el cambio de su contraseña, a continuación su código de validación: " + sCodigoValidacion, sEmail, "", "");
                                if (!string.IsNullOrEmpty(sResult))
                                    objResponseModel.sMensaje = "Por el momento no se puede enviar su correo, reintente más tarde.";
                                else
                                    objResponseModel.bRespuesta = true;
                            }
                            else
                            {
                                objResponseModel.sMensaje = "No se encontro ningun email configurado para esta cuenta de usuario.";
                            }

                            break;

                        case 2:

                            sCodigoValidacion = DES.Desencriptar(pobjRenewPass.sCodigoVerificacion, sSemilla);

                            if (sCodigoValidacion.Contains("Longitud"))
                                throw new Exception("El código ingresado es incorrecto, verifiquelo y reintente.");

                            if (sCodigoValidacion == objclsPacientes.oclsPacientesBE.sCodigoValidacion)
                            {
                                clsGeneratedPassword oclsGeneratedPassword = new clsGeneratedPassword();
                                string sPass = oclsGeneratedPassword.Generate(6);

                                SistemaSeguridad.SistemaSeguridad objSeguridad = new SistemaSeguridad.SistemaSeguridad();
                                string sNuevoCrypted = objSeguridad.Encriptar(sPass, clsEnums.sDescSemilla);

                                //string sNuevoCrypted = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(sPass, DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt());

                                if (objclsPacientes.m_Save_Password(pobjRenewPass.sUsuarioLogin, sNuevoCrypted))
                                {
                                    clsEnvioMail oclsEnvioMail = new clsEnvioMail();
                                    oclsEnvioMail.m_EnviarEmail("", "", "", "Recuperación de contraseña", "Su nueva contraseña es: " + sPass, sEmail, "", "");
                                    objResponseModel.bRespuesta = true;
                                }
                                else
                                    objResponseModel.sMensaje = "Por el momento no se pudo cambiar su contraseña, reintente más tarde.";

                            }
                            else
                                throw new Exception("El código ingresado es incorrecto, verifiquelo y reintente.");

                            break;
                        default:
                            objResponseModel.sMensaje = "El código ingresado es incorrecto, favor de verificar.";
                            break;

                    }

                }
                else
                    objResponseModel.sMensaje = objclsPacientes.oclsPacientesBE.sMensajeRespuesta;

                if (!objResponseModel.bRespuesta)
                {
                    if (string.IsNullOrEmpty(objResponseModel.sMensaje))
                        throw new Exception("ocurrio un error al realizar su recuperación de contraseña, favor de contactar al admistrador.");
                    else
                        throw new Exception(objResponseModel.sMensaje);
                }


            }
            catch (Exception ex)
            {
                oclsBitacora.m_Save(pobjRenewPass.sUsuarioLogin, "", pobjRenewPass.sUsuarioLogin, "Error: " + ex.Message, true);
                objResponseModel.sMensaje = ex.Message;
            }
            finally
            {
                oclsBitacora.m_Save(pobjRenewPass.sUsuarioLogin, "", pobjRenewPass.sUsuarioLogin, "Finaliza solicitud cambio de password.");
            }

            return objResponseModel;
        }

        public ResponseModel m_CambioContrasenaNueva(RenewPass oRenewPass)
        {
            oclsLoginBE = new clsLoginBE();
            ResponseModel objResponseModel = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(oRenewPass.sPasswordLogin))
                {
                    if (oRenewPass.sPasswordLogin.Length < 6)
                        throw new Exception("La contraseña debe contener un mínimo de 6 caracteres.");

                    SistemaSeguridad.SistemaSeguridad DES = new SistemaSeguridad.SistemaSeguridad();
                    clsGeneratedPassword oclsGeneratedPassword = new clsGeneratedPassword();

                    SistemaSeguridad.SistemaSeguridad objSeguridad = new SistemaSeguridad.SistemaSeguridad();
                    string psNuevoCrypted = objSeguridad.Encriptar(oRenewPass.sPasswordLogin, clsEnums.sDescSemilla);
                    //string psNuevoCrypted = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(oRenewPass.sPasswordLogin, DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt());

                    DbTransaction oTrans = null;
                    using (DbConnection oCnn = db.CreateConnection())
                    {
                        oCnn.Open();
                        if (!(oCnn.State == System.Data.ConnectionState.Open))
                            throw new Exception("No se pudo establecer conexión con la base de datos.");
                        else
                            oTrans = oCnn.BeginTransaction();

                        try
                        {
                            oclsLoginBE.sUsuarioLogin = oRenewPass.sUsuarioLogin;
                            oclsLoginBE.sPasswordLogin = psNuevoCrypted;
                            oclsLoginBE.m_Save_Nueva_Contrasena(db, oTrans);

                            clsPacientes objclspacientes = new clsPacientes();
                            objclspacientes.oclsPacientesBE = new clsPacientesBE();
                            objclspacientes.oclsPacientesBE.sUIDPaciente = oclsLoginBE.sUsuarioLogin;

                            if (objclspacientes.oclsPacientesBE.m_GetUserInfo(db))
                            {
                                //Se envia correo informativo al usuario
                                clsEnvioMail oclsEnvioMail = new clsEnvioMail();
                                string sResult = oclsEnvioMail.m_EnviarEmail("", "", "", "Cambio de contraseña...", "Se ha cambiado su contraseña, la nueva contraseña con la cual podrá acceder a su aplicación es: " + oRenewPass.sPasswordLogin
                                    + ".  En caso de no haber cambiado usted su contraseña, comuníquese con el administrador. Gracias.", objclspacientes.oclsPacientesBE.sEmail, "", "");
                                if (!string.IsNullOrEmpty(sResult))
                                    objResponseModel.sMensaje = "Por el momento no se puede cambiar su contraseña, reintente más tarde.";

                                objResponseModel.bRespuesta = true;

                                oTrans.Commit();
                            }
                            else
                                throw new Exception("Hubo un error al tratar de cambiar su contraseña reintente más tarde");

                        }
                        catch (Exception ex)
                        {
                            oTrans.Rollback();
                            objResponseModel.sMensaje = ex.Message;
                        }
                    }

                }
                else
                    throw new Exception("No se ingreso la nueva contraseña.");

            }
            catch (Exception e)
            {
                objResponseModel.sMensaje = e.Message;
            }
            return objResponseModel;
        }

        public void m_Save_Nueva_Contrasena()
        {
            try
            {
                if (oclsLoginBE != null)
                {
                    DbTransaction oTrans = null;
                    using (DbConnection oCnn = db.CreateConnection())
                    {
                        oCnn.Open();
                        if (!(oCnn.State == System.Data.ConnectionState.Open))
                            throw new Exception("No se pudo establecer conexión con la base de datos.");
                        else
                            oTrans = oCnn.BeginTransaction();

                        try
                        {
                            oclsLoginBE.m_Save_Nueva_Contrasena(db, oTrans);

                            oTrans.Commit();

                        }
                        catch (Exception ex)
                        {
                            oTrans.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    throw new Exception("No se puede guardar, faltan datos.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
