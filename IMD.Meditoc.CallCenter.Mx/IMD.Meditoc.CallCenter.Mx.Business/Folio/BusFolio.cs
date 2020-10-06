using IMD.Admin.Conekta.Business;
using IMD.Admin.Conekta.Data;
using IMD.Admin.Conekta.Entities;
using IMD.Admin.Conekta.Entities.Orders;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Business.Consulta;
using IMD.Meditoc.CallCenter.Mx.Business.Correo;
using IMD.Meditoc.CallCenter.Mx.Business.Empresa;
using IMD.Meditoc.CallCenter.Mx.Business.Paciente;
using IMD.Meditoc.CallCenter.Mx.Business.Producto;
using IMD.Meditoc.CallCenter.Mx.Data.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Data.Consulta;
using IMD.Meditoc.CallCenter.Mx.Data.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using log4net;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IMD.Meditoc.CallCenter.Mx.Business.Folio
{
    public class BusFolio
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusFolio));
        private readonly DatFolio datFolio;
        private readonly BusUsuario busUsuario;
        private readonly BusConsulta busConsulta;
        private readonly BusProducto busProducto;
        private readonly BusEmpresa busEmpresa;

        public BusFolio()
        {
            datFolio = new DatFolio();
            busUsuario = new BusUsuario();
            busConsulta = new BusConsulta();
            busProducto = new BusProducto();
            busEmpresa = new BusEmpresa();
        }

        /// <summary>
        /// Genera una compra desde el portal de pagos o la App
        /// </summary>
        /// <param name="entConecktaPago"></param>
        /// <returns></returns>
        public IMDResponse<EntDetalleCompra> BNuevoFolioCompra(EntCreateOrder entCreateOrder)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BNuevoFolioCompra);
            logger.Info(IMDSerialize.Serialize(67823458415062, $"Inicia {metodo}(EntConecktaPago entConecktaPago)", entCreateOrder));

            try
            {
                IMDResponse<List<EntProducto>> lstProductos = busProducto.BObtenerProductos(null);

                if (lstProductos.Result == null)
                {
                    return response = lstProductos.GetResponse<EntDetalleCompra>();
                }

                for (int i = 0; i < entCreateOrder.line_items.Count; i++)
                {
                    EntProducto oProducto = lstProductos.Result
                        .Find(x => x.iIdProducto == entCreateOrder.line_items[i].product_id);

                    if (oProducto == null)
                    {
                        response.Code = -876348767345;
                        response.Message = "Producto no válido.";
                        return response;
                    }

                    entCreateOrder.line_items[i].monthsExpiration = oProducto.iMesVigencia;
                    entCreateOrder.line_items[i].name = oProducto.sNombre;
                    entCreateOrder.line_items[i].unit_price = (int)(oProducto.fCosto * 100);
                }

                //Se manda a llamar la creaciión de orden de conekta
                BusOrder busOrder = new BusOrder();
                IMDResponse<EntOrder> resOrder = busOrder.BCreateOrder(entCreateOrder);
                if (resOrder.Code != 0)
                {
                    return response = resOrder.GetResponse<EntDetalleCompra>();
                }

                //string f = JsonConvert.SerializeObject(entOrder.Result);
                //IMDResponse<EntRequestOrder> requesOrder = new IMDResponse<EntRequestOrder>();
                //requesOrder.Result = JsonConvert.DeserializeObject<EntRequestOrder>(f);


                response = BGuardarCompraUnica(resOrder.Result, entCreateOrder.iIdOrigen);

                response.Code = 0;
                response.Message = "Operación exitosa.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458415839;
                response.Message = "Ocurrió un error inesperado al crear la orden";

                logger.Error(IMDSerialize.Serialize(67823458415839, $"Error en {metodo}(EntConecktaPago entConecktaPago): {ex.Message}", entCreateOrder, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Guarda los datos de la orden generada por Conekta
        /// </summary>
        /// <param name="entOrder"></param>
        /// <param name="entCreateOrder"></param>
        /// <returns></returns>
        public IMDResponse<EntDetalleCompra> BGuardarCompraUnica(EntOrder entOrder, int piIdOrigen)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();
            EntDetalleCompra entDetalleCompra = new EntDetalleCompra();

            string metodo = nameof(this.BGuardarCompraUnica);
            logger.Info(IMDSerialize.Serialize(67823458416616, $"Inicia {metodo}(EntRequestOrder entOrder, EntConecktaPago entConecktaPago)", entOrder, piIdOrigen));
            EntDetalleCompra oDetalleCompra = new EntDetalleCompra();


            try
            {
                entOrder.customer_info.phone = entOrder.customer_info?.phone?.Replace(" ", "").Replace(ConfigurationManager.AppSettings["CONEKTA_PHONE_ACCESS"], "");

                EntFolio entFolio = new EntFolio();
                //using (TransactionScope scope = new TransactionScope())
                //{

                //Se asigna empresa al folio
                BusEmpresa busEmpresa = new BusEmpresa();
                IMDResponse<List<EntEmpresa>> respuestaObtenerEmpresas = busEmpresa.BGetEmpresas(null, entOrder.customer_info.email);

                if (respuestaObtenerEmpresas.Code != 0)
                {
                    return respuestaObtenerEmpresas.GetResponse<EntDetalleCompra>();
                }

                if (respuestaObtenerEmpresas.Result.Count > 0)
                {
                    EntEmpresa empresaRegistrada = respuestaObtenerEmpresas.Result.FirstOrDefault();
                    entFolio.iIdEmpresa = empresaRegistrada.iIdEmpresa;
                }
                else
                {
                    EntEmpresa entEmpresa = new EntEmpresa()
                    {
                        iIdEmpresa = 0,
                        sNombre = entOrder.customer_info.name,
                        sCorreo = entOrder.customer_info.email,
                        iIdUsuarioMod = 1,
                        bActivo = true,
                        bBaja = false
                    };

                    IMDResponse<EntEmpresa> respuestaSaveEmpresa = busEmpresa.BSaveEmpresa(entEmpresa);
                    if (respuestaSaveEmpresa.Code != 0)
                    {
                        return respuestaSaveEmpresa.GetResponse<EntDetalleCompra>();
                    }

                    EntEmpresa empresaRegistrada = respuestaSaveEmpresa.Result;
                    entFolio.iIdEmpresa = empresaRegistrada.iIdEmpresa;
                }

                //Se crea el folio
                entFolio.iIdOrigen = piIdOrigen;
                entFolio.bTerminosYCondiciones = false;
                entFolio.sOrdenConekta = entOrder.id;

                //Se crea el paciente
                EntPaciente entPaciente = new EntPaciente();

                entPaciente.iIdPaciente = 0;
                entPaciente.iIdFolio = entFolio.iIdFolio;
                entPaciente.sTelefono = entOrder.customer_info.phone;
                entPaciente.sCorreo = entOrder.customer_info.email;
                entPaciente.sNombre = entOrder.customer_info.name;


                foreach (EntLineItemDetail item in entOrder.line_items.data)
                {
                    for (int i = 0; i < item.quantity; i++)
                    {
                        IMDResponse<bool> resGenerarFolio = this.BGenerarFolioCompra(entFolio, entPaciente, item, oDetalleCompra, i);
                        if (resGenerarFolio.Code != 0)
                        {
                            return resGenerarFolio.GetResponse<EntDetalleCompra>();
                        }
                    }
                }
                //    scope.Complete();
                //}

                oDetalleCompra.nTotal = oDetalleCompra.lstArticulos
                    .Sum(x => x.nCantidad * x.nPrecio);
                oDetalleCompra.nTotalPagado = entOrder.amount_paid / 100d;
                oDetalleCompra.nTotalDescuento = entOrder.amount_discount / 100d;
                oDetalleCompra.nTotalIVA = entOrder.amount_tax / 100d;
                oDetalleCompra.sCodigoCupon = entOrder.coupon_code;
                oDetalleCompra.sEmail = entOrder.customer_info.email;
                oDetalleCompra.sNombre = entOrder.customer_info.name;
                oDetalleCompra.sOrden = entOrder.id;
                oDetalleCompra.sTelefono = entOrder.customer_info.phone;
                oDetalleCompra.bAplicaIVA = entOrder.amount_tax > 0d;


                IMDResponse<bool> responseCorreo = this.BEnvioCorreo(oDetalleCompra);

                response.Code = 0;
                response.Result = oDetalleCompra;
            }
            catch (Exception ex)
            {
                response.Code = 67823458417393;
                response.Message = "Ocurrió un error inesperado durante la creación de la orden";

                logger.Error(IMDSerialize.Serialize(67823458417393, $"Error en {metodo}(EntRequestOrder entOrder, EntConecktaPago entConecktaPago): {ex.Message}", entOrder, piIdOrigen, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Genera un unico folio con los datos de la orden generada
        /// </summary>
        /// <param name="entFolio"></param>
        /// <param name="entPaciente"></param>
        /// <param name="item"></param>
        /// <param name="oDetalleCompra"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public IMDResponse<bool> BGenerarFolioCompra(EntFolio entFolio, EntPaciente entPaciente, EntLineItemDetail item, EntDetalleCompra oDetalleCompra, int i)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BGenerarFolioCompra);
            logger.Info(IMDSerialize.Serialize(67823458549483, $"Inicia {metodo}(EntFolio entFolio, EntPaciente entPaciente, line_items item, EntDetalleCompra oDetalleCompra, int i)", entFolio, entPaciente, item, oDetalleCompra, i));

            try
            {
                entFolio.iIdProducto = item.product_id;
                entFolio.iConsecutivo = item.consecutive;
                entFolio.bConfirmado = true;

                if (item.months_expiration == 0)
                {
                    entFolio.dtFechaVencimiento = DateTime.Now.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["iDiasDespuesVencimiento"]));
                }

                if (item.months_expiration != 0)
                {
                    entFolio.dtFechaVencimiento = DateTime.Now.AddMonths(item.months_expiration);
                }

                BusGeneratePassword busGenerate = new BusGeneratePassword();
                BusUsuario busUsuario = new BusUsuario();

                entFolio.sPassword = busGenerate.Generate(6);

                entFolio.sPassword = busUsuario.BEncodePassword(entFolio.sPassword);

                //Se agrega el folio

                IMDResponse<DataTable> dtFolio = datFolio.DSaveFolio(entFolio);

                if (dtFolio.Code != 0)
                {
                    return dtFolio.GetResponse<bool>();
                }

                foreach (DataRow item2 in dtFolio.Result.Rows)
                {
                    IMDDataRow dataRow = new IMDDataRow(item2);

                    entFolio.iIdFolio = dataRow.ConvertTo<int>("iIdFolio");
                    entFolio.sFolio = dataRow.ConvertTo<string>("sFolio");

                }
                //Se agrega el paciente
                BusPaciente busPaciente = new BusPaciente();

                entPaciente.iIdFolio = entFolio.iIdFolio;
                IMDResponse<EntPaciente> responsePaciente = busPaciente.BSavePaciente(entPaciente);

                if (responsePaciente.Code != 0)
                {
                    return response = responsePaciente.GetResponse<bool>();
                }

                EntDetalleCompraArticulo clsDetalleCompraArticulo = new EntDetalleCompraArticulo();

                clsDetalleCompraArticulo.sDescripcion = item.name;

                clsDetalleCompraArticulo.nCantidad = 1;
                clsDetalleCompraArticulo.nPrecio = item.unit_price / 100d;
                clsDetalleCompraArticulo.sFolio = entFolio.sFolio;
                clsDetalleCompraArticulo.sPass = busUsuario.BDeCodePassWord(entFolio.sPassword);
                clsDetalleCompraArticulo.dtFechaVencimiento = entFolio.dtFechaVencimiento;
                clsDetalleCompraArticulo.iIdProducto = item.product_id;
                clsDetalleCompraArticulo.iIndex = i + 1;

                oDetalleCompra.lstArticulos.Add(clsDetalleCompraArticulo);
            }
            catch (Exception ex)
            {
                response.Code = 67823458550260;
                response.Message = "Ocurrió un error inesperado al procesar los folios de compra";

                logger.Error(IMDSerialize.Serialize(67823458550260, $"Error en {metodo}(EntFolio entFolio, EntPaciente entPaciente, line_items item, EntDetalleCompra oDetalleCompra, int i): {ex.Message}", entFolio, entPaciente, item, oDetalleCompra, i, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Envia el corre de compra realizada en Conekta
        /// </summary>
        /// <param name="oDetalleCompra"></param>
        /// <returns></returns>
        public IMDResponse<bool> BEnvioCorreo(EntDetalleCompra oDetalleCompra)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEnvioCorreo);
            logger.Info(IMDSerialize.Serialize(67823458423609, $"Inicia {metodo}(EntDetalleCompra oDetalleCompra)", oDetalleCompra));

            try
            {
                BusCorreo busCorreo = new BusCorreo();

                string asunto = $"Meditoc - Detalle de compra {oDetalleCompra.sOrden}";
                string para = oDetalleCompra.sEmail;


                int indexName = oDetalleCompra.sNombre.IndexOf(" ");
                if (indexName > 0)
                {
                    oDetalleCompra.sNombre = oDetalleCompra.sNombre.Substring(0, indexName);
                }

                //ARTICULOS
                string htmlArticulos = string.Empty;
                List<EntDetalleCompraArticulo> articulosMostrar = oDetalleCompra.lstArticulos.GroupBy(x => x.iIdProducto).Select(x => new EntDetalleCompraArticulo
                {
                    iIdProducto = x.Key,
                    sDescripcion = x.Select(y => y.sDescripcion).First(),
                    nPrecio = x.Select(y => y.nPrecio).First(),
                    nCantidad = x.Count(),
                    dtFechaVencimiento = x.Select(y => y.dtFechaVencimiento).First()
                }).ToList();

                foreach (EntDetalleCompraArticulo item in articulosMostrar)
                {
                    string plantillaArticulo = " <tr class=\"product-detail font-table normal small center\"><td>item.sDescripcion</td><td>item.nCantidad</td><td>item.nPrecio</td></tr>";
                    plantillaArticulo = plantillaArticulo.Replace("item.sDescripcion", item.sDescripcion);
                    plantillaArticulo = plantillaArticulo.Replace("item.nCantidad", item.nCantidad.ToString());
                    plantillaArticulo = plantillaArticulo.Replace("item.nPrecio", (item.nPrecio * item.nCantidad).ToString("C", CultureInfo.CreateSpecificCulture("en-US")));

                    htmlArticulos += plantillaArticulo;
                }

                //DESCUENTOS
                string htmlDescuento = string.Empty;

                if (!string.IsNullOrEmpty(oDetalleCompra.sCodigoCupon))
                {
                    string plantillaDescuento = "<tr class=\"font-table bold small center\"><td>Código de descuento</td><td>oDetalleCompra.codigoDescuento</td><td>oDetalleCompra.montoDescuento</td></tr>";

                    plantillaDescuento = plantillaDescuento.Replace("oDetalleCompra.codigoDescuento", oDetalleCompra.sCodigoCupon);
                    plantillaDescuento = plantillaDescuento.Replace("oDetalleCompra.montoDescuento", $"-{oDetalleCompra.nTotalDescuento.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}");
                    htmlDescuento = plantillaDescuento;
                }

                //IVA
                double iva = Convert.ToDouble(ConfigurationManager.AppSettings["nIVA"]);
                string htmlIVA = string.Empty;
                if (iva > 0 && oDetalleCompra.bAplicaIVA)
                {
                    string plantillaIVA = "<tr class=\"font-table bold small center\"><td>Total sin IVA</td><td></td><td>oDetalleCompra.montoSinIVA</td></tr><tr class=\"font-table bold small center\"><td>oDetalleCompra.IVA</td><td></td><td>oDetalleCompra.montoIVA</td></tr>";
                    plantillaIVA = plantillaIVA.Replace("oDetalleCompra.IVA", $"IVA ({(iva * 100)}%)");
                    plantillaIVA = plantillaIVA.Replace("oDetalleCompra.montoIVA", $"+{oDetalleCompra.nTotalIVA.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}");
                    plantillaIVA = plantillaIVA.Replace("oDetalleCompra.montoSinIVA", $"{(oDetalleCompra.nTotalPagado - oDetalleCompra.nTotalIVA).ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}");
                    htmlIVA = plantillaIVA;
                }

                //FOLIOS
                string htmlFolios = string.Empty;
                foreach (EntDetalleCompraArticulo itemGroup in articulosMostrar)
                {
                    List<EntDetalleCompraArticulo> foliosMostrar = oDetalleCompra.lstArticulos.Where(x => x.iIdProducto == itemGroup.iIdProducto).ToList();
                    if (foliosMostrar.Count == 1)
                    {
                        string plantillaFolio = "<tr><td><table class=\"table-detail\"><tr class=\"group-detail font-unset bold small center\"><td colspan=\"3\">item.sDataGroup</td></tr><tr><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody><tr class=\"font-table bold small center table-border-b\"><td><small>1 -</small>&nbsp;item.sFolio</td><td>item.sPass</td></tr></tbody></table></td></tr></table></td></tr>";
                        string vigencia = string.Concat(" - Vigencia:", itemGroup.dtFechaVencimiento == null ? "" : itemGroup.dtFechaVencimiento?.ToString("dd/MM/yyyy - h:mm tt"));
                        plantillaFolio = plantillaFolio.Replace("item.sDataGroup", $"{itemGroup.sDescripcion}{vigencia}");
                        plantillaFolio = plantillaFolio.Replace("item.sFolio", foliosMostrar.First().sFolio);
                        plantillaFolio = plantillaFolio.Replace("item.sPass", foliosMostrar.First().sPass);

                        htmlFolios += plantillaFolio;
                    }
                    else if (foliosMostrar.Count > 1)
                    {
                        string plantillaFolio = "<tr class=\"font-table bold small center table-border-b\"><td><small>item.iIndex -</small>&nbsp;item.sFolio</td><td>item.sPass</td></tr>";

                        List<EntDetalleCompraArticulo> foliosIzquierda = foliosMostrar.Where(x => x.iIndex <= Math.Ceiling(foliosMostrar.Count / 2d)).ToList();
                        List<EntDetalleCompraArticulo> foliosDerecha = foliosMostrar.Where(x => x.iIndex > Math.Ceiling(foliosMostrar.Count / 2d)).ToList();

                        string htmlIzquierda = string.Empty;
                        foreach (EntDetalleCompraArticulo item in foliosIzquierda)
                        {
                            string sb = plantillaFolio.Replace("item.iIndex", item.iIndex.ToString());
                            sb = sb.Replace("item.sFolio", item.sFolio);
                            sb = sb.Replace("item.sPass", item.sPass);
                            htmlIzquierda += sb;
                        }

                        string htmlDerecha = string.Empty;
                        foreach (EntDetalleCompraArticulo item in foliosDerecha)
                        {
                            string sb = plantillaFolio.Replace("item.iIndex", item.iIndex.ToString());
                            sb = sb.Replace("item.sFolio", item.sFolio);
                            sb = sb.Replace("item.sPass", item.sPass);
                            htmlDerecha += sb;
                        }

                        string plantillaFolios = "<tr><td><table class=\"table-detail\"><tr class=\"group-detail font-unset bold small center\"><td colspan=\"3\">item.sDataGroup</td></tr><tr><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody>oDetalleCompra.FoliosIzquierda</tbody></table></td><td width=\"5%\"></td><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody>oDetalleCompra.FoliosDerecha</tbody></table></td></tr></table></td></tr>";
                        string vigencia = string.Concat(" - Vigencia:", itemGroup.dtFechaVencimiento == null ? "" : itemGroup.dtFechaVencimiento?.ToString("dd/MM/yyyy - h:mm tt"));
                        plantillaFolios = plantillaFolios.Replace("item.sDataGroup", $"{itemGroup.sDescripcion}{vigencia}");
                        plantillaFolios = plantillaFolios.Replace("oDetalleCompra.FoliosIzquierda", htmlIzquierda);
                        plantillaFolios = plantillaFolios.Replace("oDetalleCompra.FoliosDerecha", htmlDerecha);
                        htmlFolios += plantillaFolios;
                    }
                }

                string plantillaBody = "<!DOCTYPE html><html><head><meta charset=\"utf-8;\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><link href=\"https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap\" rel=\"stylesheet\" /><style>body {font-family: Roboto, \"Segoe UI\", Tahoma, Geneva, Verdana, sans-serif;margin: 0;}.center {text-align: center !important;}.right {text-align: right !important;}.left {text-align: left !important;}.light {font-weight: 300;}.normal {font-weight: normal;}.bold {font-weight: 500;}.small {font-size: 12px;}.large {font-size: 15px;}.font-default {color: #707070;}.font-primary {color: #11b6ca;}.font-secondary {color: #115c8a;}.font-unset {color: #ffffff;}.font-table {color: #878787;}.table {margin: auto;width: 100%;max-width: 800px;border: 1px solid #dddddd;border-spacing: 0px;border-collapse: 0px;}.table td {padding: 6px 0px;}.logo-head {background-color: #11b6ca;padding: 5px 0px;}.table-content {margin: auto;width: 90%;border-collapse: collapse;}.table-detail {margin: auto;width: 100%;border-collapse: collapse;}.table-detail td {padding: 8px;vertical-align: top;}.head-detail {background-color: #115c8a;}.product-detail {border-bottom: 1px solid #989898;}.total-detail {background-color: #989898;}.group-detail {background-color: #11b6ca;}.divider {height: 1px;border: 0;background-color: #989898;}.link {text-decoration: none;}.link:hover {text-decoration: underline;}.link-none {text-decoration: none;}.table-border {border-right: 10px solid #115c8a;}.table-border-l {border-left: 10px solid #115c8a;}.table-border-b td {border-bottom: 1px solid #ccc;}</style></head><body><table class=\"table\"><tr><td class=\"logo-head center\"><img alt=\"logo-meditoc\" src=\"sLogoMeditoc\" height=\"50px\" /></td></tr><tr><td><table class=\"table-content\"><tr><td class=\"center\"><span class=\"font-default bold large\"> Gracias por su compra </span></td></tr><tr><td class=\"normal large center\"><span class=\"font-default\"> oDetalleCompra.sNombre tu orden </span>&nbsp;<span class=\"font-primary\"> oDetalleCompra.sOrden </span>&nbsp;<span class=\"font-default\"> ha sido generada con éxito </span></td></tr><tr><td class=\"center\"><span class=\"font-default normal large\"> Fecha de compra: oDetalleCompra.sFechaCompra </span></td></tr><tr><td><table class=\"table-detail\"><tr class=\"head-detail font-unset bold small center\"><td>Descripción</td><td>Cantidad</td><td>Precio</td></tr>oDetalleCompra.articulo<tr class=\"font-table bold small center\"><td>Subtotal</td><td></td><td>oDetalleCompra.montoTotal</td></tr>oDetalleCompra.descuento oDetalleCompra.iva<tr class=\"total-detail font-unset bold small center\"><td>Total</td><td>oDetalleCompra.cantidadTotal</td><td>oDetalleCompra.MontoPagado</td></tr></table></td></tr><tr class=\"center\"><td><span class=\"font-default normal large\">Guarda tus credenciales, te servirán para acceder a Meditoc:</span></td></tr>oDetalleCompra.folios<tr class=\"center\"><td><p><span class=\"font-default normal large\">Para utilizar el servicio, descarga la app “Meditoc 360” disponible en Appstore y Playstore.</span></p></td></tr><tr class=\"center\"><td><span><a href=\"sLinkApple\" target=\"_blank\"class=\"link-none\"><img src=\"sLogoApple\" height=\"50px\" width=\"150px\"alt=\"APP\" /></a></span><span><a href=\"sLinkPlay\"target=\"_blank\" class=\"link-none\"><img src=\"sLogoPlay\" height=\"50px\" width=\"150px\"alt=\"PLAY\" /></a></span></td></tr><tr><td><hr class=\"divider\" /></td></tr><tr><td><span class=\"font-default light small\">Si requiere factura para su compra, por favor envíenos un correo electrónico a facturacion@meditoc.com con la siguiente información:<br /><ul><li>Asunto del correo con su número de orden (Ej. SOLICITUD DE FACTURA oDetalleCompra.sOrden)</li><li>Nombre o Razón social</li><li>RFC</li><li>Dirección fiscal</li><li>Monto total pagado</li><li>Número de orden</li></ul></span></td></tr><tr><td><span class=\"font-default light small\">De conformidad con la ley federal de protección de datos personales en posesión de los particulares, ponemos a su disposición nuestro&nbsp;<a href=\"sAvisoPrivacidad\" class=\"link font-secondary normal\"> Aviso de Privacidad </a>&nbsp;y&nbsp;<a href=\"sTerminosCondiciones\" class=\"link font-secondary normal\"> Términos y Condiciones. </a></span></td></tr><tr><td class=\"center\"><img alt=\"logo-conekta\" src=\"sLogoConekta\" height=\"40px\" /></td></tr></table></td></tr></table></body></html>";

                plantillaBody = plantillaBody.Replace("sLogoMeditoc", ConfigurationManager.AppSettings["sLogoMeditoc"]);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sNombre", oDetalleCompra.sNombre);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sOrden", oDetalleCompra.sOrden);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sFechaCompra", DateTime.Now.ToString("dd/MM/yyyy"));
                plantillaBody = plantillaBody.Replace("oDetalleCompra.articulo", htmlArticulos);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.montoTotal", oDetalleCompra.nTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
                plantillaBody = plantillaBody.Replace("oDetalleCompra.descuento", htmlDescuento);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.iva", htmlIVA);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.cantidadTotal", oDetalleCompra.lstArticulos.Sum(x => x.nCantidad).ToString());
                plantillaBody = plantillaBody.Replace("oDetalleCompra.MontoPagado", oDetalleCompra.nTotalPagado.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
                plantillaBody = plantillaBody.Replace("oDetalleCompra.folios", htmlFolios);
                plantillaBody = plantillaBody.Replace("sLinkApple", ConfigurationManager.AppSettings["sLinkApple"]);
                plantillaBody = plantillaBody.Replace("sLogoApple", ConfigurationManager.AppSettings["sLogoApple"]);
                plantillaBody = plantillaBody.Replace("sLinkPlay", ConfigurationManager.AppSettings["sLinkPlay"]);
                plantillaBody = plantillaBody.Replace("sLogoPlay", ConfigurationManager.AppSettings["sLogoPlay"]);
                plantillaBody = plantillaBody.Replace("sAvisoPrivacidad", ConfigurationManager.AppSettings["sAvisoDePrivacidad"]);
                plantillaBody = plantillaBody.Replace("sTerminosCondiciones", ConfigurationManager.AppSettings["sTerminosYCondiciones"]);
                plantillaBody = plantillaBody.Replace("sLogoConekta", ConfigurationManager.AppSettings["sLogoConekta"]);

                response = busCorreo.m_EnviarEmail("", "", "", asunto, plantillaBody, para, "", "");

                if (response.Code != 0)
                {
                    return response;
                }


                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458424386;
                response.Message = "Ocurrió un error inesperado al generar el correo de compra";

                logger.Error(IMDSerialize.Serialize(67823458424386, $"Error en {metodo}(EntDetalleCompra oDetalleCompra): {ex.Message}", oDetalleCompra, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Generar nuevos folios para la empresa solicitada
        /// </summary>
        /// <param name="entFolioxEmpresa"></param>
        /// <returns></returns>
        public IMDResponse<bool> BNuevosFoliosEmpresa(EntFolioxEmpresa entFolioxEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();
            IMDResponse<List<EntEmpresa>> lstEmpresa = new IMDResponse<List<EntEmpresa>>();
            IMDResponse<EntEmpresaDetalleFolio> oDetalleFolioempresa = new IMDResponse<EntEmpresaDetalleFolio>();
            List<EntDetalleCompraArticulo> lstArticulosDetalle = new List<EntDetalleCompraArticulo>();
            oDetalleFolioempresa.Result = new EntEmpresaDetalleFolio();

            string metodo = nameof(this.BNuevosFoliosEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458426717, $"Inicia {metodo}(EntFolioxEmpresa entFolioxEmpresa)", entFolioxEmpresa));

            try
            {
                if (entFolioxEmpresa.iIdEmpresa == 0)
                {
                    response.Code = -762384986834980;
                    response.Message = "Debe seleccionar una empresa";
                    response.Result = false;
                    return response;
                }

                //Validar y obtener los dtos de la empresa.
                BusEmpresa busEmpresa = new BusEmpresa();
                lstEmpresa = busEmpresa.BGetEmpresas(entFolioxEmpresa.iIdEmpresa);

                if (lstEmpresa.Code != 0)
                {
                    return response.GetResponse<bool>();
                }

                EntEmpresa oEmpresa = new EntEmpresa();
                oEmpresa = lstEmpresa.Result.FirstOrDefault();
                oDetalleFolioempresa.Result.entEmpresa = oEmpresa;

                //Se crea el folio
                EntFolio entFolio = new EntFolio();
                entFolio.iIdEmpresa = entFolioxEmpresa.iIdEmpresa;
                entFolio.iIdOrigen = entFolioxEmpresa.iIdOrigen;
                entFolio.bTerminosYCondiciones = false;
                entFolio.iIdUsuarioMod = entFolioxEmpresa.iIdUsuarioMod;

                //Se crea el paciente
                EntPaciente entPaciente = new EntPaciente();

                entPaciente.iIdPaciente = 0;
                entPaciente.iIdFolio = entFolio.iIdFolio;
                entPaciente.iIdUsuarioMod = entFolioxEmpresa.iIdUsuarioMod;
                //entPaciente.sTelefono = "9999999999";
                //entPaciente.sCorreo = "PPACIENTE@HOTMAIL.COM";
                //entPaciente.sNombre = "PACIENTE";

                IMDResponse<List<EntProducto>> lstProductos = busProducto.BObtenerProductos(null);

                if (lstProductos.Result == null)
                {
                    return response = lstProductos.GetResponse<bool>();
                }

                for (int i = 0; i < entFolioxEmpresa.lstLineItems.Count; i++)
                {
                    EntProducto oProducto = lstProductos.Result
                        .Find(x => x.iIdProducto == entFolioxEmpresa.lstLineItems[i].product_id);

                    if (oProducto == null)
                    {
                        response.Code = -73249876234999;
                        response.Message = "Producto no válido.";
                        return response;
                    }

                    entFolioxEmpresa.lstLineItems[i].monthsExpiration = oProducto.iMesVigencia;
                    entFolioxEmpresa.lstLineItems[i].name = oProducto.sNombre;
                    entFolioxEmpresa.lstLineItems[i].unit_price = (int)(oProducto.fCosto * 100);
                }

                //using (TransactionScope scope = new TransactionScope())
                //{
                this.BSaveOrderEmpresa(entFolioxEmpresa, oEmpresa);

                entFolio.sOrdenConekta = entFolioxEmpresa.uid;

                foreach (line_items item in entFolioxEmpresa.lstLineItems)
                {
                    for (int i = 0; i < item.quantity; i++)
                    {
                        IMDResponse<bool> resGenerarFolio = this.BGenerarFolioEmpresa(entFolio, entPaciente, item, lstArticulosDetalle, i);
                        if (resGenerarFolio.Code != 0)
                        {
                            return resGenerarFolio;
                        }
                    }
                }

                //    scope.Complete();
                //}
                oDetalleFolioempresa.Result.lstArticulos = lstArticulosDetalle;
                response = this.BEnvioCorreoEmpresa(oDetalleFolioempresa.Result);

                if (response.Code != 0)
                {
                    response.Message = "Ocurrio un error al enviar el correo";
                    return response;
                }

                response.Code = 0;
                response.Message = "Los folios fueron enviados al siguiente correo " + oDetalleFolioempresa.Result.entEmpresa.sCorreo;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458427494;
                response.Message = "Ocurrió un error inesperado al procesar los folios de la compra";

                logger.Error(IMDSerialize.Serialize(67823458427494, $"Error en {metodo}(EntFolioxEmpresa entFolioxEmpresa): {ex.Message}", entFolioxEmpresa, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BSaveOrderEmpresa(EntFolioxEmpresa entFolioxEmpresa, EntEmpresa entEmpresa, Guid? puid = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveOrderEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458613197, $"Inicia {metodo}(EntFolioxEmpresa entFolioxEmpresa, EntEmpresa entEmpresa)", entFolioxEmpresa, entEmpresa));

            try
            {
                Guid uid = puid == null ? Guid.NewGuid() : (Guid)puid;
                string sUid = uid.ToString();
                string origin = "meditoc_enterprise";

                double subtotal = entFolioxEmpresa.lstLineItems.Sum(x => x.unit_price * x.quantity);
                double taxIVA = Convert.ToDouble(ConfigurationManager.AppSettings["CONEKTA_IMPUESTO"]);
                double iva = subtotal * taxIVA;

                long created = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                List<EntLineItemDetail> entLineItemDetails = new List<EntLineItemDetail>();

                int consecutive = 0;

                for (int i = 0; i < entFolioxEmpresa.lstLineItems.Count; i++)
                {
                    EntLineItemDetail entLineItemDetail = new EntLineItemDetail
                    {
                        consecutive = ++consecutive,
                        name = entFolioxEmpresa.lstLineItems[i].name,
                        quantity = entFolioxEmpresa.lstLineItems[i].quantity,
                        unit_price = entFolioxEmpresa.lstLineItems[i].unit_price,
                        id = Guid.NewGuid().ToString()
                    };
                    entFolioxEmpresa.lstLineItems[i].consecutive = consecutive;
                    entLineItemDetails.Add(entLineItemDetail);
                }

                EntOrder entOrder = new EntOrder
                {
                    amount = (long)subtotal,
                    amount_paid = (long)(subtotal + iva),
                    amount_tax = (long)iva,
                    charges = new EntCharge
                    {
                        data = new List<EntChargeDetail>
                        {
                            new EntChargeDetail
                            {
                                amount = (long)(subtotal + iva),
                                created_at = created,
                                currency = "MXN",
                                status = "paid",
                                payment_method = new EntPaymentMehod
                                {
                                    country="MX",
                                    type="meditoc_enterprise",
                                    name = entEmpresa.sNombre,
                                    account_type = entEmpresa.sFolioEmpresa
                                },
                                id = sUid
                            }
                        }
                    },
                    created_at = created,
                    updated_at = created,
                    currency = "MXN",
                    payment_status = "paid",
                    customer_info = new EntCustomerInfo
                    {
                        email = entEmpresa.sCorreo,
                        name = entEmpresa.sNombre,
                        phone = entEmpresa.sFolioEmpresa
                    },
                    line_items = new EntLineItem
                    {
                        data = entLineItemDetails
                    },
                    id = sUid
                };

                entFolioxEmpresa.uid = sUid;

                DatOrder datOrder = new DatOrder();

                IMDResponse<bool> respuestaGuardarOrden = datOrder.DSaveConektaOrder(uid, entOrder, origin, entFolioxEmpresa.iIdOrigen);
                if (respuestaGuardarOrden.Code != 0)
                {
                    return respuestaGuardarOrden;
                }

                IMDResponse<bool> respuestaGuardarCliente = datOrder.DSaveCustomerInfo(uid, entOrder.customer_info);
                if (respuestaGuardarCliente.Code != 0)
                {
                    return respuestaGuardarCliente;
                }

                for (int i = 0; i < entOrder.line_items.data.Count; i++)
                {
                    IMDResponse<bool> respuestaGuardarArticulo = datOrder.DSaveLineItem(uid, entOrder.line_items.data[i]);
                    if (respuestaGuardarArticulo.Code != 0)
                    {
                        return respuestaGuardarArticulo;
                    }
                }

                IMDResponse<bool> respuestaGuardarDatosPago = datOrder.DSaveCharge(uid, entOrder.charges.data.First(), origin);
                if (respuestaGuardarDatosPago.Code != 0)
                {
                    return respuestaGuardarOrden;
                }

                response.Code = 0;
                response.Result = true;
                response.Message = "Guardado correcto";
            }
            catch (Exception ex)
            {
                response.Code = 67823458613974;
                response.Message = "Ocurrió un error inesperado al guardar la orden de la empresa.";

                logger.Error(IMDSerialize.Serialize(67823458613974, $"Error en {metodo}(EntFolioxEmpresa entFolioxEmpresa, EntEmpresa entEmpresa): {ex.Message}", entFolioxEmpresa, entEmpresa, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Genera un unico folio con los datos de la empresa y paciente
        /// </summary>
        /// <param name="entFolio"></param>
        /// <param name="entPaciente"></param>
        /// <param name="item"></param>
        /// <param name="lstArticulosDetalle"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public IMDResponse<bool> BGenerarFolioEmpresa(EntFolio entFolio, EntPaciente entPaciente, line_items item, List<EntDetalleCompraArticulo> lstArticulosDetalle, int i)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BGenerarFolioEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458546375, $"Inicia {metodo}(EntFolio entFolio, EntPaciente entPaciente, line_items item, List<EntDetalleCompraArticulo> lstArticulosDetalle, int i)", entFolio, entPaciente, item, lstArticulosDetalle, i));

            try
            {
                entFolio.iIdProducto = item.product_id;
                entFolio.iConsecutivo = item.consecutive;

                if (Enum.IsDefined(typeof(EnumProductos), entFolio.iIdProducto))
                {
                    entFolio.dtFechaVencimiento = null;
                    if (entFolio.iIdProducto == (int)EnumProductos.OrientacionEspecialistaID)
                    {
                        entFolio.bConfirmado = true;
                    }
                    else
                    {
                        entFolio.bConfirmado = false;
                    }
                }
                else
                {
                    if (item.monthsExpiration == 0)
                    {
                        entFolio.dtFechaVencimiento = DateTime.Now.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["iDiasDespuesVencimiento"]));
                    }

                    if (item.monthsExpiration != 0)
                    {
                        entFolio.dtFechaVencimiento = DateTime.Now.AddMonths(item.monthsExpiration);
                    }
                    entFolio.bConfirmado = true;
                }

                BusGeneratePassword busGenerate = new BusGeneratePassword();
                BusUsuario busUsuario = new BusUsuario();

                entFolio.sPassword = busGenerate.Generate(6);

                entFolio.sPassword = busUsuario.BEncodePassword(entFolio.sPassword);

                //Se agrega el folio

                IMDResponse<DataTable> dtFolio = datFolio.DSaveFolio(entFolio);

                if (dtFolio.Code != 0)
                {
                    response = dtFolio.GetResponse<bool>();
                    response.Message = "Ocurrio un error al guardar el folio";
                    return response;
                }

                foreach (DataRow item2 in dtFolio.Result.Rows)
                {
                    IMDDataRow dataRow = new IMDDataRow(item2);

                    entFolio.iIdFolio = dataRow.ConvertTo<int>("iIdFolio");
                    entFolio.sFolio = dataRow.ConvertTo<string>("sFolio");

                }
                //Se agrega el paciente
                BusPaciente busPaciente = new BusPaciente();

                entPaciente.iIdFolio = entFolio.iIdFolio;
                IMDResponse<EntPaciente> responsePaciente = busPaciente.BSavePaciente(entPaciente);

                if (responsePaciente.Code != 0)
                {
                    return response = responsePaciente.GetResponse<bool>();
                }

                EntDetalleCompraArticulo clsDetalleCompraArticulo = new EntDetalleCompraArticulo();

                clsDetalleCompraArticulo.sDescripcion = item.name;

                clsDetalleCompraArticulo.sFolio = entFolio.sFolio;
                clsDetalleCompraArticulo.sPass = busUsuario.BDeCodePassWord(entFolio.sPassword);
                clsDetalleCompraArticulo.dtFechaVencimiento = entFolio.dtFechaVencimiento;
                clsDetalleCompraArticulo.iIdProducto = item.product_id;
                clsDetalleCompraArticulo.iIndex = i + 1;

                lstArticulosDetalle.Add(clsDetalleCompraArticulo);
            }
            catch (Exception ex)
            {
                response.Code = 67823458547152;
                response.Message = "Ocurrió un error inesperado al generar los folios de la empresa";

                logger.Error(IMDSerialize.Serialize(67823458547152, $"Error en {metodo}(EntFolio entFolio, EntPaciente entPaciente, line_items item, List<EntDetalleCompraArticulo> lstArticulosDetalle, int i): {ex.Message}", entFolio, entPaciente, item, lstArticulosDetalle, i, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Envia el correo de los folios generados a la empresa que los solicito
        /// </summary>
        /// <param name="detalleFolio"></param>
        /// <returns></returns>
        public IMDResponse<bool> BEnvioCorreoEmpresa(EntEmpresaDetalleFolio detalleFolio)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEnvioCorreo);
            logger.Info(IMDSerialize.Serialize(67823458423609, $"Inicia {metodo}(EntEmpresaDetalleFolio detalleFolio)", detalleFolio));

            try
            {
                BusCorreo busCorreo = new BusCorreo();

                string asunto = $"Meditoc - Detalle de folios";
                string para = detalleFolio.entEmpresa.sCorreo;

                //ARTICULOS                
                List<EntDetalleCompraArticulo> articulosMostrar = detalleFolio.
                    lstArticulos.
                    GroupBy(x => x.iIdProducto).
                    Select(x => new EntDetalleCompraArticulo
                    {
                        iIdProducto = x.Key,
                        sDescripcion = x.Select(y => y.sDescripcion).First(),
                        nPrecio = x.Select(y => y.nPrecio).First(),
                        nCantidad = x.Count(),
                        dtFechaVencimiento = x.Select(y => y.dtFechaVencimiento).First()
                    }).ToList();

                //FOLIOS
                string htmlFolios = string.Empty;
                foreach (EntDetalleCompraArticulo itemGroup in articulosMostrar)
                {
                    List<EntDetalleCompraArticulo> foliosMostrar = detalleFolio.lstArticulos.Where(x => x.iIdProducto == itemGroup.iIdProducto).ToList();
                    if (foliosMostrar.Count == 1)
                    {
                        string plantillaFolio = "<tr><td><table class=\"table-detail\"><tr class=\"group-detail font-unset bold small center\"><td colspan=\"3\">item.sDataGroup</td></tr><tr><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody><tr class=\"font-table bold small center table-border-b\"><td><small>1 -</small>&nbsp;item.sFolio</td><td>item.sPass</td></tr></tbody></table></td></tr></table></td></tr>";
                        string vigencia = string.Concat(" - Vigencia:", itemGroup.dtFechaVencimiento == null ? "" : itemGroup.dtFechaVencimiento?.ToString("dd/MM/yyyy - h:mm tt"));
                        plantillaFolio = plantillaFolio.Replace("item.sDataGroup", $"{itemGroup.sDescripcion}{vigencia}");
                        plantillaFolio = plantillaFolio.Replace("item.sFolio", foliosMostrar.First().sFolio);
                        plantillaFolio = plantillaFolio.Replace("item.sPass", foliosMostrar.First().sPass);

                        htmlFolios += plantillaFolio;
                    }
                    else if (foliosMostrar.Count > 1)
                    {
                        string plantillaFolio = "<tr class=\"font-table bold small center table-border-b\"><td><small>item.iIndex -</small>&nbsp;item.sFolio</td><td>item.sPass</td></tr>";

                        List<EntDetalleCompraArticulo> foliosIzquierda = foliosMostrar.Where(x => x.iIndex <= Math.Ceiling(foliosMostrar.Count / 2d)).ToList();
                        List<EntDetalleCompraArticulo> foliosDerecha = foliosMostrar.Where(x => x.iIndex > Math.Ceiling(foliosMostrar.Count / 2d)).ToList();

                        string htmlIzquierda = string.Empty;
                        foreach (EntDetalleCompraArticulo item in foliosIzquierda)
                        {
                            string sb = plantillaFolio.Replace("item.iIndex", item.iIndex.ToString());
                            sb = sb.Replace("item.sFolio", item.sFolio);
                            sb = sb.Replace("item.sPass", item.sPass);
                            htmlIzquierda += sb;
                        }

                        string htmlDerecha = string.Empty;
                        foreach (EntDetalleCompraArticulo item in foliosDerecha)
                        {
                            string sb = plantillaFolio.Replace("item.iIndex", item.iIndex.ToString());
                            sb = sb.Replace("item.sFolio", item.sFolio);
                            sb = sb.Replace("item.sPass", item.sPass);
                            htmlDerecha += sb;
                        }

                        string plantillaFolios = "<tr><td><table class=\"table-detail\"><tr class=\"group-detail font-unset bold small center\"><td colspan=\"3\">item.sDataGroup</td></tr><tr><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody>oDetalleCompra.FoliosIzquierda</tbody></table></td><td width=\"5%\"></td><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody>oDetalleCompra.FoliosDerecha</tbody></table></td></tr></table></td></tr>";
                        string vigencia = string.Concat(" - Vigencia:", itemGroup.dtFechaVencimiento == null ? "" : itemGroup.dtFechaVencimiento?.ToString("dd/MM/yyyy - h:mm tt"));
                        plantillaFolios = plantillaFolios.Replace("item.sDataGroup", $"{itemGroup.sDescripcion}{vigencia}");
                        plantillaFolios = plantillaFolios.Replace("oDetalleCompra.FoliosIzquierda", htmlIzquierda);
                        plantillaFolios = plantillaFolios.Replace("oDetalleCompra.FoliosDerecha", htmlDerecha);
                        htmlFolios += plantillaFolios;
                    }
                }

                string plantillaBody = "<!DOCTYPE html><html><head><meta charset=\"utf-8;\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><link href=\"https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap\" rel=\"stylesheet\" /><style>body {font-family: Roboto, \"Segoe UI\", Tahoma, Geneva, Verdana, sans-serif;margin: 0;}.center {text-align: center !important;}.right {text-align: right !important;}.left {text-align: left !important;}.light {font-weight: 300;}.normal {font-weight: normal;}.bold {font-weight: 500;}.small {font-size: 12px;}.large {font-size: 15px;}.font-default {color: #707070;}.font-primary {color: #11b6ca;}.font-secondary {color: #115c8a;}.font-unset {color: #ffffff;}.font-table {color: #878787;}.table {margin: auto;width: 100%;max-width: 800px;border: 1px solid #dddddd;border-spacing: 0px;border-collapse: 0px;}.table td {padding: 6px 0px;}.logo-head {background-color: #11b6ca;padding: 5px 0px;}.table-content {margin: auto;width: 90%;border-collapse: collapse;}.table-detail {margin: auto;width: 100%;border-collapse: collapse;}.table-detail td {padding: 8px;vertical-align: top;}.head-detail {background-color: #115c8a;}.product-detail {border-bottom: 1px solid #989898;}.total-detail {background-color: #989898;}.group-detail {background-color: #11b6ca;}.divider {height: 1px;border: 0;background-color: #989898;}.link {text-decoration: none;}.link:hover {text-decoration: underline;}.link-none {text-decoration: none;}.table-border {border-right: 10px solid #115c8a;}.table-border-l {border-left: 10px solid #115c8a;}.table-border-b td {border-bottom: 1px solid #ccc;}</style></head><body><table class=\"table\"><tr><td class=\"logo-head center\"><img alt=\"logo-meditoc\" src=\"sLogoMeditoc\" height=\"50px\" /></td></tr><tr><td><table class=\"table-content\"><tr><td class=\"center\"><span class=\"font-default bold large\"> Gracias por su compra </span></td></tr><tr><td class=\"center\"><span class=\"font-default normal large\"> Folio de empresa: oDetalleCompra.sFolioEmpresa </span></td></tr><tr><td class=\"center\"><span class=\"font-default normal large\"> Fecha de compra: oDetalleCompra.sFechaCompra </span></td></tr><tr class=\"center\"><td><span class=\"font-default normal large\">Guarda tus credenciales, te servirán para acceder a Meditoc:</span></td></tr>oDetalleCompra.folios<tr class=\"center\"><td><p><span class=\"font-default normal large\">Para utilizar el servicio, descarga la app “Meditoc 360” disponible en Appstore y Playstore.</span></p></td></tr><tr class=\"center\"><td><span><a href=\"sLinkApple\" target=\"_blank\"class=\"link-none\"><img src=\"sLogoApple\" height=\"50px\" width=\"150px\"alt=\"APP\" /></a></span><span><a href=\"sLinkPlay\"target=\"_blank\" class=\"link-none\"><img src=\"sLogoPlay\" height=\"50px\" width=\"150px\"alt=\"PLAY\" /></a></span></td></tr><tr><td><hr class=\"divider\" /></td></tr><tr><td><span class=\"font-default light small\">De conformidad con la ley federal de protección de datos personales en posesión de los particulares, ponemos a su disposición nuestro&nbsp;<a href=\"sAvisoPrivacidad\" class=\"link font-secondary normal\"> Aviso de Privacidad </a>&nbsp;y&nbsp;<a href=\"sTerminosCondiciones\" class=\"link font-secondary normal\"> Términos y Condiciones. </a></span></td></tr></table></td></tr></table></body></html>";

                plantillaBody = plantillaBody.Replace("sLogoMeditoc", ConfigurationManager.AppSettings["sLogoMeditoc"]);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sNombre", detalleFolio.entEmpresa.sNombre);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sFolioEmpresa", detalleFolio.entEmpresa.sFolioEmpresa);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sFechaCompra", DateTime.Now.ToString("dd/MM/yyyy"));
                plantillaBody = plantillaBody.Replace("oDetalleCompra.folios", htmlFolios);
                plantillaBody = plantillaBody.Replace("sLinkApple", ConfigurationManager.AppSettings["sLinkApple"]);
                plantillaBody = plantillaBody.Replace("sLogoApple", ConfigurationManager.AppSettings["sLogoApple"]);
                plantillaBody = plantillaBody.Replace("sLinkPlay", ConfigurationManager.AppSettings["sLinkPlay"]);
                plantillaBody = plantillaBody.Replace("sLogoPlay", ConfigurationManager.AppSettings["sLogoPlay"]);
                plantillaBody = plantillaBody.Replace("sAvisoPrivacidad", ConfigurationManager.AppSettings["sAvisoDePrivacidad"]);
                plantillaBody = plantillaBody.Replace("sTerminosCondiciones", ConfigurationManager.AppSettings["sTerminosYCondiciones"]);
                plantillaBody = plantillaBody.Replace("sLogoConekta", ConfigurationManager.AppSettings["sLogoConekta"]);

                response = busCorreo.m_EnviarEmail("", "", "", asunto, plantillaBody, para, "", "");

                if (response.Code != 0)
                {
                    return response;
                }


                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458424386;
                response.Message = "Ocurrió un error inesperado al generar el correo de compra";

                logger.Error(IMDSerialize.Serialize(67823458424386, $"Error en {metodo}(EntEmpresaDetalleFolio detalleFolio): {ex.Message}", detalleFolio, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Genera un unico folio para una consulta programada del paciente
        /// </summary>
        /// <param name="entNuevaConsulta"></param>
        /// <returns></returns>
        public IMDResponse<EntDetalleCompra> BNuevaConsulta(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BNuevaConsulta);
            logger.Info(IMDSerialize.Serialize(67823458533943, $"Inicia {metodo}(EntNuevaConsulta entNuevaConsulta)", entNuevaConsulta));

            try
            {

                if (entNuevaConsulta.consulta.iIdColaborador == null | entNuevaConsulta.consulta.iIdColaborador <= 0)
                {
                    response.Code = -23477767234;
                    response.Message = "No se ingresaron datos del colaborador";
                    return response;
                }

                if (entNuevaConsulta == null || entNuevaConsulta?.consulta == null || entNuevaConsulta?.customerInfo == null)
                {
                    response.Code = -34234687877345;
                    response.Message = "No se ingresaron datos";
                    return response;
                }

                if (entNuevaConsulta?.consulta?.dtFechaProgramadaInicio == null || entNuevaConsulta?.consulta?.dtFechaProgramadaFin == null)
                {
                    response.Code = -92647582876547;
                    response.Message = "No se ingresaron las fechas de consulta";
                    return response;
                }
                if (entNuevaConsulta?.consulta?.dtFechaProgramadaInicio >= entNuevaConsulta?.consulta?.dtFechaProgramadaFin)
                {
                    response.Code = -3462867587263;
                    response.Message = "La fecha de fin no puede ser menor a la fecha de inicio";
                    return response;
                }

                string inicioString = entNuevaConsulta?.consulta?.dtFechaProgramadaInicio?.ToString("dd/MM/yyyy HH:mm");
                string finString = entNuevaConsulta?.consulta?.dtFechaProgramadaFin?.ToString("dd/MM/yyyy HH:mm");

                entNuevaConsulta.consulta.dtFechaProgramadaInicio = Convert.ToDateTime(inicioString);
                entNuevaConsulta.consulta.dtFechaProgramadaFin = Convert.ToDateTime(finString);

                DateTime? buscadorConsultaProgramadaInicio = entNuevaConsulta?.consulta?.dtFechaProgramadaInicio?.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["iMinToleraciaConsultaInicio"]) * -1);
                DateTime? buscadorConsultaProgramadaFin = entNuevaConsulta?.consulta?.dtFechaProgramadaFin?.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["iMinToleraciaConsultaFin"]));

                IMDResponse<List<EntDetalleConsulta>> resGetConsultas = busConsulta.BGetDisponibilidadConsulta((int)entNuevaConsulta.consulta.iIdColaborador, entNuevaConsulta.consulta.iIdConsulta, buscadorConsultaProgramadaInicio, buscadorConsultaProgramadaFin);
                if (resGetConsultas.Code != 0)
                {
                    return resGetConsultas.GetResponse<EntDetalleCompra>();
                }

                if (resGetConsultas.Result.Count != 0)
                {
                    bool existeConflictoAgenda = false;
                    resGetConsultas.Result.ForEach(consulta =>
                    {
                        if (consulta.iIdEstatusConsulta != (int)EnumEstatusConsulta.Cancelado && consulta.iIdEstatusConsulta != (int)EnumEstatusConsulta.Finalizado)
                        {
                            response.Code = -2346727776123;
                            response.Message = $"Ya hay una consulta programada de {consulta.dtFechaProgramadaInicio?.ToString("h:mm tt")} a {consulta.dtFechaProgramadaFin?.ToString("h:mm tt")} para el folio {consulta.sFolio}. El tiempo de espera entre consultas es de {ConfigurationManager.AppSettings["iMinToleraciaConsultaFin"]} min";
                            existeConflictoAgenda = true;
                        }
                    });

                    if (existeConflictoAgenda)
                    {
                        return response;
                    }
                }

                if (entNuevaConsulta.consulta.iIdConsulta == 0)
                {
                    if (string.IsNullOrWhiteSpace(entNuevaConsulta.sFolio))
                    {
                        response = this.BGenerarConsultaSinFolio(entNuevaConsulta);
                    }
                    else
                    {
                        response = this.BGenerarConsultaConFolio(entNuevaConsulta);
                    }
                }
                else
                {
                    response = this.BModificarConsultaFolio(entNuevaConsulta);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458534720;
                response.Message = "Ocurrió un error inesperado al generar la consulta del paciente";

                logger.Error(IMDSerialize.Serialize(67823458534720, $"Error en {metodo}(EntNuevaConsulta entNuevaConsulta): {ex.Message}", entNuevaConsulta, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Genera una consulta con los datos solicitados cuando existe un folio
        /// </summary>
        /// <param name="entNuevaConsulta"></param>
        /// <returns></returns>
        public IMDResponse<EntDetalleCompra> BGenerarConsultaConFolio(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BGenerarConsultaConFolio);
            logger.Info(IMDSerialize.Serialize(67823458541713, $"Inicia {metodo}(EntNuevaConsulta entNuevaConsulta)", entNuevaConsulta));

            try
            {
                IMDResponse<List<EntFolioReporte>> resGetFolioExiste = this.BGetFolios(psFolio: entNuevaConsulta.sFolio, pbActivo: null, pbBaja: null);
                if (resGetFolioExiste.Code != 0)
                {
                    return resGetFolioExiste.GetResponse<EntDetalleCompra>();
                }

                if (resGetFolioExiste.Result.Count != 1)
                {
                    response.Code = -374672348767234;
                    response.Message = "El folio proporcionado no existe";
                    return response;
                }

                EntFolioReporte folioExistente = resGetFolioExiste.Result.First();

                EntUpdPaciente entPaciente = new EntUpdPaciente
                {
                    iIdPaciente = folioExistente.iIdPaciente,
                    sCorreo = entNuevaConsulta.customerInfo.email,
                    sNombre = entNuevaConsulta.customerInfo.name,
                    sTelefono = entNuevaConsulta.customerInfo.phone,
                    iIdUsuarioMod = entNuevaConsulta.iIdUsuarioMod
                };

                BusPaciente busPaciente = new BusPaciente();
                IMDResponse<bool> resUpdPaciente = busPaciente.BUpdPaciente(entPaciente);
                if (resUpdPaciente.Code != 0)
                {
                    return resUpdPaciente.GetResponse<EntDetalleCompra>();
                }

                if (folioExistente.iIdOrigen == (int)EnumOrigen.Particular)
                {
                    EntFolioFV entFolioFV = new EntFolioFV
                    {
                        iIdEmpresa = folioExistente.iIdEmpresa,
                        iIdUsuario = entNuevaConsulta.iIdUsuarioMod,
                        lstFolios = new List<EntFolioFVItem>
                        {
                            new EntFolioFVItem
                            {
                                iIdFolio = folioExistente.iIdFolio
                            }
                        }
                    };

                    entFolioFV.dtFechaVencimiento = (DateTime)entNuevaConsulta?.consulta?.dtFechaProgramadaFin;

                    IMDResponse<bool> resUpdVencimiento = this.BUpdFechaVencimiento(entFolioFV);
                    if (resUpdVencimiento.Code != 0)
                    {
                        return resUpdVencimiento.GetResponse<EntDetalleCompra>();
                    }
                }

                entNuevaConsulta.consulta.iIdEstatusConsulta = (int)EnumEstatusConsulta.CreadoProgramado;
                entNuevaConsulta.consulta.iIdPaciente = folioExistente.iIdPaciente;
                entNuevaConsulta.customerInfo.email = folioExistente.sCorreoPaciente;

                response = this.BConfirmarNuevaConsulta(entNuevaConsulta, folioExistente.sFolio, folioExistente.sPassword);
            }
            catch (Exception ex)
            {
                response.Code = 67823458542490;
                response.Message = "Ocurrió un error inesperado al generar el folio del paciente";

                logger.Error(IMDSerialize.Serialize(67823458542490, $"Error en {metodo}(EntNuevaConsulta entNuevaConsulta): {ex.Message}", entNuevaConsulta, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Genera una consulta con los datos solicitados cuando no existe un folio
        /// </summary>
        /// <param name="entNuevaConsulta"></param>
        /// <returns></returns>
        public IMDResponse<EntDetalleCompra> BGenerarConsultaSinFolio(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BGenerarConsultaSinFolio);
            logger.Info(IMDSerialize.Serialize(67823458543267, $"Inicia {metodo}(EntNuevaConsulta entNuevaConsulta)", entNuevaConsulta));

            try
            {
                EntEmpresa entEmpresa = new EntEmpresa();

                BusEmpresa busEmpresa = new BusEmpresa();
                IMDResponse<List<EntEmpresa>> respuestaObtenerEmpresas = busEmpresa.BGetEmpresas(null, entNuevaConsulta.customerInfo.email);

                if (respuestaObtenerEmpresas.Code != 0)
                {
                    return respuestaObtenerEmpresas.GetResponse<EntDetalleCompra>();
                }

                if (respuestaObtenerEmpresas.Result.Count > 0)
                {
                    entEmpresa = respuestaObtenerEmpresas.Result.FirstOrDefault();
                }
                else
                {
                    EntEmpresa entNuevaEmpresa = new EntEmpresa()
                    {
                        iIdEmpresa = 0,
                        sNombre = entNuevaConsulta.customerInfo.name,
                        sCorreo = entNuevaConsulta.customerInfo.email,
                        iIdUsuarioMod = entNuevaConsulta.iIdUsuarioMod,
                        bActivo = true,
                        bBaja = false
                    };

                    IMDResponse<EntEmpresa> respuestaSaveEmpresa = busEmpresa.BSaveEmpresa(entNuevaEmpresa);
                    if (respuestaSaveEmpresa.Code != 0)
                    {
                        return respuestaSaveEmpresa.GetResponse<EntDetalleCompra>();
                    }
                    entEmpresa = respuestaSaveEmpresa.Result;
                }

                EntFolio entFolio = new EntFolio();
                entFolio.iIdOrigen = (int)EnumOrigen.Particular;
                entFolio.bTerminosYCondiciones = false;
                entFolio.iIdEmpresa = entEmpresa.iIdEmpresa;

                EntPaciente entPaciente = new EntPaciente();

                entPaciente.iIdPaciente = 0;
                entPaciente.iIdFolio = entFolio.iIdFolio;
                entPaciente.sTelefono = entNuevaConsulta.customerInfo.phone;
                entPaciente.sCorreo = entNuevaConsulta.customerInfo.email;
                entPaciente.sNombre = entNuevaConsulta.customerInfo.name;
                entPaciente.iIdUsuarioMod = entNuevaConsulta.iIdUsuarioMod;

                entFolio.iIdProducto = (int)EnumProductos.OrientacionEspecialistaID;
                entFolio.dtFechaVencimiento = (DateTime)entNuevaConsulta.consulta.dtFechaProgramadaFin;

                BusGeneratePassword busGenerate = new BusGeneratePassword();

                entFolio.sPassword = busGenerate.Generate(6);
                entFolio.sPassword = busUsuario.BEncodePassword(entFolio.sPassword);
                entFolio.iIdUsuarioMod = entNuevaConsulta.iIdUsuarioMod;

                IMDResponse<DataTable> dtFolio = datFolio.DSaveFolio(entFolio);

                if (dtFolio.Code != 0)
                {
                    response = dtFolio.GetResponse<EntDetalleCompra>();
                    response.Message = "Ocurrio un error al guardar el folio";
                    return response;
                }

                foreach (DataRow item2 in dtFolio.Result.Rows)
                {
                    IMDDataRow dataRow = new IMDDataRow(item2);

                    entFolio.iIdFolio = dataRow.ConvertTo<int>("iIdFolio");
                    entFolio.sFolio = dataRow.ConvertTo<string>("sFolio");

                }
                //Se agrega el paciente
                BusPaciente busPaciente = new BusPaciente();

                entPaciente.iIdFolio = entFolio.iIdFolio;
                IMDResponse<EntPaciente> responsePaciente = busPaciente.BSavePaciente(entPaciente);

                if (responsePaciente.Code != 0)
                {
                    return response = responsePaciente.GetResponse<EntDetalleCompra>();
                }

                entNuevaConsulta.consulta.iIdEstatusConsulta = (int)EnumEstatusConsulta.CreadoProgramado;
                entNuevaConsulta.consulta.iIdPaciente = responsePaciente.Result.iIdPaciente;

                response = this.BConfirmarNuevaConsulta(entNuevaConsulta, entFolio.sFolio, entFolio.sPassword);
            }
            catch (Exception ex)
            {
                response.Code = 67823458544044;
                response.Message = "Ocurrió un error inesperado al generar la consulta del paciente";

                logger.Error(IMDSerialize.Serialize(67823458544044, $"Error en {metodo}(EntNuevaConsulta entNuevaConsulta): {ex.Message}", entNuevaConsulta, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Edita los datos de una consulta con los datos solicitados
        /// </summary>
        /// <param name="entModificarConsulta"></param>
        /// <returns></returns>
        public IMDResponse<EntDetalleCompra> BModificarConsultaFolio(EntNuevaConsulta entModificarConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BModificarConsultaFolio);
            logger.Info(IMDSerialize.Serialize(67823458544821, $"Inicia {metodo}(EntNuevaConsulta entModificarConsulta)", entModificarConsulta));

            try
            {
                IMDResponse<List<EntDetalleConsulta>> resGetConsulta = busConsulta.BGetDetalleConsulta(entModificarConsulta.consulta.iIdConsulta);
                if (resGetConsulta.Code != 0)
                {
                    return response = resGetConsulta.GetResponse<EntDetalleCompra>();
                }

                if (resGetConsulta.Result.Count != 1)
                {
                    response.Code = -3452374677623478;
                    response.Message = "La consulta no existe";
                    return response;
                }

                EntDetalleConsulta consulta = resGetConsulta.Result.First();

                if (consulta.iIdOrigen == (int)EnumOrigen.Particular)
                {
                    EntFolioFV entFolioFV = new EntFolioFV
                    {
                        iIdEmpresa = (int)consulta.iIdEmpresa,
                        iIdUsuario = entModificarConsulta.iIdUsuarioMod,
                        lstFolios = new List<EntFolioFVItem>
                        {
                            new EntFolioFVItem
                            {
                                iIdFolio = (int)consulta.iIdFolio
                            }
                        }
                    };

                    entFolioFV.dtFechaVencimiento = (DateTime)entModificarConsulta?.consulta?.dtFechaProgramadaFin;
                    IMDResponse<bool> resUpdVencimiento = this.BUpdFechaVencimiento(entFolioFV);
                    if (resUpdVencimiento.Code != 0)
                    {
                        return resUpdVencimiento.GetResponse<EntDetalleCompra>();
                    }
                }

                entModificarConsulta.consulta.iIdEstatusConsulta = (int)EnumEstatusConsulta.Reprogramado;
                entModificarConsulta.consulta.iIdPaciente = consulta.iIdPaciente;
                entModificarConsulta.customerInfo.email = consulta.sCorreoPaciente;

                response = this.BConfirmarNuevaConsulta(entModificarConsulta, consulta.sFolio, consulta.sPassword, true);
            }
            catch (Exception ex)
            {
                response.Code = 67823458545598;
                response.Message = "Ocurrió un error inesperado al reprogramar la consulta del paciente";

                logger.Error(IMDSerialize.Serialize(67823458545598, $"Error en {metodo}(EntNuevaConsulta entModificarConsulta): {ex.Message}", entModificarConsulta, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Confirma la generacion de una consulta
        /// </summary>
        /// <param name="entNuevaConsulta"></param>
        /// <param name="FolioEnviar"></param>
        /// <param name="PassEnviar"></param>
        /// <param name="reprogramado"></param>
        /// <returns></returns>
        public IMDResponse<EntDetalleCompra> BConfirmarNuevaConsulta(EntNuevaConsulta entNuevaConsulta, string FolioEnviar, string PassEnviar, bool reprogramado = false)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BConfirmarNuevaConsulta);
            logger.Info(IMDSerialize.Serialize(67823458540159, $"Inicia {metodo}(EntNuevaConsulta entNuevaConsulta, string FolioEnviar, string PassEnviar, bool reprogramado = false)", entNuevaConsulta, FolioEnviar, PassEnviar, reprogramado));

            try
            {
                EntDetalleCompra entDetalleCompra = new EntDetalleCompra
                {
                    sNombre = entNuevaConsulta.customerInfo.name,
                    sEmail = entNuevaConsulta.customerInfo.email,
                    sTelefono = entNuevaConsulta.customerInfo.phone,
                    lstArticulos = new List<EntDetalleCompraArticulo>
                    {
                        new EntDetalleCompraArticulo
                        {
                            sDescripcion = "Orientación Médica Programada",
                            sFolio = FolioEnviar,
                            sPass = busUsuario.BDeCodePassWord(PassEnviar),
                            dtFechaVencimiento = (DateTime)entNuevaConsulta.consulta.dtFechaProgramadaInicio
                        }
                    }
                };

                IMDResponse<EntConsulta> resGuardarConsulta = busConsulta.BSaveConsulta(entNuevaConsulta.consulta, entNuevaConsulta.iIdUsuarioMod);
                if (resGuardarConsulta.Code != 0)
                {
                    return response = resGuardarConsulta.GetResponse<EntDetalleCompra>();
                }

                IMDResponse<bool> responseCorreo = this.BEnviarCorreoConsulta(entDetalleCompra, reprogramado);

                response.Code = 0;
                response.Message = "Consulta creada";
                response.Result = entDetalleCompra;
            }
            catch (Exception ex)
            {
                response.Code = 67823458540936;
                response.Message = "Ocurrió un error inesperado al confirmar la consulta del paciente";

                logger.Error(IMDSerialize.Serialize(67823458540936, $"Error en {metodo}(EntNuevaConsulta entNuevaConsulta, string FolioEnviar, string PassEnviar, bool reprogramado = false): {ex.Message}", entNuevaConsulta, FolioEnviar, PassEnviar, reprogramado, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Envia el correo de una consulta generada
        /// </summary>
        /// <param name="detalleFolio"></param>
        /// <param name="reprogramado"></param>
        /// <returns></returns>
        public IMDResponse<bool> BEnviarCorreoConsulta(EntDetalleCompra detalleFolio, bool reprogramado = false)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEnvioCorreo);
            logger.Info(IMDSerialize.Serialize(67823458423609, $"Inicia {metodo}(EntDetalleCompra detalleFolio, bool reprogramado = false)", detalleFolio, reprogramado));

            try
            {
                BusCorreo busCorreo = new BusCorreo();

                string asunto = !reprogramado ? "Meditoc - Cita programada" : "Meditoc - Cita reprogramada";
                string para = detalleFolio.sEmail;


                //FOLIOS
                string htmlFolios = string.Empty;
                string plantillaFolio = "<tr><td><table class=\"table-detail\"><tr class=\"group-detail font-unset bold small center\"><td colspan=\"3\">item.sDataGroup</td></tr><tr><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody><tr class=\"font-table bold small center table-border-b\"><td><small>1 -</small>&nbsp;item.sFolio</td><td>item.sPass</td></tr></tbody></table></td></tr></table></td></tr>";
                plantillaFolio = plantillaFolio.Replace("item.sDataGroup", $"Fecha de consulta: {detalleFolio.lstArticulos.First().dtFechaVencimiento:dd/MM/yyyy - h:mm tt}");
                plantillaFolio = plantillaFolio.Replace("item.sFolio", detalleFolio.lstArticulos.First().sFolio);
                plantillaFolio = plantillaFolio.Replace("item.sPass", detalleFolio.lstArticulos.First().sPass);

                htmlFolios += plantillaFolio;

                string plantillaBody = "<!DOCTYPE html><html><head><meta charset=\"utf-8;\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><link href=\"https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap\" rel=\"stylesheet\" /><style>body {font-family: Roboto, \"Segoe UI\", Tahoma, Geneva, Verdana, sans-serif;margin: 0;}.center {text-align: center !important;}.right {text-align: right !important;}.left {text-align: left !important;}.light {font-weight: 300;}.normal {font-weight: normal;}.bold {font-weight: 500;}.small {font-size: 12px;}.large {font-size: 15px;}.font-default {color: #707070;}.font-primary {color: #11b6ca;}.font-secondary {color: #115c8a;}.font-unset {color: #ffffff;}.font-table {color: #878787;}.table {margin: auto;width: 100%;max-width: 800px;border: 1px solid #dddddd;border-spacing: 0px;border-collapse: 0px;}.table td {padding: 6px 0px;}.logo-head {background-color: #11b6ca;padding: 5px 0px;}.table-content {margin: auto;width: 90%;border-collapse: collapse;}.table-detail {margin: auto;width: 100%;border-collapse: collapse;}.table-detail td {padding: 8px;vertical-align: top;}.head-detail {background-color: #115c8a;}.product-detail {border-bottom: 1px solid #989898;}.total-detail {background-color: #989898;}.group-detail {background-color: #11b6ca;}.divider {height: 1px;border: 0;background-color: #989898;}.link {text-decoration: none;}.link:hover {text-decoration: underline;}.link-none {text-decoration: none;}.table-border {border-right: 10px solid #115c8a;}.table-border-l {border-left: 10px solid #115c8a;}.table-border-b td {border-bottom: 1px solid #ccc;}</style></head><body><table class=\"table\"><tr><td class=\"logo-head center\"><img alt=\"logo-meditoc\" src=\"sLogoMeditoc\" height=\"50px\" /></td></tr><tr><td><table class=\"table-content\"><tr><td class=\"center\"><span class=\"font-default bold large\"> Gracias por usar nuestros servicios </span></td></tr><tr><td class=\"center\"><span class=\"font-default normal large\"> Fecha de solicitud de consulta: oDetalleCompra.sFechaCompra </span></td></tr><tr class=\"center\"><td><span class=\"font-default normal large\">Guarda tus credenciales, te servirán para acceder a Meditoc:</span></td></tr>oDetalleCompra.folios<tr class=\"center\"><td><p><span class=\"font-default normal large\">Para utilizar el servicio, descarga la app “Meditoc 360” disponible en Appstore y Playstore.</span></p></td></tr><tr class=\"center\"><td><span><a href=\"sLinkApple\" target=\"_blank\"class=\"link-none\"><img src=\"sLogoApple\" height=\"50px\" width=\"150px\"alt=\"APP\" /></a></span><span><a href=\"sLinkPlay\"target=\"_blank\" class=\"link-none\"><img src=\"sLogoPlay\" height=\"50px\" width=\"150px\"alt=\"PLAY\" /></a></span></td></tr><tr><td><hr class=\"divider\" /></td></tr><tr><td><span class=\"font-default light small\">De conformidad con la ley federal de protección de datos personales en posesión de los particulares, ponemos a su disposición nuestro&nbsp;<a href=\"sAvisoPrivacidad\" class=\"link font-secondary normal\"> Aviso de Privacidad </a>&nbsp;y&nbsp;<a href=\"sTerminosCondiciones\" class=\"link font-secondary normal\"> Términos y Condiciones. </a></span></td></tr></table></td></tr></table></body></html>";

                plantillaBody = plantillaBody.Replace("sLogoMeditoc", ConfigurationManager.AppSettings["sLogoMeditoc"]);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sNombre", detalleFolio.sNombre);
                plantillaBody = plantillaBody.Replace("oDetalleCompra.sFechaCompra", DateTime.Now.ToString("dd/MM/yyyy"));
                plantillaBody = plantillaBody.Replace("oDetalleCompra.folios", htmlFolios);
                plantillaBody = plantillaBody.Replace("sLinkApple", ConfigurationManager.AppSettings["sLinkApple"]);
                plantillaBody = plantillaBody.Replace("sLogoApple", ConfigurationManager.AppSettings["sLogoApple"]);
                plantillaBody = plantillaBody.Replace("sLinkPlay", ConfigurationManager.AppSettings["sLinkPlay"]);
                plantillaBody = plantillaBody.Replace("sLogoPlay", ConfigurationManager.AppSettings["sLogoPlay"]);
                plantillaBody = plantillaBody.Replace("sAvisoPrivacidad", ConfigurationManager.AppSettings["sAvisoDePrivacidad"]);
                plantillaBody = plantillaBody.Replace("sTerminosCondiciones", ConfigurationManager.AppSettings["sTerminosYCondiciones"]);
                plantillaBody = plantillaBody.Replace("sLogoConekta", ConfigurationManager.AppSettings["sLogoConekta"]);

                response = busCorreo.m_EnviarEmail("", "", "", asunto, plantillaBody, para, "", "");

                if (response.Code != 0)
                {
                    return response;
                }


                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458424386;
                response.Message = "Ocurrió un error inesperado al generar el correo de consulta para el paciente";

                logger.Error(IMDSerialize.Serialize(67823458424386, $"Error en {metodo}(EntDetalleCompra detalleFolio, bool reprogramado = false): {ex.Message}", detalleFolio, reprogramado, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Inicia sesión en la APP de Meditoc
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public IMDResponse<EntFolio> BLoginApp(string sUsuario, string sPassword)
        {
            IMDResponse<EntFolio> response = new IMDResponse<EntFolio>();
            BusUsuario busUsuario = new BusUsuario();
            EntFolio entFolio = new EntFolio();

            string metodo = nameof(this.BLoginApp);
            logger.Info(IMDSerialize.Serialize(67823458429825, $"Inicia {metodo}(string sUsuario, string sPassword)", sUsuario, sPassword));

            try
            {
                response.Code = 67823458430602;

                if (string.IsNullOrWhiteSpace(sUsuario))
                {
                    response.Message = "Su usuario o contraseña es inválida";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(sPassword))
                {
                    response.Message = "Su usuario o contraseña es inválida";
                    return response;
                }

                sPassword = busUsuario.BEncodePassword(sPassword);

                IMDResponse<DataTable> dtLoginApp = datFolio.DLoginApp(sUsuario, sPassword);

                if (dtLoginApp.Code != 0)
                {
                    response.Message = "Su usuario o contraseña es inválida";
                    return response;
                }

                if (dtLoginApp.Result.Rows.Count == 0)
                {
                    response.Message = "Su usuario o contraseña es inválida";
                    return response;
                }

                foreach (DataRow item in dtLoginApp.Result.Rows)
                {
                    IMDDataRow dataRow = new IMDDataRow(item);

                    entFolio.iIdPaciente = dataRow.ConvertTo<int>("iIdPaciente");
                    entFolio.iIdFolio = dataRow.ConvertTo<int>("iIdFolio");
                    entFolio.iIdProducto = dataRow.ConvertTo<int>("iIdTipoProducto");
                    entFolio.sFolio = dataRow.ConvertTo<string>("sFolio");
                    entFolio.sPassword = dataRow.ConvertTo<string>("sPassword");
                    entFolio.dtFechaVencimiento = dataRow.ConvertTo<DateTime?>("dtFechaVencimiento");
                    entFolio.bTerminosYCondiciones = Convert.ToBoolean(dataRow.ConvertTo<int>("bTerminosYCondiciones"));
                    entFolio.bActivo = Convert.ToBoolean(dataRow.ConvertTo<int>("bActivo"));
                    entFolio.bBaja = Convert.ToBoolean(dataRow.ConvertTo<int>("bBaja"));
                    entFolio.bTerminosYCondiciones = Convert.ToBoolean(dataRow.ConvertTo<int>("bTerminosYCondiciones"));
                }

                IMDResponse<bool> responseConsultasAgendadas = BObtenerConsultasAgendadas(entFolio.iIdPaciente);

                if (responseConsultasAgendadas.Code != 0)
                {
                    return responseConsultasAgendadas.GetResponse<EntFolio>();
                }

                entFolio.bEsAgendada = responseConsultasAgendadas.Result;
                if (!responseConsultasAgendadas.Result)
                {
                    if (entFolio.dtFechaVencimiento != null)
                    {
                        if (entFolio.dtFechaVencimiento < DateTime.Now || (!entFolio.bActivo && entFolio.bBaja))
                        {
                            response.Message = "Su folio ha expirado";
                            return response;
                        }
                    }
                    else
                    {
                        IMDResponse<bool> resSetVigencia = this.BLoginAppUpdVigencia(entFolio.iIdFolio);
                        if (resSetVigencia.Code != 0)
                        {
                            return resSetVigencia.GetResponse<EntFolio>();
                        }
                    }
                }

                response.Code = 0;
                response.Message = "Login correcto";
                response.Result = entFolio;
            }
            catch (Exception ex)
            {
                response.Code = 67823458430602;
                response.Message = "Su usuario o contraseña es inválida";

                logger.Error(IMDSerialize.Serialize(67823458430602, $"Error en {metodo}(string sUsuario, string sPassword): {ex.Message}", sUsuario, sPassword, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BLoginAppUpdVigencia(int piIdFolio)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BLoginAppUpdVigencia);
            logger.Info(IMDSerialize.Serialize(67823458622521, $"Inicia {metodo}"));

            try
            {
                IMDResponse<List<EntFolioReporte>> resGetData = this.BGetFolios(piIdFolio);
                if (resGetData.Code != 0)
                {
                    return resGetData.GetResponse<bool>();
                }

                if (resGetData.Result.Count != 1)
                {
                    response.Code = -37465876768234;
                    response.Message = "Error al validar la información de la compra";
                    return response;
                }
                EntFolioReporte folio = resGetData.Result.First();

                if (Enum.IsDefined(typeof(EnumProductos), folio.iIdProducto) && folio.iIdProducto != (int)EnumProductos.OrientacionEspecialistaID)
                {
                    DateTime nuevaFechaVencimiento;
                    if (folio.iMesVigenciaProducto == 0)
                    {
                        nuevaFechaVencimiento = DateTime.Now.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["iDiasDespuesVencimiento"]));
                    }
                    else
                    {
                        nuevaFechaVencimiento = DateTime.Now.AddMonths(folio.iMesVigenciaProducto);
                    }
                    EntFolioFV entFolioFV = new EntFolioFV
                    {
                        dtFechaVencimiento = nuevaFechaVencimiento,
                        iIdEmpresa = folio.iIdEmpresa,
                        lstFolios = new List<EntFolioFVItem>
                        {
                            new EntFolioFVItem
                            {
                                iIdFolio = piIdFolio
                            }
                        }
                    };
                    IMDResponse<bool> resUpdVigencia = this.BUpdFechaVencimiento(entFolioFV);
                    if (resUpdVigencia.Code != 0)
                    {
                        return resUpdVigencia;
                    }

                    response.Code = 0;
                    response.Message = "Se actualizó la vigencia del folio";
                    response.Result = true;
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458623298;
                response.Message = "Ocurrió un error inesperado al verificar la información de la compra";

                logger.Error(IMDSerialize.Serialize(67823458623298, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene los folios del sistema
        /// </summary>
        /// <param name="piIdFolio"></param>
        /// <param name="piIdEmpresa"></param>
        /// <param name="piIdProducto"></param>
        /// <param name="piIdOrigen"></param>
        /// <param name="psFolio"></param>
        /// <param name="psOrdenConekta"></param>
        /// <param name="pbTerminosYCondiciones"></param>
        /// <param name="pbActivo"></param>
        /// <param name="pbBaja"></param>
        /// <returns></returns>
        public IMDResponse<List<EntFolioReporte>> BGetFolios(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto = null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false)
        {
            IMDResponse<List<EntFolioReporte>> response = new IMDResponse<List<EntFolioReporte>>();

            string metodo = nameof(this.BGetFolios);
            logger.Info(IMDSerialize.Serialize(67823458434487, $"Inicia {metodo}(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto = null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false)", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja));

            try
            {
                IMDResponse<DataTable> respuestaObtenerFolios = datFolio.DGetFolios(piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja);
                if (respuestaObtenerFolios.Code != 0)
                {
                    return respuestaObtenerFolios.GetResponse<List<EntFolioReporte>>();
                }

                List<EntFolioReporte> lstFolios = new List<EntFolioReporte>();
                foreach (DataRow drFolio in respuestaObtenerFolios.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drFolio);

                    EntFolioReporte folio = new EntFolioReporte
                    {
                        bTerminosYCondiciones = Convert.ToBoolean(dr.ConvertTo<int>("bTerminosYCondiciones")),
                        bActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo")),
                        bBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja")),
                        dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion"),
                        dtFechaVencimiento = dr.ConvertTo<DateTime?>("dtFechaVencimiento"),
                        iConsecutivo = dr.ConvertTo<int>("iConsecutivo"),
                        iIdEmpresa = dr.ConvertTo<int>("iIdEmpresa"),
                        iIdFolio = dr.ConvertTo<int>("iIdFolio"),
                        iIdPaciente = dr.ConvertTo<int>("iIdPaciente"),
                        iIdOrigen = dr.ConvertTo<int>("iIdOrigen"),
                        iIdProducto = dr.ConvertTo<int>("iIdProducto"),
                        iIdTipoProducto = dr.ConvertTo<int>("iIdTipoProducto"),
                        iMesVigenciaProducto = dr.ConvertTo<int>("iMesVigenciaProducto"),
                        sCorreoEmpresa = dr.ConvertTo<string>("sCorreoEmpresa"),
                        sFolio = dr.ConvertTo<string>("sFolio"),
                        sCorreoPaciente = dr.ConvertTo<string>("sCorreoPaciente"),
                        sNombrePaciente = dr.ConvertTo<string>("sNombrePaciente"),
                        sTelefonoPaciente = dr.ConvertTo<string>("sTelefonoPaciente"),
                        sPassword = dr.ConvertTo<string>("sPassword"),
                        sFolioEmpresa = dr.ConvertTo<string>("sFolioEmpresa"),
                        sIcon = dr.ConvertTo<string>("sIcon"),
                        sNombreEmpresa = dr.ConvertTo<string>("sNombreEmpresa"),
                        sNombreProducto = dr.ConvertTo<string>("sNombreProducto"),
                        sOrdenConekta = dr.ConvertTo<string>("sOrdenConekta"),
                        sOrigen = dr.ConvertTo<string>("sOrigen"),
                        sTipoProducto = dr.ConvertTo<string>("sTipoProducto"),
                    };
                    folio.sFechaCreacion = folio.dtFechaCreacion.ToString("dd/MM/yyyy HH:mm");
                    folio.sFechaVencimiento = folio.dtFechaVencimiento == null ? "Sin asignar" : folio.dtFechaVencimiento?.ToString("dd/MM/yyyy HH:mm");

                    lstFolios.Add(folio);
                }

                lstFolios = lstFolios.OrderByDescending(x => x.iIdFolio).ToList();

                response.Code = 0;
                response.Message = "Folios consultados";
                response.Result = lstFolios;
            }
            catch (Exception ex)
            {
                response.Code = 67823458435264;
                response.Message = "Ocurrió un error inesperado al consultar los folios solicitados";

                logger.Error(IMDSerialize.Serialize(67823458435264, $"Error en {metodo}(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto = null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false): {ex.Message}", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Actualiza la fecha de vencimiento de los folios solicitados
        /// </summary>
        /// <param name="entFolioFV"></param>
        /// <returns></returns>
        public IMDResponse<bool> BUpdFechaVencimiento(EntFolioFV entFolioFV)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BUpdFechaVencimiento);
            logger.Info(IMDSerialize.Serialize(67823458439149, $"Inicia {metodo}(EntFolioFV entFolioFV)", entFolioFV));

            try
            {
                if (entFolioFV == null)
                {
                    response.Code = -23476723976234;
                    response.Message = "Por favor, ingrese los datos para continuar";
                    return response;
                }

                if (entFolioFV.lstFolios == null || entFolioFV.lstFolios?.Count < 1)
                {
                    response.Code = -65623486572344;
                    response.Message = "Por favor, seleccione los folios para continuar";
                    return response;
                }

                //using (DbConnection dbConnection = datFolio.database.CreateConnection())
                //{
                //    dbConnection.Open();
                //    using (DbTransaction dbTransaction = dbConnection.BeginTransaction())
                //    {
                foreach (EntFolioFVItem folio in entFolioFV.lstFolios)
                {
                    IMDResponse<bool> respuestaUpdFecha = datFolio.DUpdFechaVencimiento(entFolioFV.iIdEmpresa, folio.iIdFolio, entFolioFV.dtFechaVencimiento, entFolioFV.iIdUsuario);
                    if (respuestaUpdFecha.Code != 0)
                    {
                        return respuestaUpdFecha;
                    }
                }
                //        dbTransaction.Commit();
                //    }
                //}

                response.Code = 0;
                response.Message = "Folios actualizados";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458439926;
                response.Message = "Ocurrió un error inesperado al actualizar los folios";

                logger.Error(IMDSerialize.Serialize(67823458439926, $"Error en {metodo}(EntFolioFV entFolioFV): {ex.Message}", entFolioFV, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Elimina los folios solicitados
        /// </summary>
        /// <param name="entFolioFV"></param>
        /// <returns></returns>
        public IMDResponse<bool> BEliminarFoliosEmpresa(EntFolioFV entFolioFV)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEliminarFoliosEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458443811, $"Inicia {metodo}(EntFolioFV entFolioFV)", entFolioFV));

            try
            {
                if (entFolioFV == null)
                {
                    response.Code = -32423778752356;
                    response.Message = "Por favor, ingrese los datos para continuar";
                    return response;
                }

                if (entFolioFV.lstFolios == null || entFolioFV.lstFolios?.Count < 1)
                {
                    response.Code = -44587083458623;
                    response.Message = "Por favor, seleccione los folios para continuar";
                    return response;
                }

                //using (DbConnection dbConnection = datFolio.database.CreateConnection())
                //{
                //    dbConnection.Open();
                //    using (DbTransaction dbTransaction = dbConnection.BeginTransaction())
                //    {
                foreach (EntFolioFVItem folio in entFolioFV.lstFolios)
                {
                    IMDResponse<bool> respuestaUpdFecha = datFolio.DEliminarFoliosEmpresa(entFolioFV.iIdEmpresa, folio.iIdFolio, entFolioFV.iIdUsuario);
                    if (respuestaUpdFecha.Code != 0)
                    {
                        return respuestaUpdFecha;
                    }
                }
                //        dbTransaction.Commit();
                //    }
                //}

                response.Code = 0;
                response.Message = "Folios actualizados";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458444588;
                response.Message = "Ocurrió un error inesperado al intentar eliminar los folios de la empresa";

                logger.Error(IMDSerialize.Serialize(67823458444588, $"Error en {metodo}(EntFolioFV entFolioFV): {ex.Message}", entFolioFV, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Actualiza el acuerdo de términos y condiciones
        /// </summary>
        /// <param name="sFolio"></param>
        /// <returns></returns>
        public IMDResponse<bool> BTerminosYCondiciones(string sFolio = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BTerminosYCondiciones);
            logger.Info(IMDSerialize.Serialize(67823458462459, $"Inicia {metodo}(string sFolio = null)", sFolio));

            try
            {
                if (string.IsNullOrWhiteSpace(sFolio))
                {
                    response.Code = -986772347673234;
                    response.Message = "El folio se encuentra vacio";
                    response.Result = false;
                    return response;
                }

                response = datFolio.DTerminosYCondiciones(sFolio);

                if (response.Code != 0)
                {
                    response.Message = "No se pudo validar los terminos y condiciones";
                    return response;
                }

                response.Code = 0;
                response.Message = "Terminos y condiciones aceptados";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458463236;
                response.Message = "Ocurrió un error inesperado al actualizar la cuenta";

                logger.Error(IMDSerialize.Serialize(67823458463236, $"Error en {metodo}(string sFolio = null): {ex.Message}", sFolio, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Actualiza la contraseña del folio desde la APP de Meditoc
        /// </summary>
        /// <param name="sFolio"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public IMDResponse<bool> BUpdPassword(string sFolio = null, string sPassword = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BUpdPassword);
            logger.Info(IMDSerialize.Serialize(67823458499755, $"Inicia {metodo}(string sFolio = null, string sPassword = null)", sFolio, sPassword));

            try
            {
                BusUsuario busUsuario = new BusUsuario();

                sPassword = busUsuario.BEncodePassword(sPassword);

                response = datFolio.DUpdPassword(sFolio, sPassword);

                if (response.Code != 0)
                {
                    return response;
                }

                response.Code = 0;
                response.Message = "Se realizo el cambio de contraseña con exito";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458500532;
                response.Message = "Ocurrió un error inesperado al actualizar la contraseña";

                logger.Error(IMDSerialize.Serialize(67823458500532, $"Error en {metodo}: {ex.Message}(string sFolio = null, string sPassword = null)", sFolio, sPassword, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BObtenerConsultasAgendadas(int? iIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BObtenerConsultasAgendadas);
            logger.Info(IMDSerialize.Serialize(67823458594549, $"Inicia {metodo}"));

            try
            {
                DatConsulta datConsulta = new DatConsulta();
                IMDResponse<DataTable> dtConsultasAgendadas = datConsulta.DGetConsultaProgramadaByPaciente(iIdUsuario, DateTime.Now);

                if (dtConsultasAgendadas.Code != 0)
                {
                    return dtConsultasAgendadas.GetResponse<bool>();
                }

                if (dtConsultasAgendadas.Result.Rows.Count == 0)
                {
                    response.Result = false;
                    response.Message = "No cuenta con consultas agendadas";

                    return response;
                }

                response.Code = 0;
                response.Message = "el usuario cuenta con consultas asignadas";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458595326;
                response.Message = "Ocurrió un error inesperado al obtener las consultas agendadas.";

                logger.Error(IMDSerialize.Serialize(67823458595326, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BSaveFolioVC(EntFolioVentaCalle entFolioVentaCalle)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveFolioVC);
            logger.Info(IMDSerialize.Serialize(67823458605427, $"Inicia {metodo}(EntFolioVentaCalle entFolioVentaCalle)", entFolioVentaCalle));

            try
            {
                if (entFolioVentaCalle == null)
                {
                    response.Code = -3746587345;
                    response.Message = "No se ingresó información para guardar el folio";
                    return response;
                }

                if (entFolioVentaCalle.iIdEmpresa <= 0 || entFolioVentaCalle.iIdOrigen <= 0 || entFolioVentaCalle.iIdProducto <= 0)
                {
                    response.Code = -54357876234;
                    response.Message = "No se ingresó información para guardar el folio";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(entFolioVentaCalle.sFolio) || string.IsNullOrWhiteSpace(entFolioVentaCalle.sPassword))
                {
                    response.Code = -23468788234;
                    response.Message = "No se ingresó información para guardar el folio";
                    return response;
                }

                entFolioVentaCalle.sPassword = busUsuario.BEncodePassword(entFolioVentaCalle.sPassword);

                IMDResponse<DataTable> resSaveFolio = datFolio.DSaveFolioVC(
                    entFolioVentaCalle.iIdEmpresa,
                    entFolioVentaCalle.iIdProducto,
                    entFolioVentaCalle.iIdOrigen,
                    entFolioVentaCalle.sFolio,
                    entFolioVentaCalle.sPassword,
                    entFolioVentaCalle.iIdUsuarioMod,
                    entFolioVentaCalle.dtFechaVencimiento,
                    entFolioVentaCalle.sOrdenConekta,
                    entFolioVentaCalle.bConfirmado);
                if (resSaveFolio.Code != 0)
                {
                    return resSaveFolio.GetResponse<bool>();
                }

                bool bIsNew = Convert.ToBoolean(Convert.ToInt32(resSaveFolio.Result.Rows[0]["bIsNew"].ToString()));

                response.Code = 0;
                response.Message = "El folio se guardó correctamente";
                response.Result = bIsNew;
            }
            catch (Exception ex)
            {
                response.Code = 67823458606204;
                response.Message = "Ocurrió un error inesperado al guardar el folio de venta calle.";

                logger.Error(IMDSerialize.Serialize(67823458606204, $"Error en {metodo}(EntFolioVentaCalle entFolioVentaCalle): {ex.Message}", entFolioVentaCalle, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntFolioVerificarCarga>> BVerificarFoliosVentaCalle(Stream foliosExcel)
        {
            IMDResponse<List<EntFolioVerificarCarga>> response = new IMDResponse<List<EntFolioVerificarCarga>>();

            string metodo = nameof(this.BVerificarFoliosVentaCalle);
            logger.Info(IMDSerialize.Serialize(67823458608535, $"Inicia {metodo}(Stream foliosExcel)"));

            try
            {
                List<EntFolioVerificarCarga> lstEntFolioVerificarCarga = new List<EntFolioVerificarCarga>();

                using (ExcelPackage excelPackage = new ExcelPackage(foliosExcel))
                {
                    if (excelPackage == null)
                    {
                        response.Code = -87687673456;
                        response.Message = "No ha sido posible leer el archivo cargado";
                        return response;
                    }

                    ExcelWorksheets excelPaginas = excelPackage.Workbook.Worksheets;

                    if (excelPaginas.Count != 5)
                    {
                        response.Code = -87687673456;
                        response.Message = "Se ha cargado un archivo que no coincide con la plantilla de carga de folios";
                        return response;
                    }


                    for (int i = 0; i < excelPaginas.Count; i++)
                    {
                        List<EntFolioUser> lstFolioVC = new List<EntFolioUser>();
                        ExcelWorksheet excelPagina = excelPaginas[i];

                        object[,] values = (object[,])excelPagina.Cells.Value;

                        int rows = values.GetLength(0);
                        int columns = values.GetLength(1);

                        for (int j = 0; j < rows; j++)
                        {
                            for (int k = 0; k < columns; k++)
                            {
                                if (values[j, k] != null)
                                {
                                    string folio = values[j, k]?.ToString();
                                    if (folio.StartsWith("VC"))
                                    {
                                        string password = values[j, ++k].ToString();

                                        EntFolioUser entFolioUser = new EntFolioUser
                                        {
                                            sFolio = folio,
                                            sPassword = password
                                        };
                                        lstFolioVC.Add(entFolioUser);
                                    }
                                }
                            }
                        }
                        EntFolioVerificarCarga entFolioVerificarCarga = new EntFolioVerificarCarga()
                        {
                            lstFolios = lstFolioVC,
                            totalFolios = lstFolioVC.Count
                        };
                        switch (i)
                        {
                            case 0:
                                entFolioVerificarCarga.entProducto.sNombre = "Membresía 1 Mes de Venta Calle";
                                break;
                            case 1:
                                entFolioVerificarCarga.entProducto.sNombre = "Membresía 3 Meses de Venta Calle";
                                break;
                            case 2:
                                entFolioVerificarCarga.entProducto.sNombre = "Membresía 6 Meses de Venta Calle";
                                break;
                            case 3:
                                entFolioVerificarCarga.entProducto.sNombre = "Membresía 9 Meses de Venta Calle";
                                break;
                            case 4:
                                entFolioVerificarCarga.entProducto.sNombre = "Membresía 12 Meses de Venta Calle";
                                break;
                            default:
                                break;
                        }
                        lstEntFolioVerificarCarga.Add(entFolioVerificarCarga);
                    }
                }

                response.Code = 0;
                response.Message = "Folios verificados";
                response.Result = lstEntFolioVerificarCarga;
            }
            catch (Exception ex)
            {
                response.Code = 67823458609312;
                response.Message = "Ocurrió un error inesperado al validar los folios solicitados.";

                logger.Error(IMDSerialize.Serialize(67823458609312, $"Error en {metodo}(Stream foliosExcel): {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntFolioVerificarCarga> BVerificarFoliosArchivo(int piIdEmpresa, int piIdProducto, Stream foliosExcel)
        {
            IMDResponse<EntFolioVerificarCarga> response = new IMDResponse<EntFolioVerificarCarga>();

            string metodo = nameof(this.BVerificarFoliosArchivo);
            logger.Info(IMDSerialize.Serialize(67823458614751, $"Inicia {metodo}(int piIdEmpresa, int piIdProducto, Stream foliosExcel)", piIdEmpresa, piIdProducto));

            try
            {
                IMDResponse<List<EntProducto>> resGetProducto = busProducto.BObtenerProductos(piIdProducto);
                if (resGetProducto.Code != 0)
                {
                    return resGetProducto.GetResponse<EntFolioVerificarCarga>();
                }

                if (resGetProducto.Result.Count != 1)
                {
                    response.Code = -234768767234;
                    response.Message = "El producto solicitado no existe";
                    return response;
                }

                IMDResponse<List<EntEmpresa>> resGetEmpresas = busEmpresa.BGetEmpresas(iIdEmpresa: piIdEmpresa);
                if (resGetEmpresas.Code != 0)
                {
                    return resGetEmpresas.GetResponse<EntFolioVerificarCarga>();
                }

                if (resGetEmpresas.Result.Count != 1)
                {
                    response.Code = -89787345234;
                    response.Message = "No se encontró la empresa seleccionada";
                    return response;
                }

                EntProducto entProducto = resGetProducto.Result.First();
                EntEmpresa entEmpresa = resGetEmpresas.Result.First();

                using (ExcelPackage excelPackage = new ExcelPackage(foliosExcel))
                {
                    if (excelPackage == null)
                    {
                        response.Code = -87687673456;
                        response.Message = "No ha sido posible leer el archivo cargado";
                        return response;
                    }

                    ExcelWorksheets excelPaginas = excelPackage.Workbook.Worksheets;

                    if (excelPaginas.Count < 1)
                    {
                        response.Code = -87687673456;
                        response.Message = "Se ha cargado un archivo que no coincide con la plantilla de carga de folios";
                        return response;
                    }

                    ExcelWorksheet hojaFolios = excelPaginas.First();
                    List<EntFolioUser> lstFolios = new List<EntFolioUser>();

                    object[,] values = (object[,])hojaFolios.Cells.Value;

                    int columns = values.GetLength(1);
                    if (columns != 39)
                    {
                        response.Code = -823782734234;
                        response.Message = "Se ha cargado un archivo que no coincide con la plantilla de carga de folios";
                        return response;
                    }

                    int rows = values.GetLength(0);
                    for (int i = 1; i < rows; i++)
                    {
                        //1, 2 // 9, 10 // 17, 18 // 25, 26 // 33, 34
                        for (int j = 0; j < 5; j++)
                        {
                            string folio = values[i, j * 8 + 1]?.ToString();
                            if (string.IsNullOrWhiteSpace(folio))
                            {
                                continue;
                            }
                            string passw = values[i, j * 8 + 2]?.ToString();

                            if (!string.IsNullOrWhiteSpace(folio) && string.IsNullOrWhiteSpace(passw))
                            {
                                response.Code = -678273678234234;
                                response.Message = $"El folio {folio} de la fila {i} de la sección {j + 1} no tiene una contraseña definida";
                                return response;
                            }

                            EntFolioUser entFolioUser = new EntFolioUser
                            {
                                sFolio = folio,
                                sPassword = passw
                            };
                            lstFolios.Add(entFolioUser);
                        }
                    }
                    EntFolioVerificarCarga entFolioVerificarCarga = new EntFolioVerificarCarga()
                    {
                        lstFolios = lstFolios,
                        totalFolios = lstFolios.Count,
                        entProducto = entProducto,
                        entEmpresa = entEmpresa
                    };

                    response.Code = 0;
                    response.Message = "Archivo verificado";
                    response.Result = entFolioVerificarCarga;
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458615528;
                response.Message = "Ocurrió un error inesperado al verificar los folios del archivo.";

                logger.Error(IMDSerialize.Serialize(67823458615528, $"Error en {metodo}(int piIdEmpresa, int piIdProducto, Stream foliosExcel): {ex.Message}", piIdEmpresa, piIdProducto, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BGenerarFoliosArchivo(int piIdEmpresa, int piIdProducto, Stream foliosExcel, int piIdUsuarioMod)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BGenerarFoliosArchivo);
            logger.Info(IMDSerialize.Serialize(67823458617859, $"Inicia {metodo}(int piIdEmpresa, int piIdProducto, Stream foliosExcel, int piIdUsuarioMod)", piIdEmpresa, piIdProducto, piIdUsuarioMod));

            try
            {
                IMDResponse<EntFolioVerificarCarga> resGetFolios = this.BVerificarFoliosArchivo(piIdEmpresa, piIdProducto, foliosExcel);
                if (resGetFolios.Code != 0)
                {
                    return resGetFolios.GetResponse<bool>();
                }

                EntFolioVerificarCarga data = resGetFolios.Result;

                EntFolioxEmpresa entFolioxEmpresa = new EntFolioxEmpresa
                {
                    iIdEmpresa = resGetFolios.Result.entEmpresa.iIdEmpresa,
                    iIdOrigen = (int)EnumOrigen.BaseDeDatos,
                    lstLineItems = new List<line_items>
                    {
                        new line_items
                        {
                            monthsExpiration = data.entProducto.iMesVigencia,
                            name = data.entProducto.sNombre,
                            product_id = data.entProducto.iIdProducto,
                            quantity = data.lstFolios.Count,
                            unit_price = (int)data.entProducto.fCosto * 100
                        }
                    }
                };

                Guid tempOrderUID = Guid.NewGuid();
                int qty = 0;
                //using (DbConnection dbConnection = datFolio.database.CreateConnection())
                //{
                //    dbConnection.Open();
                //    using (DbTransaction dbTransaction = dbConnection.BeginTransaction())
                //    {

                foreach (EntFolioUser folio in data.lstFolios)
                {
                    EntFolioVentaCalle entFolioVentaCalle = new EntFolioVentaCalle
                    {
                        iIdEmpresa = data.entEmpresa.iIdEmpresa,
                        iIdOrigen = entFolioxEmpresa.iIdOrigen,
                        iIdProducto = data.entProducto.iIdProducto,
                        iIdUsuarioMod = piIdUsuarioMod,
                        sFolio = folio.sFolio,
                        sPassword = folio.sPassword,
                        dtFechaVencimiento = Enum.IsDefined(typeof(EnumProductos), data.entProducto.iIdProducto) ? (DateTime?)null : data.entProducto.iMesVigencia == 0 ? DateTime.Now.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["iDiasDespuesVencimiento"])) : DateTime.Now.AddMonths(data.entProducto.iMesVigencia),
                        sOrdenConekta = tempOrderUID.ToString(),
                        bConfirmado = Enum.IsDefined(typeof(EnumProductos), data.entProducto.iIdProducto) && data.entProducto.iIdProducto != (int)EnumProductos.OrientacionEspecialistaID ? false : true
                    };

                    IMDResponse<bool> resSaveFolio = this.BSaveFolioVC(entFolioVentaCalle);
                    if (resSaveFolio.Code != 0)
                    {
                        resSaveFolio.Message = $"El folio {entFolioVentaCalle.sFolio} con contraseña {entFolioVentaCalle.sPassword} no se pudo guardar. Verifique los datos y cargue el archivo nuevamente. El proceso de guardado se ha detenido";
                        return resSaveFolio;
                    }
                    if (resSaveFolio.Result)
                    {
                        qty++;
                    }
                }
                //        dbTransaction.Commit();
                //    }
                //}


                if (qty > 0)
                {
                    entFolioxEmpresa.lstLineItems[0].quantity = qty;

                    IMDResponse<bool> resSaveOrder = this.BSaveOrderEmpresa(entFolioxEmpresa, data.entEmpresa, tempOrderUID);
                    if (resSaveOrder.Code != 0)
                    {
                        return resSaveOrder;
                    }
                    response.Code = 0;
                    response.Message = "Los folios se guardaron correctamente";
                    response.Result = true;
                }
                else
                {
                    response.Code = -10;
                    response.Message = "Los folios solicitados ya han sido registrados con anterioridad. No se generó la orden.";
                    response.Result = false;
                }


            }
            catch (Exception ex)
            {
                response.Code = 67823458618636;
                response.Message = "Ocurrió un error inesperado al generar los folios solicitados";

                logger.Error(IMDSerialize.Serialize(67823458618636, $"Error en {metodo}(int piIdEmpresa, int piIdProducto, Stream foliosExcel, int piIdUsuarioMod): {ex.Message}", piIdEmpresa, piIdProducto, piIdUsuarioMod, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BGenerarFoliosVentaCalle(int piIdUsuarioMod, string sFolioEmpresa, Stream foliosExcel)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BGenerarFoliosVentaCalle);
            logger.Info(IMDSerialize.Serialize(67823458602319, $"Inicia {metodo}(int piIdUsuarioMod, string sFolioEmpresa, Stream foliosExcel)", piIdUsuarioMod, sFolioEmpresa));

            try
            {
                if (string.IsNullOrEmpty(sFolioEmpresa))
                {
                    response.Code = -87687673456;
                    response.Message = "No se ingresó el folio de la empresa par asignar los folios";
                    return response;
                }

                IMDResponse<List<EntEmpresa>> resGetEmpresas = busEmpresa.BGetEmpresas(psFolioEmpresa: sFolioEmpresa);
                if (resGetEmpresas.Code != 0)
                {
                    return resGetEmpresas.GetResponse<bool>();
                }

                if (resGetEmpresas.Result.Count != 1)
                {
                    response.Code = -87687673456;
                    response.Message = "No se encontró la empresa seleccionada";
                    return response;
                }

                EntEmpresa entEmpresa = resGetEmpresas.Result.First();
                using (ExcelPackage excelPackage = new ExcelPackage(foliosExcel))
                {
                    if (excelPackage == null)
                    {
                        response.Code = -87687673456;
                        response.Message = "No ha sido posible leer el archivo cargado";
                        return response;
                    }

                    ExcelWorksheets excelPaginas = excelPackage.Workbook.Worksheets;

                    if (excelPaginas.Count != 5)
                    {
                        response.Code = -87687673456;
                        response.Message = "Se ha cargado un archivo que no coincide con la plantilla de carga de folios";
                        return response;
                    }

                    for (int i = 0; i < excelPaginas.Count; i++)
                    {
                        int fiIdProducto = 0;
                        switch (i)
                        {
                            case 0:
                                fiIdProducto = (int)EnumProductos.MembresiaVentaCalle1Mes;
                                break;
                            case 1:
                                fiIdProducto = (int)EnumProductos.MembresiaVentaCalle3Meses;
                                break;
                            case 2:
                                fiIdProducto = (int)EnumProductos.MembresiaVentaCalle6Meses;
                                break;
                            case 3:
                                fiIdProducto = (int)EnumProductos.MembresiaVentaCalle9Meses;
                                break;
                            case 4:
                                fiIdProducto = (int)EnumProductos.MembresiaVentaCalle12Meses;
                                break;
                            default:
                                break;
                        }
                        ExcelWorksheet excelPagina = excelPaginas[i];

                        object[,] values = (object[,])excelPagina.Cells.Value;

                        int rows = values.GetLength(0);
                        int columns = values.GetLength(1);

                        for (int j = 0; j < rows; j++)
                        {
                            for (int k = 0; k < columns; k++)
                            {
                                if (values[j, k] != null)
                                {
                                    string folio = values[j, k].ToString();
                                    if (folio.StartsWith("VC"))
                                    {
                                        string password = values[j, ++k]?.ToString();
                                        if (string.IsNullOrWhiteSpace(password))
                                        {
                                            response.Code = -767652873456;
                                            response.Message = $"El folio {folio} no cuenta con contraseña. El proceso se ha detenido.";
                                            return response;
                                        }

                                        EntFolioVentaCalle entFolioVentaCalle = new EntFolioVentaCalle
                                        {
                                            iIdEmpresa = entEmpresa.iIdEmpresa,
                                            iIdOrigen = (int)EnumOrigen.BaseDeDatos,
                                            iIdProducto = fiIdProducto,
                                            iIdUsuarioMod = piIdUsuarioMod,
                                            sFolio = folio,
                                            sPassword = password
                                        };

                                        IMDResponse<bool> resSaveFolio = this.BSaveFolioVC(entFolioVentaCalle);
                                        if (resSaveFolio.Code != 0)
                                        {
                                            resSaveFolio.Message = $"El folio {entFolioVentaCalle.sFolio} con contraseña {entFolioVentaCalle.sPassword} no se pudo guardar. Verifique los datos. El proceso de guardado se ha detenido";
                                            return resSaveFolio;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                response.Code = 0;
                response.Message = "Los folios se han guardado correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458603096;
                response.Message = "Ocurrió un error inesperado al guardar los folios de venta calle solicitados.";

                logger.Error(IMDSerialize.Serialize(67823458603096, $"Error en {metodo}(int piIdUsuarioMod, string sFolioEmpresa, Stream foliosExcel): {ex.Message}", piIdUsuarioMod, sFolioEmpresa, ex, response));
            }
            return response;
        }

        public IMDResponse<MemoryStream> BGetPlantillaFolioVC()
        {
            IMDResponse<MemoryStream> response = new IMDResponse<MemoryStream>();

            string metodo = nameof(this.BGetPlantillaFolioVC);
            logger.Info(IMDSerialize.Serialize(67823458610089, $"Inicia {metodo}()"));

            try
            {
                string rutaEncriptada = ConfigurationManager.AppSettings["sRutaPlantillaFoliosVC"];

                IMDEndec iMDEndec = new IMDEndec();
                IMDResponse<string> resDesencriptarRuta = iMDEndec.BDecrypt(rutaEncriptada, "nhca7JFS73ygs6A3", "bc7DKb3M");
                if (resDesencriptarRuta.Code != 0)
                {
                    response.Code = -7645726983648;
                    response.Message = "La plantilla para la carga de folios de venta calle no se encuentra especificada";
                    return response;
                }

                byte[] plantillaByte = File.ReadAllBytes(resDesencriptarRuta.Result);
                MemoryStream memoryStream = new MemoryStream(plantillaByte);

                response.Code = 0;
                response.Message = "Plantilla descargada";
                response.Result = memoryStream;
            }
            catch (Exception ex)
            {
                response.Code = 67823458610866;
                response.Message = "Ocurrió un error inesperado al descargar la plantilla.";

                logger.Error(IMDSerialize.Serialize(67823458610866, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
