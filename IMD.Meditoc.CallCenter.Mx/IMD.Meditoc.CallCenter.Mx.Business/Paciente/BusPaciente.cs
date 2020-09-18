using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
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

        public IMDResponse<EntPaciente> BSavePaciente(EntPaciente entPaciente)
        {
            IMDResponse<EntPaciente> response = new IMDResponse<EntPaciente>();

            string metodo = nameof(this.BSavePaciente);
            logger.Info(IMDSerialize.Serialize(67823458420501, $"Inicia {metodo}(EntPaciente entPaciente)", entPaciente));

            try
            {

                IMDResponse<DataTable> imdResponse = datPaciente.DSavePaciente(entPaciente);

                if (imdResponse.Code != 0)
                {
                    return imdResponse.GetResponse<EntPaciente>();
                }

                entPaciente.iIdPaciente = Convert.ToInt32(imdResponse.Result.Rows[0]["iIdPaciente"].ToString());

                response.Code = 0;
                response.Result = entPaciente;
            }
            catch (Exception ex)
            {
                response.Code = 67823458421278;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458421278, $"Error en {metodo}(EntPaciente entPaciente): {ex.Message}", entPaciente, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntPaciente>> BGetPacientes(int? piIdPaciente = null, int? piIdFolio = null)
        {
            IMDResponse<List<EntPaciente>> response = new IMDResponse<List<EntPaciente>>();

            string metodo = nameof(this.BGetPacientes);
            logger.Info(IMDSerialize.Serialize(67823458516849, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataTable> resGetPacientes = datPaciente.DGetPacientes(piIdPaciente, piIdFolio);
                if (resGetPacientes.Code != 0)
                {
                    return resGetPacientes.GetResponse<List<EntPaciente>>();
                }

                List<EntPaciente> lstPacientes = new List<EntPaciente>();

                foreach (DataRow drPaciente in resGetPacientes.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drPaciente);

                    EntPaciente paciente = new EntPaciente()
                    {
                        iIdFolio = dr.ConvertTo<int>("iIdFolio"),
                        iIdPaciente = dr.ConvertTo<int>("iIdPaciente"),
                        iIdSexo = dr.ConvertTo<int>("iIdSexo"),
                        sApellidoMaterno = dr.ConvertTo<string>("sApellidoMaterno"),
                        sApellidoPaterno = dr.ConvertTo<string>("sApellidoPaterno"),
                        sCorreo = dr.ConvertTo<string>("sCorreo"),
                        dtFechaNacimiento = dr.ConvertTo<DateTime?>("dtFechaNacimiento"),
                        sdtFechaNacimiento = string.Empty,
                        sFolio = dr.ConvertTo<string>("sFolio"),
                        sNombre = dr.ConvertTo<string>("sNombre"),
                        sSexo = dr.ConvertTo<string>("sSexo"),
                        sTelefono = dr.ConvertTo<string>("sTelefono"),
                        sTipoSangre = dr.ConvertTo<string>("sTipoSangre"),
                    };

                    paciente.sdtFechaNacimiento = paciente.dtFechaNacimiento?.ToString("dd/MM/yyyy");

                    lstPacientes.Add(paciente);
                }

                response.Code = 0;
                response.Message = "Pacientes consultados";
                response.Result = lstPacientes;

            }
            catch (Exception ex)
            {
                response.Code = 67823458517626;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458517626, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
