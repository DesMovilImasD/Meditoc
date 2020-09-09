using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.Paciente
{
    public class BusPaciente
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPaciente));

        public DatPaciente datPaciente;


        public BusPaciente()
        {
            datPaciente = new DatPaciente();
        }

        public IMDResponse<bool> DSavePaciente(EntPaciente entPaciente)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSavePaciente);
            logger.Info(IMDSerialize.Serialize(67823458420501, $"Inicia {metodo}(EntPaciente entPaciente)", entPaciente));

            try
            {

                IMDResponse<bool> imdResponse = datPaciente.DSavePaciente(entPaciente);

                if (imdResponse.Code != 0)
                {
                    return imdResponse;
                }

                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458421278;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458421278, $"Error en {metodo}(EntPaciente entPaciente): {ex.Message}", entPaciente, ex, response));
            }
            return response;
        }


    }
}
