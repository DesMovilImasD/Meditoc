using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.Paciente
{
    public class DatPaciente
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatPaciente));
        private Database database;
        IMDCommonData imdCommonData;
        string savePaciente;
        string getPacientes;
        string updPaciente;

        public DatPaciente()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            savePaciente = "sva_meditoc_save_paciente";
            getPacientes = "svc_meditoc_pacientes";
            updPaciente = "sva_meditoc_upd_paciente";
        }

        public IMDResponse<DataTable> DSavePaciente(EntPaciente entPaciente)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSavePaciente);
            logger.Info(IMDSerialize.Serialize(67823458422055, $"Inicia {metodo}(EntPaciente entPaciente)", entPaciente));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(savePaciente))
                {
                    database.AddInParameter(dbCommand, "piIdFolio", DbType.Int32, entPaciente.iIdFolio);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entPaciente.sNombre);
                    database.AddInParameter(dbCommand, "psTelefono", DbType.String, entPaciente.sTelefono?.Replace(" ", ""));
                    database.AddInParameter(dbCommand, "psCorreo", DbType.String, entPaciente.sCorreo);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entPaciente.iIdUsuarioMod);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458422832;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el paciente";

                logger.Error(IMDSerialize.Serialize(67823458422832, $"Error en {metodo}(EntPaciente entPaciente): {ex.Message}", entPaciente, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetPacientes(int? piIdPaciente = null, int? piIdFolio = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetPacientes);
            logger.Info(IMDSerialize.Serialize(67823458515295, $"Inicia {metodo}(int? piIdPaciente = null, int? piIdFolio = null)", piIdPaciente, piIdFolio));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getPacientes))
                {
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "piIdFolio", DbType.Int32, piIdFolio);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458516072;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar los pacientes";

                logger.Error(IMDSerialize.Serialize(67823458516072, $"Error en {metodo}(int? piIdPaciente = null, int? piIdFolio = null): {ex.Message}", piIdPaciente, piIdFolio, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DUpdPaciente(int piIdPaciente, string psNombre, string psCorreo, string psTelefono, string psTipoSangre, DateTime? pdtFechaNacimiento, int? piIdSexo, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DUpdPaciente);
            logger.Info(IMDSerialize.Serialize(67823458577455, $"Inicia {metodo}(int piIdPaciente, string psNombre, string psCorreo, string psTelefono, string psTipoSangre, DateTime? pdtFechaNacimiento, int? piIdSexo, int piIdUsuarioMod)", piIdPaciente, psNombre, psCorreo, psTelefono, psTipoSangre, pdtFechaNacimiento, piIdSexo, piIdUsuarioMod));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(updPaciente))
                {
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, psNombre);
                    database.AddInParameter(dbCommand, "psCorreo", DbType.String, psCorreo);
                    database.AddInParameter(dbCommand, "psTelefono", DbType.String, psTelefono);
                    database.AddInParameter(dbCommand, "psTipoSangre", DbType.String, psTipoSangre);
                    database.AddInParameter(dbCommand, "pdtFechaNacimiento", DbType.DateTime, pdtFechaNacimiento);
                    database.AddInParameter(dbCommand, "piIdSexo", DbType.Int32, piIdSexo);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458578232;
                response.Message = "Ocurrió un error inesperado en la base de datos al actualizar los datos del paciente";

                logger.Error(IMDSerialize.Serialize(67823458578232, $"Error en {metodo}(int piIdPaciente, string psNombre, string psCorreo, string psTelefono, string psTipoSangre, DateTime? pdtFechaNacimiento, int? piIdSexo, int piIdUsuarioMod): {ex.Message}", piIdPaciente, psNombre, psCorreo, psTelefono, psTipoSangre, pdtFechaNacimiento, piIdSexo, piIdUsuarioMod, ex, response));
            }
            return response;
        }
    }
}
