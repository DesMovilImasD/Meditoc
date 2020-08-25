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
    public class BusModulo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusModulo));
        DatModulo datModulo;

        public BusModulo()
        {
            datModulo = new DatModulo();
        }

        public IMDResponse<bool> BSaveModulo(EntModulo entModulo)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveModulo);
            logger.Info(IMDSerialize.Serialize(67823458189732, $"Inicia {metodo}(EntModulo entModulo)", entModulo));

            try
            {
                if (entModulo == null)
                {
                    response.Code = 8768767234634;
                    response.Message = "No se ingresó ningun módulo.";
                    return response;
                }

                if (entModulo.sNombre == "")
                {
                    response.Code = 8768767234634;
                    response.Message = "El nombre no puede ser vacio.";
                    return response;
                }

                response = datModulo.DSaveModulo(entModulo);

                if (response.Code != 0)
                {
                    response.Message = "Hubo un error al guardar el módulo.";
                }

                response.Code = 0;
                response.Message = entModulo.iIdModulo == 0 ? "El módulo se guardó correctamente" : "El módulo se actualizo correctamente";
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
