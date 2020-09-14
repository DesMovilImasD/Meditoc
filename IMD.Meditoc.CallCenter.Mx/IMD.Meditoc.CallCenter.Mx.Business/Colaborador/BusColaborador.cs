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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    IMDResponse<EntUsuario> respuestaGuardarUsuarioCGU = busUsuario.DSaveUsuario(entUsuario);
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

                        if (string.IsNullOrWhiteSpace(entCreateColaborador.sUsuarioAdministrativo) || string.IsNullOrWhiteSpace(entCreateColaborador.sPasswordAdministrativo))
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

                    IMDResponse<EntUsuario> respuestaGuardarUsuarioCGU = busUsuario.DSaveUsuario(entUsuario);
                    if (respuestaGuardarUsuarioCGU.Code != 0)
                    {
                        return respuestaGuardarUsuarioCGU.GetResponse<bool>();
                    }

                    entUsuario.iIdTipoCuenta = (int)EnumTipoCuenta.Administrativa;
                    entUsuario.sUsuario = entCreateColaborador.sUsuarioAdministrativo;
                    entUsuario.sPassword = entCreateColaborador.sPasswordAdministrativo;

                    respuestaGuardarUsuarioCGU = busUsuario.DSaveUsuario(entUsuario);
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
    }
}