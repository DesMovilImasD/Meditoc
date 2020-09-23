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
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
                    response.Message = "No se ingresó información del colaborador";
                    return response;
                }

                //using (TransactionScope scope = new TransactionScope())
                //{
                if (entCreateColaborador.iIdTipoDoctor == (int)EnumTipoDoctor.MedicoCallCenter)
                {
                    EntUsuario entUsuario = new EntUsuario
                    {
                        bActivo = entCreateColaborador.bActivo,
                        bBaja = entCreateColaborador.bBaja,
                        dtFechaNacimiento = entCreateColaborador.dtFechaNacimientoDoctor,
                        iIdPerfil = (int)EnumPerfilPrincipal.DoctorCallCenter,
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
                        sUsuario = entCreateColaborador.sUsuarioTitular
                    };

                    IMDResponse<EntUsuario> respuestaGuardarUsuarioCGU = busUsuario.BSaveUsuario(entUsuario);
                    if (respuestaGuardarUsuarioCGU.Code != 0)
                    {
                        return respuestaGuardarUsuarioCGU.GetResponse<bool>();
                    }

                    entCreateColaborador.iIdEspecialidad = (int)EnumEspecialidadPrincipal.MedicinaGeneral;
                    entCreateColaborador.iIdUsuarioCGU = (int)respuestaGuardarUsuarioCGU.Result.iIdUsuario;
                    entCreateColaborador.iIdTipoCuenta = (int)EnumTipoCuenta.Titular;

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
                            response.Code = 873849586457;
                            response.Message = "No se ha especificado la especialidad del médico";
                            return response;
                        }

                        if (string.IsNullOrWhiteSpace(entCreateColaborador.sUsuarioAdministrativo))
                        {
                            response.Code = 873849586457;
                            response.Message = "No se han especificado los datos de sesión administrativa";
                            return response;
                        }

                        if (entCreateColaborador.iIdColaborador == 0 && string.IsNullOrWhiteSpace(entCreateColaborador.sPasswordAdministrativo))
                        {
                            response.Code = 873849586457;
                            response.Message = "No se han especificado los datos de sesión administrativa";
                            return response;
                        }
                    }

                    EntUsuario entUsuario = new EntUsuario
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
                        sUsuario = entCreateColaborador.sUsuarioTitular
                    };

                    IMDResponse<EntUsuario> respuestaGuardarUsuarioCGU = busUsuario.BSaveUsuario(entUsuario);
                    if (respuestaGuardarUsuarioCGU.Code != 0)
                    {
                        return respuestaGuardarUsuarioCGU.GetResponse<bool>();
                    }

                    entUsuario.iIdTipoCuenta = (int)EnumTipoCuenta.Administrativa;
                    entUsuario.iIdPerfil = (int)EnumPerfilPrincipal.AdministradorEspecialista;
                    entUsuario.sUsuario = entCreateColaborador.sUsuarioAdministrativo;
                    entUsuario.sPassword = entCreateColaborador.sPasswordAdministrativo;

                    respuestaGuardarUsuarioCGU = busUsuario.BSaveUsuario(entUsuario);
                    if (respuestaGuardarUsuarioCGU.Code != 0)
                    {
                        return respuestaGuardarUsuarioCGU.GetResponse<bool>();
                    }

                    entCreateColaborador.iIdUsuarioCGU = (int)respuestaGuardarUsuarioCGU.Result.iIdUsuario;
                    entCreateColaborador.iIdTipoCuenta = (int)EnumTipoCuenta.Titular;
                    IMDResponse<bool> respuestaGuardarColaborador = datColaborador.DSaveColaborador(entCreateColaborador);
                    if (respuestaGuardarColaborador.Code != 0)
                    {
                        return respuestaGuardarColaborador;
                    }
                }
                else
                {
                    response.Code = 76823456345;
                    response.Message = "No se especificó el tipo de doctor";
                    return response;
                }
                //    scope.Complete();
                //}

                response.Code = 0;
                response.Message = "El colaborador se guardó correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458458574;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458458574, $"Error en {metodo}: {ex.Message}(EntCreateColaborador entCreateColaborador)", entCreateColaborador, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntColaborador>> BGetColaborador(int? piIdColaborador = null, int? piIdTipoDoctor = null, int? piIdEspecialidad = null, int? piIdUsuarioCGU = null)
        {
            IMDResponse<List<EntColaborador>> response = new IMDResponse<List<EntColaborador>>();

            string metodo = nameof(this.BGetColaborador);
            logger.Info(IMDSerialize.Serialize(67823458474891, $"Inicia {metodo}"));

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
                        bOcupado = Convert.ToBoolean(dr.ConvertTo<int>("bOcupado")),
                        bOnline = Convert.ToBoolean(dr.ConvertTo<int>("bOnline")),
                        dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion"),
                        dtFechaNacimientoDoctor = dr.ConvertTo<DateTime>("dtFechaNacimientoDoctor"),
                        iIdColaborador = dr.ConvertTo<int>("iIdColaborador"),
                        iIdEspecialidad = dr.ConvertTo<int>("iIdEspecialidad"),
                        iIdTipoCuenta = dr.ConvertTo<int>("iIdTipoCuenta"),
                        iIdTipoDoctor = dr.ConvertTo<int>("iIdTipoDoctor"),
                        iIdUsuarioCGU = dr.ConvertTo<int>("iIdUsuarioCGU"),
                        iNumSala = dr.ConvertTo<int>("iNumSala"),
                        sApellidoMaternoDoctor = dr.ConvertTo<string>("sApellidoMaternoDoctor"),
                        sApellidoPaternoDoctor = dr.ConvertTo<string>("sApellidoPaternoDoctor"),
                        sCedulaProfecional = dr.ConvertTo<string>("sCedulaProfecional"),
                        sCorreoDirectorio = dr.ConvertTo<string>("sCorreo"),
                        sCorreoDoctor = dr.ConvertTo<string>("sCorreoDoctor"),
                        sDireccionConsultorio = dr.ConvertTo<string>("sCorreoDoctor"),
                        sDomicilioDoctor = dr.ConvertTo<string>("sDomicilioDoctor"),
                        sEspecialidad = dr.ConvertTo<string>("sEspecialidad"),
                        sFechaCreacion = string.Empty,
                        sFechaNacimientoDoctor = string.Empty,
                        sMaps = dr.ConvertTo<string>("sMaps"),
                        sNombreDirectorio = dr.ConvertTo<string>("sNombre"),
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
                    };

                    colaborador.sFechaCreacion = colaborador.dtFechaCreacion.ToString("dd/MM/yyyy HH:mm");
                    colaborador.sFechaNacimientoDoctor = colaborador.dtFechaNacimientoDoctor.ToString("dd/MM/yyyy");

                    lstColaboradores.Add(colaborador);
                }

                response.Code = 0;
                response.Message = "Colaboradores consultados";
                response.Result = lstColaboradores;
            }
            catch (Exception ex)
            {
                response.Code = 67823458475668;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458475668, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BSaveColaboradorFoto(int piIdColaborador, int piIdUsuarioMod, Stream pFoto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458479553, $"Inicia {metodo}"));

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pFoto.CopyTo(ms);
                    byte[] foto = ms.ToArray();
                    if (foto.Length == 0)
                    {
                        response.Code = 346743674567834;
                        response.Message = "El archivo esta dañado o no se cargó correctamente";
                        return response;
                    }

                    IMDResponse<bool> respuestaSaveFoto = datColaborador.DSaveColaboradorFoto(piIdColaborador, piIdColaborador, foto);
                    if (respuestaSaveFoto.Code != 0)
                    {
                        return respuestaSaveFoto;
                    }
                }

                response.Code = 0;
                response.Message = "La foto se guardó correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458480330;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458480330, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<string> BGetColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BGetColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458484215, $"Inicia {metodo}"));

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
                    response.Code = 67823458022677;
                    response.Message = "El formato de la imagen del colaborador es incorrecto o el archivo esta dañado";
                    return response;
                }

                response.Code = 0;
                response.Message = "Foto obtenida correctamente";
                response.Result = sFoto;
            }
            catch (Exception ex)
            {
                response.Code = 67823458484992;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458484992, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<MemoryStream> BDescargarColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<MemoryStream> response = new IMDResponse<MemoryStream>();

            string metodo = nameof(this.BDescargarColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458485769, $"Inicia {metodo}"));

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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458486546, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<byte[]> BConvertColaboradorFoto(int piIdColaborador)
        {
            IMDResponse<byte[]> response = new IMDResponse<byte[]>();

            string metodo = nameof(this.BConvertColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458487323, $"Inicia {metodo}"));

            try
            {
                if (piIdColaborador < 1)
                {
                    response.Code = 67823458022677;
                    response.Message = "No se ingresó información completa del colaborador";
                    return response;
                }

                IMDResponse<DataTable> resGetFoto = datColaborador.DGetColaboradorFoto(piIdColaborador);
                if (resGetFoto.Code != 0)
                {
                    return resGetFoto.GetResponse<byte[]>();
                }

                if (resGetFoto.Result.Rows.Count != 1)
                {
                    response.Code = 67823458022677;
                    response.Message = "No se encontró la foto del colaborador";
                    return response;
                }

                DataRow dr = resGetFoto.Result.Rows[0];
                byte[] foto = dr["sFoto"] is DBNull ? new byte[0] : (byte[])dr["sFoto"];
                if (foto.Length == 0)
                {
                    response.Code = 67823458022677;
                    response.Message = "El colaborador no cuenta con foto de perfil";
                    return response;
                }

                response.Code = 0;
                response.Message = "OK";
                response.Result = foto;
            }
            catch (Exception ex)
            {
                response.Code = 67823458488100;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458488100, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BEliminarColaboradorFoto(int piIdColaborador, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEliminarColaboradorFoto);
            logger.Info(IMDSerialize.Serialize(67823458493539, $"Inicia {metodo}"));

            try
            {
                IMDResponse<bool> respuestaEliminarColaboradorFoto = datColaborador.DEliminarColaboradorFoto(piIdColaborador, piIdUsuarioMod);
                if (respuestaEliminarColaboradorFoto.Code != 0)
                {
                    return respuestaEliminarColaboradorFoto;
                }

                response.Code = 0;
                response.Message = "Foto eliminada correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458494316;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458494316, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntDirectorio> BGetDirectorio(int? piIdEspecialidad = null, string psBuscador = null, int piPage = 0, int piPageSize = 0)
        {
            IMDResponse<EntDirectorio> response = new IMDResponse<EntDirectorio>();

            string metodo = nameof(this.BGetDirectorio);
            logger.Info(IMDSerialize.Serialize(67823458504417, $"Inicia {metodo}"));

            try
            {
                int piLimitInit = (piPage * piPageSize - piPageSize);
                int piLimitEnd = piPage * piPageSize - 1;

                IMDResponse<DataTable> resGetDirectorio = datColaborador.DGetDirectorio(piIdEspecialidad, psBuscador, piLimitInit, piLimitEnd);
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
                            sRFC = dr.ConvertTo<string>("sRFC"),
                            sTelefono = dr.ConvertTo<string>("sTelefono"),
                            sURL = dr.ConvertTo<string>("sURL"),
                        };
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
                response.Message = "Directorio obtenido";
                response.Result = entDirectorio;
            }
            catch (Exception ex)
            {
                response.Code = 67823458505194;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458505194, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntColaborador> BObtenerSala(int? iIdTipoProducto = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null)
        {
            IMDResponse<EntColaborador> response = new IMDResponse<EntColaborador>();
            EntColaborador oColaborador = new EntColaborador();

            string metodo = nameof(this.BObtenerSala);
            logger.Info(IMDSerialize.Serialize(67823458591441, $"Inicia {metodo}(int? iIdTipoProducto = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null)"));

            try
            {                
                IMDResponse<DataTable> dtColaborador = datColaborador.DObtenerSala(iIdTipoProducto, iIdUsuario, dtFechaConsulta);

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
                        iNumSala = rows.ConvertTo<int>("iNumSala")

                    };

                }

                response.Code = 0;
                response.Message = "Sala consultada";
                response.Result = oColaborador;
            }
            catch (Exception ex)
            {
                response.Code = 67823458592218;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458592218, $"Error en {metodo}(int? iIdTipoProducto = null, int? iIdUsuario = null, DateTime? dtFechaConsulta = null): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}