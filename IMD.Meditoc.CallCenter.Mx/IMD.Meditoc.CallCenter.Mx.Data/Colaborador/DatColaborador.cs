using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace IMD.Meditoc.CallCenter.Mx.Data.Colaborador
{
    public class DatColaborador
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatColaborador));
        private Database database;
        IMDCommonData imdCommonData;
        string spSaveColaborador;
        string spGetColaborador;
        string spSaveColaboradorFoto;
        string spGetColaboradorFoto;
        string spDeleteColaboradorFoto;
        string spGetColaboradorDirectorio;
        string spGetObtenerSala;
        string spGetColaboradorStatus;

        public DatColaborador()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spSaveColaborador = "sva_meditoc_save_colaborador";
            spGetColaborador = "svc_meditoc_colaboradores";
            spSaveColaboradorFoto = "sva_meditoc_save_colaboradorfoto";
            spGetColaboradorFoto = "svc_meditoc_colaboradorfoto";
            spDeleteColaboradorFoto = "sva_meditoc_del_colaboradorfoto";
            spGetColaboradorDirectorio = "svc_meditoc_colaboradordirectorio";
            spGetObtenerSala = "svc_app_ObtenerSala";
            spGetColaboradorStatus = "svc_meditoc_colaborador_status";
        }

        public IMDResponse<bool> DSaveColaborador(EntCreateColaborador entCreateColaborador)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveColaborador);
            logger.Info(IMDSerialize.Serialize(67823458456243, $"Inicia {metodo}(EntCreateColaborador entCreateColaborador)", entCreateColaborador));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveColaborador))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, entCreateColaborador.iIdColaborador);
                    database.AddInParameter(dbCommand, "piIdTipoDoctor", DbType.Int32, entCreateColaborador.iIdTipoDoctor);
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, entCreateColaborador.iIdEspecialidad);
                    database.AddInParameter(dbCommand, "piIdUsuarioCGU", DbType.Int32, entCreateColaborador.iIdUsuarioCGU);
                    database.AddInParameter(dbCommand, "piIdTipoCuenta", DbType.Int32, entCreateColaborador.iIdTipoCuenta);
                    database.AddInParameter(dbCommand, "piNumSala", DbType.Int32, entCreateColaborador.iNumSala);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entCreateColaborador.sNombreDirectorio);
                    database.AddInParameter(dbCommand, "psNombreConsultorio", DbType.String, entCreateColaborador.sNombreConsultorio);
                    database.AddInParameter(dbCommand, "psCedulaProfecional", DbType.String, entCreateColaborador.sCedulaProfecional);
                    database.AddInParameter(dbCommand, "psTelefono", DbType.String, entCreateColaborador.sTelefonoDirectorio);
                    database.AddInParameter(dbCommand, "psWhatsApp", DbType.String, entCreateColaborador.sWhatsApp);
                    database.AddInParameter(dbCommand, "psCorreo", DbType.String, entCreateColaborador.sCorreoDirectorio);
                    database.AddInParameter(dbCommand, "psDireccionConsultorio", DbType.String, entCreateColaborador.sDireccionConsultorio);
                    database.AddInParameter(dbCommand, "psRFC", DbType.String, entCreateColaborador.sRFC);
                    database.AddInParameter(dbCommand, "psURL", DbType.String, entCreateColaborador.sURL);
                    database.AddInParameter(dbCommand, "psMaps", DbType.String, entCreateColaborador.sMaps);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entCreateColaborador.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entCreateColaborador.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entCreateColaborador.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458457020;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar el colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458457020, $"Error en {metodo}(EntCreateColaborador entCreateColaborador): {ex.Message}", entCreateColaborador, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetColaborador(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetColaborador);
            logger.Info(IMDSerialize.Serialize(67823458470229, $"Inicia {metodo}(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null)", piIdColaborador, piIdTipoDoctor, piIdEspecialidad, piIdUsuarioCGU));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetColaborador))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdTipoDoctor", DbType.Int32, piIdTipoDoctor);
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, piIdEspecialidad);
                    database.AddInParameter(dbCommand, "piIdUsuarioCGU", DbType.Int32, piIdUsuarioCGU);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458471006;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar los colaboradores.";

                logger.Error(IMDSerialize.Serialize(67823458471006, $"Error en {metodo}(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null): {ex.Message}", piIdColaborador, piIdTipoDoctor, piIdEspecialidad, piIdUsuarioCGU, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DSaveColaboradorFoto(int piIdColaborador, int piIdUsuarioMod, byte[] pFoto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458477999, $"Inicia {metodo}(int piIdColaborador, int piIdUsuarioMod, byte[] pFoto)", piIdColaborador, piIdUsuarioMod));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveColaboradorFoto))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);
                    database.AddInParameter(dbCommand, "pFoto", DbType.Binary, pFoto);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458478776;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458478776, $"Error en {metodo}(int piIdColaborador, int piIdUsuarioMod, byte[] pFoto): {ex.Message}", piIdColaborador, piIdUsuarioMod, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458482661, $"Inicia {metodo}(int piIdColaborador)", piIdColaborador));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetColaboradorFoto))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458483438;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458483438, $"Error en {metodo}(int piIdColaborador): {ex.Message}", piIdColaborador, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DEliminarColaboradorFoto(int piIdColaborador, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DEliminarColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458491985, $"Inicia {metodo}(int piIdColaborador, int piIdUsuarioMod)", piIdColaborador, piIdUsuarioMod));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spDeleteColaboradorFoto))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458492762;
                response.Message = "Ocurrió un error inesperado en la base de datos al eliminar la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458492762, $"Error en {metodo}(int piIdColaborador, int piIdUsuarioMod): {ex.Message}", piIdColaborador, piIdUsuarioMod, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetDirectorio(int? piIdEspecialidad = null, string psBuscador = null, int piLimitInit = 0, int piLimitEnd = 0, bool? pbAcceso = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetDirectorio);
            logger.Info(IMDSerialize.Serialize(67823458496647, $"Inicia {metodo}(int? piIdEspecialidad = null, string psBuscador = null, int piLimitInit = 0, int piLimitEnd = 0)", piIdEspecialidad, psBuscador, piLimitInit, piLimitEnd));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetColaboradorDirectorio))
                {
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, piIdEspecialidad);
                    database.AddInParameter(dbCommand, "psBuscador", DbType.String, psBuscador);
                    database.AddInParameter(dbCommand, "piLimitInit", DbType.Int32, piLimitInit);
                    database.AddInParameter(dbCommand, "piLimitEnd", DbType.Int32, piLimitEnd);
                    database.AddInParameter(dbCommand, "pbAcceso", DbType.Boolean, pbAcceso);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458497424;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar el directorio de médicos.";

                logger.Error(IMDSerialize.Serialize(67823458497424, $"Error en {metodo}(int? piIdEspecialidad = null, string psBuscador = null, int piLimitInit = 0, int piLimitEnd = 0): {ex.Message}", piIdEspecialidad, psBuscador, piLimitInit, piLimitEnd, ex, response));
            }
            return response;
        }


        public IMDResponse<DataTable> DObtenerSala(bool? bAgendada = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObtenerSala);
            logger.Info(IMDSerialize.Serialize(67823458592995, $"Inicia {metodo}(bool? bAgendada = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null)", bAgendada, iIdUsuario, dtFechaConsulta));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetObtenerSala))
                {
                    database.AddInParameter(dbCommand, "pbEsAgendada", DbType.Boolean, bAgendada);
                    database.AddInParameter(dbCommand, "piIdUsuario", DbType.String, iIdUsuario);
                    database.AddInParameter(dbCommand, "pdtFechaConsulta", DbType.DateTime, dtFechaConsulta);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458593772;
                response.Message = "Ocurrió un error inesperado en la base de datos al obtener la sala del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458593772, $"Error en {metodo}(bool? bAgendada = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null): {ex.Message}", bAgendada, iIdUsuario, dtFechaConsulta, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DGetColaboradorStatus(int piIdColaborador)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetColaboradorStatus);
            logger.Info(IMDSerialize.Serialize(67823458647385, $"Inicia {metodo}(int piIdColaborador)", piIdColaborador));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetColaboradorStatus))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, piIdColaborador);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458648162;
                response.Message = "Ocurrió un error inesperado en la base de datos el consultar el status del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458648162, $"Error en {metodo}(int piIdColaborador): {ex.Message}", piIdColaborador, ex, response));
            }
            return response;
        }
    }
}
