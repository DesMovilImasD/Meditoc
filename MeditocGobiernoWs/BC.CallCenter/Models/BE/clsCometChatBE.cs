using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Configuration;
using BC.CallCenter.Models.Info;
using BC.CallCenter.Models.Interfaces;
using BC.CallCenter.Models.Repositorios;
using Newtonsoft.Json.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BC.CallCenterPortable.Models;

namespace BC.CallCenter.Models.BE
{    
    public class clsCometChatBE : clsCometChatInfo
    {

        [NonSerialized]
        internal ICometChatRepository gbloclsCometChatRepository;

        public clsCometChatBE()
            : this(new clsCometChatRepository())
        {
        }

        public clsCometChatBE(ICometChatRepository repository)
           : base()
        {
            gbloclsCometChatRepository = repository;
        }

        private JavaScriptSerializer jsonSerializer;

        /// <summary>
        /// Descripción: Método para realizar aplicaciones de ordenes en la API.
        /// </summary>
        /// <param name="psURL">Dirección compuesta por directiva y resource de la api.</param>
        /// <param name="psApiKey">Key de la API.</param>
        /// <param name="plistParametros">Lista con los parametros necesarios para el API.</param>
        public bool m_APIAplica(string psURL, string psApiKey, List<string> plistParametros)
        {
            bool bResult = false;
            int iStatus = 0;

            try
            {
                WebClient wcCometChat = new WebClient();
                wcCometChat.Headers.Add("api-key", psApiKey);
                foreach (string sParametro in plistParametros)
                {
                    string[] sParametroSplit = sParametro.Split(',');

                    wcCometChat.QueryString.Add(sParametroSplit[0], sParametroSplit[1]);
                }

               
                var data = wcCometChat.UploadValues(psURL, "POST", wcCometChat.QueryString);

                // data here is optional, in case we recieve any string data back from the POST request.
                string sRespJson = UnicodeEncoding.UTF8.GetString(data);

                if (sRespJson.Contains("success"))
                {
                    jsonSerializer = new JavaScriptSerializer();
                    dynamic objJson = jsonSerializer.Deserialize<dynamic>(sRespJson);
                    bResult = Int32.TryParse(objJson["success"]["status"].ToString(), out iStatus);

                    int iStatusVal = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("iStatus")) ? 2000 : Convert.ToInt32(ConfigurationManager.AppSettings.Get("iStatus"));
                    if (iStatus == iStatusVal)
                        bResult = true;
                    else
                        bResult = false;
                }

            }
            catch (WebException webex)
            {
                HttpWebResponse webResp = (HttpWebResponse)webex.Response;

                throw new Exception("Plataforma CC. Error " + webResp.StatusCode);

            }
            catch (Exception)
            {
                throw new Exception("Por el momento no esta disponible el servicio. Reintente más tarde. Error: Plataforma CC.");
            }

            return bResult;
        }

        /// <summary>
        /// Descripción: Método para realizar aplicaciones de ordenes en la API.
        /// USando una lista espliteada con @
        /// </summary>
        /// <param name="psURL">Dirección compuesta por directiva y resource de la api.</param>
        /// <param name="psApiKey">Key de la API.</param>
        /// <param name="plistParametros">Lista con los parametros necesarios para el API.</param>
        public bool m_APIAplica_Nw(string psURL, string psApiKey, List<string> plistParametros)
        {
            bool bResult = false;
            int iStatus = 0;

            try
            {
                WebClient wcCometChat = new WebClient();
                wcCometChat.Headers.Add("api-key", psApiKey);
                foreach (string sParametro in plistParametros)
                {
                    string[] sParametroSplit = sParametro.Split('@');

                    wcCometChat.QueryString.Add(sParametroSplit[0], sParametroSplit[1]);
                }


                var data = wcCometChat.UploadValues(psURL, "POST", wcCometChat.QueryString);

                // data here is optional, in case we recieve any string data back from the POST request.
                string sRespJson = UnicodeEncoding.UTF8.GetString(data);

                if (sRespJson.Contains("success"))
                {
                    jsonSerializer = new JavaScriptSerializer();
                    dynamic objJson = jsonSerializer.Deserialize<dynamic>(sRespJson);
                    bResult = Int32.TryParse(objJson["success"]["status"].ToString(), out iStatus);

                    int iStatusVal = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("iStatus")) ? 2000 : Convert.ToInt32(ConfigurationManager.AppSettings.Get("iStatus"));
                    if (iStatus == iStatusVal)
                        bResult = true;
                    else
                        bResult = false;
                }

            }
            catch (WebException webex)
            {
                HttpWebResponse webResp = (HttpWebResponse)webex.Response;

                throw new Exception("Plataforma CC. Error " + webResp.StatusCode);

            }
            catch (Exception)
            {
                throw new Exception("Por el momento no esta disponible el servicio. Reintente más tarde. Error: Plataforma CC.");
            }

            return bResult;
        }

        /// <summary>
        /// Descripción: Método para obtener la informacion de usuario de la API.
        /// </summary>
        /// <param name="psURL">Dirección compuesta por directiva y resource de la api.</param>
        /// <param name="psApiKey">Key de la API.</param>
        /// <param name="plistParametros">Lista con los parametros necesarios para el API.</param>
        public bool m_ValidaUser(string psURL, string psApiKey, List<string> plistParametros)
        {
            bool bRespuesta = false;
            string sRespJson = string.Empty;
            try
            {          
                WebClient wcCometChat = new WebClient();
                wcCometChat.Headers.Add("api-key", psApiKey);
                foreach (string sParametro in plistParametros)
                {
                    string[] sParametroSplit = sParametro.Split(',');

                    wcCometChat.QueryString.Add(sParametroSplit[0], sParametroSplit[1]);
                }

                try
                {
                    var data = wcCometChat.UploadValues(psURL, "POST", wcCometChat.QueryString);

                    // data here is optional, in case we recieve any string data back from the POST request.
                    sRespJson = UnicodeEncoding.UTF8.GetString(data);
                }
                catch { throw new Exception("Hubo un error al verificar el usuario, favor de reintentar."); }

                if (sRespJson.Contains("success"))
                {
                    int iStatus = 0;
                    jsonSerializer = new JavaScriptSerializer();
                    dynamic objJson = jsonSerializer.Deserialize<dynamic>(sRespJson);
                    bool bResult = Int32.TryParse(objJson["success"]["status"].ToString(), out iStatus);

                    if (bResult)
                    {
                        int iStatusVal = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("iStatus")) ? 2000 : Convert.ToInt32(ConfigurationManager.AppSettings.Get("iStatus"));
                        if (iStatus == iStatusVal)
                            bRespuesta = true;
                        else
                            throw new Exception("Por el momento hay problemas con la plataforma.");
                    }
                }

            }
            catch (WebException webex)
            {
                HttpWebResponse webResp = (HttpWebResponse)webex.Response;

                throw new Exception("Plataforma CC. Error " + webResp.StatusCode);

            }
            catch (Exception)
            {
                throw new Exception("Por el momento no esta disponible el servicio. Reintente más tarde. Error: Plataforma CC.");
            }

            return bRespuesta;

        }

        /// <summary>
        /// Descripción: Método para obtener la informacion de usuario de la API.
        /// </summary>
        /// <param name="psURL">Dirección compuesta por directiva y resource de la api.</param>
        /// <param name="psApiKey">Key de la API.</param>
        /// <param name="plistParametros">Lista con los parametros necesarios para el API.</param>
        public UserModel m_GetUser(string psURL, string psApiKey, List<string> plistParametros)
        {
            UserModel objUserModel = new UserModel();

            try
            {
                WebClient wcCometChat = new WebClient();
                wcCometChat.Headers.Add("api-key", psApiKey);
                foreach (string sParametro in plistParametros)
                {
                    string[] sParametroSplit = sParametro.Split(',');

                    wcCometChat.QueryString.Add(sParametroSplit[0], sParametroSplit[1]);
                }

                var data = wcCometChat.UploadValues(psURL, "POST", wcCometChat.QueryString);

                // data here is optional, in case we recieve any string data back from the POST request.
                string sRespJson = UnicodeEncoding.UTF8.GetString(data);

                JObject objJSON = JObject.Parse(sRespJson);

                JToken objjTokenUser = objJSON["success"]["data"]["user"];
                
                objUserModel.sUIDCliente = objjTokenUser["uid"].ToString();
                objUserModel.sNameDisplay = objjTokenUser["name"].ToString();
                objUserModel.sFriends = objjTokenUser["friends"].ToString();
                objUserModel.sRol = objjTokenUser["role"].ToString();
                objUserModel.sURLPerfil = objjTokenUser["link"].ToString();
                objUserModel.sURLAvatar = objjTokenUser["avatar"].ToString();

                //foreach (JObject content in objJSON["success"]["data"]["user"].Children())
                //{
                //    objUserModel.sFriends = (string)content.GetValue("friends");
                //    objUserModel.sRol = (string)content.GetValue("role");
                //    objUserModel.sURLPerfil = (string)content.GetValue("link");
                //    objUserModel.sURLAvatar = (string)content.GetValue("avatar");

                //}

            }
            catch (WebException webex)
            {
                HttpWebResponse webResp = (HttpWebResponse)webex.Response;

                throw new Exception("Plataforma CC. Error " + webResp.StatusCode);

            }
            catch (Exception ex)
            {
                throw new Exception("Por el momento no esta disponible el servicio. Reintente más tarde. Error: Plataforma CC.");
            }

            return objUserModel;

        }

        /// <summary>
        /// Descripción: Método para guardar los mensajes uno a uno.
        /// </summary>
        /// <param name="psURL">Dirección compuesta por directiva y resource de la api.</param>
        /// <param name="psApiKey">Key de la API.</param>
        /// <param name="plistParametros">Lista con los parametros necesarios para el API.</param>
        /// <param name="iUltimoMSG">Ultimo mensaje almacenado en la DB.</param>
        ///<param name = "psUID"> UID del usuario solicitado.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_GetMessages(string psURL, string psApiKey, List<string> plistParametros, int iUltimoMSG, string psUID, Database pdb)
        {
            try
            {
                WebClient wcCometChat = new WebClient();
                wcCometChat.Headers.Add("api-key", psApiKey);
                foreach (string sParametro in plistParametros)
                {
                    string[] sParametroSplit = sParametro.Split(',');

                    wcCometChat.QueryString.Add(sParametroSplit[0], sParametroSplit[1]);
                }

                var data = wcCometChat.UploadValues(psURL, "POST", wcCometChat.QueryString);

                // data here is optional, in case we recieve any string data back from the POST request.
                string sRespJson = UnicodeEncoding.UTF8.GetString(data);    

                JObject objJSON = JObject.Parse(sRespJson);

                if (!sRespJson.Substring(0, 15).Contains("failed"))
                {
                    foreach (JObject content in objJSON["success"]["data"][psUID]["one-on-one"].Children<JObject>())
                    {
                        clsMensagesCCModel objMensaje = new clsMensagesCCModel();

                        objMensaje.iMessage_id = (int)content.GetValue("message_id");
                        objMensaje.sSender_uid = (string)content.GetValue("sender_uid");
                        objMensaje.sReciever_uid = (string)content.GetValue("reciever_uid");
                        objMensaje.sMessage = (string)content.GetValue("message");
                        objMensaje.sTimestamp = (string)content.GetValue("timestamp");
                        objMensaje.sRead = (string)content.GetValue("read");
                        objMensaje.sVisibility = (string)content.GetValue("visibility");

                        //Si el mensaje anterior guardado es menor al actual se guarda el actual
                        if (objMensaje.iMessage_id > iUltimoMSG)
                        {
                            try
                            {
                                gbloclsCometChatRepository.m_SaveMensages(pdb, objMensaje);
                            }
                            catch (Exception)
                            {
                                //Bitacora

                            }
                        }

                    }
                }

            }
            catch (WebException webex)
            {
                HttpWebResponse webResp = (HttpWebResponse)webex.Response;

                throw new Exception("Plataforma CC. Error " + webResp.StatusCode);

            }
            catch (Exception)
            {
                throw new Exception("Por el momento no esta disponible el servicio. Reintente más tarde. Error: Plataforma CC.");
            }

        }

        /// <summary>
        /// Descripción: Método para guardar los mensajes de los grupos.
        /// </summary>
        /// <param name="psURL">Dirección compuesta por directiva y resource de la api.</param>
        /// <param name="psApiKey">Key de la API.</param>
        /// <param name="plistParametros">Lista con los parametros necesarios para el API.</param>
        /// <param name="iUltimoMSG">Ultimo mensaje almacenado en la DB.</param>
        /// <param name="psGrupo">UID del Grupo solicitado.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_GetMessagesGroup(string psURL, string psApiKey, List<string> plistParametros, int iUltimoMSG, string psGrupo, Database pdb)
        {
            try
            {
                WebClient wcCometChat = new WebClient();
                wcCometChat.Headers.Add("api-key", psApiKey);
                foreach (string sParametro in plistParametros)
                {
                    string[] sParametroSplit = sParametro.Split(',');

                    wcCometChat.QueryString.Add(sParametroSplit[0], sParametroSplit[1]);
                }

                var data = wcCometChat.UploadValues(psURL, "POST", wcCometChat.QueryString);

                // data here is optional, in case we recieve any string data back from the POST request.
                string sRespJson = UnicodeEncoding.UTF8.GetString(data);

                JObject objJSON = JObject.Parse(sRespJson);

                if (!sRespJson.Substring(0,15).Contains("failed"))
                {
                    foreach (JObject content in objJSON["success"]["data"][psGrupo]["groupchats"].Children<JObject>())
                    {
                        clsMsgGroupCCModel objMsgGroup = new clsMsgGroupCCModel();

                        objMsgGroup.iMessage_id = (int)content.GetValue("message_id");
                        objMsgGroup.sGuid = (string)content.GetValue("guid");
                        objMsgGroup.sSender_uid = (string)content.GetValue("sender_uid");
                        objMsgGroup.sMessage = (string)content.GetValue("message");
                        objMsgGroup.sTimestamp = (string)content.GetValue("timestamp");
                        objMsgGroup.bGrupo = true;
                        //Si el mensaje anterior guardado es menor al actual se guarda el actual
                        if (objMsgGroup.iMessage_id > iUltimoMSG)
                        {
                            try
                            {
                                gbloclsCometChatRepository.m_SaveMsgGroup(pdb, objMsgGroup);
                            }
                            catch (Exception)
                            {
                                //Bitacora

                            }
                        }

                    }
                }

            }
            catch (WebException webex)
            {
                HttpWebResponse webResp = (HttpWebResponse)webex.Response;

                throw new Exception("Plataforma CC. Error " + webResp.StatusCode);

            }
            catch (Exception)
            {
                throw new Exception("Por el momento no esta disponible el servicio. Reintente más tarde. Error: Plataforma CC.");
            }

        }

    }
}
