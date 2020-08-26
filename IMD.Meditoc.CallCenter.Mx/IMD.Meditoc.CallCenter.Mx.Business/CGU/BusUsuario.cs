using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IMDResponse<bool> DSaveUsuario(EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveUsuario);
            logger.Info(IMDSerialize.Serialize(67823458344355, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));

            try
            {
                if (entUsuario == null)
                {
                    response.Code = 67823458345132;
                    response.Message = "No se ingresó ningun usuario.";
                    return response;
                }

                response = bValidaDatos(entUsuario);

                if (!response.Result) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                {
                    return response;
                }

                response = datUsuario.DSaveUsuario(entUsuario);

                response.Code = 0;
                response.Message = entUsuario.iIdUsuario == 0 ? "El usuario se guardó correctamente" : "El usuario se actualizo correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458345132;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458345132, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }


        public IMDResponse<bool> bValidaDatos(EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.bValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458345132, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));
            try
            {

                if (entUsuario.sNombres == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El nombre no puede ser vacio.";
                    response.Result = false;

                    return response;
                }


                if (entUsuario.sUsuario == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El nombre del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }                
                
                if (entUsuario.sPassword == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "La contraseña del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.sApellidoPaterno == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El apellido paterno del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.sApellidoMaterno == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El apellido materno del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458342024;
                response.Message = "Ocurrió un error al intentar guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458341247, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }
    }
}
