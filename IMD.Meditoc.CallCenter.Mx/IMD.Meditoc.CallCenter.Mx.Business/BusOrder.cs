using IMD.Admin.Conekta.Data;
using IMD.Admin.Conekta.Entities;
using IMD.Admin.Conekta.Entities.Orders;
using IMD.Admin.Conekta.Entities.Promotions;
using IMD.Admin.Conekta.Services;
using IMD.Admin.Conekta.Web.Business;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Admin.Conekta.Business
{
    public class BusOrder
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusOrder));

#if DEBUG
        private ServOrder servOrder;
        private DatOrder datOrder;
        private BusAgent busAgent;
        private BusPromociones busPromociones;
#else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ServOrder servOrder;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DatOrder datOrder;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BusAgent busAgent;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BusPromociones busPromociones;
#endif
        public BusOrder()
        {
            servOrder = new ServOrder();
            datOrder = new DatOrder();
            busAgent = new BusAgent();
            busPromociones = new BusPromociones();
        }

        /// <summary>
        /// Función: Crea un orden con los datos de la compra
        /// Creado: Cristopher Noh 03/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="entCreateOrder">Datos de la compra</param>
        /// <returns>Datos de la orden generada</returns>
        public IMDResponse<EntOrder> BCreateOrder(EntCreateOrder entCreateOrder)
        {
            IMDResponse<EntOrder> response = new IMDResponse<EntOrder>();

            string metodo = nameof(this.BCreateOrder);
            logger.Info(IMDSerialize.Serialize(67823458108147, $"Inicia {metodo}(EntCreateOrder entCreateOrder)", entCreateOrder));

            try
            {
                string defaultPhoneNumber = "+525555555555";
                if (entCreateOrder == null)
                {
                    response.Code = 67823458112809;
                    response.Message = "No se ingresaron datos de la compra";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(entCreateOrder.currency))
                {
                    response.Code = 67823458217704;
                    response.Message = "No se ingresó el tipo de moneda";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(entCreateOrder.customer_info?.email))
                {
                    response.Code = 67823458113586;
                    response.Message = "El correo electrónico es requerido";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(entCreateOrder.customer_info.name))
                {
                    response.Code = 45654345434543;
                    response.Message = "El nombre del cliente es requerido";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(entCreateOrder.customer_info.phone))
                {
                    entCreateOrder.customer_info.phone = defaultPhoneNumber;
                }
                else
                {
                    entCreateOrder.customer_info.phone = entCreateOrder.customer_info.phone.Replace(" ", "");
                    if (entCreateOrder.customer_info.phone.Length != 10)
                    {
                        response.Code = 67823458114363;
                        response.Message = "El número de teléfono proporcionado debe tener 10 dígitos";
                        return response;
                    }
                    else
                    {

                        entCreateOrder.customer_info.phone = $"{ConfigurationManager.AppSettings["CONEKTA_PHONE_ACCESS"]}{entCreateOrder.customer_info.phone}";
                    }
                }

                if (entCreateOrder.line_items?.Count == 0 || entCreateOrder.line_items == null)
                {
                    response.Code = 67823458114363;
                    response.Message = "No se ingresaron artículos para generar la orden";
                    return response;
                }

                foreach (EntCreateLineItem articulo in entCreateOrder.line_items)
                {
                    if (string.IsNullOrWhiteSpace(articulo?.name) || articulo?.quantity <= 0 || articulo?.unit_price <= 0)
                    {
                        response.Code = 67823458115140;
                        response.Message = "La información de los artículos es incompleta";
                        return response;
                    }
                }

                string mensajeErrorMetodoPago = "No se ingresó información de pago";

                if (entCreateOrder.charges?.Count == 0)
                {
                    response.Code = 67823458115917;
                    response.Message = mensajeErrorMetodoPago;
                    return response;
                }
                if (entCreateOrder.charges?[0]?.payment_method == null)
                {
                    response.Code = 67823458116694;
                    response.Message = mensajeErrorMetodoPago;
                    return response;
                }

                entCreateOrder.charges[0].amount = entCreateOrder.line_items.Sum(articulo => articulo.unit_price * articulo.quantity);

                if (entCreateOrder.charges[0].payment_method.type == "oxxo_cash")
                {
                    int horasLimitePagoOxxo = 24;
                    try
                    {
                        int configurationHorasLimitePagoOxxo = Convert.ToInt32(ConfigurationManager.AppSettings["CONEKTA_CASH_EXPIRE_HOURS"]);
                        horasLimitePagoOxxo = configurationHorasLimitePagoOxxo <= 0 ? 24 : configurationHorasLimitePagoOxxo;
                    }
                    catch (Exception)
                    {
                    }
                    DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    long unixTimeStampInTicks = (DateTime.Now.AddHours(horasLimitePagoOxxo).ToUniversalTime() - unixStart).Ticks;
                    entCreateOrder.charges[0].payment_method.expires_at = unixTimeStampInTicks / TimeSpan.TicksPerSecond;
                }
                else if (entCreateOrder.charges[0].payment_method.type == "card")
                {
                    if (string.IsNullOrWhiteSpace(entCreateOrder.charges[0].payment_method.token_id))
                    {
                        response.Code = 67823458117471;
                        response.Message = mensajeErrorMetodoPago;
                        return response;
                    }
                }
                else
                {
                    response.Code = 67823458118248;
                    response.Message = mensajeErrorMetodoPago;
                    return response;
                }

                long amount = entCreateOrder.charges[0].amount;
                bool aplicaPromocion = false;
                string couponCode = null;
                long discount = 0;

                if (entCreateOrder.coupon != null)
                {
                    IMDResponse<EntCupon> respuestaValidarPromocion = busPromociones.BValidarCupon(piIdCupon: entCreateOrder.coupon);
                    if (respuestaValidarPromocion.Code != 0)
                    {
                        entCreateOrder.coupon = null;
                    }
                    else
                    {
                        double porcentajeDescuentoMaximo = Convert.ToDouble(ConfigurationManager.AppSettings["CONEKTA_DESCUENTO_MAXIMO"]);

                        switch (respuestaValidarPromocion.Result.fiIdCuponCategoria)
                        {
                            case (int)EnumCategoriaCupon.DescuentoMonto:
                                double descuentoMaximo = entCreateOrder.charges[0].amount * porcentajeDescuentoMaximo;

                                if (respuestaValidarPromocion.Result.fnMontoDescuento > descuentoMaximo)
                                {
                                    respuestaValidarPromocion.Result.fnMontoDescuento = descuentoMaximo;
                                }
                                discount = (long)respuestaValidarPromocion.Result.fnMontoDescuento;
                                entCreateOrder.discount_lines = new List<EntCreateDiscountLine>
                                {
                                    new EntCreateDiscountLine
                                    {
                                        type="coupon",
                                        amount = discount,
                                        code = respuestaValidarPromocion.Result.fsCodigo
                                    }
                                };
                                aplicaPromocion = true;
                                couponCode = respuestaValidarPromocion.Result.fsCodigo;
                                break;

                            case (int)EnumCategoriaCupon.DescuentoPorcentaje:
                                if (respuestaValidarPromocion.Result.fnPorcentajeDescuento > porcentajeDescuentoMaximo)
                                {
                                    respuestaValidarPromocion.Result.fnPorcentajeDescuento = porcentajeDescuentoMaximo;
                                }

                                discount = (long)(entCreateOrder.charges[0].amount * respuestaValidarPromocion.Result.fnPorcentajeDescuento);
                                entCreateOrder.discount_lines = new List<EntCreateDiscountLine>
                                {
                                    new EntCreateDiscountLine
                                    {
                                        type="coupon",
                                        amount = discount,
                                        code = respuestaValidarPromocion.Result.fsCodigo
                                    }
                                };
                                aplicaPromocion = true;
                                couponCode = respuestaValidarPromocion.Result.fsCodigo;
                                break;
                        }
                    }
                }

                entCreateOrder.charges[0].amount -= discount;

                long tax = 0;

                double taxIVA = Convert.ToDouble(ConfigurationManager.AppSettings["CONEKTA_IMPUESTO"]);

                if (taxIVA > 0d)
                {
                    tax = (long)(entCreateOrder.charges[0].amount * taxIVA);

                    entCreateOrder.tax_lines = new List<EntCreateTaxLine> {
                        new EntCreateTaxLine
                        {
                            amount = tax,
                            description = "IVA"
                        }
                    };
                }

                entCreateOrder.charges[0].amount += tax;

                IMDResponse<EntCreateUserAgent> respuestaObtenerAgenteUsuario = busAgent.BGetUserAgent();
                if (respuestaObtenerAgenteUsuario.Code != 0)
                {
                    return respuestaObtenerAgenteUsuario.GetResponse<EntOrder>();
                }

                int[] mesesSinInteresesValidos = { 3, 6, 9, 12, 18 };

                List<string> ocultarPropiedades = new List<string> { "coupon", "tax", "product_id", "monthsExpiration", "iIdOrigen" };

                if (!mesesSinInteresesValidos.Contains(entCreateOrder.charges[0].payment_method.monthly_installments))
                {
                    ocultarPropiedades.Add("monthly_installments");
                }

                IMDResponse<string> respuestaServicioCrearOrden = servOrder.SCreateOrder(entCreateOrder, respuestaObtenerAgenteUsuario.Result, ocultarPropiedades.ToArray());
                if (respuestaServicioCrearOrden.Code != 0 && respuestaServicioCrearOrden.Code != -1500000)
                {
                    return respuestaServicioCrearOrden.GetResponse<EntOrder>();
                }

                ///Guardar respuesta textual del servicio en log 
                ///Intentar convetir la respuesta en la entidad EntOrder
                ///Si no se puede convertir solo lo dejamos en null pero no se marca error porque la compra ya se abrá realizado
                logger.Info(IMDSerialize.Serialize(67823458119025, $"Respuesta del servicio pago por Conekta", entCreateOrder, respuestaObtenerAgenteUsuario.Result, respuestaServicioCrearOrden));
                EntOrder entOrder = null;
                try
                {
                    if (respuestaServicioCrearOrden.Code == 0)
                    {
                        entOrder = JsonConvert.DeserializeObject<EntOrder>(respuestaServicioCrearOrden.Result);
                    }
                    else
                    {
                        EntOrderDeclined entOrderDeclined = JsonConvert.DeserializeObject<EntOrderDeclined>(respuestaServicioCrearOrden.Result);
                        if (entOrderDeclined != null)
                        {
                            entOrder = entOrderDeclined.data;
                            if(entOrder != null)
                            {
                                entOrder.payment_status = entOrder.charges?.data[0]?.status;
                            }
                        }
                    }
                    if (entOrder != null)
                    {
                        entOrder.amount = amount;
                        entOrder.amount_paid = entCreateOrder.charges[0].amount;
                        entOrder.coupon_code = couponCode;
                        entOrder.amount_discount = discount;
                        entOrder.amount_tax = tax;
                        if (entOrder.customer_info?.phone == defaultPhoneNumber)
                        {
                            entOrder.customer_info.phone = null;
                        }
                    }
                }
                catch (Exception)
                {
                    entOrder = null;
                }

                string guardarOrdenEnBD = ConfigurationManager.AppSettings["CONEKTA_SAVE_DB"];
                if (Convert.ToBoolean(guardarOrdenEnBD) && entOrder != null)
                {
                    IMDResponse<bool> respuestaGuardarBD = this.BSaveOrder(entOrder, entCreateOrder);
                    if (respuestaGuardarBD.Code != 0)
                    {
                        logger.Error(IMDSerialize.Serialize(67823458119802, $"Error en guardado en base de datos de orden Conekta", entOrder, respuestaGuardarBD));
                    }
                }
                if (respuestaServicioCrearOrden.Code == -1500000)
                {
                    response.Code = respuestaServicioCrearOrden.Code;
                    response.Result = entOrder;
                    response.Message = respuestaServicioCrearOrden.Message;
                }
                else
                {
                    if (aplicaPromocion)
                    {
                        IMDResponse<bool> respuestaAplicarCupon = busPromociones.BAplicarCupon((int)entCreateOrder.coupon);
                        if (respuestaAplicarCupon.Code != 0)
                        {
                            logger.Error(IMDSerialize.Serialize(67823458119803, $"Error en aplicar promoción", entCreateOrder));

                        }
                    }

                    response.Code = 0;
                    response.Result = entOrder;
                    response.Message = "Orden creada";
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458108924;
                response.Message = "No pudimos procesar el pago de tu pedido, revisa nuevamente los datos ingresados o intenta con otra tarjeta.";

                logger.Error(IMDSerialize.Serialize(67823458108924, $"Error en {metodo}(EntCreateOrder entCreateOrder): {ex.Message}", entCreateOrder, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Consulta los datos de una orden generada en Conekta
        /// Creado: Cristopher Noh 03/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="orderId">Id de ordan Conekta</param>
        /// <returns>Datos de la orden</returns>
        public IMDResponse<EntOrder> BGetOrder(string orderId)
        {
            IMDResponse<EntOrder> response = new IMDResponse<EntOrder>();

            string metodo = nameof(this.BGetOrder);
            logger.Info(IMDSerialize.Serialize(67823458109701, $"Inicia {metodo}(string orderId)", orderId));

            try
            {
                if (string.IsNullOrWhiteSpace(orderId))
                {
                    response.Code = 67823458120579;
                    response.Message = "No se ingresó el id de la orden";
                    return response;
                }

                IMDResponse<EntCreateUserAgent> respuestaObtenerAgenteUsuario = busAgent.BGetUserAgent();
                if (respuestaObtenerAgenteUsuario.Code != 0)
                {
                    return respuestaObtenerAgenteUsuario.GetResponse<EntOrder>();
                }

                IMDResponse<string> respuestaServicioConsultarOrden = servOrder.SGetOrder(orderId, respuestaObtenerAgenteUsuario.Result);
                if (respuestaServicioConsultarOrden.Code != 0)
                {
                    return respuestaServicioConsultarOrden.GetResponse<EntOrder>();
                }

                logger.Info(IMDSerialize.Serialize(67823458218481, $"Respuesta del servicio consulta por Conekta", orderId, respuestaObtenerAgenteUsuario.Result, respuestaServicioConsultarOrden));


                EntOrder entOrder = null;
                try
                {
                    entOrder = JsonConvert.DeserializeObject<EntOrder>(respuestaServicioConsultarOrden.Result);
                }
                catch (Exception)
                {
                    entOrder = null;
                }

                response.Code = 0;
                response.Result = entOrder;
                response.Message = entOrder == null ? respuestaServicioConsultarOrden.Result : "Orden obtenida";
            }
            catch (Exception ex)
            {
                response.Code = 67823458110478;
                response.Message = "Ocurrió un error inesperado al recopilar la información de la orden";

                logger.Error(IMDSerialize.Serialize(67823458110478, $"Error en {metodo}(string orderId): {ex.Message}", orderId, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Guarda los datos de la orden generada en la base de datos proporcionada
        /// Creado: Cristopher Noh 03/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="entOrder">Datos de la orden</param>
        /// <param name="piIdCupon">Cupón si aplicó</param>
        /// <returns>Guardado correcto</returns>
        private IMDResponse<bool> BSaveOrder(EntOrder entOrder, EntCreateOrder entCreateOrder)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveOrder);
            logger.Info(IMDSerialize.Serialize(67823458111255, $"Inicia {metodo}(EntOrder entOrder, int? piIdCupon = null)", entOrder, entCreateOrder));

            try
            {
                Guid uid = Guid.NewGuid();

                string origin = "conekta_service";
                //using (TransactionScope scope = new TransactionScope())
                //{
                IMDResponse<bool> respuestaGuardarOrden = datOrder.DSaveConektaOrder(uid, entOrder, origin, entCreateOrder.iIdOrigen, entCreateOrder.coupon);
                if (respuestaGuardarOrden.Code != 0)
                {
                    return respuestaGuardarOrden;
                }

                IMDResponse<bool> respuestaGuardarCliente = datOrder.DSaveCustomerInfo(uid, entOrder.customer_info);
                if (respuestaGuardarCliente.Code != 0)
                {
                    return respuestaGuardarCliente;
                }

                int consecutivoArticulo = 0;
                for (int i = 0; i < entOrder.line_items.data.Count; i++)
                {
                    EntCreateLineItem itemRef = entCreateOrder.line_items.Find(x => x.name == entOrder.line_items.data[i].name);
                    entOrder.line_items.data[i].consecutive = ++consecutivoArticulo;
                    entOrder.line_items.data[i].product_id = itemRef.product_id;
                    entOrder.line_items.data[i].months_expiration = itemRef.monthsExpiration;
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

                //    scope.Complete();
                //}

                response.Code = 0;
                response.Message = "Guardado completo";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458112032;
                response.Message = "Ocurrió un error inesperado al guardar la información en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458112032, $"Error en {metodo}(EntOrder entOrder, int? piIdCupon = null): {ex.Message}", entOrder, entCreateOrder, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Obtiene el GUID de la orden previamente almacenada
        /// Creado: Cristopher Noh 03/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="psOrderId">ID de orden Conekta</param>
        /// <returns></returns>
        public IMDResponse<Guid> BGetOrderGuid(string psOrderId)
        {
            IMDResponse<Guid> response = new IMDResponse<Guid>();

            string metodo = nameof(this.BGetOrderGuid);
            logger.Info(IMDSerialize.Serialize(67823458160206, $"Inicia {metodo}(string psOrderId)", psOrderId));

            try
            {
                if (string.IsNullOrEmpty(psOrderId))
                {
                    response.Code = 67823458219258;
                    response.Message = "No se ha podido determinar el ID de la orden";
                    return response;
                }

                IMDResponse<DataTable> respuestaGetOrderDB = datOrder.DGetOrder(psOrderId);
                if (respuestaGetOrderDB.Code != 0)
                {
                    return respuestaGetOrderDB.GetResponse<Guid>();
                }
                if (respuestaGetOrderDB.Result.Rows.Count < 1)
                {
                    response.Code = 67823458220035;
                    response.Message = "No se encontró la orden en la base de datos";
                    return response;
                }

                Guid uID = new Guid(respuestaGetOrderDB.Result.Rows[0]["uId"].ToString());

                response.Code = 0;
                response.Message = "uId obtenido";
                response.Result = uID;
            }
            catch (Exception ex)
            {
                response.Code = 67823458160983;
                response.Message = "Ocurrió un error inesperado al consultar el detalle de orden";

                logger.Error(IMDSerialize.Serialize(67823458160983, $"Error en {metodo}(string psOrderId): {ex.Message}", psOrderId, ex, response));
            }
            return response;
        }
    }
}
