using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.Consulta
{
    public class DatConsulta
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatConsulta));
        private Database database;
        IMDCommonData imdCommonData;
        string saveConsulta;
        string getHistorialMedico;
        string getDetalleConsulta;
        string getDisponibilidadConsulta;

        public DatConsulta()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveConsulta = "sva_meditoc_save_consulta";
            getHistorialMedico = "svc_meditoc_historialclinico";
            getDetalleConsulta = "svc_meditoc_consultas";
            getDisponibilidadConsulta = "svc_meditoc_consultas_disponibilidad";
        }

        public IMDResponse<DataTable> DSaveConsulta(int piIdConsulta, int piIdUsuarioMod, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSaveConsulta);
            logger.Info(IMDSerialize.Serialize(67823458518403, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveConsulta))
                {
                    database.AddInParameter(dbCommand, "piIdConsulta", DbType.Int32, piIdConsulta);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdEstatusConsulta", DbType.Int32, piIdEstatusConsulta);
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaInicio", DbType.DateTime, pdtFechaProgramadaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaFin", DbType.DateTime, pdtFechaProgramadaFin);
                    database.AddInParameter(dbCommand, "pdtFechaConsultaInicio", DbType.DateTime, pdtFechaConsultaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaConsultaFin", DbType.DateTime, pdtFechaConsultaFin);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458519180;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458519180, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetHistorialMedico(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetHistorialMedico);
            logger.Info(IMDSerialize.Serialize(67823458523065, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getHistorialMedico))
                {
                    database.AddInParameter(dbCommand, "piIdHistorialClinico", DbType.Int32, piIdHistorialClinico);
                    database.AddInParameter(dbCommand, "piIdConsulta", DbType.Int32, piIdConsulta);
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdFolio", DbType.Int32, piIdFolio);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458523842;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458523842, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetDetalleConsulta(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetDetalleConsulta);
            logger.Info(IMDSerialize.Serialize(67823458529281, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getDetalleConsulta))
                {
                    database.AddInParameter(dbCommand, "piIdConsulta", DbType.Int32, piIdConsulta);
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdEstatusConsulta", DbType.Int32, piIdEstatusConsulta);
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaInicio", DbType.DateTime, pdtFechaProgramadaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaFin", DbType.DateTime, pdtFechaProgramadaFin);
                    database.AddInParameter(dbCommand, "pdtFechaConsultaInicio", DbType.DateTime, pdtFechaConsultaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaConsultaFin", DbType.DateTime, pdtFechaConsultaFin);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458530058;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458530058, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetDisponibilidadConsulta(int piIdColaborador, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetDisponibilidadConsulta);
            logger.Info(IMDSerialize.Serialize(67823458535497, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getDisponibilidadConsulta))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaInicio", DbType.DateTime, pdtFechaProgramadaInicio);
                    database.AddInParameter(dbCommand, "pdtFechaProgramadaFin", DbType.DateTime, pdtFechaProgramadaFin);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458536274;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458536274, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
