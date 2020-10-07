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

        /// <summary>
        /// Inserta, actualiza o borra un botón del sistema
        /// </summary>
        /// <param name="entBoton"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveBoton(EntBoton entBoton)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveBoton);
            logger.Info(IMDSerialize.Serialize(67823458339693, $"Inicia {metodo}(EntBoton entBoton)", entBoton));

            try
            {
                if (entBoton == null)
                {
                    response.Code = -8769879283478;
                    response.Message = "No se ingresó información del botón.";
                    return response;
                }

                if (entBoton.iIdModulo == 0)
                {
                    response.Code = -776723458769823;
                    response.Message = "El botón debe tener un módulo contenedor.";
                    return response;
                }

                if (entBoton.iIdSubModulo == 0)
                {
                    response.Code = -33345899238987;
                    response.Message = "El botón debe tener un submódulo contenedor.";
                    return response;
                }

                if (entBoton.bActivo && !entBoton.bBaja)
                {
                    if (string.IsNullOrWhiteSpace(entBoton.sNombre))
                    {

                        response.Code = -66723454387234;
                        response.Message = "El nombre del botón no puede ser vacío.";
                        return response;
                    }
                }

                response = datBoton.DSaveBoton(entBoton);
                if (response.Code != 0)
                {
                    return response;
                }

                response.Code = 0;
                response.Message = entBoton.iIdBoton == 0 ? "El botón ha sido guardado correctamente" : !entBoton.bActivo ? "El botón ha sido eliminado correctamente." : "El botón ha sido actualizado correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458339693;
                response.Message = "Ocurrió un error al intentar guardar el botón.";

                logger.Error(IMDSerialize.Serialize(67823458339693, $"Error en {metodo}(EntBoton entBoton): {ex.Message}", entBoton, ex, response));
            }
            return response;
        }
    }
}
