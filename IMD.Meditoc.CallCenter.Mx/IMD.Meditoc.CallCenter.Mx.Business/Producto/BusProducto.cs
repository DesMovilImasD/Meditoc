using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Producto;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                response = BValidaDatos(entProducto);
                if (response.Code != 0)
                {
                    return response;
                }

                response = datProducto.DSaveProducto(entProducto);

                if (response.Code != 0)
                {
                    response.Result = false;
                    return response;
                }

                response.Message = entProducto.iIdProducto == 0 ? "Se guardo con exito" : "Se actualizo con exito";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458395637;
                response.Message = "Ocurrió un error inesperado";

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

                if (dtProductos.Result.Rows.Count == 0)
                {
                    response = dtProductos.GetResponse<List<EntProducto>>();
                    response.Code = 67823458396414;
                    response.Message = "No cuenta con registros de productos.";
                    return response;
                }

                foreach (DataRow item in dtProductos.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntProducto producto = new EntProducto();

                    producto.iIdProducto = dr.ConvertTo<int>("iIdProducto");
                    producto.iIdTipoProducto = dr.ConvertTo<int>("iIdTipoProducto");
                    producto.sTipoProducto = dr.ConvertTo<string>("sTipoProducto");
                    producto.sNombre = dr.ConvertTo<string>("sNombre");
                    producto.sNombreCorto = dr.ConvertTo<string>("sNombreCorto");
                    producto.sDescripcion = dr.ConvertTo<string>("sDescripcion");
                    producto.fCosto = dr.ConvertTo<double>("fCosto");
                    producto.iMesVigencia = dr.ConvertTo<int>("iMesVigencia");
                    producto.sIcon = dr.ConvertTo<string>("sIcon");
                    producto.sPrefijoFolio = dr.ConvertTo<string>("sPrefijoFolio");
                    producto.bComercial = Convert.ToBoolean(dr.ConvertTo<int>("bComercial"));
                    producto.bActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo"));
                    producto.bBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja"));

                    lstProductos.Add(producto);
                }


                response.Code = 0;
                response.Result = lstProductos;
                response.Message = "Lista de productos";
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

                if (entProducto.sNombre == "")
                {
                    response.Message = "El nombre no puede ser vacio.";
                    return response;
                }

                if (entProducto.sNombreCorto == "")
                {
                    response.Message = "El nombre corto no puede ser vacio.";
                    return response;
                }


                if (entProducto.sDescripcion == "")
                {
                    response.Message = "La descripción no puede ser vacio.";
                    return response;
                }

                if (entProducto.fCosto <= 0)
                {
                    response.Message = "El costo debe ser mayor a 0.";
                    return response;
                }

                if (entProducto.iMesVigencia <= 0)
                {
                    response.Message = "La vigencia debe ser mayor a 0.";
                    return response;
                }


                if (entProducto.sIcon == "")
                {
                    response.Message = "El icono no puede ser vacio.";
                    return response;
                }


                if (entProducto.sPrefijoFolio == "")
                {
                    response.Message = "El prefijo no puede ser vacio.";
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458411177, $"Error en {metodo}(EntProducto entProducto): {ex.Message}", entProducto, ex, response));
            }
            return response;
        }
    }
}
