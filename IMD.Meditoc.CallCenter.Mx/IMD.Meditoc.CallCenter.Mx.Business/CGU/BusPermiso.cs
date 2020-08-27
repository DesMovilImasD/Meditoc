using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        public IMDResponse<bool> DSavePermiso(List<EntPermiso> entPermisos)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSavePermiso);
            logger.Info(IMDSerialize.Serialize(67823458347463, $"Inicia {metodo}(EntPermiso entPermiso)", entPermisos));

            try
            {
                if (entPermisos == null)
                {
                    response.Code = 67823458339693;
                    response.Message = "No se ingresó ningun permiso.";
                    return response;
                }

                foreach (EntPermiso entPermiso in entPermisos)
                {


                    response = bValidaDatos(entPermiso);

                    if (!response.Result) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                    {
                        return response;
                    }

                    response = datPermiso.DSavePermiso(entPermiso);
                    if(response.Code != 0)
                    {
                        response.Code = 67823458339693;
                        response.Message = "No se pudieron guardar todos los permisos. Actualice la página antes de intentar de nuevo";
                        return response;
                    }
                }

                response.Code = 0;
                //response.Message = entPermiso.iid == 0 ? "El perfil se guardó correctamente" : "El perfil se actualizo correctamente";
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Code = 67823458348240;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458348240, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermisos, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntPermisoSistema>> BObtenerPermisoxPerfil(int? iIdPermiso)
        {
            IMDResponse<List<EntPermisoSistema>> response = new IMDResponse<List<EntPermisoSistema>>();

            string metodo = nameof(this.BObtenerPermisoxPerfil);
            logger.Info(IMDSerialize.Serialize(67823458354456, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataSet> dtPermisos = datPermiso.DObtenerPermisosPorPerfil(iIdPermiso);

                var drBotones = dtPermisos.Result.Tables[2].Rows;
                var drSubmodulos = dtPermisos.Result.Tables[1].Rows;
                var drModulos = dtPermisos.Result.Tables[0].Rows;

                List<EntPermisoSistema> lstPermisoSistema = new List<EntPermisoSistema>();
                List<EntSubModuloPermiso> lstPermisoSubModulo = new List<EntSubModuloPermiso>();
                List<EntBotonPermiso> lstPermisoBotones = new List<EntBotonPermiso>();

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
                    entSubModulo.lstBotones = lstPermisoBotones.Where(x => x.iIdModulo == entSubModulo.iIdModulo && x.iIdSubModulo == entSubModulo.iIdSubModulo).ToList();

                    lstPermisoSubModulo.Add(entSubModulo);
                }

                foreach (DataRow item in drModulos)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntPermisoSistema permiso = new EntPermisoSistema();

                    permiso.iIdModulo = dr.ConvertTo<int>("iIdModulo");
                    permiso.sNombre = dr.ConvertTo<string>("sNombre");
                    permiso.lstSubModulo = lstPermisoSubModulo.Where(x => x.iIdModulo == permiso.iIdModulo).ToList();

                    lstPermisoSistema.Add(permiso);
                }

                response.Message = "Lista de permisos";
                response.Result = lstPermisoSistema;

            }
            catch (Exception ex)
            {
                response.Code = 67823458355233;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458355233, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> bValidaDatos(EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.bValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458349017, $"Inicia {metodo}(EntPermiso entPermiso)", entPermiso));

            try
            {
                if (entPermiso.iIdPerfil == 0)
                {
                    response.Code = 67823458342024;
                    response.Message = "El id de perfil no puede ser 0";
                    response.Result = false;

                    return response;
                }

                if (entPermiso.iIdModulo == 0)
                {
                    response.Code = 67823458342024;
                    response.Message = "El id de módulo no puede ser 0";
                    response.Result = false;

                    return response;
                }

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458349794;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458349794, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
            }
            return response;
        }
    }
}
