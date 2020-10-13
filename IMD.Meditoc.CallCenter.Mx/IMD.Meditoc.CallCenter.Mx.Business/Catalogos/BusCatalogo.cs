using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;

namespace IMD.Meditoc.CallCenter.Mx.Business.Catalogos
{
    public class BusCatalogo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusCatalogo));

        DatCatalogo datEspecialidad;

        public BusCatalogo()
        {
            datEspecialidad = new DatCatalogo();
        }

        public IMDResponse<EntCatalogos> BGetCatalogos()
        {
            IMDResponse<EntCatalogos> response = new IMDResponse<EntCatalogos>();

            string metodo = nameof(this.BGetCatalogos);
            logger.Info(IMDSerialize.Serialize(67823458644277, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataSet> resGetCat = datEspecialidad.DGetCatalogos();
                if (resGetCat.Code != 0)
                {
                    return resGetCat.GetResponse<EntCatalogos>();
                }
                if (resGetCat.Result.Tables.Count < 1)
                {
                    response.Code = -234768745;
                    response.Message = "El sistema no cuenta con catálogos registrados.";
                }

                EntCatalogos entCatalogos = new EntCatalogos
                {
                    catCuponCategoria = this.BReadCat(resGetCat.Result, 0),
                    catEspecialidad = this.BReadCat(resGetCat.Result, 1),
                    catEstatusConsulta = this.BReadCat(resGetCat.Result, 2),
                    catGrupoProducto = this.BReadCat(resGetCat.Result, 3),
                    catOrigen = this.BReadCat(resGetCat.Result, 4),
                    catSexo = this.BReadCat(resGetCat.Result, 5),
                    catTipoDoctor = this.BReadCat(resGetCat.Result, 6),
                    catTipoProducto = this.BReadCat(resGetCat.Result, 7),
                    catUsuarioAccion = this.BReadCat(resGetCat.Result, 8),
                    catTipoCuenta = this.BReadCat(resGetCat.Result, 9),
                    catPerfil = this.BReadCat(resGetCat.Result, 10),
                    catIcon = this.BReadCat(resGetCat.Result, 11),
                };

                response.Code = 0;
                response.Message = "Se han consultado los catálogos del sistema.";
                response.Result = entCatalogos;
            }
            catch (Exception ex)
            {
                response.Code = 67823458645054;
                response.Message = "Ocurrió un error inesperado al consultar los catalogos del sistema.";

                logger.Error(IMDSerialize.Serialize(67823458645054, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public List<EntCatalogo> BReadCat(DataSet ds, int index)
        {
            List<EntCatalogo> values = new List<EntCatalogo>();
            try
            {
                DataTable dt = ds.Tables[index];
                foreach (DataRow item in dt.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);

                    EntCatalogo entCatalogo = new EntCatalogo
                    {
                        fiId = dr.ConvertTo<int>("fiId"),
                        fsDescripcion = dr.ConvertTo<string>("fsDescripcion")
                    };
                    values.Add(entCatalogo);
                }
            }
            catch (Exception)
            {

            }
            return values;
        }

        /// <summary>
        /// Guardar una especialidad
        /// </summary>
        /// <param name="entEspecialidad"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveEspecialidad(EntEspecialidad entEspecialidad)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458450027, $"Inicia {metodo}(EntEspecialidad entEspecialidad)", entEspecialidad));

            try
            {
                if (entEspecialidad == null)
                {
                    response.Code = -876348289919;
                    response.Message = "La información para guardar la especialidad está incompleta.";
                    return response;
                }

                if (entEspecialidad.bActivo && !entEspecialidad.bBaja)
                {
                    if (string.IsNullOrWhiteSpace(entEspecialidad.sNombre))
                    {
                        response.Code = -398763241989882;
                        response.Message = "No se ha ingresado el nombre de la especialidad.";
                        return response;
                    }
                }

                IMDResponse<bool> respuestaSaveEspecialidad = datEspecialidad.DSaveEspecialidad(entEspecialidad.iIdEspecialidad, entEspecialidad.sNombre, entEspecialidad.iIdUsuarioMod, entEspecialidad.bActivo, entEspecialidad.bBaja);
                if (respuestaSaveEspecialidad.Code != 0)
                {
                    return respuestaSaveEspecialidad;
                }

                response.Code = 0;
                response.Message = "La especialidad se guardó correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458450804;
                response.Message = "Ocurrió un error inesperado al guardar la especialidad.";

                logger.Error(IMDSerialize.Serialize(67823458450804, $"Error en {metodo}(EntEspecialidad entEspecialidad): {ex.Message}", entEspecialidad, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtener la lista de las especialidades
        /// </summary>
        /// <param name="piIdEspecialidad"></param>
        /// <returns></returns>
        public IMDResponse<List<EntEspecialidad>> BGetEspecialidad(int? piIdEspecialidad = null)
        {
            IMDResponse<List<EntEspecialidad>> response = new IMDResponse<List<EntEspecialidad>>();

            string metodo = nameof(this.BGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458451581, $"Inicia {metodo}(int? piIdEspecialidad = null)", piIdEspecialidad));

            try
            {
                IMDResponse<DataTable> respuestaGetEspecialidades = datEspecialidad.DGetEspecialidad(piIdEspecialidad);
                if (respuestaGetEspecialidades.Code != 0)
                {
                    return respuestaGetEspecialidades.GetResponse<List<EntEspecialidad>>();
                }

                List<EntEspecialidad> lstEspecialidades = new List<EntEspecialidad>();
                foreach (DataRow drEspecialidad in respuestaGetEspecialidades.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drEspecialidad);

                    EntEspecialidad especialidad = new EntEspecialidad
                    {
                        bActivo = dr.ConvertTo<bool>("bActivo"),
                        bBaja = dr.ConvertTo<bool>("bBaja"),
                        dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion"),
                        iIdEspecialidad = dr.ConvertTo<int>("iIdEspecialidad"),
                        sNombre = dr.ConvertTo<string>("sNombre")
                    };

                    especialidad.sFechaCreacion = especialidad.dtFechaCreacion.ToString("dd/MM/yyyy HH:mm");

                    lstEspecialidades.Add(especialidad);
                }

                response.Code = 0;
                response.Message = "Las especialidades han sido obtenidas.";
                response.Result = lstEspecialidades;
            }
            catch (Exception ex)
            {
                response.Code = 67823458452358;
                response.Message = "Ocurrió un error inesperado al consultar las especialidades.";

                logger.Error(IMDSerialize.Serialize(67823458452358, $"Error en {metodo}(int? piIdEspecialidad = null): {ex.Message}", piIdEspecialidad, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtener la lista de las especialidades que solo contienen especialidades con doctores registrados
        /// </summary>
        /// <param name="piIdEspecialidad"></param>
        /// <returns></returns>
        public IMDResponse<List<EntEspecialidad>> BGetEspecialidadFiltrado(int? piIdEspecialidad = null)
        {
            IMDResponse<List<EntEspecialidad>> response = new IMDResponse<List<EntEspecialidad>>();

            string metodo = nameof(this.BGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458451581, $"Inicia {metodo}(int? piIdEspecialidad = null)", piIdEspecialidad));

            try
            {
                IMDResponse<DataTable> respuestaGetEspecialidades = datEspecialidad.DGetEspecialidadFiltrado(piIdEspecialidad);
                if (respuestaGetEspecialidades.Code != 0)
                {
                    return respuestaGetEspecialidades.GetResponse<List<EntEspecialidad>>();
                }

                List<EntEspecialidad> lstEspecialidades = new List<EntEspecialidad>();
                foreach (DataRow drEspecialidad in respuestaGetEspecialidades.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drEspecialidad);

                    EntEspecialidad especialidad = new EntEspecialidad
                    {
                        bActivo = dr.ConvertTo<bool>("bActivo"),
                        bBaja = dr.ConvertTo<bool>("bBaja"),
                        dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion"),
                        iIdEspecialidad = dr.ConvertTo<int>("iIdEspecialidad"),
                        sNombre = dr.ConvertTo<string>("sNombre")
                    };

                    especialidad.sFechaCreacion = especialidad.dtFechaCreacion.ToString("dd/MM/yyyy HH:mm");

                    lstEspecialidades.Add(especialidad);
                }

                response.Code = 0;
                response.Message = "Las especialidades han sido obtenidas.";
                response.Result = lstEspecialidades;
            }
            catch (Exception ex)
            {
                response.Code = 67823458452358;
                response.Message = "Ocurrió un error inesperado al consultar las especialidades.";

                logger.Error(IMDSerialize.Serialize(67823458452358, $"Error en {metodo}(int? piIdEspecialidad = null): {ex.Message}", piIdEspecialidad, ex, response));
            }
            return response;
        }
    }
}
