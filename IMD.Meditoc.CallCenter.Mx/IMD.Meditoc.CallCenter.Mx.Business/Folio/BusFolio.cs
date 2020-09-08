using IMD.Admin.Conekta.Business;
using IMD.Admin.Conekta.Entities;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Business.Correo;
using IMD.Meditoc.CallCenter.Mx.Business.Paciente;
using IMD.Meditoc.CallCenter.Mx.Business.Producto;
using IMD.Meditoc.CallCenter.Mx.Data.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.Folio
{
    public class BusFolio
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusFolio));
        DatFolio datFolio;

        public BusFolio()
        {
            datFolio = new DatFolio();
        }

        public IMDResponse<EntDetalleCompra> CSaveNuevoFolio(EntConecktaPago entConecktaPago)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.CSaveNuevoFolio);
            logger.Info(IMDSerialize.Serialize(67823458415062, $"Inicia {metodo}"));

            try
            {
                BusProducto busProducto = new BusProducto();
                IMDResponse<List<EntProducto>> lstProductos = busProducto.BObtenerProductos(null);

                if (lstProductos.Result == null)
                {
                    return response = lstProductos.GetResponse<EntDetalleCompra>();
                }

                for (int i = 0; i < entConecktaPago.lstLineItems.Count; i++)
                {
                    EntProducto oProducto = lstProductos.Result
                        .Find(x => x.iIdProducto == entConecktaPago.lstLineItems[i].product_id);

                    if (oProducto == null)
                    {
                        response.Code = 67823458415839;
                        response.Message = "Producto no válido.";
                        return response;
                    }

                    entConecktaPago.lstLineItems[i].monthsExpiration = oProducto.iMesVigencia;
                    entConecktaPago.lstLineItems[i].name = oProducto.sNombre;
                    entConecktaPago.lstLineItems[i].unit_price = (int)(oProducto.fCosto * 100);
                }

                //Se manda a llamar la creaciión de orden de conekta
                BusOrder busOrder = new BusOrder();

                EntCreateOrder entCreateOrder = new EntCreateOrder();

                string datosSerealizados = JsonConvert.SerializeObject(entConecktaPago);
                entCreateOrder = JsonConvert.DeserializeObject<EntCreateOrder>(datosSerealizados);

                IMDResponse<EntOrder> entOrder = busOrder.BCreateOrder(entCreateOrder);

                string f = JsonConvert.SerializeObject(entOrder.Result);
                IMDResponse<EntRequestOrder> requesOrder = new IMDResponse<EntRequestOrder>();
                requesOrder.Result = JsonConvert.DeserializeObject<EntRequestOrder>(f);

                if (entOrder.Code != 0)
                {
                    return response = entOrder.GetResponse<EntDetalleCompra>();
                }

                response = BGuardarCompraUnica(requesOrder, entConecktaPago);

                response.Code = 0;
                response.Message = "Operación exitosa.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458415839;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458415839, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntDetalleCompra> BGuardarCompraUnica(IMDResponse<EntRequestOrder> entOrder, EntConecktaPago entConecktaPago)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();
            EntDetalleCompra entDetalleCompra = new EntDetalleCompra();

            string metodo = nameof(this.BGuardarCompraUnica);
            logger.Info(IMDSerialize.Serialize(67823458416616, $"Inicia {metodo}(EntRequestOrder entOrder, EntConecktaPago entConecktaPago)", entOrder, entConecktaPago));
            EntDetalleCompra oDetalleCompra = new EntDetalleCompra();


            try
            {
                //Se crea el folio
                EntFolio entFolio = new EntFolio();
                entFolio.iIdEmpresa = entConecktaPago.iIdEmpresa;
                entFolio.iIdOrigen = entConecktaPago.iIdOrigen;
                entFolio.bTerminosYCondiciones = false;
                entFolio.sOrdenConekta = entOrder.Result.id;

                //Se crea el paciente
                EntPaciente entPaciente = new EntPaciente();

                entPaciente.iIdPaciente = 0;
                entPaciente.iIdFolio = entFolio.iIdFolio;
                entPaciente.sTelefono = entConecktaPago.pacienteUnico.sTelefono;
                entPaciente.sCorreo = entConecktaPago.pacienteUnico.sCorreo;
                entPaciente.sNombre = entConecktaPago.pacienteUnico.sNombre;


                entFolio.iConsecutivo = entConecktaPago.lstLineItems.Count;

                foreach (line_items item in entConecktaPago.lstLineItems)
                {
                    for (int i = 0; i < item.quantity; i++)
                    {
                        entFolio.iIdProducto = item.product_id;

                        if (item.monthsExpiration == 0)
                        {
                            entFolio.dtFechaVencimiento = DateTime.Now.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["iDiasDespuesVencimiento"]));
                        }

                        if (item.monthsExpiration != 0)
                        {
                            entFolio.dtFechaVencimiento = DateTime.Now.AddMonths(item.monthsExpiration);
                        }

                        BusGeneratePassword busGenerate = new BusGeneratePassword();
                        BusUsuario busUsuario = new BusUsuario();

                        entFolio.sPassword = busGenerate.Generate(6);

                        entFolio.sPassword = busUsuario.BEncodePassword(entFolio.sPassword);

                        //Se agrega el folio

                        IMDResponse<DataTable> dtFolio = datFolio.DSaveFolio(entFolio);

                        if (dtFolio.Code != 0)
                        {
                            response = dtFolio.GetResponse<EntDetalleCompra>();
                            response.Message = "Ocurrio un erro al guardar el folio";
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
                        IMDResponse<bool> responsePaciente = busPaciente.DSavePaciente(entPaciente);

                        if (responsePaciente.Code != 0)
                        {
                            return response = responsePaciente.GetResponse<EntDetalleCompra>();
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
                    entFolio.iConsecutivo--;
                }

                oDetalleCompra.nTotal = oDetalleCompra.lstArticulos
                    .Sum(x => x.nCantidad * x.nPrecio);
                oDetalleCompra.nTotalPagado = entOrder.Result.amount_paid / 100d;
                oDetalleCompra.nTotalDescuento = entOrder.Result.amount_discount / 100d;
                oDetalleCompra.nTotalIVA = entOrder.Result.amount_tax / 100d;
                oDetalleCompra.sCodigoCupon = entOrder.Result.coupon_code;
                oDetalleCompra.sEmail = entConecktaPago.pacienteUnico.sCorreo;
                oDetalleCompra.sNombre = entConecktaPago.pacienteUnico.sNombre;
                oDetalleCompra.sOrden = entOrder.Result.id;
                oDetalleCompra.sTelefono = entConecktaPago.pacienteUnico.sTelefono;
                oDetalleCompra.bAplicaIVA = entConecktaPago.tax;

                IMDResponse<bool> responseCorreo = this.BEnvioCorreo(oDetalleCompra);

            }
            catch (Exception ex)
            {
                response.Code = 67823458417393;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458417393, $"Error en {metodo}(EntRequestOrder entOrder, EntConecktaPago entConecktaPago): {ex.Message}", entOrder, entConecktaPago, ex, response));
            }
            return response;
        }

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
                        plantillaFolio = plantillaFolio.Replace("item.sDataGroup", $"{itemGroup.sDescripcion} - Vigencia: {itemGroup.dtFechaVencimiento:dd/MM/yyyy - h:mm tt}");
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
                        plantillaFolios = plantillaFolios.Replace("item.sDataGroup", $"{itemGroup.sDescripcion} - Vigencia: {itemGroup.dtFechaVencimiento:dd/MM/yyyy - h:mm tt}");
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458424386, $"Error en {metodo}(EntDetalleCompra oDetalleCompra): {ex.Message}", oDetalleCompra, ex, response));
            }
            return response;
        }
    }
}
