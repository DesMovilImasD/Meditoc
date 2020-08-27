using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusBoton
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusBoton));
        DatBoton datBoton;

        public BusBoton()
        {
            datBoton = new DatBoton();
        }

        public IMDResponse<bool> BSaveBoton(EntBoton entBoton)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveBoton);
            logger.Info(IMDSerialize.Serialize(67823458339693, $"Inicia {metodo}(EntBoton entCreateModulo)", entBoton));

            try
            {
                if (entBoton == null)
                {
                    response.Code = 67823458339693;
                    response.Message = "No se ingresó ningun botón.";
                    return response;
                }

                if (entBoton.iIdModulo == 0)
                {
                    response.Code = 67823458339693;
                    response.Message = "Debe contener un módulo agregado.";
                    return response;
                }
                if (entBoton.iIdSubModulo == 0)
                {
                    response.Code = 67823458339693;
                    response.Message = "Debe contener un submodulo agregado.";
                    return response;
                }


                if (entBoton.sNombre == "")
                {
                    response.Code = 67823458339693;
                    response.Message = "El nombre no puede ser vacio.";
                    return response;
                }



                response = datBoton.DSaveBoton(entBoton);

                if (response.Code != 0)
                {
                    response.Message = "Hubo un error al guardar el botón.";
                }

                response.Code = 0;
                response.Message = entBoton.iIdBoton == 0 ? "El botón se guardó correctamente" : "El botón se actualizo correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458339693;
                response.Message = "Ocurrió un error al intentar guardar el botón.";

                logger.Error(IMDSerialize.Serialize(67823458339693, $"Error en {metodo}(EntCreateCupon entCreateCupon): {ex.Message}", entBoton, ex, response));
            }
            return response;
        }
    }
}
