using BC.CallCenter.Models.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BC.CallCenterPortable.Models;

namespace BC.CallCenter.Clases
{
    public class clsCometChat
    {
        clsCometChatBE objclsCometChatBE = new clsCometChatBE();
        private string sApiKey, sGetuser, sURLAPICometChat, sGetMessages, sCreatGrupo, sDelGrupo, sAddToGroup, sDelFromGroup;
        private string sAddFriend, sDelFriend, sCreateUser, sGetGroupMessages, sDeleteUser, sSendMessage;

        public clsCometChat()
        {
            this.sURLAPICometChat = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sURLAPICometChat")) ? "" : ConfigurationManager.AppSettings.Get("sURLAPICometChat");
            this.sApiKey = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sApikey")) ? "" : ConfigurationManager.AppSettings.Get("sApikey");
            this.sGetuser = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sGetuser")) ? "" : ConfigurationManager.AppSettings.Get("sGetuser");
            this.sGetMessages = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sGetMessages")) ? "" : ConfigurationManager.AppSettings.Get("sGetMessages");
            this.sCreatGrupo = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sCreatGroup")) ? "" : ConfigurationManager.AppSettings.Get("sCreatGroup");
            this.sDelGrupo = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sDelGroup")) ? "" : ConfigurationManager.AppSettings.Get("sDelGroup");
            this.sAddToGroup = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sAddToGroup")) ? "" : ConfigurationManager.AppSettings.Get("sAddToGroup");
            this.sDelFromGroup = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sDelFromGroup")) ? "" : ConfigurationManager.AppSettings.Get("sDelFromGroup");
            this.sAddFriend = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sAddFriend")) ? "" : ConfigurationManager.AppSettings.Get("sAddFriend");
            this.sDelFriend = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sDelFriend")) ? "" : ConfigurationManager.AppSettings.Get("sDelFriend");
            this.sCreateUser = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sCreateUser")) ? "" : ConfigurationManager.AppSettings.Get("sCreateUser");
            this.sGetGroupMessages = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sGetGroupMessages")) ? "" : ConfigurationManager.AppSettings.Get("sGetGroupMessages");
            this.sDeleteUser = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sDeleteUser")) ? "" : ConfigurationManager.AppSettings.Get("sDeleteUser");
            this.sSendMessage = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("sSendMessage")) ? "" : ConfigurationManager.AppSettings.Get("sSendMessage");
        }

        /// <summary>
        /// Descripción: Método para validar si un usuario existe.
        /// </summary>
        /// <param name="psUID">UID del Usuario.</param>
        public bool m_Validaser(string psUID)
        {
            bool bResult= false;
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("UID,"+psUID);
                olistParam.Add("returnFriends,true");

                if (objclsCometChatBE.m_ValidaUser(sURLAPICometChat + sGetuser, sApiKey, olistParam))
                    bResult = true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bResult;
        }

        /// <summary>
        /// Descripción: Método para obtener la información de un usuario.
        /// </summary>
        /// <param name="psUID">UID del Usuario.</param>
        public UserModel m_GetUser(string psUID)
        {
            UserModel objUserModel = new UserModel();
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("UID," + psUID);
                olistParam.Add("returnFriends,true");

                objUserModel = objclsCometChatBE.m_GetUser(sURLAPICometChat + sGetuser, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objUserModel;
        }
        /// <summary>
        /// Descripción: Método para crear nuevos usuarios en CometChat.
        /// </summary>
        /// <param name="psUID">UID del Usuario.</param>
        /// <param name="psDisplayName">Nombre para mostrar.</param>
        /// <param name="psAvatarURL">URL del avatar para visualizar.</param>
        /// <param name="psProfileURL">Url del perfil del DR.</param>
        /// <param name="psUID">UID del Usuario.</param>
        public bool m_CreatUser(string psUID, string psDisplayName, string psAvatarURL, string psProfileURL, string psRole)
        {
            bool bResult = false;
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("UID," + psUID);
                olistParam.Add("name,"+psDisplayName);
                olistParam.Add("avatarURL,"+psAvatarURL);
                olistParam.Add("profileURL,"+psProfileURL);
                olistParam.Add("role,"+psRole);


                bResult = objclsCometChatBE.m_APIAplica(sURLAPICometChat + sCreateUser, sApiKey, olistParam);               

            }
            catch (WebException webex)
            {
                HttpWebResponse webResp = (HttpWebResponse)webex.Response;

                throw new Exception("Plataforma CC. Error " + webResp.StatusCode);

            }
            catch (Exception ex)
            {
                throw new Exception("Plataforma CC. Error " + ex.Message);
            }
            return bResult;
        }

        
        public void m_GetMessagesUser(string psUID, int iUltimoMSG, Database pdb)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("UIDs," + psUID);
                olistParam.Add("offset,0");
                olistParam.Add("limit,0");

                objclsCometChatBE.m_GetMessages(sURLAPICometChat + sGetMessages, sApiKey, olistParam, iUltimoMSG, psUID, pdb);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void m_GetMessagesGroup(string psUID, int iUltimoMSG, Database pdb)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("GUIDs," + psUID);
                olistParam.Add("offset,0");
                olistParam.Add("limit,0");

                objclsCometChatBE.m_GetMessagesGroup(sURLAPICometChat + sGetGroupMessages, sApiKey, olistParam, iUltimoMSG, psUID, pdb);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para crear nuevos grupos.
        /// </summary>
        /// <param name="psUIDGrupo">UID del grupo acrear.</param>
        /// <param name="psNameGroup">Nombre del grupo acrear.</param>
        public bool m_CreaGroup(string psUIDGrupo, string psNameGroup)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("GUID," + psUIDGrupo);
                olistParam.Add("name,"+ psNameGroup);
                olistParam.Add("type,2");
              //  olistParam.Add("password," + psNameGroup);

                return objclsCometChatBE.m_APIAplica(sURLAPICometChat + sCreatGrupo, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para agregar usuarios a un grupo.
        /// Este método espera un usuario o usuarios separados por comas ",".
        /// </summary>
        /// <param name="psNameGroup">Nombre del grupo donde se uniran los usuarios.</param>
        /// <param name="psUIDUsuarios">Nombre o nombres de los uaurios a quitar separados por coma si es más de uno.</param>
        public bool m_AddToGroup(string psNameGroup, string psUIDUsuarios)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("GUID," + psNameGroup);
                olistParam.Add("UIDs," + psUIDUsuarios);

                return objclsCometChatBE.m_APIAplica(sURLAPICometChat + sAddToGroup, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Descripción: Método para eliminar usuario o usuarios de un grupo.
        /// </summary>
        /// <param name="psNameGroup">Nombre del grupo donde se eliminaran los usuarios.</param>
        /// <param name="psUIDUsuarios">Nombre o nombres de los uaurios a quitar separados por coma si es más de uno.</param>
        public bool m_DelFromGroup(string psNameGroup, string psUIDUsuarios)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("GUID," + psNameGroup);
                olistParam.Add("UIDs," + psUIDUsuarios);

                return objclsCometChatBE.m_APIAplica(sURLAPICometChat + sCreatGrupo, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Descripción: Método para eliminar un grupo.
        /// </summary>
        /// <param name="psNameGroup">Nombre del grupo a eliminar.</param>
        public bool m_DelGroup(string psNameGroup)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("GUID," + psNameGroup);

                return objclsCometChatBE.m_APIAplica(sURLAPICometChat + sDelGrupo, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Descripción: Método para eliminar un grupo.
        /// </summary>
        /// <param name="psNameUser">Nombre del Usuario a eliminar.</param>
        public bool m_DelUser(string psNameUser)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("UID," + psNameUser);

                return objclsCometChatBE.m_APIAplica(sURLAPICometChat + sDeleteUser, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para agregar amigos a usuario.
        /// </summary>
        /// <param name="psUIDUsuario">Nombre del grupo donde se uniran los usuarios.</param>
        /// <param name="psUIDAmigo">Nombre o nombres de los uaurios a quitar separados por coma si es más de uno.</param>
        public bool m_AddFriend(string psUIDUsuario, string psUIDAmigo)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("UID," + psUIDUsuario);
                olistParam.Add("friendsUID," + psUIDAmigo);
                olistParam.Add("clearExisting,true");

                return objclsCometChatBE.m_APIAplica(sURLAPICometChat + sAddFriend, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para eliminar amigos a usuario.
        /// </summary>
        /// <param name="psUIDUsuario">Nombre del grupo donde se uniran los usuarios.</param>
        /// <param name="psUIDAmigo">Nombre o nombres de los uaurios a quitar separados por coma si es más de uno.</param>
        public bool m_DelFriend(string psUIDUsuario, string psUIDAmigo)
        {
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("UID," + psUIDUsuario);
                olistParam.Add("friendsUID," + psUIDAmigo);

                return objclsCometChatBE.m_APIAplica(sURLAPICometChat + sDelFriend, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para eliminar amigos a usuario.
        /// </summary>
        /// <param name="psUIDUsuario">Nombre del grupo donde se uniran los usuarios.</param>
        /// <param name="psUIDAmigo">Nombre o nombres de los uaurios a quitar separados por coma si es más de uno.</param>
        public bool m_SendMessage(string psSend, string psReceiver, string psMensaje, int piVisibility, bool pbEsGrupo = false)
        {
            //Parameter visibility. If isGroup is set to 0 (i.e. one-on-one message), 
            //then you can choose where you want to display the message: 
            // values: 0 -> Both sender and receiver, 1 -> Only receiver, 2 -> Only sender.

            int iGrupo = 0;
            try
            {
                List<string> olistParam = new List<string>();
                olistParam.Add("senderUID@" + psSend);
                olistParam.Add("receiverUID@" + psReceiver);

                if (pbEsGrupo) iGrupo = 1; else iGrupo = 0;

                olistParam.Add("isGroup@" + iGrupo);
                olistParam.Add("message@" + psMensaje);
                olistParam.Add("visibility@" + piVisibility);

                return objclsCometChatBE.m_APIAplica_Nw(sURLAPICometChat + sSendMessage, sApiKey, olistParam);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
