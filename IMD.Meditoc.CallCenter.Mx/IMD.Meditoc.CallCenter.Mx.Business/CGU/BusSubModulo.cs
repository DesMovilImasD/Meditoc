using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusSubModulo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusSubModulo));
        DatSubModulo datSubModulo;

        public BusSubModulo()
        {
            datSubModulo = new DatSubModulo();
        }

        /// <summary>
        /// Guarda un submódulo del sistema
        /// </summary>
        /// <param name="entSubModulo"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveSubModulo(EntSubModulo entSubModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveSubModulo);
            logger.Info(IMDSerialize.Serialize(67823458189732, $"Inicia {metodo}(EntSubModulo entSubModulo)", entSubModulo));

            try
            {
                if (entSubModulo == null)
                {
                    response.Code = -87687687263498;
                    response.Message = "No se ingresó ningun sub módulo.";
                    response.Result = false;
                    return response;
                }

                response = BValidaDatos(entSubModulo);

                if (response.Code != 0)
                {
                    return response;
                }

                response = datSubModulo.DSaveSubModulo(entSubModulo);
                if (response.Code != 0)
                {
                    return response;
                }

                response.Code = 0;
                response.Message = entSubModulo.iIdSubModulo == 0 ? "El sub módulo se guardó correctamente" : "El sub módulo se actualizo correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458190509;
                response.Message = "Ocurrió un error al intentar guardar el módulo.";

                logger.Error(IMDSerialize.Serialize(67823458190509, $"Error en {metodo}(EntSubModulo entSubModulo): {ex.Message}", entSubModulo, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Valida los datos para guardar un submodulo del sistema
        /// </summary>
        /// <param name="entSubModulo"></param>
        /// <returns></returns>
        public IMDResponse<bool> BValidaDatos(EntSubModulo entSubModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458189732, $"Inicia {metodo}(EntSubModulo entCreateModulo)", entSubModulo));
            try
            {
                if (entSubModulo.iIdModulo == 0)
                {
                    response.Code = -768276382360982;
                    response.Message = "Debe asignarle un módulo.";
                    response.Result = false;
                    return response;
                }

                if (entSubModulo.bActivo && !entSubModulo.bBaja)
                {
                    if (string.IsNullOrWhiteSpace(entSubModulo.sNombre))
                    {
                        response.Code = -227619869874;
                        response.Message = "El nombre del submodulo no puede ser vacío";
                        response.Result = false;

                        return response;
                    }
                }

                response.Code = 0;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Code = 67823458190509;
                response.Message = "Ocurrió un error al intentar guardar el submódulo.";

                logger.Error(IMDSerialize.Serialize(67823458190509, $"Error en {metodo}(EntSubModulo entSubModulo): {ex.Message}", entSubModulo, ex, response));
            }
            return response;
        }
    }
}
