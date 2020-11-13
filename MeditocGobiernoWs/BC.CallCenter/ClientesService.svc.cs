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

namespace BC.CallCenter
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ClientesService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ClientesService.svc o ClientesService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ClientesService : IServiceClientes
    {
        public string sLigaServicio = ConfigurationManager.AppSettings["sLigaServicio"];
        public string sTokenGobierno = ConfigurationManager.AppSettings["sTokenGobierno"];

        public void DoWork()
        {
        }

        public LoginModel LoginClient(LoginModel poLoginModel)
        {
            LoginModel objLoginModel = new LoginModel();
            objLoginModel.bResult = false;

            try
            {
                clsLogin objclsLogin = new clsLogin();

                objLoginModel = objclsLogin.m_Login(poLoginModel);
                objLoginModel.bFolio = Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarFolio"].ToString());
            }
            catch (Exception ex)
            {
                objLoginModel.sMensajeRespuesta = ex.Message;
            }
            return objLoginModel;
        }

        public ResponseModel FinalizaChat(DrModel poDrModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                DrModel oDrModel = poDrModel;

                clsPacientes objclsPacientes = new clsPacientes();

                objResponseModel = objclsPacientes.m_FinalizaChat(oDrModel);

            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;
        }

        public ResponseModel RecoveryPass(RenewPass poRenewPass)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                RenewPass objRenewPass = poRenewPass;

                clsLogin objclsLogin = new clsLogin();

                objResponseModel = objclsLogin.m_RecoveryPassword(objRenewPass);

            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;

        }

        public bool OcuparDR(DrModel poDrModel)
        {
            bool bResultado = false;

            try
            {
                clsPacientes objclsPacientes = new clsPacientes();

                poDrModel.bEstado = false;
                bResultado = objclsPacientes.m_Marca_DR(poDrModel.iIdDRCGU, poDrModel.bEstado, "");

            }
            catch (Exception ex)
            {
                bResultado = false;
            }
            return bResultado;

        }

        public ResponseModel LoginDoctor(DrModel poDrModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                DrModel objDrModel = poDrModel;

                clsDoctores objclsDoctores = new clsDoctores();

                objResponseModel = objclsDoctores.m_Valida_Crea_Usuario(objDrModel);

            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;

        }

        public ResponseModel CreaUser(UserModel poUserModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                clsPacientes objclsPacientes = new clsPacientes();

                // objResponseModel = objclsPacientes.m_CreaUsuario(poUserModel);
            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;
        }

        public ResponseModel SolicitaChat(ChatModel poChatModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                ChatModel oChatModel = poChatModel;

                clsPacientes objclsPacientes = new clsPacientes();

                // objResponseModel = objclsPacientes.m_SolicitaChat(oChatModel);

            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;
        }

        public ResponseModel SolicitaVideoChat(ChatVideoModel poChatVideoModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                ChatVideoModel oChatVideoModel = poChatVideoModel;

                clsPacientes objclsPacientes = new clsPacientes();

                //objResponseModel = objclsPacientes.m_SolicitaVideoChat(poChatVideoModel);
                objResponseModel.sMensaje = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sMensajeVideoDemo")) ? "La versión demo no cuenta con esta característica." : ConfigurationManager.AppSettings.Get("sMensajeVideoDemo");
            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;
        }

        public ResponseModel SolicitaGrupo(ChatModel poChatModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                ChatModel oChatModel = poChatModel;

                clsPacientes objclsPacientes = new clsPacientes();

                // objResponseModel = objclsPacientes.m_SolicitaChat(oChatModel);

            }
            catch (Exception ex)
            {
                objResponseModel.bRespuesta = false;
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;
        }

        public ResponseModel IniciaVideoChat(ChatVideoModel pochatVideoModel)
        {

            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                ChatVideoModel objChatVideoModel = pochatVideoModel;

                clsPacientes objclsPacientes = new clsPacientes();
                //objResponseModel = objclsPacientes.m_IniciaVideoChat(objChatVideoModel);

            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;

        }

        public ResponseModel IniciaVideoChatIOS(ChatVideoModel pochatVideoModel)
        {

            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                ChatVideoModel objChatVideoModel = pochatVideoModel;

                clsPacientes objclsPacientes = new clsPacientes();
                // objResponseModel = objclsPacientes.m_IniciaVideoChatIOS(objChatVideoModel);

            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;

        }

        public ResponseModel m_CambioContrasenaNueva(RenewPass poRenewPass)
        {

            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                RenewPass objRenewPass = poRenewPass;

                clsLogin objclsLogin = new clsLogin();
                objResponseModel = objclsLogin.m_CambioContrasenaNueva(objRenewPass);

            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;

        }

        public ResponseModel AceptarTerminos(LoginModel poLoginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                LoginModel objLoginModel = poLoginModel;

                clsPacientes objclsPacientes = new clsPacientes();
                objResponseModel = objclsPacientes.m_Aceptar_Terminos_y_condiciones(objLoginModel);

            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;

        }

        public ResponseModel LiberarUsuario(LoginModel poLoginModel)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                LoginModel objLoginModel = poLoginModel;

                clsPacientes objclsPacientes = new clsPacientes();
                objResponseModel = objclsPacientes.m_Liberar_Usuario(objLoginModel);

            }
            catch (Exception ex)
            {
                objResponseModel.sMensaje = ex.Message;
            }
            return objResponseModel;

        }

        public ResponseModel SolicitaMedico(BaseModel poChatModel)
        {
            ResponseModel oResponseModel = new ResponseModel();

            try
            {
                clsPacientes objclsPacientes = new clsPacientes();

                oResponseModel = objclsPacientes.m_getSala_DR(poChatModel);

            }
            catch (Exception ex)
            {
                oResponseModel.bRespuesta = false;
                oResponseModel.sMensaje = ex.Message;
            }
            return oResponseModel;
        }

        public ResponseModel FolioValido(CuestionarioModel oCuestionario)
        {
            ResponseModel oResponseModel = new ResponseModel();
            clsBitacora oclsBitacora = new clsBitacora();
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
            }
            catch (Exception e)
            {
                oclsBitacora.m_Save(oCuestionario.sNumero, "", oCuestionario.sNumero, "El folio ingresado no es valido", false, oCuestionario.sLatitud + "," + oCuestionario.sLongitud, oCuestionario.sFolio, oCuestionario.sNumero);
                oResponseModel.bRespuesta = false;
                oResponseModel.sMensaje = ConfigurationManager.AppSettings["sMensajeFolio"];
            }

            return oResponseModel;
        }

        public List<PreguntasModel> DesplegarPreguntas()
        {
            clsTblpreguntas oPreguntas = new clsTblpreguntas();
            List<PreguntasModel> lstPreguntas = new List<PreguntasModel>();
            PreguntasModel oPreguntass;

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
            }
            catch (Exception e)
            {

            }
            return lstPreguntas;
        }

        public ResponseModel ValidarFormulario(CuestionarioModel oCuestionario)
        {
            ResponseModel oResponseModel = new ResponseModel();
            try
            {
                string sNumero = "";
                string sCP = "";
                List<string> lstTextDialogos = new List<string>();
                string URL = sLigaServicio;
                WebClient oConsumo = new WebClient();
                var sParametros = new NameValueCollection();
                clsTblcodigopostal oCodigoPostal = new clsTblcodigopostal();
                clsTblcatlada oLada = new clsTblcatlada();
                clsBitacora oclsBitacora = new clsBitacora();

                if (oCuestionario.sError == null)
                    oCuestionario.sError = "";

                sParametros.Add("unidad_notificante", "MEDITOC");

                foreach (var item in oCuestionario.lstPreguntas)
                {
                    if (item.sNombre != null)
                    {
                        if (item.sParam == "cp")
                        {
                            sCP = item.sNombre;
                            oCodigoPostal.ValidarCP(item.sNombre);
                        }

                        if (item.sParam == "telefono")
                        {
                            sNumero = item.sNombre;
                            //if (!Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarGeolocalizacion"]))
                            //    oLada.ValidarLada(item.sNombre);
                        }

                        sParametros.Add(item.sParam, item.sNombre);
                    }
                    else
                    {
                        sParametros.Add(item.sParam, item.bPrecionado.ToString());
                    }

                }

                oConsumo.Headers.Set("token", sTokenGobierno);

                var Json = oConsumo.UploadValues(URL, "POST", sParametros);
                dynamic m = JsonConvert.DeserializeObject(Encoding.ASCII.GetString(Json));
                oResponseModel.sFolio = m.data.FolioApp;

                oResponseModel.bRespuesta = true;
                if (oResponseModel.sFolio != null)
                {
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sTituloFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloUno"].Replace("\\n", "\n"));
                    //lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloDos"]);
                    lstTextDialogos.Add(oResponseModel.sFolio + ConfigurationManager.AppSettings["sSubTituloDos"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloTres"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCuatro"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCinco"].Replace("\\n", "\n"));

                    oResponseModel.bTipoFolio = true;
                    oclsBitacora.m_Save(sNumero, "", sNumero, "Información del paciente" + oCuestionario.sError, false, oCuestionario.sLatitud + "," + oCuestionario.sLongitud, oResponseModel.sFolio, sNumero, "Folio Tipo 2", sCP);
                }
                else
                {
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sTituloFolioSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloUnoSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloDosSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloTresSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCuatroSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCincoSinFolio"].Replace("\\n", "\n"));

                    oResponseModel.bTipoFolio = false;
                    oclsBitacora.m_Save(sNumero, "", sNumero, "Información del paciente" + oCuestionario.sError, false, oCuestionario.sLatitud + "," + oCuestionario.sLongitud, oResponseModel.sFolio, sNumero, "Folio Tipo 1", sCP);
                }
                oResponseModel.sParameter1 = JsonConvert.SerializeObject(lstTextDialogos);
            }
            catch (Exception e)
            {
                oResponseModel.bRespuesta = false;
                oResponseModel.sMensaje = e.Message;
            }
            return oResponseModel;
        }
    }
}
