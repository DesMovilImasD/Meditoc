using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace IMD.Meditoc.CallCenter.Mx.Data.Consulta
{
    public class DatConsulta
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatConsulta));
        private Database database;
        IMDCommonData imdCommonData;
        string spSaveConsulta;
        string spGetHistorialMedico;
        string spGetDetalleConsulta;
        string spGetDisponibilidadConsulta;
        string spDelConsulta;
        string spSaveHistorialClinico;
        string spGetConsultaMomento;
        string spGetConsultaPaciente;

        public DatConsulta()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spSaveConsulta = "sva_meditoc_save_consulta";
            spGetHistorialMedico = "svc_meditoc_historialclinico";
            spGetDetalleConsulta = "svc_meditoc_consultas";
            spGetDisponibilidadConsulta = "svc_meditoc_consultas_disponibilidad";
            spDelConsulta = "sva_meditoc_del_consulta";
            spSaveHistorialClinico = "sva_meditoc_save_historialclinico";
            spGetConsultaMomento = "svc_meditoc_consultas_momento";
            spGetConsultaPaciente = "svc_ObtenerConsultasByPaciente";
        }

        public IMDResponse<DataTable> DSaveConsulta(int piIdConsulta, int piIdUsuarioMod, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DSaveConsulta);
            logger.Info(IMDSerialize.Serialize(67823458518403, $"Inicia {metodo}(int piIdConsulta, int piIdUsuarioMod, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)", piIdConsulta, piIdUsuarioMod, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveConsulta))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458519180, $"Error en {metodo}(int piIdConsulta, int piIdUsuarioMod, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null): {ex.Message}", piIdConsulta, piIdUsuarioMod, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetHistorialMedico(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null, string psIdTipoDoctor = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetHistorialMedico);
            logger.Info(IMDSerialize.Serialize(67823458523065, $"Inicia {metodo}(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null)", piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetHistorialMedico))
                {
                    database.AddInParameter(dbCommand, "piIdHistorialClinico", DbType.Int32, piIdHistorialClinico);
                    database.AddInParameter(dbCommand, "piIdConsulta", DbType.Int32, piIdConsulta);
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdFolio", DbType.Int32, piIdFolio);
                    database.AddInParameter(dbCommand, "psIdTipoDoctor", DbType.String, psIdTipoDoctor);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458523842;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar el historial médico.";

                logger.Error(IMDSerialize.Serialize(67823458523842, $"Error en {metodo}(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null): {ex.Message}", piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetDetalleConsulta(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetDetalleConsulta);
            logger.Info(IMDSerialize.Serialize(67823458529281, $"Inicia {metodo}(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)", piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetDetalleConsulta))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al obtener el detalle de la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458530058, $"Error en {metodo}(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null): {ex.Message}", piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetDisponibilidadConsulta(int? piIdColaborador = null, int? piIdConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetDisponibilidadConsulta);
            logger.Info(IMDSerialize.Serialize(67823458535497, $"Inicia {metodo}(int piIdColaborador, int piIdConsulta, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null)", piIdColaborador, piIdConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetDisponibilidadConsulta))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar la disponibilidad del doctor.";

                logger.Error(IMDSerialize.Serialize(67823458536274, $"Error en {metodo}(int piIdColaborador, int piIdConsulta, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null): {ex.Message}", piIdColaborador, piIdConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DCancelarConsulta(int piIdConsulta, int piIdUsuarioMod, int piIdEstatusConsulta)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DCancelarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458551037, $"Inicia {metodo}(int piIdConsulta, int piIdUsuarioMod, int piIdEstatusConsulta)", piIdConsulta, piIdUsuarioMod, piIdEstatusConsulta));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spDelConsulta))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al cancelar la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458551814, $"Error en {metodo}(int piIdConsulta, int piIdUsuarioMod, int piIdEstatusConsulta): {ex.Message}", piIdConsulta, piIdUsuarioMod, piIdEstatusConsulta, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DSaveHistorialMedico(int piIdConsulta, int piIdUsuarioMod, string psSintomas = null, string psDiagnostico = null, string psTratamiento = null, double? pfPeso = null, double? pfAltura = null, string psAlergias = null, string psComentarios = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveHistorialMedico);
            logger.Info(IMDSerialize.Serialize(67823458582117, $"Inicia {metodo}(int piIdConsulta, int piIdUsuarioMod, string psSintomas = null, string psDiagnostico = null, string psTratamiento = null, double? pfPeso = null, double? pfAltura = null, string psAlergias = null, string psComentarios = null)", piIdConsulta, piIdUsuarioMod, psSintomas, psDiagnostico, psTratamiento, pfPeso, pfAltura, psAlergias, psComentarios));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveHistorialClinico))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el historial médico.";

                logger.Error(IMDSerialize.Serialize(67823458582894, $"Error en {metodo}(int piIdConsulta, int piIdUsuarioMod, string psSintomas = null, string psDiagnostico = null, string psTratamiento = null, double? pfPeso = null, double? pfAltura = null, string psAlergias = null, string psComentarios = null): {ex.Message}", piIdConsulta, piIdUsuarioMod, psSintomas, psDiagnostico, psTratamiento, pfPeso, pfAltura, psAlergias, psComentarios, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetConsultaProgramada(int piIdPaciente, int piIdColaborador, int piMinutosAntes, int piMinutosDespues)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetConsultaProgramada);
            logger.Info(IMDSerialize.Serialize(67823458586779, $"Inicia {metodo}(int piIdPaciente, int piIdColaborador, int piMinutosAntes, int piMinutosDespues)", piIdPaciente, piIdColaborador, piMinutosAntes, piMinutosDespues));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetConsultaMomento))
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
                response.Message = "Ocurrió un error inesperado en la base de datos al obtener el detalle de la consulta programada.";

                logger.Error(IMDSerialize.Serialize(67823458587556, $"Error en {metodo}(int piIdPaciente, int piIdColaborador, int piMinutosAntes, int piMinutosDespues): {ex.Message}", piIdPaciente, piIdColaborador, piMinutosAntes, piMinutosDespues, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetConsultaProgramadaByPaciente(int? piIdPaciente, DateTime dtFechaActual)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetConsultaProgramada);
            logger.Info(IMDSerialize.Serialize(67823458586780, $"Inicia {metodo}(int? piIdPaciente, DateTime dtFechaActual)", piIdPaciente, dtFechaActual));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetConsultaPaciente))
                {
                    database.AddInParameter(dbCommand, "piIdPaciente", DbType.Int32, piIdPaciente);
                    database.AddInParameter(dbCommand, "pdtFechaConsulta", DbType.DateTime, dtFechaActual);


                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458587556;
                response.Message = "Ocurrió un error inesperado en la base de datos al obtener el detalle de la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458587557, $"Error en {metodo}(int? piIdPaciente, DateTime dtFechaActual): {ex.Message}", piIdPaciente, dtFechaActual, ex, response));
            }
            return response;
        }
    }
}
