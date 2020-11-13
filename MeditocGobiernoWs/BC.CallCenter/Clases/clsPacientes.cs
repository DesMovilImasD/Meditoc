using BC.CallCenter.Models.BE;
using BC.CallCenterPortable.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Configuration;
using System.Linq;

namespace BC.CallCenter.Clases
{
    public class clsPacientes
    {
        public clsPacientesBE oclsPacientesBE;

        Database db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");

        private clsBitacora oclsBitacora = new clsBitacora();

        //private clsCometChat oclsCometChat = new clsCometChat();

        private int iloop = 1;

        public ResponseModel m_FinalizaChat(DrModel poDrModel)
        {
            ResponseModel oResponseModel = new ResponseModel();
            int iUltimoMsg = 0;
            try
            {
                //Bitacora
                oclsBitacora.m_Save("Id DR: " + poDrModel.iIdDRCGU, poDrModel.iIdDRCGU.ToString(), poDrModel.sUIDCliente, "Inicia finalización de solicitud de chat");

                //Desmarca el DR
                oclsPacientesBE = new clsPacientesBE();
                oclsPacientesBE.iIdCGUDR = poDrModel.iIdDRCGU;

                //Recuperar el UID del DR mediante el ID del CGU
                oclsPacientesBE.m_GET_UID_By_IdCGU(db);

                //desocupar al DR.
                oclsPacientesBE.bOcupado = false;

                int iLoopDesocupar = 0;
                do
                {
                    try
                    {
                        //System.Threading.Thread.Sleep(1000);
                        oclsPacientesBE.m_OcuparDR(db);
                        iLoopDesocupar = 2;
                    }
                    catch
                    {
                        oclsBitacora.m_Save("Id DR: " + poDrModel.iIdDRCGU, poDrModel.iIdDRCGU.ToString(), poDrModel.sUIDCliente, "No se pudo desocupar al Dr: " + poDrModel.iIdDRCGU);
                    }
                    iLoopDesocupar++;
                } while (iLoopDesocupar < 2);

                //Recupera paciente de la amistad con el DR
                UserModel objUserModel = new UserModel();

                string sUIDPaciente = "";
                //Se obtiene el paciente 
                if (objUserModel.sFriends.Contains(','))
                    sUIDPaciente = objUserModel.sFriends.Split(',')[0];
                else
                    sUIDPaciente = objUserModel.sFriends;

                //Se desmarca el usuario del paciente como en uso bEnServicio
                oclsPacientesBE.sUIDPaciente = sUIDPaciente;
                oclsPacientesBE.bEnServicio = false;

                int iLoopServicio = 0;
                do
                {
                    try
                    {
                        //System.Threading.Thread.Sleep(1000);
                        oclsPacientesBE.m_Marcar_EnServicio(db);
                        iLoopServicio = 2;
                    }
                    catch
                    {
                        oclsBitacora.m_Save("Id DR: " + poDrModel.iIdDRCGU, poDrModel.iIdDRCGU.ToString(), poDrModel.sUIDCliente, "No se pudo desmarcar paciente en servicio p: " + sUIDPaciente);
                    }
                    iLoopServicio++;
                } while (iLoopServicio < 2);


                //REcuperar ultimo mensaje guardado del Usuario de la DB
                oclsPacientesBE.bGrupo = false;

                oclsPacientesBE.sUIDDR = poDrModel.iIdDRCGU.ToString(); //Se cambia el SUID por el ID del CGU modificación 

                //Se recuperan mensajes del Config
                bool bSendMsjFinal = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("bSendMsjFinal")) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings.Get("bSendMsjFinal"));
                string sMensajeFinal = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sMensajeFinal")) ? "Chat Finalizado." : ConfigurationManager.AppSettings.Get("sMensajeFinal");

                try
                {
                    oclsPacientesBE.m_Get_No_Msg(db);
                }
                catch
                {
                    oclsPacientesBE.iNoMensaje = 0;
                }

                oResponseModel.bRespuesta = true;
            }
            catch (Exception ex)
            {
                //Bitacora save Error
                oclsBitacora.m_Save("Id DR: " + poDrModel.iIdDRCGU, poDrModel.iIdDRCGU.ToString(), poDrModel.sUIDCliente, "Error: " + ex.Message, true);

                oResponseModel.sMensaje = "Error:" + ex;
                // throw new Exception(ex.Message);
            }
            finally
            {
                //Bitacora save finaliza solicitud
                oclsBitacora.m_Save("Id DR: " + poDrModel.iIdDRCGU, poDrModel.iIdDRCGU.ToString(), poDrModel.sUIDCliente, "Finaliza solicitud de finalización de chat");
            }
            return oResponseModel;
        }

        private bool m_InicializaUsuario(string psUsuario)
        {
            bool bResult = false;
            try
            {
                //oclsCometChat = new clsCometChat();

                //UserModel objUserModel = new UserModel();

                //objUserModel = oclsCometChat.m_GetUser(psUsuario);

                //int iLoopUser = 3;
                //do
                //{
                //    if (oclsCometChat.m_DelUser(psUsuario))
                //    {
                //        System.Threading.Thread.Sleep(400);
                //        if (oclsCometChat.m_CreatUser(objUserModel.sUIDCliente, objUserModel.sNameDisplay, objUserModel.sURLAvatar, objUserModel.sURLPerfil, objUserModel.sRol))
                bResult = true;
                //    }

                //    iLoopUser--;
                //} while (iLoopUser > 0);

            }
            catch
            {

            }
            return bResult;
        }

        //private void m_RollBack_VideoChat(clsPacientesBE oclsPacientesBE)
        //{
        //    oclsCometChat = new clsCometChat();

        //    oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Inicia Rollback de chat");

        //    try
        //    {
        //        if (oclsCometChat.m_DelFriend(oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente))
        //            oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Se elimino la amistad entre el DR: " + oclsPacientesBE.sUIDDR + " y el paciente: " + oclsPacientesBE.sUIDPaciente);

        //        if (oclsCometChat.m_DelFriend(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR))
        //            oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Se elimino la amistad entre el paciente: " + oclsPacientesBE.sUIDPaciente + " y el DR: " + oclsPacientesBE.sUIDDR);
        //    }
        //    catch (Exception ex)
        //    {
        //        oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Error en Rollback de chat" + ex.Message);
        //    }
        //    finally
        //    {
        //        oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Inicia Rollback de chat");
        //    }

        //}

        //private void m_RollBack_Chat(clsPacientesBE oclsPacientesBE)
        //{
        //    oclsCometChat = new clsCometChat();

        //    oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Inicia Rollback de chat");

        //    try
        //    {
        //        if (oclsCometChat.m_DelGroup(oclsPacientesBE.sUIDDR))
        //            oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Se elimino el Grupo: " + oclsPacientesBE.sUIDDR + " con los usuarios; " +
        //                oclsPacientesBE.sUIDDR + " y " + oclsPacientesBE.sUIDPaciente);

        //        if (oclsCometChat.m_DelFriend(oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente))
        //            oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Se elimino la amistad entre el DR: " + oclsPacientesBE.sUIDDR + " y el paciente: " + oclsPacientesBE.sUIDPaciente);

        //        if (oclsCometChat.m_DelFriend(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR))
        //            oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Se elimino la amistad entre el paciente: " + oclsPacientesBE.sUIDPaciente + " y el DR: " + oclsPacientesBE.sUIDDR);
        //    }
        //    catch (Exception ex)
        //    {
        //        oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Error en Rollback de chat" + ex.Message);
        //    }
        //    finally
        //    {
        //        oclsBitacora.m_Save(oclsPacientesBE.sUIDPaciente, oclsPacientesBE.sUIDDR, oclsPacientesBE.sUIDPaciente, "Inicia Rollback de chat");
        //    }

        //}

        public bool m_Save_Password(string psUsuario, string psPassword)
        {
            bool bResult = false;

            try
            {
                oclsPacientesBE = new clsPacientesBE();
                oclsBitacora.m_Save(psUsuario, "0", "0", "Inicia solicitud cambio password.");
                oclsPacientesBE.m_Save_Password(db, psUsuario, psPassword);
                bResult = true;
                oclsBitacora.m_Save(psUsuario, "0", "0", "se actualizo el Password del usuario.");
            }
            catch (Exception ex)
            {
                oclsBitacora.m_Save(psUsuario, "0", "0", "Error al actualizar Password: " + ex.Message, true);
            }
            finally
            {
                oclsBitacora.m_Save(psUsuario, "0", "0", "Finaliza solicitud cambio password.");
            }

            return bResult;
        }

        public bool m_Marca_DR(int piIdUsuario, bool pbEstado, string sFolio)
        {
            bool bResult = false;

            try
            {
                oclsPacientesBE = new clsPacientesBE();
                oclsBitacora.m_Save("ID: " + piIdUsuario, piIdUsuario.ToString(), "0", "Inicia solicitud ocupar DR.");
                oclsPacientesBE.iIdCGUDR = piIdUsuario;
                oclsPacientesBE.bOcupado = pbEstado;
                oclsPacientesBE.sFolio = sFolio;
                oclsPacientesBE.m_OcuparDR(db);
                bResult = true;
                oclsBitacora.m_Save("ID: " + piIdUsuario, piIdUsuario.ToString(), "0", "Se ocupo al DR.");
            }
            catch (Exception ex)
            {
                oclsBitacora.m_Save("ID: " + piIdUsuario, piIdUsuario.ToString(), "0", "Error al ocupar DR: " + ex.Message, true);
            }
            finally
            {
                oclsBitacora.m_Save("ID: " + piIdUsuario, piIdUsuario.ToString(), "0", "Finaliza solicitud ocupar DR.");
            }

            return bResult;
        }

        private void m_GeneraMsjInicial(clsPacientesBE poclsPacientesBE)
        {
            try
            {
                bool bSendMsjInicial = true;
                string sMensajeInicial = "";

                switch (poclsPacientesBE.sChat)
                {

                    case "CHAT":
                        //Se recuperan mensajes del Config
                        bSendMsjInicial = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("bSendMsjInitChat")) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings.Get("bSendMsjInitChat"));
                        sMensajeInicial = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sMensajeInitChat")) ? "Inicia Chat." : ConfigurationManager.AppSettings.Get("sMensajeInitChat");

                        break;
                    case "VIDEO":
                        bSendMsjInicial = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("bSendMsjInitVideoChat")) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings.Get("bSendMsjInitVideoChat"));
                        sMensajeInicial = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sMensajeIniVideotChat")) ? "Inicia Video Chat." : ConfigurationManager.AppSettings.Get("sMensajeIniVideotChat");

                        break;
                    default:
                        return;
                }

                //Recupera Folio
                poclsPacientesBE.m_Get_Folio(db);

                //Armado Mensaje
                sMensajeInicial = sMensajeInicial.Replace("//", poclsPacientesBE.sFolio.ToString());

                //Se envia mensaje de finalización al chat
                // if (bSendMsjInicial)
                ///oclsCometChat.m_SendMessage(poclsPacientesBE.sUIDDR, poclsPacientesBE.sUIDPaciente, sMensajeInicial, 0);
            }
            catch { }
        }

        public ResponseModel m_Aceptar_Terminos_y_condiciones(LoginModel poLoginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                oclsPacientesBE = new clsPacientesBE();
                oclsBitacora.m_Save("ID: " + poLoginModel.sUIDCliente, "0", "0", "Inicia solicitud Aceptar Terminos y condiciones.");
                oclsPacientesBE.sUIDPaciente = poLoginModel.sUIDCliente;
                oclsPacientesBE.bTerminosyCondiciones = poLoginModel.bAceptoTerminoCondicion;
                oclsPacientesBE.m_Acepta_Termino_y_Condiciones(db);
                objResponseModel.bRespuesta = true;
                oclsBitacora.m_Save("ID: " + poLoginModel.sUIDCliente, "0", "0", "Se actualizo terminos y condiciones.");
            }
            catch (Exception ex)
            {
                oclsBitacora.m_Save("ID: " + poLoginModel.sUIDCliente, "0", "0", "Error: " + ex.Message, true);
                objResponseModel.sMensaje = ex.Message;
            }
            finally
            {
                oclsBitacora.m_Save("ID: " + poLoginModel.sUIDCliente, "0", "0", "Finaliza termino y condiciones.");
            }

            return objResponseModel;
        }

        public ResponseModel m_Liberar_Usuario(LoginModel poLoginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                oclsPacientesBE = new clsPacientesBE();
                oclsBitacora.m_Save("USER: " + poLoginModel.sUIDCliente, poLoginModel.sUIDDR, poLoginModel.sUIDCliente, "Inicia solicitud Liberar Usuario.");
                oclsPacientesBE.sUIDPaciente = poLoginModel.sUIDCliente;
                oclsPacientesBE.bEnServicio = false;
                oclsPacientesBE.m_Marcar_EnServicio(db);
                objResponseModel.bRespuesta = true;
                oclsBitacora.m_Save("ID: " + poLoginModel.sUIDCliente, poLoginModel.sUIDDR, poLoginModel.sUIDCliente, "Se libero el usuario.");
            }
            catch (Exception ex)
            {
                oclsBitacora.m_Save("ID: " + poLoginModel.sUIDCliente, poLoginModel.sUIDDR, poLoginModel.sUIDCliente, "Error: " + ex.Message, true);
                objResponseModel.sMensaje = ex.Message;
            }
            finally
            {
                oclsBitacora.m_Save("ID: " + poLoginModel.sUIDCliente, poLoginModel.sUIDDR, poLoginModel.sUIDCliente, "Finaliza Liberación usuario.");
            }

            return objResponseModel;
        }

        public ResponseModel m_getSala_DR(BaseModel poChatModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                oclsPacientesBE = new clsPacientesBE();


                //BUSCAR ID DOCTOR LIBRE
                objResponseModel = oclsPacientesBE.m_getSala_DR(db);

                if (objResponseModel.bRespuesta)
                {
                    objResponseModel.sFolio = poChatModel.sFolio;
                    if (!m_Marca_DR(Convert.ToInt32(objResponseModel.sMensaje), true, poChatModel.sFolio))
                    {
                        objResponseModel = new ResponseModel();
                        throw new ArgumentException(ConfigurationManager.AppSettings["sMensajeFolio"]);
                    }

                }
                else
                {
                    throw new ArgumentException("Por el momento todos los doctores se encuentran ocupados, intente de nuevo más tarde.");
                }
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }

            return objResponseModel;
        }
    }
}
