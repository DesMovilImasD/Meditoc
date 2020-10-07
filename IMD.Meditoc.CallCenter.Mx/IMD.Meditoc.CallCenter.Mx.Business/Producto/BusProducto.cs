using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Producto;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IMD.Meditoc.CallCenter.Mx.Business.Producto
{
    public class BusProducto
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusProducto));
        DatProducto datProducto;

        public BusProducto()
        {
            datProducto = new DatProducto();
        }

        public IMDResponse<bool> BSaveProducto(EntProducto entProducto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveProducto);
            logger.Info(IMDSerialize.Serialize(67823458394860, $"Inicia {metodo}(EntProducto entProducto)", entProducto));

            try
            {
                if (entProducto.bActivo && !entProducto.bBaja)
                {
                    response = BValidaDatos(entProducto);

                    if (response.Code != 0)
                    {
                        return response;
                    }
                }

                response = datProducto.DSaveProducto(entProducto);

                if (response.Code != 0)
                {
                    response.Result = false;
                    return response;
                }

                response.Message = entProducto.iIdProducto == 0 ? "El producto ha sido guardado correctamente." : !entProducto.bActivo ? "El producto ha sido eliminado correctamente." : "El producto ha sido actualizado correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458395637;
                response.Message = "Ocurrió un error inesperado al guardar el producto.";

                logger.Error(IMDSerialize.Serialize(67823458395637, $"Error en {metodo}(EntProducto entProducto): {ex.Message}", entProducto, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntProducto>> BObtenerProductos(int? iIdProducto)
        {
            IMDResponse<List<EntProducto>> response = new IMDResponse<List<EntProducto>>();

            string metodo = nameof(this.BObtenerProductos);
            logger.Info(IMDSerialize.Serialize(67823458396414, $"Inicia {metodo}(int? iIdProducto)", iIdProducto));

            try
            {
                List<EntProducto> lstProductos = new List<EntProducto>();
                IMDResponse<DataTable> dtProductos = datProducto.DObterProductos(iIdProducto);

                if (dtProductos.Code != 0)
                {
                    response = dtProductos.GetResponse<List<EntProducto>>();
                    return response;
                }

                if (dtProductos.Result.Rows.Count == 0)
                {
                    response = dtProductos.GetResponse<List<EntProducto>>();
                    response.Code = 67823458396414;
                    response.Message = "No se encontraron productos en el sistema.";
                    return response;
                }

                foreach (DataRow item in dtProductos.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntProducto producto = new EntProducto
                    {
                        iIdProducto = dr.ConvertTo<int>("iIdProducto"),
                        iIdTipoProducto = dr.ConvertTo<int>("iIdTipoProducto"),
                        iIdGrupoProducto = dr.ConvertTo<int>("iIdGrupoProducto"),
                        sTipoProducto = dr.ConvertTo<string>("sTipoProducto"),
                        sGrupoProducto = dr.ConvertTo<string>("sGrupoProducto"),
                        sNombre = dr.ConvertTo<string>("sNombre"),
                        sNombreCorto = dr.ConvertTo<string>("sNombreCorto"),
                        sDescripcion = dr.ConvertTo<string>("sDescripcion"),
                        fCosto = dr.ConvertTo<double>("fCosto"),
                        iMesVigencia = dr.ConvertTo<int>("iMesVigencia"),
                        sIcon = dr.ConvertTo<string>("sIcon"),
                        sPrefijoFolio = dr.ConvertTo<string>("sPrefijoFolio"),
                        bComercial = Convert.ToBoolean(dr.ConvertTo<int>("bComercial")),
                        bActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo")),
                        bBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja"))
                    };
                    producto.sComercial = producto.bComercial ? "Si" : "No";
                    producto.sCosto = producto.fCosto.ToString("C");

                    lstProductos.Add(producto);
                }


                response.Code = 0;
                response.Result = lstProductos;
                response.Message = "La lista de productos ha sido obtenida.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458397191;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458397191, $"Error en {metodo}(int? iIdProducto): {ex.Message}", iIdProducto, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BValidaDatos(EntProducto entProducto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458410400, $"Inicia {metodo}(EntProducto entProducto)", entProducto));

            try
            {
                response.Code = 67823458410400;

                if (entProducto.iIdTipoProducto < 1)
                {
                    response.Message = "No se ha especificado el tipo de producto.";
                    return response;
                }
                if (entProducto.iIdGrupoProducto < 1)
                {
                    response.Message = "No se ha especificado el grupo del producto.";
                    return response;
                }

                if (entProducto.sNombre == "")
                {
                    response.Message = "El nombre del producto no puede ser vacío.";
                    return response;
                }

                if (entProducto.sNombreCorto == "")
                {
                    response.Message = "El nombre corto del producto no puede ser vacío.";
                    return response;
                }


                if (entProducto.sDescripcion == "")
                {
                    response.Message = "La descripción del producto no puede ser vacía.";
                    return response;
                }

                if (entProducto.fCosto <= 0)
                {
                    response.Message = "El costo del producto debe ser mayor a 0.";
                    return response;
                }


                if (entProducto.iMesVigencia <= 0 && entProducto.iIdTipoProducto == (int)EnumTipoProducto.Membresia)
                {
                    response.Message = "La duración de la vigencia (en meses) del producto de una membresía debe ser mayor a 0.";
                    return response;
                }


                if (entProducto.sIcon == "")
                {
                    response.Message = "El ícono del producto no puede ser vacío.";
                    return response;
                }


                if (entProducto.sPrefijoFolio == "")
                {
                    response.Message = "El prefijo de generación de folios no puede ser vacío.";
                    return response;
                }

                //IMDResponse<List<EntProducto>> lstProductos = BObtenerProductos(null);

                //if (lstProductos.Result.Count > 0)
                //{
                //    if (lstProductos.Result.Exists(c => c.sPrefijoFolio == entProducto.sPrefijoFolio))
                //    {
                //        response.Message = "Ya existe un prefijo con el mismo nombre.";
                //        return response;
                //    }
                //}

                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458411177;
                response.Message = "Ocurrió un error inesperado al validar los datos del producto.";

                logger.Error(IMDSerialize.Serialize(67823458411177, $"Error en {metodo}(EntProducto entProducto): {ex.Message}", entProducto, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntProducto>> BGetServices()
        {
            IMDResponse<List<EntProducto>> response = new IMDResponse<List<EntProducto>>();

            string metodo = nameof(this.BGetServices);
            logger.Info(IMDSerialize.Serialize(67823458467121, $"Inicia {metodo}"));

            try
            {
                response = BObtenerProductos(null);

                response.Result = response.Result
                    .Where(x => x.iIdTipoProducto == 2 && x.bComercial && x.iIdGrupoProducto == (int)EnumGrupoProducto.Meditoc360Products).ToList();

                response.Code = 0;
                response.Message = "Lista de servicios consultados.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458467898;
                response.Message = "Ocurrió un error inesperado al consultar la lista de servicios.";

                logger.Error(IMDSerialize.Serialize(67823458467898, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntProducto>> BGetMembership()
        {
            IMDResponse<List<EntProducto>> response = new IMDResponse<List<EntProducto>>();

            string metodo = nameof(this.BGetMembership);
            logger.Info(IMDSerialize.Serialize(67823458468675, $"Inicia {metodo}"));

            try
            {
                response = BObtenerProductos(null);

                response.Result = response.Result
                    .Where(x => x.iIdTipoProducto == 1 && x.bComercial && x.iIdGrupoProducto == (int)EnumGrupoProducto.Meditoc360Products).OrderBy(x => x.fCosto)
                    .ToList();

                response.Code = 0;
                response.Message = "Lista de membresías consultadas.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458469452;
                response.Message = "Ocurrió un error inesperado al consultar la lista de membresías.";

                logger.Error(IMDSerialize.Serialize(67823458469452, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntProductosNutricionalPsicologia> BGetProductosNutricionalPsicologia()
        {
            IMDResponse<EntProductosNutricionalPsicologia> response = new IMDResponse<EntProductosNutricionalPsicologia>();

            string metodo = nameof(this.BGetProductosNutricionalPsicologia);
            logger.Info(IMDSerialize.Serialize(67823458634953, $"Inicia {metodo}()"));

            try
            {
                IMDResponse<List<EntProducto>> resGetProducts = this.BObtenerProductos(null);
                if (resGetProducts.Code != 0)
                {
                    return resGetProducts.GetResponse<EntProductosNutricionalPsicologia>();
                }

                EntProductosNutricionalPsicologia entProductos = new EntProductosNutricionalPsicologia
                {
                    lstNutritionalProducts = resGetProducts.Result.Where(x => x.iIdGrupoProducto == (int)EnumGrupoProducto.NutritionalProducts && x.bComercial).OrderBy(x => x.fCosto).ToList(),
                    lstPsychologyProducts = resGetProducts.Result.Where(x => x.iIdGrupoProducto == (int)EnumGrupoProducto.PsychologyProducts && x.bComercial).OrderBy(x => x.fCosto).ToList()
                };

                response.Code = 0;
                response.Message = "Lista de productos consultados";
                response.Result = entProductos;
            }
            catch (Exception ex)
            {
                response.Code = 67823458635730;
                response.Message = "Ocurrió un error inesperado al consultar los productos disponibles.";

                logger.Error(IMDSerialize.Serialize(67823458635730, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
