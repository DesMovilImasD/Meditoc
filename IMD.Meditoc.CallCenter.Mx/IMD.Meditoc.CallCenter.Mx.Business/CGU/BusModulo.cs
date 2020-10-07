using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusModulo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusModulo));
        DatModulo datModulo;

        public BusModulo()
        {
            datModulo = new DatModulo();
        }

        /// <summary>
        /// Inserta, actualiza o borra un módulo del sistema
        /// </summary>
        /// <param name="entModulo"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveModulo(EntModulo entModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveModulo);
            logger.Info(IMDSerialize.Serialize(67823458189732, $"Inicia {metodo}(EntModulo entModulo)", entModulo));

            try
            {
                if (entModulo == null)
                {
                    response.Code = -877656732870195;
                    response.Message = "No se ingresó información para guardar el módulo.";
                    return response;
                }

                if (entModulo.bActivo && !entModulo.bBaja)
                {
                    if (string.IsNullOrWhiteSpace(entModulo.sNombre))
                    {
                        response.Code = -59928366733867;
                        response.Message = "El nombre del módulo no puede ser vacío.";
                        return response;
                    }
                }

                response = datModulo.DSaveModulo(entModulo);
                if (response.Code != 0)
                {
                    return response;
                }

                response.Code = 0;
                response.Message = entModulo.iIdModulo == 0 ? "El módulo ha sido guardado correctamente." : !entModulo.bActivo ? "El módulo ha sido eliminado correctamente." : "El módulo ha sido actualizado correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458190509;
                response.Message = "Ocurrió un error al intentar guardar el módulo.";

                logger.Error(IMDSerialize.Serialize(67823458190509, $"Error en {metodo}(EntModulo entModulo): {ex.Message}", entModulo, ex, response));
            }
            return response;
        }
    }
}
