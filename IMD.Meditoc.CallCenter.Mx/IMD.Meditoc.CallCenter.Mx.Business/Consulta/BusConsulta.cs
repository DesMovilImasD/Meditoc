using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Data.Consulta;
using IMD.Meditoc.CallCenter.Mx.Entities.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

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

        /// <summary>
        /// Guarda la consulta para un paciente
        /// </summary>
        /// <param name="entConsulta"></param>
        /// <param name="piIdUsuarioMod"></param>
        /// <returns></returns>
        public IMDResponse<EntConsulta> BSaveConsulta(EntConsulta entConsulta, int piIdUsuarioMod = 1)
        {
            IMDResponse<EntConsulta> response = new IMDResponse<EntConsulta>();

            string metodo = nameof(this.BSaveConsulta);
            logger.Info(IMDSerialize.Serialize(67823458519957, $"Inicia {metodo}(EntConsulta entConsulta, int piIdUsuarioMod = 1)", entConsulta, piIdUsuarioMod));

            try
            {
                if (entConsulta == null)
                {
                    response.Code = -8783458839487;
                    response.Message = "No se ingresó información para guardar la consulta.";
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
                    response.Code = -7623486876234;
                    response.Message = "No se ha sido posible acceder a la consulta en este momento. Intente de nuevo.";
                    return response;
                }

                entConsulta.iIdConsulta = Convert.ToInt32(resSaveConsulta.Result.Rows[0]["iIdConsulta"].ToString());

                response.Code = 0;
                response.Message = "La consulta ha sido guardada correctamente.";
                response.Result = entConsulta;
            }
            catch (Exception ex)
            {
                response.Code = 67823458520734;
                response.Message = "Ocurrió un error inesperado al guardar la consulta.";

                logger.Error(IMDSerialize.Serialize(67823458520734, $"Error en {metodo}(EntConsulta entConsulta, int piIdUsuarioMod = 1): {ex.Message}", entConsulta, piIdUsuarioMod, ex, response));
            }
            return response;
        }

        //Obtiene el historial clinico del paciente
        public IMDResponse<List<EntHistorialClinico>> BGetHistorialMedico(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null, string psIdTipoDoctor = null)
        {
            IMDResponse<List<EntHistorialClinico>> response = new IMDResponse<List<EntHistorialClinico>>();

            string metodo = nameof(this.BGetHistorialMedico);
            logger.Info(IMDSerialize.Serialize(67823458524619, $"Inicia {metodo}(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null)", piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio));

            try
            {
                IMDResponse<DataTable> resGetHistorialClinico = datConsulta.DGetHistorialMedico(piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio, psIdTipoDoctor);
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
                        dtFechaConsultaFin = dr.ConvertTo<DateTime?>("dtFechaConsultaFin"),
                        dtFechaConsultaInicio = dr.ConvertTo<DateTime?>("dtFechaConsultaInicio"),
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

                    entHistorialClinico.sFechaCreacion = entHistorialClinico.dtFechaCreacion?.ToString("ddd dd/MM/yyyy hh:mm tt");
                    entHistorialClinico.sFechaConsultaInicio = entHistorialClinico.dtFechaConsultaInicio?.ToString("ddd dd/MM/yyyy hh:mm tt");
                    entHistorialClinico.sFechaConsultaFin = entHistorialClinico.dtFechaConsultaFin?.ToString("ddd dd/MM/yyyy hh:mm tt");

                    TimeSpan? dff = entHistorialClinico.dtFechaConsultaFin - entHistorialClinico.dtFechaConsultaInicio;

                    entHistorialClinico.sDuracionConsulta = dff?.ToString(@"hh\:mm\:ss");

                    lstHistorial.Add(entHistorialClinico);
                }

                response.Code = 0;
                response.Message = "Se ha consultado el historial clínico.";
                response.Result = lstHistorial;
            }
            catch (Exception ex)
            {
                response.Code = 67823458525396;
                response.Message = "Ocurrió un error inesperado al consultar el historial clínico.";

                logger.Error(IMDSerialize.Serialize(67823458525396, $"Error en {metodo}(int? piIdHistorialClinico = null, int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdFolio = null): {ex.Message}", piIdHistorialClinico, piIdConsulta, piIdPaciente, piIdColaborador, piIdFolio, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene la lista de detalles de la consulta solicitada
        /// </summary>
        /// <param name="piIdConsulta"></param>
        /// <param name="piIdPaciente"></param>
        /// <param name="piIdColaborador"></param>
        /// <param name="piIdEstatusConsulta"></param>
        /// <param name="pdtFechaProgramadaInicio"></param>
        /// <param name="pdtFechaProgramadaFin"></param>
        /// <param name="pdtFechaConsultaInicio"></param>
        /// <param name="pdtFechaConsultaFin"></param>
        /// <returns></returns>
        public IMDResponse<List<EntDetalleConsulta>> BGetDetalleConsulta(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<List<EntDetalleConsulta>> response = new IMDResponse<List<EntDetalleConsulta>>();

            string metodo = nameof(this.BGetDetalleConsulta);
            logger.Info(IMDSerialize.Serialize(67823458530835, $"Inicia {metodo}(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)", piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                pdtFechaProgramadaInicio = pdtFechaProgramadaInicio?.Date;
                pdtFechaProgramadaFin = pdtFechaProgramadaFin?.AddDays(1).Date;

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
                        iIdOrigen = dr.ConvertTo<int?>("iIdOrigen"),
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
                        sTelefonoPaciente = dr.ConvertTo<string>("sTelefonoPaciente"),
                        sTipoDoctor = dr.ConvertTo<string>("sTipoDoctor"),
                        sTipoProducto = dr.ConvertTo<string>("sTipoProducto"),
                        sTipoSangrePaciente = dr.ConvertTo<string>("sTipoSangrePaciente"),
                    };

                    consulta.sFechaConsultaFin = consulta.dtFechaConsultaFin?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaConsultaInicio = consulta.dtFechaConsultaInicio?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaCreacion = consulta.dtFechaCreacion?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaNacimientoPaciente = consulta.dtFechaNacimientoPaciente?.ToString("dd/MM/yyyy");
                    consulta.sFechaProgramadaFin = consulta.dtFechaProgramadaFin?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaProgramadaInicio = consulta.dtFechaProgramadaInicio?.ToString("dd/MM/yyyy HH:mm");
                    consulta.sFechaVencimiento = consulta.dtFechaVencimiento?.ToString("dd/MM/yyyy HH:mm");

                    lstConsultas.Add(consulta);
                }

                response.Code = 0;
                response.Message = lstConsultas.Count == 0 ? "No se encontraron las consultas solicitadas." : "Se han obtenido los detalles de las consultas.";
                response.Result = lstConsultas;
            }
            catch (Exception ex)
            {
                response.Code = 67823458531612;
                response.Message = "Ocurrió un error inesperado al consultar el detalle de la consulta médica.";

                logger.Error(IMDSerialize.Serialize(67823458531612, $"Error en {metodo}(int? piIdConsulta = null, int? piIdPaciente = null, int? piIdColaborador = null, int? piIdEstatusConsulta = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null, DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null): {ex.Message}", piIdConsulta, piIdPaciente, piIdColaborador, piIdEstatusConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene las consultas programadas en el horario solicitado para una nueva consulta
        /// </summary>
        /// <param name="piIdColaborador"></param>
        /// <param name="piIdConsulta"></param>
        /// <param name="pdtFechaProgramadaInicio"></param>
        /// <param name="pdtFechaProgramadaFin"></param>
        /// <returns></returns>
        public IMDResponse<List<EntDetalleConsulta>> BGetDisponibilidadConsulta(int piIdColaborador, int piIdConsulta, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null)
        {
            IMDResponse<List<EntDetalleConsulta>> response = new IMDResponse<List<EntDetalleConsulta>>();

            string metodo = nameof(this.BGetDisponibilidadConsulta);
            logger.Info(IMDSerialize.Serialize(67823458537051, $"Inicia {metodo}(int piIdColaborador, int piIdConsulta, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null)", piIdColaborador, piIdConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin));

            try
            {
                IMDResponse<DataTable> resGetConsulta = datConsulta.DGetDisponibilidadConsulta(piIdColaborador, piIdConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin);
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
                response.Message = "Se ha consultado la disponibilidad del colaborador.";
                response.Result = lstConsultas;
            }
            catch (Exception ex)
            {
                response.Code = 67823458537828;
                response.Message = "Ocurrió un error inesperado al consultar la disponibilidad del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458537828, $"Error en {metodo}(int piIdColaborador, int piIdConsulta, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFin = null): {ex.Message}", piIdColaborador, piIdConsulta, pdtFechaProgramadaInicio, pdtFechaProgramadaFin, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Cancela la consulta y el folio de un paciente
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public IMDResponse<bool> BCancelarConsulta(EntNuevaConsulta consulta)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BCancelarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458552591, $"Inicia {metodo}(EntNuevaConsulta consulta)", consulta));

            try
            {
                if (consulta == null)
                {
                    response.Code = -767234562313709;
                    response.Message = "No se ingresó información de la consulta a cancelar";
                    return response;
                }

                IMDResponse<List<EntDetalleConsulta>> resGetDetalleConsulta = this.BGetDetalleConsulta(consulta.consulta.iIdConsulta);
                if (resGetDetalleConsulta.Code != 0)
                {
                    return resGetDetalleConsulta.GetResponse<bool>();
                }

                if (resGetDetalleConsulta.Result.Count != 1)
                {
                    response.Code = -5723613487698;
                    response.Message = "La consulta proporcionada no se encuentra programada.";
                    return response;
                }

                EntDetalleConsulta detalleConsulta = resGetDetalleConsulta.Result.First();

                consulta.consulta.iIdEstatusConsulta = (int)EnumEstatusConsulta.Cancelado;

                IMDResponse<bool> resDelConsulta = datConsulta.DCancelarConsulta(consulta.consulta.iIdConsulta, consulta.iIdUsuarioMod, (int)consulta.consulta.iIdEstatusConsulta);
                if (resDelConsulta.Code != 0)
                {
                    return resDelConsulta;
                }

                if (detalleConsulta.iIdOrigen == (int)EnumOrigen.Particular)
                {
                    EntFolioFV entFolio = new EntFolioFV
                    {
                        iIdEmpresa = (int)detalleConsulta.iIdEmpresa,
                        iIdUsuario = consulta.iIdUsuarioMod,
                        lstFolios = new List<EntFolioFVItem>
                        {
                            new EntFolioFVItem
                            {
                                iIdFolio = (int)detalleConsulta.iIdFolio
                            }
                        }
                    };
                    BusFolio busFolio = new BusFolio();
                    IMDResponse<bool> resDesactivarFolios = busFolio.BEliminarFoliosEmpresa(entFolio);
                    if (resDesactivarFolios.Code != 0)
                    {
                        return resDesactivarFolios;
                    }
                }

                response.Code = 0;
                response.Result = true;
                response.Message = "La consulta ha sido cancelada correctamente.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458553368;
                response.Message = "Ocurrió un error inesperado al cancelar la consulta del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458553368, $"Error en {metodo}(EntNuevaConsulta consulta): {ex.Message}", consulta, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Guarda el hisotirial clinico de un paciente
        /// </summary>
        /// <param name="entHistorialClinico"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveHistorialClinico(EntHistorialClinico entHistorialClinico)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveHistorialClinico);
            logger.Info(IMDSerialize.Serialize(67823458583671, $"Inicia {metodo}(EntHistorialClinico entHistorialClinico)", entHistorialClinico));

            try
            {
                if (entHistorialClinico == null)
                {
                    response.Code = -65766123898345;
                    response.Message = "No se ingresó información del historial clínico.";
                    return response;
                }

                IMDResponse<bool> resSaveHistorial = datConsulta.DSaveHistorialMedico(
                    entHistorialClinico.iIdConsulta,
                    entHistorialClinico.iIdUsuarioMod,
                    entHistorialClinico.sSintomas,
                    entHistorialClinico.sDiagnostico,
                    entHistorialClinico.sTratamiento,
                    entHistorialClinico.fPeso,
                    entHistorialClinico.fAltura,
                    entHistorialClinico.sAlergias,
                    entHistorialClinico.sComentarios);

                if (resSaveHistorial.Code != 0)
                {
                    return resSaveHistorial;
                }

                response.Code = 0;
                response.Message = "El historial clínico se guardó correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458584448;
                response.Message = "Ocurrió un error inesperado al guardar el historial clínico del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458584448, $"Error en {metodo}(EntHistorialClinico entHistorialClinico): {ex.Message}", entHistorialClinico, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene los detalles de la consulta del paciente que debe estar programada en el momento de la solicitud
        /// </summary>
        /// <param name="piIdPaciente"></param>
        /// <param name="piIdColaborador"></param>
        /// <returns></returns>
        public IMDResponse<List<EntDetalleConsulta>> BGetConsultaMomento(int piIdPaciente, int piIdColaborador)
        {
            IMDResponse<List<EntDetalleConsulta>> response = new IMDResponse<List<EntDetalleConsulta>>();

            string metodo = nameof(this.BGetConsultaMomento);
            logger.Info(IMDSerialize.Serialize(67823458588333, $"Inicia {metodo}(int piIdPaciente, int piIdColaborador)", piIdPaciente, piIdColaborador));

            try
            {
                int iMinutosToleranciaAntes = Convert.ToInt32(ConfigurationManager.AppSettings["iMinToleraciaConsultaInicio"]);
                int iMinutosToleranciaDespues = Convert.ToInt32(ConfigurationManager.AppSettings["iMinToleraciaConsultaFin"]);

                IMDResponse<DataTable> resGetConsulta = datConsulta.DGetConsultaProgramada(piIdPaciente, piIdColaborador, iMinutosToleranciaAntes, iMinutosToleranciaDespues);
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
                        iIdColaborador = dr.ConvertTo<int?>("iIdColaborador"),
                        iIdConsulta = dr.ConvertTo<int?>("iIdConsulta"),
                        iIdEstatusConsulta = dr.ConvertTo<int?>("iIdEstatusConsulta"),
                        iIdPaciente = dr.ConvertTo<int?>("iIdPaciente"),
                    };

                    lstConsultas.Add(consulta);
                }

                response.Code = 0;
                response.Message = "Se han obtenido las consultas programadas en el horario programado actual.";
                response.Result = lstConsultas;
            }
            catch (Exception ex)
            {
                response.Code = 67823458589110;
                response.Message = "Ocurrió un error inesperado al verificar el horario de consulta del paciente.";

                logger.Error(IMDSerialize.Serialize(67823458589110, $"Error en {metodo}(int piIdPaciente, int piIdColaborador): {ex.Message}", piIdPaciente, piIdColaborador, ex, response));
            }
            return response;
        }
    }
}
