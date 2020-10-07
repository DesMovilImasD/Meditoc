using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusSubmodulo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusSubmodulo));
        DatSubmodulo datSubmodulo;

        public BusSubmodulo()
        {
            datSubmodulo = new DatSubmodulo();
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
                    response.Message = "No se ingresó información para guardar el submódulo.";
                    response.Result = false;
                    return response;
                }

                response = BValidaDatos(entSubModulo);

                if (response.Code != 0)
                {
                    return response;
                }

                response = datSubmodulo.DSaveSubModulo(entSubModulo);
                if (response.Code != 0)
                {
                    return response;
                }

                response.Code = 0;
                response.Message = entSubModulo.iIdSubModulo == 0 ? "El submódulo ha sido guardado correctamente." : !entSubModulo.bActivo ? "El submódulo ha sido eliminado correctamente." : "El submódulo ha sido actualizado correctamente.";
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
                    response.Message = "No se ha especificado el módulo conetenedor.";
                    response.Result = false;
                    return response;
                }

                if (entSubModulo.bActivo && !entSubModulo.bBaja)
                {
                    if (string.IsNullOrWhiteSpace(entSubModulo.sNombre))
                    {
                        response.Code = -227619869874;
                        response.Message = "El nombre del submódulo no puede ser vacío.";
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
