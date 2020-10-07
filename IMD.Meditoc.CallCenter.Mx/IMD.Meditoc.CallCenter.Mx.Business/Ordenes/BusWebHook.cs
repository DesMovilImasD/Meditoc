using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IMD.Meditoc.CallCenter.Mx.Business.Ordenes
{
    public class BusWebHook
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusWebHook));

#if DEBUG
        private BusOrder busOrder;
        private DatOrder datOrder;
#else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BusOrder busOrder;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DatOrder datOrder;
#endif
        public BusWebHook()
        {
            busOrder = new BusOrder();
            datOrder = new DatOrder();
        }

        /// <summary>
        /// Función: Actualiza los status de la orden escuchando las llamadas de conekta
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="entWebHook"></param>
        /// <returns></returns>
        public IMDResponse<bool> BUpdateState(EntWebHook entWebHook)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BUpdateState);
            logger.Info(IMDSerialize.Serialize(67823458158652, $"Inicia {metodo}(EntWebHook entWebHook)", entWebHook));

            try
            {
                if (entWebHook == null)
                {
                    response.Code = 65723765236345;
                    response.Message = "No se ingresó información de la orden.";
                    return response;
                }
                IMDResponse<Guid> respuestaObtenerUID = new IMDResponse<Guid>();
                if (entWebHook.data?.@object?.@object == "order")
                {
                    respuestaObtenerUID = busOrder.BGetOrderGuid(entWebHook.data?.@object?.id);
                }
                else if (entWebHook.data?.@object?.@object == "charge")
                {
                    respuestaObtenerUID = busOrder.BGetOrderGuid(entWebHook.data?.@object?.order_id);
                }
                else
                {
                    if (entWebHook.@object?.@object == "order")
                    {
                        respuestaObtenerUID = busOrder.BGetOrderGuid(entWebHook.@object?.id);
                    }
                    else if (entWebHook.@object?.@object == "charge")
                    {
                        respuestaObtenerUID = busOrder.BGetOrderGuid(entWebHook.@object?.order_id);
                    }
                    else
                    {
                        response.Code = 68763459686234;
                        response.Message = "El tipo de objeto no es ORDER ni CHARGE.";
                        return response;
                    }
                }

                if (respuestaObtenerUID.Code != 0)
                {
                    return respuestaObtenerUID.GetResponse<bool>();
                }

                Guid uId = respuestaObtenerUID.Result;
                string origin = "conekta_webhook";

                if (entWebHook.type == "order.created")
                {
                    string status = "order created";
                    EntOrder entOrder = new EntOrder
                    {
                        payment_status = status,
                        charges = new EntCharge
                        {
                            data = new List<EntChargeDetail>
                            {
                               new EntChargeDetail
                               {
                                   status = status,
                                   payment_method = new EntPaymentMehod()
                               }
                            }
                        }
                    };

                    string mensajeErrorGuardado = "";

                    IMDResponse<bool> respuestaGuardarOrden = datOrder.DSaveConektaOrder(uId, entOrder, origin);
                    IMDResponse<bool> respuestaGuardarCargo = datOrder.DSaveCharge(uId, entOrder.charges.data.First(), origin);
                    if (respuestaGuardarOrden.Code != 0 || respuestaGuardarCargo.Code != 0)
                    {
                        mensajeErrorGuardado = " No a sido posible guardar en la base de datos.";
                    }

                    response.Code = 0;
                    response.Message = $"Se actualizó la orden con uID {uId} a: {status}.{mensajeErrorGuardado}";
                    response.Result = true;
                }
                else
                {
                    string statusOrder = null;
                    if (entWebHook.data?.@object?.@object == "order")
                    {
                        statusOrder = entWebHook.data?.@object?.payment_status;
                    }
                    else if (entWebHook.data?.@object?.@object == "charge")
                    {
                        statusOrder = entWebHook.data?.@object?.status;
                    }
                    else
                    {
                        if (entWebHook.@object?.@object == "order")
                        {
                            statusOrder = entWebHook.@object?.payment_status;
                        }
                        else if (entWebHook.@object?.@object == "charge")
                        {
                            statusOrder = entWebHook.@object?.status;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(statusOrder))
                    {
                        response.Code = 687634596456434;
                        response.Message = "No se pudo determinar el status de la orden.";
                        return response;
                    }

                    string statusCharge = entWebHook.data?.@object?.charges?.data?[0]?.status;
                    if (string.IsNullOrWhiteSpace(statusCharge))
                    {
                        statusCharge = entWebHook.@object?.charges?.data?[0]?.status;
                        if (string.IsNullOrWhiteSpace(statusCharge))
                        {
                            statusCharge = statusOrder;
                        }
                    }

                    EntOrder entOrder = new EntOrder
                    {
                        payment_status = statusOrder,
                        charges = new EntCharge
                        {
                            data = new List<EntChargeDetail>
                            {
                               new EntChargeDetail
                               {
                                   status = statusCharge,
                                   payment_method = new EntPaymentMehod()
                               }
                            }
                        }
                    };

                    string mensajeErrorGuardado = "";
                    IMDResponse<bool> respuestaGuardarOrden = datOrder.DSaveConektaOrder(uId, entOrder, origin);
                    IMDResponse<bool> respuestaGuardarCargo = datOrder.DSaveCharge(uId, entOrder.charges.data.First(), origin);
                    if (respuestaGuardarOrden.Code != 0 || respuestaGuardarCargo.Code != 0)
                    {
                        mensajeErrorGuardado = " No a sido posible guardar en la base de datos.";
                    }

                    response.Code = 0;
                    response.Message = $"Se actualizó la orden con uID {uId} a: {statusOrder}.{mensajeErrorGuardado}";
                    response.Result = true;
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458159429;
                response.Message = "Ocurrió un error al procesar la información de la orden.";

                logger.Error(IMDSerialize.Serialize(67823458159429, $"Error en {metodo}(EntWebHook entWebHook): {ex.Message}", entWebHook, ex, response));
            }
            return response;
        }
    }
}
