using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BC.CallCenterPortable.Models;

namespace BC.CallCenter
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServiceClientes" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceClientes
    {
        //LISTO
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        LoginModel LoginClient(LoginModel Login);
        //LISTO
        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ResponseModel RecoveryPass(RenewPass poRenewPass);

        //LISTO
        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        ResponseModel m_CambioContrasenaNueva(RenewPass poRenewPass);

        //LISTO
        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ResponseModel AceptarTerminos(LoginModel poLoginModel);
        //

        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        ResponseModel SolicitaMedico(BaseModel poChatModel);




        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ResponseModel SolicitaChat(ChatModel poChatModel);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ResponseModel SolicitaVideoChat(ChatVideoModel poChatVideoModel);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ResponseModel CreaUser(UserModel poUserModel);




        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ResponseModel FinalizaChat(DrModel poDrModel);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool OcuparDR(DrModel poDrModel);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ResponseModel LoginDoctor(DrModel poDrModel);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ResponseModel IniciaVideoChat(ChatVideoModel pochatVideoModel);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ResponseModel IniciaVideoChatIOS(ChatVideoModel pochatVideoModel);



        [OperationContract]
        [WebInvoke(Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        ResponseModel LiberarUsuario(LoginModel poLoginModel);

        [OperationContract]
        [WebInvoke(Method = "POST",
  RequestFormat = WebMessageFormat.Json,
  ResponseFormat = WebMessageFormat.Json)]
        ResponseModel FolioValido(CuestionarioModel oCuestinoario);

        [OperationContract]
        [WebInvoke(Method = "POST",
RequestFormat = WebMessageFormat.Json,
ResponseFormat = WebMessageFormat.Json)]
        List<PreguntasModel> DesplegarPreguntas();

        [OperationContract]
        [WebInvoke(Method = "POST",
RequestFormat = WebMessageFormat.Json,
ResponseFormat = WebMessageFormat.Json)]
        ResponseModel ValidarFormulario(CuestionarioModel oCuestionario);

    }
}
