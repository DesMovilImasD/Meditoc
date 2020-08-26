using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

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
        public IMDResponse<bool> DSavePermiso(EntPermiso entPermiso)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSavePermiso);
            logger.Info(IMDSerialize.Serialize(67823458347463, $"Inicia {metodo}(EntPermiso entPermiso)", entPermiso));

            try
            {
                if (entPermiso == null)
                {
                    response.Code = 67823458339693;
                    response.Message = "No se ingresó ningun permiso.";
                    return response;
                }

                response = bValidaDatos(entPermiso);

                if (!response.Result) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                {
                    return response;
                }

                response = datPermiso.DSavePermiso(entPermiso);

                response.Code = 0;
                //response.Message = entPermiso.iid == 0 ? "El perfil se guardó correctamente" : "El perfil se actualizo correctamente";
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Code = 67823458348240;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458348240, $"Error en {metodo}(EntPermiso entPermiso): {ex.Message}", entPermiso, ex, response));
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
                IMDResponse<DataTable> dtPermisos = datPermiso.DObtenerPermisosPorPerfil(iIdPermiso);
                List<EntPermisoSistema> lstPermisoSistema = new List<EntPermisoSistema>();
                List<EntSubModuloPermiso> lstPermisoSubModulo = new List<EntSubModuloPermiso>();
                List<EntBotonPermiso> lstPermisoBotones = new List<EntBotonPermiso>();

                foreach (DataRow item in dtPermisos.Result.DataSet.Tables[0].Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntPermisoSistema permiso = new EntPermisoSistema();

                    permiso.iIdModulo = dr.ConvertTo<int>("iIdModulo");
                    permiso.sNombre = dr.ConvertTo<string>("sNombre");
                    permiso.lstSubModulo = new List<EntSubModuloPermiso>();

                    foreach (DataRow item2 in dtPermisos.Result.DataSet.Tables[1].Rows)
                    {
                        IMDDataRow dr2 = new IMDDataRow(item2);
                        EntSubModuloPermiso entSubModulo = new EntSubModuloPermiso();

                        entSubModulo.iIdModulo = dr2.ConvertTo<int>("iIdModulo");
                        entSubModulo.iIdSubModulo = dr2.ConvertTo<int>("iIdSubModulo");
                        entSubModulo.sNombre = dr2.ConvertTo<string>("sNombre");
                        entSubModulo.lstBotones = new List<EntBotonPermiso>();

                        foreach (DataRow item3 in dtPermisos.Result.DataSet.Tables[1].Rows)
                        {
                            IMDDataRow dr3 = new IMDDataRow(item3);
                            EntBotonPermiso entBoton = new EntBotonPermiso();

                            entBoton.iIdBoton = dr3.ConvertTo<int>("iIdBoton");
                            entBoton.iIdModulo = dr3.ConvertTo<int>("iIdModulo");
                            entBoton.iIdSubModulo = dr3.ConvertTo<int>("iIdSubModulo");
                            entBoton.sNombre = dr3.ConvertTo<string>("sNombre");

                            entSubModulo.lstBotones.Add(entBoton);
                        }

                        permiso.lstSubModulo.Add(entSubModulo);
                    }

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
                if (entPermiso.sNombre == "")
                {
                    response.Code = 67823458342024;
                    response.Message = "El nombre del permiso no puede ser vacio.";
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
