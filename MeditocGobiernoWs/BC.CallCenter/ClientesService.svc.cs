using BC.CallCenterPortable.Models;
using System;
using BC.CallCenter.Clases;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using BC.Clases;
using System.Collections.Specialized;
using System.Text;
using BC.CallCenter.NuevaImplementacion.Business;
using BC.CallCenter.NuevaImplementacion.DTO;
using log4net;

namespace BC.CallCenter
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ClientesService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ClientesService.svc o ClientesService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ClientesService : IServiceClientes
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ClientesService));

        public string sLigaServicio = ConfigurationManager.AppSettings["sLigaServicio"];
        public string sTokenGobierno = ConfigurationManager.AppSettings["sTokenGobierno"];

        public void DoWork()
        {
        }

        public LoginModel LoginClient(LoginModel poLoginModel)
        {
            LoginModel objLoginModel = new LoginModel();
            objLoginModel.bResult = false;

            logger.Info(SerializeModel.Serialize(67823458261216, $"Inicia LoginModel LoginClient(LoginModel poLoginModel)", poLoginModel));

            try
            {
                clsLogin objclsLogin = new clsLogin();
                objLoginModel = objclsLogin.m_Login(poLoginModel);
                objLoginModel.bFolio = Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarFolio"].ToString());

                logger.Warn(SerializeModel.Serialize(67823458290742, $"Response LoginModel LoginClient(LoginModel poLoginModel)", objLoginModel));
            }
            catch (Exception ex)
            {
                objLoginModel.sMensajeRespuesta = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458261993, $"Error en LoginModel LoginClient(LoginModel poLoginModel): {ex.Message}", poLoginModel, ex, objLoginModel));
            }
            return objLoginModel;
        }

        public ResponseModel FinalizaChat(DrModel poDrModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458262770, $"Inicia ResponseModel FinalizaChat(DrModel poDrModel)", poDrModel));

            try
            {
                DrModel oDrModel = poDrModel;
                clsPacientes objclsPacientes = new clsPacientes();
                objResponseModel = objclsPacientes.m_FinalizaChat(oDrModel);

                logger.Warn(SerializeModel.Serialize(67823458291519, $"Response ResponseModel FinalizaChat(DrModel poDrModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458263547, $"Error en ResponseModel FinalizaChat(DrModel poDrModel): {ex.Message}", poDrModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel RecoveryPass(RenewPass poRenewPass)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458264324, $"Inicia ResponseModel RecoveryPass(RenewPass poRenewPass)", poRenewPass));

            try
            {
                RenewPass objRenewPass = poRenewPass;
                clsLogin objclsLogin = new clsLogin();
                //objResponseModel = objclsLogin.m_RecoveryPassword(objRenewPass);

                logger.Warn(SerializeModel.Serialize(67823458292296, $"Response ResponseModel RecoveryPass(RenewPass poRenewPass)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458265101, $"Error en ResponseModel RecoveryPass(RenewPass poRenewPass): {ex.Message}", poRenewPass, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public bool OcuparDR(DrModel poDrModel)
        {
            bool bResultado = false;

            logger.Info(SerializeModel.Serialize(67823458265878, $"Inicia bool OcuparDR(DrModel poDrModel)", poDrModel));

            try
            {
                clsPacientes objclsPacientes = new clsPacientes();
                poDrModel.bEstado = false;
                bResultado = objclsPacientes.m_Marca_DR(poDrModel.iIdDRCGU, poDrModel.bEstado, "", "");

                logger.Warn(SerializeModel.Serialize(67823458293073, $"Response bool OcuparDR(DrModel poDrModel)", bResultado));
            }
            catch (Exception ex)
            {
                bResultado = false;

                logger.Error(SerializeModel.Serialize(67823458266655, $"Error en bool OcuparDR(DrModel poDrModel): {ex.Message}", poDrModel, ex, bResultado));
            }
            return bResultado;
        }

        public ResponseModel LoginDoctor(DrModel poDrModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458267432, $"Inicia ResponseModel LoginDoctor(DrModel poDrModel)", poDrModel));

            try
            {
                DrModel objDrModel = poDrModel;
                clsDoctores objclsDoctores = new clsDoctores();
                objResponseModel = objclsDoctores.m_Valida_Crea_Usuario(objDrModel);

                logger.Warn(SerializeModel.Serialize(67823458293850, $"Response ResponseModel LoginDoctor(DrModel poDrModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458268209, $"Error en ResponseModel LoginDoctor(DrModel poDrModel): {ex.Message}", poDrModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel CreaUser(UserModel poUserModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458268986, $"Inicia ResponseModel CreaUser(UserModel poUserModel)", poUserModel));

            try
            {
                clsPacientes objclsPacientes = new clsPacientes();
                // objResponseModel = objclsPacientes.m_CreaUsuario(poUserModel);

                logger.Warn(SerializeModel.Serialize(67823458294627, $"Response ResponseModel CreaUser(UserModel poUserModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458269763, $"Error en ResponseModel CreaUser(UserModel poUserModel): {ex.Message}", poUserModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel SolicitaChat(ChatModel poChatModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458270540, $"Inicia ResponseModel SolicitaChat(ChatModel poChatModel)", poChatModel));

            try
            {
                ChatModel oChatModel = poChatModel;
                clsPacientes objclsPacientes = new clsPacientes();
                // objResponseModel = objclsPacientes.m_SolicitaChat(oChatModel);

                logger.Warn(SerializeModel.Serialize(67823458295404, $"Response ResponseModel SolicitaChat(ChatModel poChatModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458271317, $"Error en ResponseModel SolicitaChat(ChatModel poChatModel): {ex.Message}", poChatModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel SolicitaVideoChat(ChatVideoModel poChatVideoModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458272094, $"Inicia ResponseModel SolicitaVideoChat(ChatVideoModel poChatVideoModel)", poChatVideoModel));

            try
            {
                ChatVideoModel oChatVideoModel = poChatVideoModel;
                clsPacientes objclsPacientes = new clsPacientes();
                //objResponseModel = objclsPacientes.m_SolicitaVideoChat(poChatVideoModel);

                objResponseModel.sMensaje = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sMensajeVideoDemo")) ? "La versión demo no cuenta con esta característica." : ConfigurationManager.AppSettings.Get("sMensajeVideoDemo");

                logger.Warn(SerializeModel.Serialize(67823458296181, $"Response ResponseModel SolicitaVideoChat(ChatVideoModel poChatVideoModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458272871, $"Error en ResponseModel SolicitaVideoChat(ChatVideoModel poChatVideoModel): {ex.Message}", poChatVideoModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel SolicitaGrupo(ChatModel poChatModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458273648, $"Inicia ResponseModel SolicitaGrupo(ChatModel poChatModel)", poChatModel));

            try
            {
                ChatModel oChatModel = poChatModel;
                clsPacientes objclsPacientes = new clsPacientes();
                // objResponseModel = objclsPacientes.m_SolicitaChat(oChatModel);

                logger.Warn(SerializeModel.Serialize(67823458296958, $"Response ResponseModel SolicitaGrupo(ChatModel poChatModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458274425, $"Error en ResponseModel SolicitaGrupo(ChatModel poChatModel): {ex.Message}", poChatModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel IniciaVideoChat(ChatVideoModel pochatVideoModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458275202, $"Inicia ResponseModel IniciaVideoChat(ChatVideoModel pochatVideoModel)", pochatVideoModel));

            try
            {
                ChatVideoModel objChatVideoModel = pochatVideoModel;
                clsPacientes objclsPacientes = new clsPacientes();
                //objResponseModel = objclsPacientes.m_IniciaVideoChat(objChatVideoModel);

                logger.Warn(SerializeModel.Serialize(67823458297735, $"Response ResponseModel IniciaVideoChat(ChatVideoModel pochatVideoModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458275979, $"Error en ResponseModel IniciaVideoChat(ChatVideoModel pochatVideoModel): {ex.Message}", pochatVideoModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel IniciaVideoChatIOS(ChatVideoModel pochatVideoModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458276756, $"Inicia ResponseModel IniciaVideoChatIOS(ChatVideoModel pochatVideoModel)", pochatVideoModel));

            try
            {
                ChatVideoModel objChatVideoModel = pochatVideoModel;
                clsPacientes objclsPacientes = new clsPacientes();
                // objResponseModel = objclsPacientes.m_IniciaVideoChatIOS(objChatVideoModel);

                logger.Warn(SerializeModel.Serialize(67823458298512, $"Response ResponseModel IniciaVideoChatIOS(ChatVideoModel pochatVideoModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458277533, $"Error en ResponseModel IniciaVideoChatIOS(ChatVideoModel pochatVideoModel): {ex.Message}", pochatVideoModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel m_CambioContrasenaNueva(RenewPass poRenewPass)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458278310, $"Inicia ResponseModel m_CambioContrasenaNueva(RenewPass poRenewPass)", poRenewPass));

            try
            {
                RenewPass objRenewPass = poRenewPass;
                clsLogin objclsLogin = new clsLogin();
                //objResponseModel = objclsLogin.m_CambioContrasenaNueva(objRenewPass);

                logger.Warn(SerializeModel.Serialize(67823458299289, $"Response ResponseModel m_CambioContrasenaNueva(RenewPass poRenewPass)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458279087, $"Error en ResponseModel m_CambioContrasenaNueva(RenewPass poRenewPass): {ex.Message}", poRenewPass, ex, objResponseModel));
            }
            return objResponseModel;

        }

        public ResponseModel AceptarTerminos(LoginModel poLoginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458279864, $"Inicia ResponseModel AceptarTerminos(LoginModel poLoginModel)", poLoginModel));

            try
            {
                LoginModel objLoginModel = poLoginModel;
                clsPacientes objclsPacientes = new clsPacientes();
                objResponseModel = objclsPacientes.m_Aceptar_Terminos_y_condiciones(objLoginModel);

                logger.Warn(SerializeModel.Serialize(67823458300066, $"Response ResponseModel AceptarTerminos(LoginModel poLoginModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458280641, $"Error en ResponseModel AceptarTerminos(LoginModel poLoginModel): {ex.Message}", poLoginModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel LiberarUsuario(LoginModel poLoginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458281418, $"Inicia ResponseModel LiberarUsuario(LoginModel poLoginModel)", poLoginModel));

            try
            {
                LoginModel objLoginModel = poLoginModel;
                clsPacientes objclsPacientes = new clsPacientes();
                objResponseModel = objclsPacientes.m_Liberar_Usuario(objLoginModel);

                logger.Warn(SerializeModel.Serialize(67823458300843, $"Response ResponseModel LiberarUsuario(LoginModel poLoginModel)", objResponseModel));
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458282195, $"Error en ResponseModel LiberarUsuario(LoginModel poLoginModel): {ex.Message}", poLoginModel, ex, objResponseModel));
            }
            return objResponseModel;
        }

        public ResponseModel SolicitaMedico(BaseModel poChatModel)
        {
            ResponseModel oResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458282972, $"Inicia ResponseModel SolicitaMedico(BaseModel poChatModel)", poChatModel));

            try
            {
                clsPacientes objclsPacientes = new clsPacientes();
                oResponseModel = objclsPacientes.m_getSala_DR(poChatModel);

                logger.Warn(SerializeModel.Serialize(67823458301620, $"Response ResponseModel SolicitaMedico(BaseModel poChatModel)", oResponseModel));
            }
            catch (Exception ex)
            {
                oResponseModel.bRespuesta = false;
                oResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458283749, $"Error en ResponseModel SolicitaMedico(BaseModel poChatModel): {ex.Message}", poChatModel, ex, oResponseModel));
            }
            return oResponseModel;
        }

        public ResponseModel FolioValido(CuestionarioModel oCuestionario)
        {
            ResponseModel oResponseModel = new ResponseModel();
            clsBitacora oclsBitacora = new clsBitacora();

            logger.Info(SerializeModel.Serialize(67823458284526, $"Inicia ResponseModel FolioValido(CuestionarioModel oCuestionario)", oCuestionario));

            try
            {
                string URL = sLigaServicio + "?folioapp=" + oCuestionario.sFolio;

                WebClient oConsumo = new WebClient();
                oConsumo.Headers.Set("Content-Type", "application/json");
                oConsumo.Headers.Set("token", sTokenGobierno);

                var Json = oConsumo.DownloadString(URL);
                dynamic m = JsonConvert.DeserializeObject(Json);

                oclsBitacora.m_Save(oCuestionario.sNumero, "", oCuestionario.sNumero, "El folio ingresado es valido", false, oCuestionario.sLatitud + "," + oCuestionario.sLongitud, oCuestionario.sFolio, oCuestionario.sNumero);
                oResponseModel.sFolio = oCuestionario.sFolio;
                oResponseModel.sMensaje = ConfigurationManager.AppSettings["sMensajeFolioValido"];
                oResponseModel.bRespuesta = true;

                logger.Warn(SerializeModel.Serialize(67823458302397, $"Response ResponseModel FolioValido(CuestionarioModel oCuestionario)", oResponseModel));
            }
            catch (Exception ex)
            {
                oclsBitacora.m_Save(oCuestionario.sNumero, "", oCuestionario.sNumero, "El folio ingresado no es valido", false, oCuestionario.sLatitud + "," + oCuestionario.sLongitud, oCuestionario.sFolio, oCuestionario.sNumero);
                oResponseModel.bRespuesta = false;
                oResponseModel.sMensaje = ConfigurationManager.AppSettings["sMensajeFolio"];

                logger.Error(SerializeModel.Serialize(67823458285303, $"Error enResponseModel FolioValido(CuestionarioModel oCuestionario): {ex.Message}", oCuestionario, ex, oResponseModel));
            }
            return oResponseModel;
        }

        public List<PreguntasModel> DesplegarPreguntas()
        {
            clsTblpreguntas oPreguntas = new clsTblpreguntas();
            List<PreguntasModel> lstPreguntas = new List<PreguntasModel>();
            PreguntasModel oPreguntass;

            logger.Info(SerializeModel.Serialize(67823458286080, $"Inicia List<PreguntasModel> DesplegarPreguntas()"));

            try
            {
                oPreguntas.m_Load_All();

                foreach (var item in oPreguntas.gbloclsTblpreguntasBE.gblListclsTblpreguntasBE)
                {
                    oPreguntass = new PreguntasModel();
                    oPreguntass.sNombre = item.sNombre;
                    oPreguntass.sParam = item.sParam;
                    oPreguntass.iOrden = item.iOrden;

                    lstPreguntas.Add(oPreguntass);
                }

                logger.Warn(SerializeModel.Serialize(67823458303174, $"Response List<PreguntasModel> DesplegarPreguntas()", lstPreguntas));
            }
            catch (Exception ex)
            {
                logger.Error(SerializeModel.Serialize(67823458286857, $"Error en List<PreguntasModel> DesplegarPreguntas(): {ex.Message}", ex, lstPreguntas));
            }
            return lstPreguntas;
        }

        public ResponseModel ValidarFormulario(CuestionarioModel oCuestionario)
        {
            ResponseModel oResponseModel = new ResponseModel();

            logger.Info(SerializeModel.Serialize(67823458287634, $"Inicia ResponseModel ValidarFormulario(CuestionarioModel oCuestionario)", oCuestionario));

            try
            {
                EncuestaBusiness encuestaBusiness = new EncuestaBusiness();
                oResponseModel = encuestaBusiness.ValidarCuestionario(oCuestionario, sTokenGobierno, sLigaServicio);

                oResponseModel.bRespuesta = true;

                logger.Warn(SerializeModel.Serialize(67823458303951, $"Response ResponseModel ValidarFormulario(CuestionarioModel oCuestionario)", oResponseModel));
            }
            catch (Exception ex)
            {
                oResponseModel.bRespuesta = false;
                oResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458288411, $"Error en ResponseModel ValidarFormulario(CuestionarioModel oCuestionario): {ex.Message}", oCuestionario, ex, oResponseModel));
            }
            return oResponseModel;
        }

        public ResponseModel SaveTrazado(List<TrazadoDTO> lstTrazado)
        {
            ResponseModel oResponseModel = new ResponseModel();
            TrazadoBusiness trazadoBusiness = new TrazadoBusiness();

            logger.Info(SerializeModel.Serialize(67823458289188, $"Inicia ResponseModel SaveTrazado(List<TrazadoDTO> lstTrazado)", lstTrazado));

            try
            {
                oResponseModel = trazadoBusiness.RealizarGuardadoTrazado(lstTrazado);
                logger.Warn(SerializeModel.Serialize(67823458304728, $"Response ResponseModel SaveTrazado(List<TrazadoDTO> lstTrazado)", oResponseModel));
            }
            catch (Exception ex)
            {
                oResponseModel.sMensaje = ex.Message;

                logger.Error(SerializeModel.Serialize(67823458289965, $"Error en ResponseModel SaveTrazado(List<TrazadoDTO> lstTrazado): {ex.Message}", lstTrazado, ex, oResponseModel));
            }
            return oResponseModel;
        }
    }
}
