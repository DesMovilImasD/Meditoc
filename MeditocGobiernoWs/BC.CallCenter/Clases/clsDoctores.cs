using BC.CallCenter.Models.BE;
using BC.CallCenterPortable.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Clases
{
    public class clsDoctores
    {
        public clsDoctoresBE oclsDoctoresBE;

        Database db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");

        private clsBitacora oclsBitacora = new clsBitacora();

        //private clsCometChat oclsCometChat = new clsCometChat();

        public ResponseModel m_Valida_Crea_Usuario(DrModel poDrModel)
        {
            ResponseModel oResponseModel = new ResponseModel();

            try
            {
                //Bitacora save solicitud DR
               // oclsBitacora.m_Save(poUserModel.sUIDDR, "Inicia sesion doctor");

                //Se consulta DR disponible y asignacion de DR
                oclsDoctoresBE = new clsDoctoresBE();

                if (string.IsNullOrEmpty(poDrModel.sUIDDR))
                    throw new Exception("No se proporciono el UID del doctor.");
                else
                    oclsDoctoresBE.sUIDDR = poDrModel.sUIDDR;

                if (string.IsNullOrEmpty(poDrModel.sNameDisplay))
                    throw new Exception("No se proporciono el nombre a mostrar del doctor.");
                else
                    oclsDoctoresBE.sNameDisplay = poDrModel.sNameDisplay;

                oclsDoctoresBE.sURLAvatar = string.IsNullOrEmpty(poDrModel.sURLAvatar) ? "" : poDrModel.sURLAvatar;
                oclsDoctoresBE.sURLPerfil = string.IsNullOrEmpty(poDrModel.sURLPerfil) ? "" : poDrModel.sURLPerfil;
                oclsDoctoresBE.sRol = string.IsNullOrEmpty(poDrModel.sRol) ? "" : poDrModel.sRol;

              //  oclsBitacora.m_Save(poUserModel.sUIDCliente, "Se verifica existencia del doctor en CometCaht.");

                //if (!oclsCometChat.m_Validaser(oclsDoctoresBE.sUIDDR))               
                //    if (oclsCometChat.m_CreatUser(oclsDoctoresBE.sUIDDR, oclsDoctoresBE.sNameDisplay, oclsDoctoresBE.sURLAvatar,
                //        oclsDoctoresBE.sURLPerfil, oclsDoctoresBE.sRol))
                //    {
                //        oResponseModel.sMensaje = "Usuario: " + poDrModel.sUIDDR + ", se creo correctamente.";
                //        oResponseModel.bRespuesta = true;
                //      //  oclsBitacora.m_Save(poUserModel.sUIDDR, "Usuario: " + poUserModel.sUIDDR + ", se creo correctamente.");
                //    }
            }
            catch (Exception ex)
            {
                //Bitacora save Error
                oclsBitacora.m_Save(poDrModel.sUIDDR, poDrModel.sUIDDR,poDrModel.sUIDCliente, "Error: " + ex.Message, true);

                oResponseModel.sMensaje = "Error: " + ex.Message;
            }
            finally
            {
                //Bitacora save finaliza solicitud
              //  oclsBitacora.m_Save(poUserModel.sUIDCliente, "Finaliza inicio sesion doctor.");
            }

            return oResponseModel;
        }

    }
}
