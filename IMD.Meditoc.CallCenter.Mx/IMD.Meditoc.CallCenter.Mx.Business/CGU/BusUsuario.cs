using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;

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

        public IMDResponse<EntUsuario> BSaveUsuario(EntUsuario entUsuario)
        {
            IMDResponse<EntUsuario> response = new IMDResponse<EntUsuario>();

            string metodo = nameof(this.BSaveUsuario);
            logger.Info(IMDSerialize.Serialize(67823458344355, $"Inicia {metodo}(EntUsuario entUsuario)", entUsuario));

            try
            {
                if (entUsuario == null)
                {
                    response.Code = 67823458345132;
                    response.Message = "No se ingresó ningun usuario.";
                    return response;
                }

                if (entUsuario.bActivo == true && entUsuario.bBaja == false)
                {

                    IMDResponse<bool> resValidaDatos = bValidaDatos(entUsuario);

                    if (!resValidaDatos.Result) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                    {
                        return resValidaDatos.GetResponse<EntUsuario>();
                    }
                    entUsuario.sPassword = BEncodePassword(entUsuario.sPassword);
                }

                IMDResponse<DataTable> resSaveUsuario = datUsuario.DSaveUsuario(entUsuario);
                if (resSaveUsuario.Code != 0)
                {
                    return resSaveUsuario.GetResponse<EntUsuario>();
                }

                IMDDataRow dr = new IMDDataRow(resSaveUsuario.Result.Rows[0]);
                entUsuario.iIdUsuario = dr.ConvertTo<int>("iIdUsuario");

                response.Code = 0;
                response.Message = "El usuario se guardó correctamente";
                response.Result = entUsuario;
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
                List<EntUsuario> lstUsuario = new List<EntUsuario>();

                if (dtUsuario.Code != 0)
                {
                    return dtUsuario.GetResponse<List<EntUsuario>>();
                }

                if (dtUsuario.Code != 0)
                {
                    return dtUsuario.GetResponse<List<EntUsuario>>();
                }

                foreach (DataRow item in dtUsuario.Result.Rows)
                {

                    IMDDataRow dr = new IMDDataRow(item);
                    EntUsuario entUsuario = new EntUsuario();

                    entUsuario.iIdUsuario = dr.ConvertTo<int>("iIdUsuario");
                    entUsuario.iIdTipoCuenta = dr.ConvertTo<int>("iIdTipoCuenta");
                    entUsuario.sTipoCuenta = dr.ConvertTo<string>("sTipoCuenta");
                    entUsuario.iIdPerfil = dr.ConvertTo<int>("iIdPerfil");
                    entUsuario.sPerfil = dr.ConvertTo<string>("sPerfil");
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

                    lstUsuario.Add(entUsuario);
                }


                response.Message = "Lista de usuarios";
                response.Result = lstUsuario;
            }
            catch (Exception ex)
            {
                response.Code = 67823458363003;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458363003, $"Error en {metodo}(int? iIdUsuario, int? iIdTipoCuenta, int? iIdPerfil, string sUsuario, string sPassword, bool bActivo, bool bBaja): {ex.Message}", iIdUsuario, iIdTipoCuenta, iIdPerfil, sUsuario, sPassword, bActivo, bBaja, ex, response));
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


                if (entUsuario.iIdUsuario == 0)
                {

                    response = datUsuario.DValidaUsuarioYCorreo(entUsuario.sUsuario, "", true);
                    if (!response.Result)
                    {
                        response.Code = 67823458345132;
                        response.Message = "Ya existe un usuario registrado.";
                        response.Result = false;

                        return response;
                    }
                }

                if (entUsuario.sPassword == "")
                {
                    if (entUsuario.iIdUsuario == 0)
                    {
                        response.Code = 67823458345132;
                        response.Message = "La contraseña del usuario no puede ser vacio.";
                        response.Result = false;

                        return response;
                    }
                }

                if (entUsuario.sApellidoPaterno == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El apellido paterno del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                //if (entUsuario.sApellidoMaterno == "")
                //{
                //    response.Code = 67823458345132;
                //    response.Message = "El apellido materno del usuario no puede ser vacio.";
                //    response.Result = false;

                //    return response;
                //}

                if (entUsuario.sCorreo == "")
                {
                    response.Code = 67823458345132;
                    response.Message = "El correo del usuario no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                if (entUsuario.iIdTipoCuenta == (int)EnumTipoCuenta.Titular)
                {
                    if (entUsuario.iIdUsuario == 0)
                    {

                        response = datUsuario.DValidaUsuarioYCorreo("", entUsuario.sCorreo, false);
                        if (!response.Result)
                        {
                            response.Code = 67823458345132;
                            response.Message = "Ya existe un correo registrado.";
                            response.Result = false;

                            return response;
                        }
                    }
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458342024;
                response.Message = "Ocurrió un error al intentar guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458341247, $"Error en {metodo}(EntUsuario entUsuario): {ex.Message}", entUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BCambiarContrasenia(int iIdUsuario, string sPassword, int iIdUsuarioUltMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BCambiarContrasenia);
            logger.Info(IMDSerialize.Serialize(67823458369996, $"Inicia {metodo}(int iIdUsuario, string sPassword)", iIdUsuario, sPassword));

            try
            {

                sPassword = BEncodePassword(sPassword);

                response = datUsuario.DCambiarContrasenia(iIdUsuario, sPassword, iIdUsuarioUltMod);

                if (response.Code != 0)
                {
                    response.Message = "Ocurrio un error al modificar la contraseña";
                    response.Result = false;

                    return response;
                }

                response.Result = true;
                response.Message = "La contraseña se modifico correctamente";
            }
            catch (Exception ex)
            {
                response.Code = 67823458370773;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458370773, $"Error en {metodo}(int iIdUsuario, string sPassword): {ex.Message}", iIdUsuario, sPassword, ex, response));
            }
            return response;
        }

        public IMDResponse<EntUsuario> BLogin(string sUsuario, string sPassword)
        {
            IMDResponse<EntUsuario> response = new IMDResponse<EntUsuario>();

            string metodo = nameof(this.BLogin);
            logger.Info(IMDSerialize.Serialize(67823458374658, $"Inicia {metodo}(string sUsuario, string sPassword)", sUsuario, sPassword));

            try
            {
                sPassword = BEncodePassword(sPassword);

                IMDResponse<DataTable> dtUsuario = datUsuario.DLogin(sUsuario, sPassword);

                if (dtUsuario.Code != 0)
                {
                    return dtUsuario.GetResponse<EntUsuario>();
                }

                if (dtUsuario.Result.Rows.Count != 1)
                {
                    response.Code = 78772637586;
                    response.Message = "Usuario o contraseña incorrecta.";
                    return response;
                }

                EntUsuario entUsuario = new EntUsuario();

                foreach (DataRow item in dtUsuario.Result.Rows)
                {

                    IMDDataRow dr = new IMDDataRow(item);

                    entUsuario.iIdUsuario = dr.ConvertTo<int>("iIdUsuario");
                    entUsuario.iIdTipoCuenta = dr.ConvertTo<int>("iIdTipoCuenta");
                    entUsuario.sTipoCuenta = dr.ConvertTo<string>("sTipoCuenta");
                    entUsuario.iIdPerfil = dr.ConvertTo<int>("iIdPerfil");
                    entUsuario.sPerfil = dr.ConvertTo<string>("sPerfil");
                    entUsuario.sUsuario = dr.ConvertTo<string>("sUsuario");
                    //entUsuario.sPassword = dr.ConvertTo<string>("sPassword");
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
                }

                response.Result = entUsuario;



                response.Message = "Inicio de sesión existoso.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458375435;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458375435, $"Error en {metodo}(string sUsuario, string sPassword): {ex.Message}", sUsuario, sPassword, ex, response));
            }
            return response;
        }

        public string BEncodePassword(string sPassWord)
        {
            IMDResponse<string> response = new IMDResponse<string>();
            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BEncrypt(sPassWord, "M3diT0cPassword1", "Evector1");

            }
            catch (Exception)
            {

                throw;
            }
            return response.Result;
        }

        public string BEncodePassword(string sCadena, string sKey, string sVector)
        {
            IMDResponse<string> response = new IMDResponse<string>();
            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BEncrypt(sCadena, sKey, sVector);

            }
            catch (Exception)
            {

                throw;
            }
            return response.Result;
        }

        public string BDeCodePassWord(string sPassWord)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BDeCodePassWord);
            logger.Info(IMDSerialize.Serialize(67823458366888, $"Inicia {metodo}(string sPassWord)"));

            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BDecrypt(sPassWord, "M3diT0cPassword1", "Evector1");

                sPassWord = response.Result;
            }
            catch (Exception ex)
            {
                response.Code = 67823458367665;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458367665, $"Error en {metodo}(string sPassWord): {ex.Message}", ex, response));
            }
            return sPassWord;
        }

        public string BDeCodePassWord(string sCadena, string sKey, string sVector)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BDeCodePassWord);
            logger.Info(IMDSerialize.Serialize(67823458366888, $"Inicia {metodo}(string sCadena, string sKey, string sVector)"));

            try
            {
                IMDEndec authentication = new IMDEndec();

                response = authentication.BDecrypt(sCadena, sKey, sVector);

                sCadena = response.Result;
            }
            catch (Exception ex)
            {
                response.Code = 67823458367665;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458367665, $"Error en {metodo}(string sCadena, string sKey, string sVector): {ex.Message}", ex, response));
            }
            return sCadena;
        }
    }
}
