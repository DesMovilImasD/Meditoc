using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusUsuario
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusUsuario));
        DatUsuario datUsuario;

        public BusUsuario()
        {
            datUsuario = new DatUsuario();
        }

        public IMDResponse<bool> DSaveUsuario(EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveUsuario);
            logger.Info(IMDSerialize.Serialize(67823458344355, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));

            try
            {
                if (entUsuario == null)
                {
                    response.Code = 67823458345132;
                    response.Message = "No se ingresó ningun usuario.";
                    return response;
                }

                response = bValidaDatos(entUsuario);

                if (!response.Result) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                {
                    return response;
                }

                response = datUsuario.DSaveUsuario(entUsuario);

                response.Code = 0;
                response.Message = entUsuario.iIdUsuario == 0 ? "El usuario se guardó correctamente" : "El usuario se actualizo correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458345132;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458345132, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntUsuario>> BObtenerUsuario(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool? bActivo, bool? bBaja)
        {
            IMDResponse<List<EntUsuario>> response = new IMDResponse<List<EntUsuario>>();

            string metodo = nameof(this.BObtenerUsuario);
            logger.Info(IMDSerialize.Serialize(67823458362226, $"Inicia {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja)", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja));

            try
            {

                IMDResponse<DataTable> dtUsuario = datUsuario.DObtenerUsuario(iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja);
                List<EntUsuario> lstUsuaeios = new List<EntUsuario>();

                foreach (DataRow item in dtUsuario.Result.Rows)
                {

                    IMDDataRow dr = new IMDDataRow(item);
                    EntUsuario entUsuario = new EntUsuario();

                    entUsuario.iIdUsuario = dr.ConvertTo<int>("iIdUsuario");
                    entUsuario.iIdTipoCuenta = dr.ConvertTo<int>("iIdTipoCuenta");
                    entUsuario.iIdPerfil = dr.ConvertTo<int>("iIdPerfil");
                    entUsuario.sUsuario = dr.ConvertTo<string>("sUsuario");
                    entUsuario.sPassword = dr.ConvertTo<string>("sPassword");
                    entUsuario.sNombres = dr.ConvertTo<string>("sNombres");
                    entUsuario.sApellidoPaterno = dr.ConvertTo<string>("sApellidoPaterno");
                    entUsuario.sApellidoMaterno = dr.ConvertTo<string>("sApellidoMaterno");
                    entUsuario.dtFechaNacimiento = dr.ConvertTo<DateTime>("dtFechaNacimiento");
                    entUsuario.sTelefono = dr.ConvertTo<string>("sTelefono");
                    entUsuario.sCorreo = dr.ConvertTo<string>("sCorreo");
                    entUsuario.sDomicilio = dr.ConvertTo<string>("sDomicilio");
                    entUsuario.iIdUsuarioMod = dr.ConvertTo<int>("iIdUsuarioMod");
                    entUsuario.bActivo = dr.ConvertTo<bool>("bActivo");
                    entUsuario.bBaja = dr.ConvertTo<bool>("bBaja");

                    lstUsuaeios.Add(entUsuario);
                }


                response.Message = "Lista de usuarios";
                response.Result = lstUsuaeios;
            }
            catch (Exception ex)
            {
                response.Code = 67823458363003;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458363003, $"Error en {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja): {ex.Message}", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja ,ex, response));
            }
            return response;
        }
        public IMDResponse<bool> bValidaDatos(EntUsuario entUsuario)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.bValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458345132, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));
            try
            {

                if (entUsuario.sNombres == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El nombre no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.iIdPerfil == 0)
                {
                    response.Code = 67823458345132;
                    response.Message = "Debe tener asignado un perfil.";
                    response.Result = false;

                    return response;
                }


                if (entUsuario.sUsuario == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El nombre del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.sPassword == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "La contraseña del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.sApellidoPaterno == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El apellido paterno del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.sApellidoMaterno == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El apellido materno del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458342024;
                response.Message = "Ocurrió un error al intentar guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458341247, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }
    }
}
