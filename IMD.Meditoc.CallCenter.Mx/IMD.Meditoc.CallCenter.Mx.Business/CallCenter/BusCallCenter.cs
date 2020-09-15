using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Business.Consulta;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Business.Paciente;
using IMD.Meditoc.CallCenter.Mx.Data.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Entities.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.CallCenter
{
    public class BusCallCenter
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusCallCenter));
        DatCallCenter datCallCenter;
        BusColaborador busColaborador;
        BusFolio busFolio;
        BusPaciente busPaciente;
        BusConsulta busConsulta;

        public BusCallCenter()
        {
            datCallCenter = new DatCallCenter();
            busColaborador = new BusColaborador();
            busFolio = new BusFolio();
            busPaciente = new BusPaciente();
            busConsulta = new BusConsulta();
        }

        public IMDResponse<bool> BCallCenterOnline(EntOnlineMod entOnlineMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BCallCenterOnline);
            logger.Info(IMDSerialize.Serialize(67823458509079, $"Inicia {metodo}"));

            try
            {
                if (entOnlineMod == null)
                {
                    response.Code = 7687672346;
                    response.Message = "No se ingresó información del colaborador";
                    return response;
                }

                IMDResponse<bool> resSvaOnline = datCallCenter.DCallCenterOnline(entOnlineMod.iIdColaborador, entOnlineMod.bOnline, entOnlineMod.bOcupado, entOnlineMod.iIdUsuarioMod);
                if (resSvaOnline.Code != 0)
                {
                    return resSvaOnline;
                }

                response.Code = 0;
                response.Result = true;
                response.Message = entOnlineMod.bOnline ? "Se ha cambiado el estatus a EN LÍNEA" + (entOnlineMod.bOcupado ? " - OCUPADO" : " - DISPONIBLE") : "Se ha cambiado el estatus a FUERA DE LÍNEA";

            }
            catch (Exception ex)
            {
                response.Code = 67823458509856;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458509856, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntCallCenter> BCallCenterStartWithFolio(int iIdColaborador, string sFolio)
        {
            IMDResponse<EntCallCenter> response = new IMDResponse<EntCallCenter>();

            string metodo = nameof(this.BCallCenterStartWithFolio);
            logger.Info(IMDSerialize.Serialize(67823458513741, $"Inicia {metodo}"));

            try
            {
                EntCallCenter entCallCenter = new EntCallCenter();

                IMDResponse<List<EntColaborador>> resGetColaborador = busColaborador.BGetColaborador(piIdColaborador: iIdColaborador);
                if (resGetColaborador.Code != 0)
                {
                    return resGetColaborador.GetResponse<EntCallCenter>();
                }
                if (resGetColaborador.Result.Count != 1)
                {
                    response.Code = 8793487583457;
                    response.Message = "El colaborador no existe";
                    return response;
                }

                entCallCenter.entColaborador = resGetColaborador.Result.First();

                IMDResponse<List<EntFolioReporte>> resGetFolio = busFolio.BGetFolios(psFolio: sFolio);
                if (resGetFolio.Code != 0)
                {
                    return resGetFolio.GetResponse<EntCallCenter>();
                }
                if (resGetFolio.Result.Count != 1)
                {
                    response.Code = 8793487583457;
                    response.Message = "El folio no existe";
                    return response;
                }

                entCallCenter.entFolio = resGetFolio.Result.First();

                IMDResponse<List<EntPaciente>> resGetPaciente = busPaciente.BGetPacientes(piIdFolio: entCallCenter.entFolio.iIdFolio);
                if (resGetPaciente.Code != 0)
                {
                    return resGetPaciente.GetResponse<EntCallCenter>();
                }

                if (resGetPaciente.Result.Count != 1)
                {
                    response.Code = 8793487583457;
                    response.Message = "El paciente no existe";
                    return response;
                }

                entCallCenter.entPaciente = resGetPaciente.Result.First();

                if (entCallCenter.entColaborador.iIdTipoDoctor == (int)EnumTipoDoctor.MedicoCallCenter)
                {
                    EntConsulta entConsulta = new EntConsulta
                    {
                        iIdColaborador = entCallCenter.entColaborador.iIdColaborador,
                        iIdPaciente = entCallCenter.entPaciente.iIdPaciente,
                        iIdEstatusConsulta = (int)EnumEstatusConsulta.CreadoProgramado,
                        dtFechaProgramadaInicio = DateTime.Now
                    };

                    IMDResponse<EntConsulta> resSaveConsulta = busConsulta.BSaveConsulta(entConsulta, entCallCenter.entColaborador.iIdUsuarioCGU);
                    if (resSaveConsulta.Code != 0)
                    {
                        return resSaveConsulta.GetResponse<EntCallCenter>();
                    }

                    entCallCenter.entConsulta = resSaveConsulta.Result;
                }

                IMDResponse<List<EntHistorialClinico>> resGetHistorial = busConsulta.BGetHistorialMedico(piIdPaciente: entCallCenter.entPaciente.iIdPaciente);
                if (resGetHistorial.Code != 0)
                {
                    return resGetHistorial.GetResponse<EntCallCenter>();
                }

                entCallCenter.lstHistorialClinico = resGetHistorial.Result;

                response.Code = 0;
                response.Result = entCallCenter;

            }
            catch (Exception ex)
            {
                response.Code = 67823458514518;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458514518, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BIniciarConsulta(int iIdConsulta, int iIdColaborador, int iIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BIniciarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458526173, $"Inicia {metodo}"));

            try
            {
                if (iIdConsulta == 0)
                {
                    response.Code = 8793487583457;
                    response.Message = "La consulta no existe";
                    return response;
                }

                EntConsulta entConsulta = new EntConsulta
                {
                    iIdConsulta = iIdConsulta,
                    dtFechaConsultaInicio = DateTime.Now,
                    iIdEstatusConsulta = (int)EnumEstatusConsulta.EnConsulta
                };

                IMDResponse<EntConsulta> resSaveConsulta = busConsulta.BSaveConsulta(entConsulta, iIdUsuarioMod);
                if (resSaveConsulta.Code != 0)
                {
                    return resSaveConsulta.GetResponse<bool>();
                }

                EntOnlineMod entOnlineMod = new EntOnlineMod
                {
                    bOcupado = true,
                    bOnline = true,
                    iIdColaborador = iIdColaborador,
                    iIdUsuarioMod = iIdUsuarioMod
                };

                IMDResponse<bool> resUpdColaborador = this.BCallCenterOnline(entOnlineMod);
                if (resSaveConsulta.Code != 0)
                {
                    return resUpdColaborador;
                }

                response.Code = 0;
                response.Message = "Consulta iniciada. " + resUpdColaborador.Message;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458526950;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458526950, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BFinalizarConsulta(int iIdConsulta, int iIdColaborador, int iIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BFinalizarConsulta);
            logger.Info(IMDSerialize.Serialize(67823458527727, $"Inicia {metodo}"));

            try
            {
                if (iIdConsulta == 0)
                {
                    response.Code = 8793487583457;
                    response.Message = "La consulta no existe";
                    return response;
                }

                EntConsulta entConsulta = new EntConsulta
                {
                    iIdConsulta = iIdConsulta,
                    dtFechaConsultaFin = DateTime.Now,
                    dtFechaProgramadaFin = DateTime.Now,
                    iIdEstatusConsulta = (int)EnumEstatusConsulta.Finalizado
                };

                IMDResponse<EntConsulta> resSaveConsulta = busConsulta.BSaveConsulta(entConsulta, iIdUsuarioMod);
                if (resSaveConsulta.Code != 0)
                {
                    return resSaveConsulta.GetResponse<bool>();
                }

                IMDResponse<List<EntDetalleConsulta>> resGetConsulta = busConsulta.BGetDetalleConsulta(piIdConsulta: iIdConsulta);
                if (resGetConsulta.Code != 0)
                {
                    return resGetConsulta.GetResponse<bool>();
                }

                if (resGetConsulta.Result.Count < 1)
                {
                    response.Code = 8793487583457;
                    response.Message = "La consulta no existe";
                    return response;
                }

                EntDetalleConsulta consulta = resGetConsulta.Result.First();

                if (consulta.iIdTipoProducto == (int)EnumTipoProducto.Servicio)
                {
                    EntFolioFV entFolio = new EntFolioFV
                    {
                        iIdEmpresa = (int)consulta.iIdEmpresa,
                        iIdUsuario = iIdUsuarioMod,
                        lstFolios = new List<EntFolioFVItem>
                        {
                            new EntFolioFVItem
                            {
                                iIdFolio = (int)consulta.iIdFolio
                            }
                        }
                    };

                    IMDResponse<bool> resDesactivarFolios = busFolio.BEliminarFoliosEmpresa(entFolio);
                    if (resDesactivarFolios.Code != 0)
                    {
                        return resDesactivarFolios;
                    }
                }

                response.Code = 0;
                response.Message = "Consulta finalizada.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458528504;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458528504, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
