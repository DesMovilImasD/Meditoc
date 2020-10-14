using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Data.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;

namespace IMD.Meditoc.CallCenter.Mx.Business.Colaborador
{
    public class BusColaborador
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusColaborador));

        DatColaborador datColaborador;
        BusUsuario busUsuario;

        public BusColaborador()
        {
            datColaborador = new DatColaborador();
            busUsuario = new BusUsuario();
        }

        /// <summary>
        /// Guarda un colaborador y sus respectivas cuentas en el CGU
        /// </summary>
        /// <param name="entCreateColaborador"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveColaborador(EntCreateColaborador entCreateColaborador)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveColaborador);
            logger.Info(IMDSerialize.Serialize(67823458457797, $"Inicia {metodo}(EntCreateColaborador entCreateColaborador)", entCreateColaborador));

            try
            {
                if (entCreateColaborador == null)
                {
                    response.Code = 76823456345;
                    response.Message = "No se ingresó información del colaborador.";
                    return response;
                }

                //using (TransactionScope scope = new TransactionScope())
                //{
                if (entCreateColaborador.iIdTipoDoctor == (int)EnumTipoDoctor.MedicoCallCenter || entCreateColaborador.iIdTipoDoctor == (int)EnumTipoDoctor.MedicoAdministrativo)
                {
                    EntUsuario entUsuario = new EntUsuario
                    {
                        bActivo = entCreateColaborador.bActivo,
                        bBaja = entCreateColaborador.bBaja,
                        dtFechaNacimiento = entCreateColaborador.dtFechaNacimientoDoctor,
                        iIdPerfil = entCreateColaborador.bAdministrador ? (int)EnumPerfilPrincipal.DoctorAdministrador : (int)EnumPerfilPrincipal.DoctorCallCenter,
                        iIdTipoCuenta = (int)EnumTipoCuenta.Titular,
                        iIdUsuario = entCreateColaborador.iIdUsuarioCGU,
                        iIdUsuarioMod = entCreateColaborador.iIdUsuarioMod,
                        sApellidoMaterno = entCreateColaborador.sApellidoMaternoDoctor,
                        sApellidoPaterno = entCreateColaborador.sApellidoPaternoDoctor,
                        sCorreo = entCreateColaborador.sCorreoDoctor,
                        sDomicilio = entCreateColaborador.sDomicilioDoctor,
                        sNombres = entCreateColaborador.sNombresDoctor,
                        sPassword = entCreateColaborador.sPasswordTitular,
                        sTelefono = entCreateColaborador.sTelefonoDoctor,
                        sUsuario = entCreateColaborador.sUsuarioTitular,
                        bAcceso = entCreateColaborador.bAcceso
                    };

                    IMDResponse<EntUsuario> respuestaGuardarUsuarioCGU = busUsuario.BSaveUsuario(entUsuario, true);
                    if (respuestaGuardarUsuarioCGU.Code != 0)
                    {
                        return respuestaGuardarUsuarioCGU.GetResponse<bool>();
                    }

                    entCreateColaborador.iIdEspecialidad = (int)EnumEspecialidadPrincipal.MedicinaGeneral;
                    entCreateColaborador.iIdUsuarioCGU = (int)respuestaGuardarUsuarioCGU.Result.iIdUsuario;
                    entCreateColaborador.iIdTipoCuenta = (int)EnumTipoCuenta.Titular;
                    entCreateColaborador.iIdTipoDoctor = entCreateColaborador.bAdministrador ? (int)EnumTipoDoctor.MedicoAdministrativo : entCreateColaborador.iIdTipoDoctor;

                    IMDResponse<bool> respuestaGuardarColaborador = datColaborador.DSaveColaborador(entCreateColaborador);
                    if (respuestaGuardarColaborador.Code != 0)
                    {
                        return respuestaGuardarColaborador;
                    }
                }
                else if (entCreateColaborador.iIdTipoDoctor == (int)EnumTipoDoctor.MedicoEspecialista)
                {
                    if (entCreateColaborador.bActivo && !entCreateColaborador.bBaja)
                    {
                        if (entCreateColaborador.iIdEspecialidad == 0)
                        {
                            response.Code = -876862348762374;
                            response.Message = "No se ha especificado la especialidad del médico colaborador.";
                            return response;
                        }

                        if (string.IsNullOrWhiteSpace(entCreateColaborador.sUsuarioAdministrativo) && entCreateColaborador.bAcceso)
                        {
                            response.Code = -7234869627782;
                            response.Message = "No se han especificado los datos de cuenta administrativa.";
                            return response;
                        }

                        if (entCreateColaborador.iIdColaborador == 0 && string.IsNullOrWhiteSpace(entCreateColaborador.sPasswordAdministrativo) && entCreateColaborador.bAcceso)
                        {
                            response.Code = -324778287623;
                            response.Message = "No se han especificado los datos de cuenta administrativa.";
                            return response;
                        }
                    }

                    EntUsuario entUsuarioTitular = new EntUsuario
                    {
                        bActivo = entCreateColaborador.bActivo,
                        bBaja = entCreateColaborador.bBaja,
                        dtFechaNacimiento = entCreateColaborador.dtFechaNacimientoDoctor,
                        iIdPerfil = (int)EnumPerfilPrincipal.DoctorEspecialista,
                        iIdTipoCuenta = (int)EnumTipoCuenta.Titular,
                        iIdUsuario = entCreateColaborador.iIdUsuarioCGU,
                        iIdUsuarioMod = entCreateColaborador.iIdUsuarioMod,
                        sApellidoMaterno = entCreateColaborador.sApellidoMaternoDoctor,
                        sApellidoPaterno = entCreateColaborador.sApellidoPaternoDoctor,
                        sCorreo = entCreateColaborador.sCorreoDoctor,
                        sDomicilio = entCreateColaborador.sDomicilioDoctor,
                        sNombres = entCreateColaborador.sNombresDoctor,
                        sPassword = entCreateColaborador.sPasswordTitular,
                        sTelefono = entCreateColaborador.sTelefonoDoctor,
                        sUsuario = entCreateColaborador.sUsuarioTitular,
                        bAcceso = entCreateColaborador.bAcceso
                    };

                    EntUsuario entUsuarioAdministrativo = new EntUsuario
                    {
                        bActivo = entCreateColaborador.bActivo,
                        bBaja = entCreateColaborador.bBaja,
                        dtFechaNacimiento = entCreateColaborador.dtFechaNacimientoDoctor,
                        iIdPerfil = (int)EnumPerfilPrincipal.AdministradorEspecialista,
                        iIdTipoCuenta = (int)EnumTipoCuenta.Administrativa,
                        iIdUsuario = entCreateColaborador.iIdUsuarioCGU,
                        iIdUsuarioMod = entCreateColaborador.iIdUsuarioMod,
                        sApellidoMaterno = entCreateColaborador.sApellidoMaternoDoctor,
                        sApellidoPaterno = entCreateColaborador.sApellidoPaternoDoctor,
                        sCorreo = entCreateColaborador.sCorreoDoctor,
                        sDomicilio = entCreateColaborador.sDomicilioDoctor,
                        sNombres = entCreateColaborador.sNombresDoctor,
                        sPassword = entCreateColaborador.sPasswordAdministrativo,
                        sTelefono = entCreateColaborador.sTelefonoDoctor,
                        sUsuario = entCreateColaborador.sUsuarioAdministrativo,
                        bAcceso = entCreateColaborador.bAcceso
                    };


                    if (entCreateColaborador.bActivo && !entCreateColaborador.bBaja)
                    {
                        IMDResponse<bool> resValidacionTitular = busUsuario.BValidaDatos(entUsuarioTitular);
                        if (resValidacionTitular.Code != 0)
                        {
                            return resValidacionTitular;
                        }

                        IMDResponse<bool> resValidacionAdministrativo = busUsuario.BValidaDatos(entUsuarioAdministrativo);
                        if (resValidacionAdministrativo.Code != 0)
                        {
                            return resValidacionAdministrativo;
                        }
                    }


                    bool activacionUsuario = false;
                    if (entCreateColaborador.bAcceso)
                    {
                        if (entCreateColaborador.iIdColaborador != 0)
                        {
                            IMDResponse<List<EntColaborador>> resGetColaborador = this.BGetColaborador(entCreateColaborador.iIdColaborador);
                            if (resGetColaborador.Code == 0)
                            {
                                if (resGetColaborador.Result.Count == 1)
                                {
                                    EntColaborador colaborador = resGetColaborador.Result.First();
                                    if (!colaborador.bAcceso)
                                    {
                                        activacionUsuario = true;
                                    }
                                }
                            }
                        }
                    }

                    IMDResponse<EntUsuario> respuestaGuardarUsuarioCGU = busUsuario.BSaveUsuario(entUsuarioTitular);
                    if (respuestaGuardarUsuarioCGU.Code != 0)
                    {
                        return respuestaGuardarUsuarioCGU.GetResponse<bool>();
                    }

                    entCreateColaborador.iIdUsuarioCGU = (int)respuestaGuardarUsuarioCGU.Result.iIdUsuario;
                    entCreateColaborador.iIdTipoCuenta = (int)EnumTipoCuenta.Titular;

                    entUsuarioAdministrativo.iIdUsuario = respuestaGuardarUsuarioCGU.Result.iIdUsuario;

                    respuestaGuardarUsuarioCGU = busUsuario.BSaveUsuario(entUsuarioAdministrativo);
                    if (respuestaGuardarUsuarioCGU.Code != 0)
                    {
                        return respuestaGuardarUsuarioCGU.GetResponse<bool>();
                    }

                    IMDResponse<bool> respuestaGuardarColaborador = datColaborador.DSaveColaborador(entCreateColaborador);
                    if (respuestaGuardarColaborador.Code != 0)
                    {
                        return respuestaGuardarColaborador;
                    }
                    if (entCreateColaborador.bAcceso)
                    {

                        if ((entCreateColaborador.bActivo && !entCreateColaborador.bBaja && (!string.IsNullOrWhiteSpace(entCreateColaborador.sPasswordTitular) || !string.IsNullOrWhiteSpace(entCreateColaborador.sPasswordAdministrativo))) || activacionUsuario)
                        {
                            List<string> users = new List<string> { entCreateColaborador.sUsuarioTitular, entCreateColaborador.sUsuarioAdministrativo };
                            IMDResponse<bool> resEnviarCredenciales = busUsuario.BEnviarCredenciales(entCreateColaborador.sCorreoDoctor, entCreateColaborador.iIdColaborador == 0 || activacionUsuario ? EnumEmailActionPass.Crear : EnumEmailActionPass.Modificar, users);
                        }
                    }
                }
                else
                {
                    response.Code = -72348767232323;
                    response.Message = "No se ha especificado el tipo de médico colaborador.";
                    return response;
                }
                //    scope.Complete();
                //}

                response.Code = 0;
                response.Message = "El colaborador ha sido guardado correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458458574;
                response.Message = "Ocurrió un error inesperado al guardar el colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458458574, $"Error en {metodo}(EntCreateColaborador entCreateColaborador): {ex.Message}", entCreateColaborador, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene o filtra la lista de colaboradores
        /// </summary>
        /// <param name="piIdColaborador"></param>
        /// <param name="piIdTipoDoctor"></param>
        /// <param name="piIdEspecialidad"></param>
        /// <param name="piIdUsuarioCGU"></param>
        /// <returns></returns>
        public IMDResponse<List<EntColaborador>> BGetColaborador(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null)
        {
            IMDResponse<List<EntColaborador>> response = new IMDResponse<List<EntColaborador>>();

            string metodo = nameof(this.BGetColaborador);
            logger.Info(IMDSerialize.Serialize(67823458474891, $"Inicia {metodo}(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null)", piIdColaborador, piIdTipoDoctor, piIdEspecialidad, piIdUsuarioCGU));

            try
            {
                IMDResponse<DataTable> resGetColaboradores = datColaborador.DGetColaborador(piIdColaborador, piIdTipoDoctor, piIdEspecialidad, piIdUsuarioCGU);
                if (resGetColaboradores.Code != 0)
                {
                    return resGetColaboradores.GetResponse<List<EntColaborador>>();
                }

                List<EntColaborador> lstColaboradores = new List<EntColaborador>();

                foreach (DataRow drColaborador in resGetColaboradores.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drColaborador);

                    EntColaborador colaborador = new EntColaborador
                    {
                        bActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo")),
                        bBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja")),
                        bAcceso = Convert.ToBoolean(dr.ConvertTo<int>("bAcceso")),
                        bOcupado = Convert.ToBoolean(dr.ConvertTo<int>("bOcupado")),
                        bOnline = Convert.ToBoolean(dr.ConvertTo<int>("bOnline")),
                        dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion"),
                        dtFechaNacimientoDoctor = dr.ConvertTo<DateTime>("dtFechaNacimientoDoctor"),
                        iIdColaborador = dr.ConvertTo<int>("iIdColaborador"),
                        iIdEspecialidad = dr.ConvertTo<int>("iIdEspecialidad"),
                        iIdTipoCuenta = dr.ConvertTo<int>("iIdTipoCuenta"),
                        iIdTipoDoctor = dr.ConvertTo<int>("iIdTipoDoctor"),
                        iIdUsuarioCGU = dr.ConvertTo<int>("iIdUsuarioCGU"),
                        iNumSala = dr.ConvertTo<int?>("iNumSala"),
                        sApellidoMaternoDoctor = dr.ConvertTo<string>("sApellidoMaternoDoctor"),
                        sApellidoPaternoDoctor = dr.ConvertTo<string>("sApellidoPaternoDoctor"),
                        sCedulaProfecional = dr.ConvertTo<string>("sCedulaProfecional"),
                        sCorreoDirectorio = dr.ConvertTo<string>("sCorreo"),
                        sCorreoDoctor = dr.ConvertTo<string>("sCorreoDoctor"),
                        sDireccionConsultorio = dr.ConvertTo<string>("sDireccionConsultorio"),
                        sDomicilioDoctor = dr.ConvertTo<string>("sDomicilioDoctor"),
                        sEspecialidad = dr.ConvertTo<string>("sEspecialidad"),
                        sFechaCreacion = string.Empty,
                        sFechaNacimientoDoctor = string.Empty,
                        sMaps = dr.ConvertTo<string>("sMaps"),
                        sNombreDirectorio = dr.ConvertTo<string>("sNombre"),
                        sNombreConsultorio = dr.ConvertTo<string>("sNombreConsultorio"),
                        sNombresDoctor = dr.ConvertTo<string>("sNombresDoctor"),
                        sPasswordAdministrativo = dr.ConvertTo<string>("sPasswordAdministrativo"),
                        sPasswordTitular = dr.ConvertTo<string>("sPasswordTitular"),
                        sRFC = dr.ConvertTo<string>("sRFC"),
                        sTelefonoDirectorio = dr.ConvertTo<string>("sTelefono"),
                        sTelefonoDoctor = dr.ConvertTo<string>("sTelefonoDoctor"),
                        sTipoCuenta = dr.ConvertTo<string>("sTipoCuenta"),
                        sTipoDoctor = dr.ConvertTo<string>("sTipoDoctor"),
                        sURL = dr.ConvertTo<string>("sURL"),
                        sUsuarioAdministrativo = dr.ConvertTo<string>("sUsuarioAdministrativo"),
                        sUsuarioTitular = dr.ConvertTo<string>("sUsuarioTitular"),
                        sWhatsApp = dr.ConvertTo<string>("sWhatsApp"),
                    };

                    colaborador.sFechaCreacion = colaborador.dtFechaCreacion.ToString("dd/MM/yyyy HH:mm");
                    colaborador.sFechaNacimientoDoctor = colaborador.dtFechaNacimientoDoctor.ToString("dd/MM/yyyy");
                    colaborador.sAcceso = colaborador.bAcceso ? "SI" : "NO";
                    colaborador.bAdministrador = colaborador.iIdTipoDoctor == (int)EnumTipoDoctor.MedicoAdministrativo;

                    lstColaboradores.Add(colaborador);
                }

                response.Code = 0;
                response.Message = "Se han obtenido los colaboradores del sistema.";
                response.Result = lstColaboradores;
            }
            catch (Exception ex)
            {
                response.Code = 67823458475668;
                response.Message = "Ocurrió un error inesperado al consultar los colaboradores del sistema.";

                logger.Error(IMDSerialize.Serialize(67823458475668, $"Error en {metodo}(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null): {ex.Message}", piIdColaborador, piIdTipoDoctor, piIdEspecialidad, piIdUsuarioCGU, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Guarda la foto del colaborador
        /// </summary>
        /// <param name="piIdColaborador"></param>
        /// <param name="piIdUsuarioMod"></param>
        /// <param name="pFoto"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveColaboradorFoto(int piIdColaborador, int piIdUsuarioMod, Stream pFoto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458479553, $"Inicia {metodo}(int piIdColaborador, int piIdUsuarioMod, Stream pFoto)", piIdColaborador, piIdUsuarioMod));

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pFoto.CopyTo(ms);
                    byte[] foto = ms.ToArray();
                    if (foto.Length == 0)
                    {
                        response.Code = -2318178987823;
                        response.Message = "El archivo esta dañado o no se cargó correctamente.";
                        return response;
                    }

                    IMDResponse<bool> respuestaSaveFoto = datColaborador.DSaveColaboradorFoto(piIdColaborador, piIdColaborador, foto);
                    if (respuestaSaveFoto.Code != 0)
                    {
                        return respuestaSaveFoto;
                    }
                }

                response.Code = 0;
                response.Message = "La foto del colaborador ha sido guardado correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458480330;
                response.Message = "Ocurrió un error inesperado al guardar la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458480330, $"Error en {metodo}(int piIdColaborador, int piIdUsuarioMod, Stream pFoto): {ex.Message}", piIdColaborador, piIdUsuarioMod, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene la foto del colaborador
        /// </summary>
        /// <param name="piIdColaborador"></param>
        /// <returns></returns>
        public IMDResponse<string> BGetColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BGetColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458484215, $"Inicia {metodo}(int piIdColaborador)", piIdColaborador));

            try
            {
                IMDResponse<byte[]> resGetFoto = this.BConvertColaboradorFoto(piIdColaborador);
                if (resGetFoto.Code != 0)
                {
                    return resGetFoto.GetResponse<string>();
                }

                string sFoto = Convert.ToBase64String(resGetFoto.Result);
                if (string.IsNullOrWhiteSpace(sFoto))
                {
                    response.Code = -232876708789;
                    response.Message = "El formato de la imagen del colaborador es incorrecto o el archivo esta dañado.";
                    return response;
                }

                response.Code = 0;
                response.Message = "La foto del colaborador ha sido obtenida.";
                response.Result = sFoto;
            }
            catch (Exception ex)
            {
                response.Code = 67823458484992;
                response.Message = "Ocurrió un error inesperado al consultar la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458484992, $"Error en {metodo}(int piIdColaborador): {ex.Message}", piIdColaborador, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Descarga la foto del colaborador
        /// </summary>
        /// <param name="piIdColaborador"></param>
        /// <returns></returns>
        public IMDResponse<MemoryStream> BDescargarColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<MemoryStream> response = new IMDResponse<MemoryStream>();

            string metodo = nameof(this.BDescargarColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458485769, $"Inicia {metodo}(int piIdColaborador)", piIdColaborador));

            try
            {
                IMDResponse<byte[]> resGetFoto = this.BConvertColaboradorFoto(piIdColaborador);
                if (resGetFoto.Code != 0)
                {
                    return resGetFoto.GetResponse<MemoryStream>();
                }

                response.Code = 0;
                response.Message = "OK";
                response.Result = new MemoryStream(resGetFoto.Result);
            }
            catch (Exception ex)
            {
                response.Code = 67823458486546;
                response.Message = "Ocurrió un error inesperado al intentar descargar la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458486546, $"Error en {metodo}: {ex.Message}(int piIdColaborador)", piIdColaborador, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene el arreglo de bytes de la foto del colaborador
        /// </summary>
        /// <param name="piIdColaborador"></param>
        /// <returns></returns>
        public IMDResponse<byte[]> BConvertColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<byte[]> response = new IMDResponse<byte[]>();

            string metodo = nameof(this.BConvertColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458487323, $"Inicia {metodo}(int piIdColaborador)", piIdColaborador));

            try
            {
                if (piIdColaborador < 1)
                {
                    response.Code = -7262876723423;
                    response.Message = "No se ingresó información del colaborador";
                    return response;
                }

                IMDResponse<DataTable> resGetFoto = datColaborador.DGetColaboradorFoto(piIdColaborador);
                if (resGetFoto.Code != 0)
                {
                    return resGetFoto.GetResponse<byte[]>();
                }

                if (resGetFoto.Result.Rows.Count != 1)
                {
                    response.Code = -98672347786234;
                    response.Message = "No se encontró la foto del colaborador.";
                    return response;
                }

                DataRow dr = resGetFoto.Result.Rows[0];
                byte[] foto = dr["sFoto"] is DBNull ? new byte[0] : (byte[])dr["sFoto"];
                if (foto.Length == 0)
                {
                    response.Code = -1000;
                    response.Message = "El colaborador aún no cuenta con foto de perfil.";
                    return response;
                }

                response.Code = 0;
                response.Message = "OK";
                response.Result = foto;
            }
            catch (Exception ex)
            {
                response.Code = 67823458488100;
                response.Message = "Ocurrió un error inesperado al leer la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458488100, $"Error en {metodo}(int piIdColaborador): {ex.Message}", piIdColaborador, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Elimina la foto del colaborador
        /// </summary>
        /// <param name="piIdColaborador"></param>
        /// <param name="piIdUsuarioMod"></param>
        /// <returns></returns>
        public IMDResponse<bool> BEliminarColaboradorFoto(int piIdColaborador, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEliminarColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458493539, $"Inicia {metodo}(int piIdColaborador, int piIdUsuarioMod)", piIdColaborador, piIdUsuarioMod));

            try
            {
                IMDResponse<bool> respuestaEliminarColaboradorFoto = datColaborador.DEliminarColaboradorFoto(piIdColaborador, piIdUsuarioMod);
                if (respuestaEliminarColaboradorFoto.Code != 0)
                {
                    return respuestaEliminarColaboradorFoto;
                }

                response.Code = 0;
                response.Message = "La foto del colaborador ha sido eliminada correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458494316;
                response.Message = "Ocurrió un error inesperado al intentar eliminar la foto del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458494316, $"Error en {metodo}(int piIdColaborador, int piIdUsuarioMod): {ex.Message}", piIdColaborador, piIdUsuarioMod, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene y filtra el directorio de médicos especialistas
        /// </summary>
        /// <param name="piIdEspecialidad"></param>
        /// <param name="psBuscador"></param>
        /// <param name="piPage"></param>
        /// <param name="piPageSize"></param>
        /// <returns></returns>
        public IMDResponse<EntDirectorio> BGetDirectorio(int? piIdEspecialidad = null, string psBuscador = null, int piPage = 0, int piPageSize = 0, bool? pbAcceso = null)
        {
            IMDResponse<EntDirectorio> response = new IMDResponse<EntDirectorio>();

            string metodo = nameof(this.BGetDirectorio);
            logger.Info(IMDSerialize.Serialize(67823458504417, $"Inicia {metodo}(int? piIdEspecialidad = null, string psBuscador = null, int piPage = 0, int piPageSize = 0)", piIdEspecialidad, psBuscador, piPage, piPageSize));

            try
            {
                int piLimitInit = (piPage * piPageSize - piPageSize);
                int piLimitEnd = piPage * piPageSize - 1;

                IMDResponse<DataTable> resGetDirectorio = datColaborador.DGetDirectorio(piIdEspecialidad, psBuscador, piLimitInit, piLimitEnd, pbAcceso);
                if (resGetDirectorio.Code != 0)
                {
                    return resGetDirectorio.GetResponse<EntDirectorio>();
                }

                EntDirectorio entDirectorio = new EntDirectorio();

                foreach (DataRow drItem in resGetDirectorio.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drItem);

                    int iIdColaborador = dr.ConvertTo<int>("iIdColaborador");

                    if (iIdColaborador != 0)
                    {

                        EntColaboradorDirectorio entColaborador = new EntColaboradorDirectorio
                        {
                            iIdColaborador = iIdColaborador,
                            iIdEspecialidad = dr.ConvertTo<int>("iIdEspecialidad"),
                            sCedulaProfecional = dr.ConvertTo<string>("sCedulaProfecional"),
                            sCorreo = dr.ConvertTo<string>("sCorreo"),
                            sDireccionConsultorio = dr.ConvertTo<string>("sDireccionConsultorio"),
                            sEspecialidad = dr.ConvertTo<string>("sEspecialidad"),
                            sFoto = string.Empty,
                            sMaps = dr.ConvertTo<string>("sMaps"),
                            sNombre = dr.ConvertTo<string>("sNombre"),
                            sNombreConsultorio = dr.ConvertTo<string>("sNombreConsultorio"),
                            sRFC = dr.ConvertTo<string>("sRFC"),
                            sTelefono = dr.ConvertTo<string>("sTelefono"),
                            sWhatsApp = dr.ConvertTo<string>("sWhatsApp"),
                            sURL = dr.ConvertTo<string>("sURL"),
                        };

                        string sinInformacion = ConfigurationManager.AppSettings["sLeyendaSinInformacion"];
                        entColaborador.sCedulaProfecional = string.IsNullOrEmpty(entColaborador.sCedulaProfecional) ? sinInformacion : entColaborador.sCedulaProfecional;
                        entColaborador.sCorreo = string.IsNullOrEmpty(entColaborador.sCorreo) ? sinInformacion : entColaborador.sCorreo;
                        entColaborador.sDireccionConsultorio = string.IsNullOrEmpty(entColaborador.sDireccionConsultorio) ? sinInformacion : entColaborador.sDireccionConsultorio;
                        entColaborador.sEspecialidad = string.IsNullOrEmpty(entColaborador.sEspecialidad) ? sinInformacion : entColaborador.sEspecialidad;
                        entColaborador.sNombre = string.IsNullOrEmpty(entColaborador.sNombre) ? sinInformacion : entColaborador.sNombre;
                        entColaborador.sNombreConsultorio = string.IsNullOrEmpty(entColaborador.sNombreConsultorio) ? sinInformacion : entColaborador.sNombreConsultorio;
                        entColaborador.sRFC = string.IsNullOrEmpty(entColaborador.sRFC) ? sinInformacion : entColaborador.sRFC;
                        entColaborador.sTelefono = string.IsNullOrEmpty(entColaborador.sTelefono) ? sinInformacion : entColaborador.sTelefono;
                        entColaborador.sURL = string.IsNullOrEmpty(entColaborador.sURL) ? sinInformacion : entColaborador.sURL;
                        entColaborador.sWhatsApp = string.IsNullOrEmpty(entColaborador.sWhatsApp) ? sinInformacion : entColaborador.sWhatsApp;

                        try
                        {
                            byte[] foto = drItem["sFoto"] is DBNull ? new byte[0] : (byte[])drItem["sFoto"];
                            string sFoto = Convert.ToBase64String(foto);
                            if (string.IsNullOrWhiteSpace(sFoto))
                            {
                                sFoto = string.Empty;
                            }

                            entColaborador.sFoto = sFoto;
                        }
                        catch (Exception)
                        {
                        }

                        entDirectorio.lstColaboradores.Add(entColaborador);
                    }
                }

                int iCount = 0;
                if (resGetDirectorio.Result.Rows.Count > 0)
                {
                    iCount = Convert.ToInt32(resGetDirectorio.Result.Rows[0]["iCount"].ToString());
                }

                entDirectorio.iTotalPaginas = (int)Math.Ceiling(iCount / (double)piPageSize);

                response.Code = 0;
                response.Message = "El directorio médico ha sido obtenido.";
                response.Result = entDirectorio;
            }
            catch (Exception ex)
            {
                response.Code = 67823458505194;
                response.Message = "Ocurrió un error inesperado al consultar el directorio médico.";

                logger.Error(IMDSerialize.Serialize(67823458505194, $"Error en {metodo}(int? piIdEspecialidad = null, string psBuscador = null, int piPage = 0, int piPageSize = 0): {ex.Message}", piIdEspecialidad, psBuscador, piPage, piPageSize, ex, response));
            }
            return response;
        }

        public IMDResponse<EntColaborador> BObtenerSala(bool? bAgendada = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null)
        {
            IMDResponse<EntColaborador> response = new IMDResponse<EntColaborador>();
            EntColaborador oColaborador = new EntColaborador();

            string metodo = nameof(this.BObtenerSala);
            logger.Info(IMDSerialize.Serialize(67823458591441, $"Inicia {metodo}(int? iIdTipoProducto = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null)"));

            try
            {
                IMDResponse<DataTable> dtColaborador = datColaborador.DObtenerSala(bAgendada, iIdUsuario, dtFechaConsulta);

                if (dtColaborador.Code != 0)
                {
                    return dtColaborador.GetResponse<EntColaborador>();
                }


                foreach (DataRow item in dtColaborador.Result.Rows)
                {
                    IMDDataRow rows = new IMDDataRow(item);

                    oColaborador = new EntColaborador
                    {
                        iIdColaborador = rows.ConvertTo<int>("iIdColaborador"),
                        iNumSala = rows.ConvertTo<int?>("iNumSala")

                    };

                }
                string medicosOcupados = "Todos los médicos se encuentran ocupados en este momento.";
                if (oColaborador.iNumSala == null)
                {
                    response.Code = -7262876723423;
                    response.Message = medicosOcupados;
                    return response;
                }

                response.Code = 0;
                response.Message = dtColaborador.Result.Rows.Count > 0 ? "Se ha encontrado un sala disponible." : medicosOcupados;
                response.Result = oColaborador;
            }
            catch (Exception ex)
            {
                response.Code = 67823458592218;
                response.Message = "Ocurrió un error inesperado al obtener la sala del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458592218, $"Error en {metodo}(int? iIdTipoProducto = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null): {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntColaboradorStatus> BGetColaboradorStatus(int piIdColaborador)
        {
            IMDResponse<EntColaboradorStatus> response = new IMDResponse<EntColaboradorStatus>();

            string metodo = nameof(this.BGetColaboradorStatus);
            logger.Info(IMDSerialize.Serialize(67823458648939, $"Inicia {metodo}(int piIdColaborador)", piIdColaborador));

            try
            {
                if (piIdColaborador < 1)
                {
                    response.Code = -345687324773;
                    response.Message = "No se especificó el colaborador.";
                    return response;
                }

                IMDResponse<DataTable> resGetStatus = datColaborador.DGetColaboradorStatus(piIdColaborador);
                if (resGetStatus.Code != 0)
                {
                    return resGetStatus.GetResponse<EntColaboradorStatus>();
                }

                if (resGetStatus.Result.Rows.Count != 1)
                {
                    response.Code = -345687324773;
                    response.Message = "No se encontró el colaborador especificado.";
                    return response;
                }

                IMDDataRow dr = new IMDDataRow(resGetStatus.Result.Rows[0]);

                EntColaboradorStatus entColaboradorStatus = new EntColaboradorStatus
                {
                    bOcupado = Convert.ToBoolean(dr.ConvertTo<int>("bOcupado")),
                    bOnline = Convert.ToBoolean(dr.ConvertTo<int>("bOnline")),
                };

                response.Code = 0;
                response.Message = "Se ha consultado el estatus del colaborador.";
                response.Result = entColaboradorStatus;
            }
            catch (Exception ex)
            {
                response.Code = 67823458649716;
                response.Message = "Ocurrió un error inesperado al consultar el status del colaborador.";

                logger.Error(IMDSerialize.Serialize(67823458649716, $"Error en {metodo}(int piIdColaborador): {ex.Message}", piIdColaborador, ex, response));
            }
            return response;
        }
    }
}