using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusPermiso
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPermiso));
        DatPermiso datPermiso;

        public BusPermiso()
        {
            datPermiso = new DatPermiso();
        }

        /// <summary>
        /// Guardar o actualizar los permisos del perfil
        /// </summary>
        /// <param name="entPermisos"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSavePermiso(List<EntPermiso> entPermisos)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSavePermiso);
            logger.Info(IMDSerialize.Serialize(67823458347463, $"Inicia {metodo}(EntPermiso entPermiso)", entPermisos));

            try
            {
                if (entPermisos == null)
                {
                    response.Code = -76823947687234;
                    response.Message = "No se ingresó información de los permisos.";
                    return response;
                }

                foreach (EntPermiso entPermiso in entPermisos)
                {
                    response = BValidaDatos(entPermiso);

                    if (!response.Result) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                    {
                        return response;
                    }

                    response = datPermiso.DSavePermiso(entPermiso);
                    if (response.Code != 0)
                    {
                        response.Code = -823487677772384;
                        response.Message = "No se pudieron guardar todos los permisos. Recargue la página antes de intentar de nuevo.";
                        return response;
                    }
                }

                response.Code = 0;
                response.Message = "Los permisos del perfil han sido actualizados";
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Code = 67823458348240;
                response.Message = "Ocurrió un error inesperado al guardar el permiso solicitado";

                logger.Error(IMDSerialize.Serialize(67823458348240, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermisos, ex, response));
            }
            return response;
        }
        
        /// <summary>
        /// Obtener los permisos del sistema o de un perfil proporcionado
        /// </summary>
        /// <param name="iIdPerfil"></param>
        /// <returns></returns>
        //NO SE USA
        public IMDResponse<List<EntPermisoSistema>> BObtenerPermisoxPerfil(int? iIdPerfil)
        {
            IMDResponse<List<EntPermisoSistema>> response = new IMDResponse<List<EntPermisoSistema>>();

            string metodo = nameof(this.BObtenerPermisoxPerfil);
            logger.Info(IMDSerialize.Serialize(67823458354456, $"Inicia {metodo}(int? iIdPermiso)", iIdPerfil));

            try
            {
                IMDResponse<DataSet> dtPermisos = datPermiso.DObtenerPermisosPorPerfil(iIdPerfil);
                if (dtPermisos.Code != 0)
                {
                    return dtPermisos.GetResponse<List<EntPermisoSistema>>();
                }

                DataRowCollection drBotones = dtPermisos.Result.Tables[2].Rows;
                DataRowCollection drSubmodulos = dtPermisos.Result.Tables[1].Rows;
                DataRowCollection drModulos = dtPermisos.Result.Tables[0].Rows;

                List<EntPermisoSistema> lstPermisoSistema = new List<EntPermisoSistema>();
                List<EntSubModuloPermiso> lstPermisoSubModulo = new List<EntSubModuloPermiso>();
                List<EntBotonPermiso> lstPermisoBotones = new List<EntBotonPermiso>();

                //Leer los módulos, submódulos y botones que se tienen permitidos al perfil
                foreach (DataRow item in drBotones)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntBotonPermiso entBoton = new EntBotonPermiso();

                    entBoton.iIdBoton = dr.ConvertTo<int>("iIdBoton");
                    entBoton.iIdModulo = dr.ConvertTo<int>("iIdModulo");
                    entBoton.iIdSubModulo = dr.ConvertTo<int>("iIdSubModulo");
                    entBoton.sNombre = dr.ConvertTo<string>("sNombre");

                    lstPermisoBotones.Add(entBoton);
                }

                foreach (DataRow item in drSubmodulos)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntSubModuloPermiso entSubModulo = new EntSubModuloPermiso();

                    entSubModulo.iIdModulo = dr.ConvertTo<int>("iIdModulo");
                    entSubModulo.iIdSubModulo = dr.ConvertTo<int>("iIdSubModulo");
                    entSubModulo.sNombre = dr.ConvertTo<string>("sNombre");

                    lstPermisoSubModulo.Add(entSubModulo);
                }

                foreach (DataRow item in drModulos)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntPermisoSistema permiso = new EntPermisoSistema();

                    permiso.iIdModulo = dr.ConvertTo<int>("iIdModulo");
                    permiso.sNombre = dr.ConvertTo<string>("sNombre");

                    lstPermisoSistema.Add(permiso);
                }

                //Armar obtejo lista de los permisos
                lstPermisoBotones = lstPermisoBotones.GroupBy(x => new
                {
                    x.iIdModulo,
                    x.iIdSubModulo,
                    x.iIdBoton
                }).Select(x => new EntBotonPermiso
                {
                    iIdModulo = x.Key.iIdModulo,
                    iIdSubModulo = x.Key.iIdSubModulo,
                    iIdBoton = x.Key.iIdBoton,
                    sNombre = x.Select(y => y.sNombre).First()
                }).ToList();

                lstPermisoSubModulo = lstPermisoSubModulo.GroupBy(x => new
                {
                    x.iIdModulo,
                    x.iIdSubModulo
                }).Select(x => new EntSubModuloPermiso
                {
                    iIdModulo = x.Key.iIdModulo,
                    iIdSubModulo = x.Key.iIdSubModulo,
                    sNombre = x.Select(y => y.sNombre).First(),
                    lstBotones = lstPermisoBotones.Where(y => y.iIdModulo == x.Key.iIdModulo && y.iIdSubModulo == x.Key.iIdSubModulo).ToList()
                }).ToList();

                lstPermisoSistema = lstPermisoSistema.GroupBy(x => x.iIdModulo).Select(x => new EntPermisoSistema
                {
                    iIdModulo = x.Key,
                    sNombre = x.Select(y => y.sNombre).First(),
                    lstSubModulo = lstPermisoSubModulo.Where(y => y.iIdModulo == x.Key).ToList()
                }).ToList();

                response.Code = 0;
                response.Message = "Lista de permisos";
                response.Result = lstPermisoSistema;

            }
            catch (Exception ex)
            {
                response.Code = 67823458355233;
                response.Message = "Ocurrió un error inesperado al consultar los permisos.";

                logger.Error(IMDSerialize.Serialize(67823458355233, $"Error en {metodo}(int? iIdPermiso): {ex.Message}", iIdPerfil, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtener objeto de los permisos para mostrar/ocultar los elementos del sistema
        /// </summary>
        /// <param name="piIdPerfil"></param>
        /// <returns></returns>
        public IMDResponse<object> BGetUsuarioPermisos(int piIdPerfil)
        {
            IMDResponse<object> response = new IMDResponse<Object>();

            string metodo = nameof(this.BGetUsuarioPermisos);
            logger.Info(IMDSerialize.Serialize(67823458638061, $"Inicia {metodo}"));

            try
            {
                //Consultar los permisos del perfil, el perfil de superadministrador trae todos los elementos
                int? perfil = piIdPerfil == (int)EnumPerfilPrincipal.Superadministrador ? (int?)null : piIdPerfil;
                IMDResponse<List<EntPermisoSistema>> resGetPermisos = this.BObtenerPermisoxPerfil(perfil);
                if (resGetPermisos.Code != 0)
                {
                    return resGetPermisos.GetResponse<object>();
                }
                if (resGetPermisos.Result.Count == 0)
                {
                    response.Code = -23476876345;
                    response.Message = "El perfil del usuario aún no cuenta con permisos.";
                }

                JObject objModulos = new JObject();

                //Armar JSON con los permisos consultados
                foreach (EntPermisoSistema modulo in resGetPermisos.Result)
                {
                    JObject objSubmodulos = new JObject();
                    foreach (EntSubModuloPermiso submodulo in modulo.lstSubModulo)
                    {
                        JObject objBotones = new JObject();
                        foreach (EntBotonPermiso boton in submodulo.lstBotones)
                        {
                            JObject objBoton = new JObject();
                            objBoton.Add("Nombre", boton.sNombre);

                            objBotones.Add(boton.iIdBoton.ToString(), objBoton);
                        }

                        JObject objSubmodulo = new JObject();
                        objSubmodulo.Add("Nombre", submodulo.sNombre);
                        objSubmodulo.Add("Botones", objBotones);

                        objSubmodulos.Add(submodulo.iIdSubModulo.ToString(), objSubmodulo);
                    }

                    JObject objModulo = new JObject();
                    objModulo.Add("Nombre", modulo.sNombre);
                    objModulo.Add("Submodulos", objSubmodulos);

                    objModulos.Add(modulo.iIdModulo.ToString(), objModulo);
                }

                object obj = objModulos.ToObject<object>();

                response.Code = 0;
                response.Message = "Se han obtenido los permisos del usuario.";
                response.Result = obj;

            }
            catch (Exception ex)
            {
                response.Code = 67823458638838;
                response.Message = "Ocurrió un error inesperado al consultar los permisos del usuario.";

                logger.Error(IMDSerialize.Serialize(67823458638838, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        /// <summary>
        /// Validar los datos para actualizar los permisos
        /// </summary>
        /// <param name="entPermiso"></param>
        /// <returns></returns>
        public IMDResponse<bool> BValidaDatos(EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458349017, $"Inicia {metodo}(EntPermiso entPermiso)", entPermiso));

            try
            {
                if (entPermiso.iIdPerfil == 0)
                {
                    response.Code = -987876827364;
                    response.Message = "La información para guardar el permiso está incompleta. No se especificó el perfil.";
                    response.Result = false;

                    return response;
                }

                if (entPermiso.iIdModulo == 0)
                {
                    response.Code = -767819247987123;
                    response.Message = "La información para guardar el permiso está incompleta.";
                    response.Result = false;

                    return response;
                }

                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458349794;
                response.Message = "Ocurrió un error inesperado al validar los permisos solicitados.";

                logger.Error(IMDSerialize.Serialize(67823458349794, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
            }
            return response;
        }
    }
}
