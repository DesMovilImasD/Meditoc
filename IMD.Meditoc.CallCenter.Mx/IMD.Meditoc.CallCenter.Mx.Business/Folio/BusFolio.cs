using IMD.Admin.Conekta.Business;
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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IMD.Meditoc.CallCenter.Mx.Business.Folio
{
    public class BusFolio
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusFolio));
        DatFolio datFolio;
        BusUsuario busUsuario;
        BusConsulta busConsulta;
        BusProducto busProducto;

        public BusFolio()
        {
            datFolio = new DatFolio();
            busUsuario = new BusUsuario();
            busConsulta = new BusConsulta();
            busProducto = new BusProducto();
        }

        public IMDResponse<EntDetalleCompra> BNuevoFolioCompra(EntConecktaPago entConecktaPago)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BNuevoFolioCompra);
            logger.Info(IMDSerialize.Serialize(67823458415062, $"Inicia {metodo}(EntConecktaPago entConecktaPago)", entConecktaPago));

            try
            {
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
                BusOrder busOrder = new BusOrder("MeditocComercial", "Meditoc1");

                EntCreateOrder entCreateOrder = new EntCreateOrder();

                string datosSerealizados = JsonConvert.SerializeObject(entConecktaPago);
                entCreateOrder = JsonConvert.DeserializeObject<EntCreateOrder>(datosSerealizados);

                IMDResponse<EntOrder> entOrder = busOrder.BCreateOrder(entCreateOrder);
                if (entOrder.Code != 0)
                {
                    return response = entOrder.GetResponse<EntDetalleCompra>();
                }

                string f = JsonConvert.SerializeObject(entOrder.Result);
                IMDResponse<EntRequestOrder> requesOrder = new IMDResponse<EntRequestOrder>();
                requesOrder.Result = JsonConvert.DeserializeObject<EntRequestOrder>(f);


                response = BGuardarCompraUnica(requesOrder, entConecktaPago);

                response.Code = 0;
                response.Message = "Operación exitosa.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458415839;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458415839, $"Error en {metodo}(EntConecktaPago entConecktaPago): {ex.Message}", entConecktaPago, ex, response));
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
                EntFolio entFolio = new EntFolio();

                //using (TransactionScope scope = new TransactionScope())
                //{

                //Se asigna empresa al folio
                BusEmpresa busEmpresa = new BusEmpresa();
                IMDResponse<List<EntEmpresa>> respuestaObtenerEmpresas = busEmpresa.BGetEmpresas(null, entConecktaPago.pacienteUnico.sCorreo);

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
                        sNombre = entConecktaPago.pacienteUnico.sNombre,
                        sCorreo = entConecktaPago.pacienteUnico.sCorreo,
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
                        IMDResponse<bool> resGenerarFolio = this.BGenerarFolioCompra(entFolio, entPaciente, item, oDetalleCompra, i);
                        if (resGenerarFolio.Code != 0)
                        {
                            return resGenerarFolio.GetResponse<EntDetalleCompra>();
                        }
                    }
                    entFolio.iConsecutivo--;
                }
                //    scope.Complete();
                //}

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

                response.Code = 0;
                response.Result = oDetalleCompra;
            }
            catch (Exception ex)
            {
                response.Code = 67823458417393;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458417393, $"Error en {metodo}(EntRequestOrder entOrder, EntConecktaPago entConecktaPago): {ex.Message}", entOrder, entConecktaPago, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BGenerarFolioCompra(EntFolio entFolio, EntPaciente entPaciente, line_items item, EntDetalleCompra oDetalleCompra, int i)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BGenerarFolioCompra);
            logger.Info(IMDSerialize.Serialize(67823458549483, $"Inicia {metodo}"));

            try
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458550260, $"Error en {metodo}: {ex.Message}", ex, response));
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

        public IMDResponse<bool> BNuevosFoliosEmpresa(EntFolioxEmpresa entFolioxEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();
            IMDResponse<List<EntEmpresa>> lstEmpresa = new IMDResponse<List<EntEmpresa>>();
            IMDResponse<EntEmpresaDetalleFolio> oDetalleFolioempresa = new IMDResponse<EntEmpresaDetalleFolio>();
            List<EntDetalleCompraArticulo> lstArticulosDetalle = new List<EntDetalleCompraArticulo>();
            oDetalleFolioempresa.Result = new EntEmpresaDetalleFolio();

            string metodo = nameof(this.BNuevosFoliosEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458426717, $"Inicia {metodo}"));

            try
            {
                if (entFolioxEmpresa.iIdEmpresa == 0)
                {
                    response.Code = 67823458426717;
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

                //Se crea el paciente
                EntPaciente entPaciente = new EntPaciente();

                entPaciente.iIdPaciente = 0;
                entPaciente.iIdFolio = entFolio.iIdFolio;
                entPaciente.sTelefono = "9999999999";
                entPaciente.sCorreo = "PPACIENTE@HOTMAIL.COM";
                entPaciente.sNombre = "PACIENTE";

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
                        response.Code = 67823458415839;
                        response.Message = "Producto no válido.";
                        return response;
                    }

                    entFolioxEmpresa.lstLineItems[i].monthsExpiration = oProducto.iMesVigencia;
                    entFolioxEmpresa.lstLineItems[i].name = oProducto.sNombre;
                    entFolioxEmpresa.lstLineItems[i].unit_price = (int)(oProducto.fCosto * 100);
                }

                //using (TransactionScope scope = new TransactionScope())
                //{

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
                    response.Message = "Ocurrio un error al mandar el mensaje";
                    return response;
                }

                response.Code = 0;
                response.Message = "Los folios fueron enviados al siguiente correo " + oDetalleFolioempresa.Result.entEmpresa.sCorreo;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458427494;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458427494, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BGenerarFolioEmpresa(EntFolio entFolio, EntPaciente entPaciente, line_items item, List<EntDetalleCompraArticulo> lstArticulosDetalle, int i)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BGenerarFolioEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458546375, $"Inicia {metodo}"));

            try
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
                    response = dtFolio.GetResponse<bool>();
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458547152, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458424386, $"Error en {metodo}(EntEmpresaDetalleFolio detalleFolio): {ex.Message}", detalleFolio, ex, response));
            }
            return response;
        }

        public IMDResponse<EntDetalleCompra> BNuevaConsulta(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BNuevaConsulta);
            logger.Info(IMDSerialize.Serialize(67823458533943, $"Inicia {metodo}"));


            try
            {

                if (entNuevaConsulta.consulta.iIdColaborador == null | entNuevaConsulta.consulta.iIdColaborador <= 0)
                {
                    response.Code = 8767348576345;
                    response.Message = "No se ingresaron datos del colaborador";
                    return response;
                }

                if (entNuevaConsulta == null || entNuevaConsulta?.consulta == null || entNuevaConsulta?.customerInfo == null)
                {
                    response.Code = 8767348576345;
                    response.Message = "No se ingresaron datos";
                    return response;
                }

                if (entNuevaConsulta?.consulta?.dtFechaProgramadaInicio == null || entNuevaConsulta?.consulta?.dtFechaProgramadaFin == null)
                {
                    response.Code = 8767348576345;
                    response.Message = "No se ingresaron las fechas de consulta";
                    return response;
                }
                if (entNuevaConsulta?.consulta?.dtFechaProgramadaInicio >= entNuevaConsulta?.consulta?.dtFechaProgramadaFin)
                {
                    response.Code = 8767348576345;
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
                    EntDetalleConsulta consultaProgramada = resGetConsultas.Result.First();
                    response.Code = -2000;
                    response.Message = $"Ya hay una consulta programada de {consultaProgramada.dtFechaProgramadaInicio?.ToString("h:mm tt")} a {consultaProgramada.dtFechaProgramadaFin?.ToString("h:mm tt")} para el folio {consultaProgramada.sFolio}. El tiempo de espera entre consultas es de {ConfigurationManager.AppSettings["iMinToleraciaConsultaFin"]} min";
                    return response;
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458534720, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntDetalleCompra> BGenerarConsultaConFolio(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BGenerarConsultaConFolio);
            logger.Info(IMDSerialize.Serialize(67823458541713, $"Inicia {metodo}"));

            try
            {
                IMDResponse<List<EntFolioReporte>> resGetFolioExiste = this.BGetFolios(psFolio: entNuevaConsulta.sFolio, pbActivo: null, pbBaja: null);
                if (resGetFolioExiste.Code != 0)
                {
                    return resGetFolioExiste.GetResponse<EntDetalleCompra>();
                }

                if (resGetFolioExiste.Result.Count != 1)
                {
                    response.Code = 8987837456743;
                    response.Message = "El folio proporcionado no existe";
                    return response;
                }

                EntFolioReporte folioExistente = resGetFolioExiste.Result.First();

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

                if (folioExistente.iIdOrigen == (int)EnumOrigen.Particular)
                {
                    entFolioFV.dtFechaVencimiento = (DateTime)entNuevaConsulta?.consulta?.dtFechaProgramadaFin;
                }
                else
                {
                    entFolioFV.dtFechaVencimiento = (DateTime)(entNuevaConsulta?.consulta?.dtFechaProgramadaFin > folioExistente.dtFechaVencimiento ? entNuevaConsulta?.consulta?.dtFechaProgramadaFin : folioExistente.dtFechaVencimiento);
                }


                IMDResponse<bool> resUpdVencimiento = this.BUpdFechaVencimiento(entFolioFV);
                if (resUpdVencimiento.Code != 0)
                {
                    return resUpdVencimiento.GetResponse<EntDetalleCompra>();
                }

                entNuevaConsulta.consulta.iIdEstatusConsulta = (int)EnumEstatusConsulta.CreadoProgramado;
                entNuevaConsulta.consulta.iIdPaciente = folioExistente.iIdPaciente;
                entNuevaConsulta.customerInfo.email = folioExistente.sCorreoPaciente;

                response = this.BConfirmarNuevaConsulta(entNuevaConsulta, folioExistente.sFolio, folioExistente.sPassword);
            }
            catch (Exception ex)
            {
                response.Code = 67823458542490;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458542490, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntDetalleCompra> BGenerarConsultaSinFolio(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BGenerarConsultaSinFolio);
            logger.Info(IMDSerialize.Serialize(67823458543267, $"Inicia {metodo}"));

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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458544044, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntDetalleCompra> BModificarConsultaFolio(EntNuevaConsulta entNuevaConsulta)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BModificarConsultaFolio);
            logger.Info(IMDSerialize.Serialize(67823458544821, $"Inicia {metodo}"));

            try
            {
                IMDResponse<List<EntDetalleConsulta>> resGetConsulta = busConsulta.BGetDetalleConsulta(entNuevaConsulta.consulta.iIdConsulta);
                if (resGetConsulta.Code != 0)
                {
                    return response = resGetConsulta.GetResponse<EntDetalleCompra>();
                }

                if (resGetConsulta.Result.Count != 1)
                {
                    response.Code = 8767348576345;
                    response.Message = "La consulta no existe";
                    return response;
                }

                EntDetalleConsulta consulta = resGetConsulta.Result.First();

                EntFolioFV entFolioFV = new EntFolioFV
                {
                    dtFechaVencimiento = (DateTime)entNuevaConsulta?.consulta?.dtFechaProgramadaFin?.AddHours(1),
                    iIdEmpresa = (int)consulta.iIdEmpresa,
                    iIdUsuario = entNuevaConsulta.iIdUsuarioMod,
                    lstFolios = new List<EntFolioFVItem>
                        {
                            new EntFolioFVItem
                            {
                                iIdFolio = (int)consulta.iIdFolio
                            }
                        }
                };

                IMDResponse<bool> resUpdVencimiento = this.BUpdFechaVencimiento(entFolioFV);
                if (resUpdVencimiento.Code != 0)
                {
                    return resUpdVencimiento.GetResponse<EntDetalleCompra>();
                }

                entNuevaConsulta.consulta.iIdEstatusConsulta = (int)EnumEstatusConsulta.Reprogramado;
                entNuevaConsulta.consulta.iIdPaciente = consulta.iIdPaciente;
                entNuevaConsulta.customerInfo.email = consulta.sCorreoPaciente;

                response = this.BConfirmarNuevaConsulta(entNuevaConsulta, consulta.sFolio, consulta.sPassword, true);
            }
            catch (Exception ex)
            {
                response.Code = 67823458545598;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458545598, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntDetalleCompra> BConfirmarNuevaConsulta(EntNuevaConsulta entNuevaConsulta, string FolioEnviar, string PassEnviar, bool reprogramado = false)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BConfirmarNuevaConsulta);
            logger.Info(IMDSerialize.Serialize(67823458540159, $"Inicia {metodo}"));

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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458540936, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BEnviarCorreoConsulta(EntDetalleCompra detalleFolio, bool reprogramado = false)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEnvioCorreo);
            logger.Info(IMDSerialize.Serialize(67823458423609, $"Inicia {metodo}(EntEmpresaDetalleFolio detalleFolio)", detalleFolio));

            try
            {
                BusCorreo busCorreo = new BusCorreo();

                string asunto = !reprogramado ? "Meditoc - Consulta programada" : "Meditoc - Consulta reprogramada";
                string para = detalleFolio.sEmail;


                //FOLIOS
                string htmlFolios = string.Empty;
                string plantillaFolio = "<tr><td><table class=\"table-detail\"><tr class=\"group-detail font-unset bold small center\"><td colspan=\"3\">item.sDataGroup</td></tr><tr><td><table class=\"table-detail\"><thead><tr class=\"font-table bold small font-secondary\"><th>Usuario</th><th>Contraseña</th></tr></thead><tbody><tr class=\"font-table bold small center table-border-b\"><td><small>1 -</small>&nbsp;item.sFolio</td><td>item.sPass</td></tr></tbody></table></td></tr></table></td></tr>";
                plantillaFolio = plantillaFolio.Replace("item.sDataGroup", $"Fecha de consulta: {detalleFolio.lstArticulos.First().dtFechaVencimiento:dd/MM/yyyy - h:mm tt}");
                plantillaFolio = plantillaFolio.Replace("item.sFolio", detalleFolio.lstArticulos.First().sFolio);
                plantillaFolio = plantillaFolio.Replace("item.sPass", detalleFolio.lstArticulos.First().sPass);

                htmlFolios += plantillaFolio;

                string plantillaBody = "<!DOCTYPE html><html><head><meta charset=\"utf-8;\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><link href=\"https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap\" rel=\"stylesheet\" /><style>body {font-family: Roboto, \"Segoe UI\", Tahoma, Geneva, Verdana, sans-serif;margin: 0;}.center {text-align: center !important;}.right {text-align: right !important;}.left {text-align: left !important;}.light {font-weight: 300;}.normal {font-weight: normal;}.bold {font-weight: 500;}.small {font-size: 12px;}.large {font-size: 15px;}.font-default {color: #707070;}.font-primary {color: #11b6ca;}.font-secondary {color: #115c8a;}.font-unset {color: #ffffff;}.font-table {color: #878787;}.table {margin: auto;width: 100%;max-width: 800px;border: 1px solid #dddddd;border-spacing: 0px;border-collapse: 0px;}.table td {padding: 6px 0px;}.logo-head {background-color: #11b6ca;padding: 5px 0px;}.table-content {margin: auto;width: 90%;border-collapse: collapse;}.table-detail {margin: auto;width: 100%;border-collapse: collapse;}.table-detail td {padding: 8px;vertical-align: top;}.head-detail {background-color: #115c8a;}.product-detail {border-bottom: 1px solid #989898;}.total-detail {background-color: #989898;}.group-detail {background-color: #11b6ca;}.divider {height: 1px;border: 0;background-color: #989898;}.link {text-decoration: none;}.link:hover {text-decoration: underline;}.link-none {text-decoration: none;}.table-border {border-right: 10px solid #115c8a;}.table-border-l {border-left: 10px solid #115c8a;}.table-border-b td {border-bottom: 1px solid #ccc;}</style></head><body><table class=\"table\"><tr><td class=\"logo-head center\"><img alt=\"logo-meditoc\" src=\"sLogoMeditoc\" height=\"50px\" /></td></tr><tr><td><table class=\"table-content\"><tr><td class=\"center\"><span class=\"font-default bold large\"> Gracias por su usar nuestros servicios </span></td></tr><tr><td class=\"center\"><span class=\"font-default normal large\"> Fecha de solicitud de consulta: oDetalleCompra.sFechaCompra </span></td></tr><tr class=\"center\"><td><span class=\"font-default normal large\">Guarda tus credenciales, te servirán para acceder a Meditoc:</span></td></tr>oDetalleCompra.folios<tr class=\"center\"><td><p><span class=\"font-default normal large\">Para utilizar el servicio, descarga la app “Meditoc 360” disponible en Appstore y Playstore.</span></p></td></tr><tr class=\"center\"><td><span><a href=\"sLinkApple\" target=\"_blank\"class=\"link-none\"><img src=\"sLogoApple\" height=\"50px\" width=\"150px\"alt=\"APP\" /></a></span><span><a href=\"sLinkPlay\"target=\"_blank\" class=\"link-none\"><img src=\"sLogoPlay\" height=\"50px\" width=\"150px\"alt=\"PLAY\" /></a></span></td></tr><tr><td><hr class=\"divider\" /></td></tr><tr><td><span class=\"font-default light small\">De conformidad con la ley federal de protección de datos personales en posesión de los particulares, ponemos a su disposición nuestro&nbsp;<a href=\"sAvisoPrivacidad\" class=\"link font-secondary normal\"> Aviso de Privacidad </a>&nbsp;y&nbsp;<a href=\"sTerminosCondiciones\" class=\"link font-secondary normal\"> Términos y Condiciones. </a></span></td></tr></table></td></tr></table></body></html>";

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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458424386, $"Error en {metodo}(EntEmpresaDetalleFolio detalleFolio): {ex.Message}", detalleFolio, ex, response));
            }
            return response;
        }

        public IMDResponse<EntFolio> BLoginApp(string sUsuario, string sPassword)
        {
            IMDResponse<EntFolio> response = new IMDResponse<EntFolio>();
            BusUsuario busUsuario = new BusUsuario();
            EntFolio entFolio = new EntFolio();

            string metodo = nameof(this.BLoginApp);
            logger.Info(IMDSerialize.Serialize(67823458429825, $"Inicia {metodo}(string sUsuario, string sPassword)", sUsuario, sPassword));

            try
            {
                response.Code = 67823458429825;

                if (sUsuario == "")
                {
                    response.Message = "El usuario no puede ser vacio";
                    return response;
                }

                if (sPassword == "")
                {
                    response.Message = "La contraseña no puede ser vacia";
                    return response;
                }

                sPassword = busUsuario.BEncodePassword(sPassword);

                IMDResponse<DataTable> dtLoginApp = datFolio.DLoginApp(sUsuario, sPassword);

                if (dtLoginApp.Code != 0)
                {
                    return response = dtLoginApp.GetResponse<EntFolio>();
                }

                if (dtLoginApp.Result.Rows.Count == 0)
                {
                    response.Message = "Su usuario o contraseña es invalida";
                    return response;
                }

                foreach (DataRow item in dtLoginApp.Result.Rows)
                {
                    IMDDataRow dataRow = new IMDDataRow(item);

                    entFolio.iIdFolio = dataRow.ConvertTo<int>("iIdFolio");
                    entFolio.iIdProducto = dataRow.ConvertTo<int>("iIdTipoProducto");
                    entFolio.sFolio = dataRow.ConvertTo<string>("sFolio");
                    entFolio.sPassword = dataRow.ConvertTo<string>("sPassword");
                    entFolio.dtFechaVencimiento = dataRow.ConvertTo<DateTime>("dtFechaVencimiento");
                    entFolio.bTerminosYCondiciones = Convert.ToBoolean(dataRow.ConvertTo<int>("bTerminosYCondiciones"));
                    entFolio.bEsAgendada = Convert.ToBoolean(dataRow.ConvertTo<int>("bEsAgendada"));
                }

                if (entFolio.dtFechaVencimiento < DateTime.Now)
                {
                    response.Message = "Su folio a expirado.";
                    return response;
                }

                response.Code = 0;
                response.Message = "Login correcto";
                response.Result = entFolio;
            }
            catch (Exception ex)
            {
                response.Code = 67823458430602;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458430602, $"Error en {metodo}(string sUsuario, string sPassword): {ex.Message}", sUsuario, sPassword, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntFolioReporte>> BGetFolios(int? piIdFolio = null, int? piIdEmpresa = null, int? piIdProducto = null, int? piIdOrigen = null, string psFolio = null, string psOrdenConekta = null, bool? pbTerminosYCondiciones = null, bool? pbActivo = true, bool? pbBaja = false)
        {
            IMDResponse<List<EntFolioReporte>> response = new IMDResponse<List<EntFolioReporte>>();

            string metodo = nameof(this.BGetFolios);
            logger.Info(IMDSerialize.Serialize(67823458434487, $"Inicia {metodo}", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja));

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
                        bTerminosYCondiciones = dr.ConvertTo<bool>("bTerminosYCondiciones"),
                        dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion"),
                        dtFechaVencimiento = dr.ConvertTo<DateTime?>("dtFechaVencimiento"),
                        iConsecutivo = dr.ConvertTo<int>("iConsecutivo"),
                        iIdEmpresa = dr.ConvertTo<int>("iIdEmpresa"),
                        iIdFolio = dr.ConvertTo<int>("iIdFolio"),
                        iIdPaciente = dr.ConvertTo<int>("iIdPaciente"),
                        iIdOrigen = dr.ConvertTo<int>("iIdOrigen"),
                        iIdProducto = dr.ConvertTo<int>("iIdProducto"),
                        iIdTipoProducto = dr.ConvertTo<int>("iIdTipoProducto"),
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

                response.Code = 0;
                response.Message = "Folios consultados";
                response.Result = lstFolios;
            }
            catch (Exception ex)
            {
                response.Code = 67823458435264;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458435264, $"Error en {metodo}: {ex.Message}", piIdFolio, piIdEmpresa, piIdProducto, piIdOrigen, psFolio, psOrdenConekta, pbTerminosYCondiciones, pbActivo, pbBaja, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BUpdFechaVencimiento(EntFolioFV entFolioFV)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BUpdFechaVencimiento);
            logger.Info(IMDSerialize.Serialize(67823458439149, $"Inicia {metodo}"));

            try
            {
                if (entFolioFV == null)
                {
                    response.Code = 345678453;
                    response.Message = "Por favor, ingrese los datos para continuar";
                    return response;
                }

                if (entFolioFV.lstFolios == null || entFolioFV.lstFolios?.Count < 1)
                {
                    response.Code = 345678453;
                    response.Message = "Por favor, seleccione los folios para continuar";
                    return response;
                }

                //using (TransactionScope scope = new TransactionScope())
                //{
                foreach (EntFolioFVItem folio in entFolioFV.lstFolios)
                {
                    IMDResponse<bool> respuestaUpdFecha = datFolio.DUpdFechaVencimiento(entFolioFV.iIdEmpresa, folio.iIdFolio, entFolioFV.dtFechaVencimiento, entFolioFV.iIdUsuario);
                    if (respuestaUpdFecha.Code != 0)
                    {
                        return respuestaUpdFecha;
                    }
                    //}
                    //scope.Complete();
                }

                response.Code = 0;
                response.Message = "Folios actualizados";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458439926;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458439926, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BEliminarFoliosEmpresa(EntFolioFV entFolioFV)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BEliminarFoliosEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458443811, $"Inicia {metodo}"));

            try
            {
                if (entFolioFV == null)
                {
                    response.Code = 345678453;
                    response.Message = "Por favor, ingrese los datos para continuar";
                    return response;
                }

                if (entFolioFV.lstFolios == null || entFolioFV.lstFolios?.Count < 1)
                {
                    response.Code = 345678453;
                    response.Message = "Por favor, seleccione los folios para continuar";
                    return response;
                }

                //using (TransactionScope scope = new TransactionScope())
                //{
                foreach (EntFolioFVItem folio in entFolioFV.lstFolios)
                {
                    IMDResponse<bool> respuestaUpdFecha = datFolio.DEliminarFoliosEmpresa(entFolioFV.iIdEmpresa, folio.iIdFolio, entFolioFV.iIdUsuario);
                    if (respuestaUpdFecha.Code != 0)
                    {
                        return respuestaUpdFecha;
                    }
                }
                //    scope.Complete();
                //}

                response.Code = 0;
                response.Message = "Folios actualizados";
                response.Result = true;
            }
            catch (Exception ex)
            {

                response.Code = 67823458444588;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458444588, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BTerminosYCondiciones(string sFolio = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BTerminosYCondiciones);
            logger.Info(IMDSerialize.Serialize(67823458462459, $"Inicia {metodo}"));

            try
            {
                if (sFolio == null || sFolio == "")
                {
                    response.Code = 67823458462459;
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458463236, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458500532, $"Error en {metodo}: {ex.Message}(string sFolio = null, string sPassword = null)", sFolio, sPassword, ex, response));
            }
            return response;
        }
    }
}
