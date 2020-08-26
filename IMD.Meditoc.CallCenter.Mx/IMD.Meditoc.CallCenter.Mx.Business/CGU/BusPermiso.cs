using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusPermiso
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPermiso));
        DatPermiso datPermiso;

        public IMDResponse<bool> DSavePermiso(EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSavePermiso);
            logger.Info(IMDSerialize.Serialize(67823458347463, $"Inicia {metodo}(EntPermiso entPermiso)", entPermiso));

            try
            {
                if (entPermiso == null)
                {
                    response.Code = 67823458339693;
                    response.Message = "No se ingresó ningun permiso.";
                    return response;
                }

                response = bValidaDatos(entPermiso);

                if (!response.Result) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                {
                    return response;
                }

                response = datPermiso.DSavePermiso(entPermiso);

                response.Code = 0;
                //response.Message = entPermiso.iid == 0 ? "El perfil se guardó correctamente" : "El perfil se actualizo correctamente";
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Code = 67823458348240;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458348240, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> bValidaDatos(EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.bValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458349017, $"Inicia {metodo}(EntPermiso entPermiso)", entPermiso));

            try
            {
                if (entPermiso.sNombre == "")
                {
                    response.Code = 67823458342024;
                    response.Message = "El nombre del permiso no puede ser vacio.";
                    response.Result = false;

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458349794;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458349794, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
            }
            return response;
        }
    }
}
