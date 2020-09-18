using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Consulta;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.Consulta
{
    public class BusConsulta
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusConsulta));

        DatConsulta datConsulta;

        public BusConsulta()
        {
            datConsulta = new DatConsulta();
        }

        public IMDResponse<EntConsulta> BSaveConsulta(EntConsulta entConsulta, int piIdUsuarioMod = 1)
        {
            IMDResponse<EntConsulta> response = new IMDResponse<EntConsulta>();

            string metodo = nameof(this.BSaveConsulta);
            logger.Info(IMDSerialize.Serialize(67823458519957, $"Inicia {metodo}"));

            try
            {
                if (entConsulta == null)
                {
                    response.Code = 9837987634567;
                    response.Message = "No se ingresó información de consulta";
                    return response;
                }

                IMDResponse<DataTable> resSaveConsulta = datConsulta.DSaveConsulta(
                    entConsulta.iIdConsulta,
                    piIdUsuarioMod,
                    entConsulta.iIdPaciente,
                    entConsulta.iIdColaborador,
                    entConsulta.iIdEstatusConsulta,
                    entConsulta.dtFechaProgramadaInicio,
                    entConsulta.dtFechaProgramadaFin,
                    entConsulta.dtFechaConsultaInicio,
                    entConsulta.dtFechaConsultaFin);
                if (resSaveConsulta.Code != 0)
                {
                    return resSaveConsulta.GetResponse<EntConsulta>();
                }

                if (resSaveConsulta.Result.Rows.Count != 1)
                {
                    response.Code = 9837987634567;
                    response.Message = "No se pudo generar la consulta";
                    return response;
                }

                entConsulta.iIdConsulta = Convert.ToInt32(resSaveConsulta.Result.Rows[0]["iIdConsulta"].ToString());

                response.Code = 0;
                response.Message = "Consulta creada";
                response.Result = entConsulta;
            }
            catch (Exception ex)
            {
                response.Code = 67823458520734;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458520734, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntHistorialClinico>> BGetHistorialMedico(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null)
        {
            IMDResponse<List<EntHistorialClinico>> response = new IMDResponse<List<EntHistorialClinico>>();

            string metodo = nameof(this.BGetHistorialMedico);
            logger.Info(IMDSerialize.Serialize(67823458524619, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataTable> resGetHistorialClinico = datConsulta.DGetHistorialMedico(piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio);
                if (resGetHistorialClinico.Code != 0)
                {
                    return resGetHistorialClinico.GetResponse<List<EntHistorialClinico>>();
                }

                List<EntHistorialClinico> lstHistorial = new List<EntHistorialClinico>();

                foreach (DataRow drHistorial in resGetHistorialClinico.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drHistorial);

                    EntHistorialClinico entHistorialClinico = new EntHistorialClinico
                    {
                        dtFechaCreacion = dr.ConvertTo<DateTime?>("dtFechaCreacion"),
                        fAltura = dr.ConvertTo<double>("fAltura"),
                        fPeso = dr.ConvertTo<double>("fPeso"),
                        iIdConsulta = dr.ConvertTo<int>("iIdConsulta"),
                        iIdHistorialClinico = dr.ConvertTo<int>("iIdHistorialClinico"),
                        sAlergias = dr.ConvertTo<string>("sAlergias"),
                        sComentarios = dr.ConvertTo<string>("sComentarios"),
                        sDiagnostico = dr.ConvertTo<string>("sDiagnostico"),
                        sFechaCreacion = string.Empty,
                        sSintomas = dr.ConvertTo<string>("sSintomas"),
                        sTratamiento = dr.ConvertTo<string>("sTratamiento"),
                    };

                    entHistorialClinico.sFechaCreacion = entHistorialClinico.dtFechaCreacion?.ToString("dd/MM/yyyy HH:mm");

                    lstHistorial.Add(entHistorialClinico);
                }

                response.Code = 0;
                response.Message = "Historial consultado";
                response.Result = lstHistorial;
            }
            catch (Exception ex)
            {
                response.Code = 67823458525396;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458525396, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntDetalleConsulta>> BGetDetalleConsulta(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<List<EntDetalleConsulta>> response = new IMDResponse<List<EntDetalleConsulta>>();

            string metodo = nameof(this.BGetDetalleConsulta);
            logger.Info(IMDSerialize.Serialize(67823458530835, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataTable> resGetConsulta = datConsulta.DGetDetalleConsulta(piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin);
                if (resGetConsulta.Code != 0)
                {
                    return resGetConsulta.GetResponse<List<EntDetalleConsulta>>();
                }

                List<EntDetalleConsulta> lstConsultas = new List<EntDetalleConsulta>();

                foreach (DataRow drConsulta in resGetConsulta.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drConsulta);

                    EntDetalleConsulta consulta = new EntDetalleConsulta
                    {
                        bTerminosYCondiciones = Convert.ToBoolean(dr.ConvertTo<int>("bTerminosYCondiciones")),
                        dtFechaConsultaFin = dr.ConvertTo<DateTime?>("dtFechaConsultaFin"),
                        dtFechaConsultaInicio = dr.ConvertTo<DateTime?>("dtFechaConsultaInicio"),
                        dtFechaCreacion = dr.ConvertTo<DateTime?>("dtFechaCreacion"),
                        dtFechaNacimientoPaciente = dr.ConvertTo<DateTime?>("dtFechaNacimientoPaciente"),
                        dtFechaProgramadaFin = dr.ConvertTo<DateTime?>("dtFechaProgramadaFin"),
                        dtFechaProgramadaInicio = dr.ConvertTo<DateTime?>("dtFechaProgramadaInicio"),
                        dtFechaVencimiento = dr.ConvertTo<DateTime?>("dtFechaVencimiento"),
                        iIdColaborador = dr.ConvertTo<int?>("iIdColaborador"),
                        iIdConsulta = dr.ConvertTo<int?>("iIdConsulta"),
                        iIdEmpresa = dr.ConvertTo<int?>("iIdEmpresa"),
                        iIdEspecialidad = dr.ConvertTo<int?>("iIdEspecialidad"),
                        iIdEstatusConsulta = dr.ConvertTo<int?>("iIdEstatusConsulta"),
                        iIdFolio = dr.ConvertTo<int?>("iIdFolio"),
                        iIdPaciente = dr.ConvertTo<int?>("iIdPaciente"),
                        iIdTipoDoctor = dr.ConvertTo<int?>("iIdTipoDoctor"),
                        iIdTipoProducto = dr.ConvertTo<int?>("iIdTipoProducto"),
                        iNumSala = dr.ConvertTo<int?>("iNumSala"),
                        sApellidoMaternoPaciente = dr.ConvertTo<string>("sApellidoMaternoPaciente"),
                        sApellidoPaternoPaciente = dr.ConvertTo<string>("sApellidoPaternoPaciente"),
                        sCorreoColaborador = dr.ConvertTo<string>("sCorreoColaborador"),
                        sCorreoEmpresa = dr.ConvertTo<string>("sCorreoEmpresa"),
                        sCorreoPaciente = dr.ConvertTo<string>("sCorreoPaciente"),
                        sEspecialidad = dr.ConvertTo<string>("sEspecialidad"),
                        sEstatusConsulta = dr.ConvertTo<string>("sEstatusConsulta"),
                        sFechaConsultaFin = string.Empty,
                        sFechaConsultaInicio = string.Empty,
                        sFechaCreacion = string.Empty,
                        sFechaNacimientoPaciente = string.Empty,
                        sFechaProgramadaFin = string.Empty,
                        sFechaProgramadaInicio = string.Empty,
                        sFechaVencimiento = string.Empty,
                        sFolio = dr.ConvertTo<string>("sFolio"),
                        sPassword = dr.ConvertTo<string>("sPassword"),
                        sFolioEmpresa = dr.ConvertTo<string>("sFolioEmpresa"),
                        sNombreColaborador = dr.ConvertTo<string>("sNombreColaborador"),
                        sNombreEmpresa = dr.ConvertTo<string>("sNombreEmpresa"),
                        sNombrePaciente = dr.ConvertTo<string>("sNombrePaciente"),
                        sNombreProducto = dr.ConvertTo<string>("sNombreProducto"),
                        sOrdenConekta = dr.ConvertTo<string>("sOrdenConekta"),
                        sOrigen = dr.ConvertTo<string>("sOrigen"),
                        sSexoPaciente = dr.ConvertTo<string>("sSexoPaciente"),
                        sTipoDoctor = dr.ConvertTo<string>("sTipoDoctor"),
                        sTipoProducto = dr.ConvertTo<string>("sTipoProducto"),
                        sTipoSangrePaciente = dr.ConvertTo<string>("sTipoSangrePaciente"),
                    };

                    consulta.sFechaConsultaFin = consulta.dtFechaConsultaFin?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaConsultaInicio = consulta.dtFechaConsultaInicio?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaCreacion = consulta.dtFechaCreacion?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaNacimientoPaciente = consulta.dtFechaNacimientoPaciente?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaProgramadaFin = consulta.dtFechaProgramadaFin?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaProgramadaInicio = consulta.dtFechaProgramadaInicio?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaVencimiento = consulta.dtFechaVencimiento?.ToString("dd/MM/yyyy HH:mm");

                    lstConsultas.Add(consulta);
                }

                response.Code = 0;
                response.Message = "Consultas obtenidas";
                response.Result = lstConsultas;
            }
            catch (Exception ex)
            {
                response.Code = 67823458531612;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458531612, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntDetalleConsulta>> BGetDisponibilidadConsulta(int piIdColaborador, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null)
        {
            IMDResponse<List<EntDetalleConsulta>> response = new IMDResponse<List<EntDetalleConsulta>>();

            string metodo = nameof(this.BGetDisponibilidadConsulta);
            logger.Info(IMDSerialize.Serialize(67823458537051, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataTable> resGetConsulta = datConsulta.DGetDisponibilidadConsulta(piIdColaborador, pdtFechaProgramadaInicio, pdtFechaProgramadaFin);
                if (resGetConsulta.Code != 0)
                {
                    return resGetConsulta.GetResponse<List<EntDetalleConsulta>>();
                }

                List<EntDetalleConsulta> lstConsultas = new List<EntDetalleConsulta>();

                foreach (DataRow drConsulta in resGetConsulta.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drConsulta);

                    EntDetalleConsulta consulta = new EntDetalleConsulta
                    {
                        dtFechaConsultaFin = dr.ConvertTo<DateTime?>("dtFechaConsultaFin"),
                        dtFechaConsultaInicio = dr.ConvertTo<DateTime?>("dtFechaConsultaInicio"),
                        dtFechaCreacion = dr.ConvertTo<DateTime?>("dtFechaCreacion"),
                        dtFechaProgramadaFin = dr.ConvertTo<DateTime?>("dtFechaProgramadaFin"),
                        dtFechaProgramadaInicio = dr.ConvertTo<DateTime?>("dtFechaProgramadaInicio"),
                        dtFechaVencimiento = dr.ConvertTo<DateTime?>("dtFechaVencimiento"),
                        iIdColaborador = dr.ConvertTo<int?>("iIdColaborador"),
                        iIdConsulta = dr.ConvertTo<int?>("iIdConsulta"),
                        iIdEmpresa = dr.ConvertTo<int?>("iIdEmpresa"),
                        iIdEspecialidad = dr.ConvertTo<int?>("iIdEspecialidad"),
                        iIdEstatusConsulta = dr.ConvertTo<int?>("iIdEstatusConsulta"),
                        iIdFolio = dr.ConvertTo<int?>("iIdFolio"),
                        iIdPaciente = dr.ConvertTo<int?>("iIdPaciente"),
                        iIdTipoDoctor = dr.ConvertTo<int?>("iIdTipoDoctor"),
                        iIdTipoProducto = dr.ConvertTo<int?>("iIdTipoProducto"),
                        iNumSala = dr.ConvertTo<int?>("iNumSala"),
                        sFolio = dr.ConvertTo<string>("sFolio"),
                        sFolioEmpresa = dr.ConvertTo<string>("sFolioEmpresa"),
                    };

                    consulta.sFechaConsultaFin = consulta.dtFechaConsultaFin?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaConsultaInicio = consulta.dtFechaConsultaInicio?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaCreacion = consulta.dtFechaCreacion?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaNacimientoPaciente = consulta.dtFechaNacimientoPaciente?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaProgramadaFin = consulta.dtFechaProgramadaFin?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaProgramadaInicio = consulta.dtFechaProgramadaInicio?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaVencimiento = consulta.dtFechaVencimiento?.ToString("dd/MM/yyyy HH:mm");

                    lstConsultas.Add(consulta);
                }

                response.Code = 0;
                response.Message = "Consultas obtenidas";
                response.Result = lstConsultas;
            }
            catch (Exception ex)
            {
                response.Code = 67823458537828;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458537828, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
