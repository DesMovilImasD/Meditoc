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
        string delConsulta;
        string saveHistorialClinico;
        string getConsultaMomento;
        string getConsultaPaciente;

        public DatConsulta()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveConsulta = "sva_meditoc_save_consulta";
            getHistorialMedico = "svc_meditoc_historialclinico";
            getDetalleConsulta = "svc_meditoc_consultas";
            getDisponibilidadConsulta = "svc_meditoc_consultas_disponibilidad";
            delConsulta = "sva_meditoc_del_consulta";
            saveHistorialClinico = "sva_meditoc_save_historialclinico";
            getConsultaMomento = "svc_meditoc_consultas_momento";
            getConsultaPaciente = "svc_ObtenerConsultasByPaciente";
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

        public IMDResponse<DataTable> DGetDisponibilidadConsulta(int piIdColaborador, int piIdConsulta, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetDisponibilidadConsulta);
            logger.Info(IMDSerialize.Serialize(67823458535497, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getDisponibilidadConsulta))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdConsulta", DbType.Int32, piIdConsulta);
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

        public IMDResponse<bool> DCancelarConsulta(int piIdConsulta, int piIdUsuarioMod, int piIdEstatusConsulta)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DCancelarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458551037, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(delConsulta))
                {
                    database.AddInParameter(dbCommand, "piIdConsulta", DbType.Int32, piIdConsulta);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);
                    database.AddInParameter(dbCommand, "piIdEstatusConsulta", DbType.Int32, piIdEstatusConsulta);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458551814;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458551814, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DSaveHistorialMedico(int piIdConsulta, int piIdUsuarioMod, string psSintomas = null, string psDiagnostico = null, string psTratamiento = null, double? pfPeso = null, double? pfAltura = null, string psAlergias = null, string psComentarios = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveHistorialMedico);
            logger.Info(IMDSerialize.Serialize(67823458582117, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveHistorialClinico))
                {
                    database.AddInParameter(dbCommand, "piIdConsulta", DbType.Int32, piIdConsulta);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);
                    database.AddInParameter(dbCommand, "psSintomas", DbType.String, psSintomas);
                    database.AddInParameter(dbCommand, "psDiagnostico", DbType.String, psDiagnostico);
                    database.AddInParameter(dbCommand, "psTratamiento", DbType.String, psTratamiento);
                    database.AddInParameter(dbCommand, "psAlergias", DbType.String, psAlergias);
                    database.AddInParameter(dbCommand, "psComentarios", DbType.String, psComentarios);
                    database.AddInParameter(dbCommand, "pfPeso", DbType.Double, pfPeso);
                    database.AddInParameter(dbCommand, "pfAltura", DbType.Double, pfAltura);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458582894;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458582894, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetConsultaProgramada(int piIdPaciente, int piIdColaborador, int piMinutosAntes, int piMinutosDespues)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetConsultaProgramada);
            logger.Info(IMDSerialize.Serialize(67823458586779, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getConsultaMomento))
                {
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piMinutosAntes", DbType.Int32, piMinutosAntes);
                    database.AddInParameter(dbCommand, "piMinutosDespues", DbType.Int32, piMinutosDespues);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458587556;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458587556, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetConsultaProgramadaByPaciente(int? piIdPaciente, DateTime dtFechaActual)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetConsultaProgramada);
            logger.Info(IMDSerialize.Serialize(67823458586780, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getConsultaPaciente))
                {
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "pdtFechaConsulta", DbType.DateTime, dtFechaActual);


                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458587556;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458587557, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
