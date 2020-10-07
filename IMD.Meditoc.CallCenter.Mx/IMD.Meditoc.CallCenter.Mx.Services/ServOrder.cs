using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace IMD.Meditoc.CallCenter.Mx.Services
{
    public class ServOrder
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ServOrder));

        private readonly string urlServicioConektaCrearOrden;

#if DEBUG
        private readonly string conketaApiKey;
        private readonly string conketaVersion;
        private readonly string conektaLocale;
#else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string conketaApiKey;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string conketaVersion;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string conektaLocale;
#endif

        public ServOrder()
        {
            urlServicioConektaCrearOrden = ConfigurationManager.AppSettings["CONEKTA_ORDERS"];
            string conektaApiKeyEncriptada = ConfigurationManager.AppSettings["CONEKTA_APIKEY"];

            IMDEndec imdEndec = new IMDEndec();
            conketaApiKey = imdEndec.BDecrypt(conektaApiKeyEncriptada, "MeditocComercial", "Meditoc1").Result;
            conketaVersion = ConfigurationManager.AppSettings["CONEKTA_VERSION"];
            conektaLocale = ConfigurationManager.AppSettings["CONEKTA_LOCALE"];
        }

        /// <summary>
        /// Función: Consume el servicio de Conekta para generar una orden
        /// Creado: Cristopher Noh 03/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="entCreateOrder">Datos de la compra</param>
        /// <param name="entUserAgent">Agente de usuario del server</param>
        /// <returns></returns>
        public IMDResponse<string> SCreateOrder(EntCreateOrder entCreateOrder, EntCreateUserAgent entUserAgent, string[] pPropiedadesOcultar = null)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.SCreateOrder);
            logger.Info(IMDSerialize.Serialize(67823458103485, $"Inicia {metodo}(EntCreateOrder entCreateOrder, EntCreateUserAgent entUserAgent)", entCreateOrder, entUserAgent));

            string mensajePagoError = "No pudimos procesar el pago de tu pedido, revisa nuevamente los datos ingresados o intenta con otra tarjeta.";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest peticion = (HttpWebRequest)WebRequest.Create(urlServicioConektaCrearOrden);
                peticion.Accept = $"application/vnd.conekta-v{conketaVersion}+json";
                peticion.UserAgent = $"Conekta/v1 DotNetBindings10/Conekta::{conketaVersion}";
                peticion.Method = "POST";

                byte[] conektaApiKeyBytes = Encoding.UTF8.GetBytes(conketaApiKey);
                string conektaApiKeyBase64 = Convert.ToBase64String(conektaApiKeyBytes);
                string agenteUsuarioSerializado = JsonConvert.SerializeObject(entUserAgent);

                peticion.Headers.Add("Authorization", $"Basic {conektaApiKeyBase64}:");
                peticion.Headers.Add("Accept-Language", conektaLocale);
                peticion.Headers.Add("X-Conekta-Client-User-Agent", agenteUsuarioSerializado);

                string datosCrearOrdenSerializado = string.Empty;

                if (pPropiedadesOcultar != null)
                {
                    datosCrearOrdenSerializado = JsonConvert.SerializeObject(entCreateOrder, new JsonSerializerSettings { ContractResolver = new ServContractResolver(pPropiedadesOcultar) });
                }
                else
                {
                    datosCrearOrdenSerializado = JsonConvert.SerializeObject(entCreateOrder);
                }

                string json = JsonConvert.SerializeObject(entCreateOrder, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new ServContractResolver(pPropiedadesOcultar) });

                byte[] bytesDatosCrearOrden = Encoding.UTF8.GetBytes(datosCrearOrdenSerializado);
                peticion.ContentLength = bytesDatosCrearOrden.Length;
                peticion.ContentType = "application/json";

                Stream peticionStream = peticion.GetRequestStream();
                peticionStream.Write(bytesDatosCrearOrden, 0, bytesDatosCrearOrden.Length);

                HttpWebResponse respuesta = (HttpWebResponse)peticion.GetResponse();
                StreamReader respuestaStream = new StreamReader(respuesta.GetResponseStream());
                string body = respuestaStream.ReadToEnd();

                response.Code = 0;
                response.Message = "Respuesta de servicio obtenida.";
                response.Result = body;
            }
            catch (WebException webex)
            {
                if (webex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)webex.Response;
                    string body = string.Empty;
                    using (StreamReader content = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        body = content.ReadToEnd();
                    }
                    int statusCode = (int)httpResponse.StatusCode;

                    if (statusCode == 402)
                    {
                        //No cambiar
                        response.Code = -1500000;
                        response.Message = "No pudimos procesar el pago, la transacción ha sido declinada. Intente con otra tarjeta o comuníquese con su banco emisor.";
                        response.Result = body;
                    }
                    else
                    {
                        response.Code = 67823458104262;
                        response.Message = mensajePagoError;
                    }
                }
                else
                {
                    response.Code = 67823458104262;
                    response.Message = mensajePagoError;
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458104262;
                response.Message = mensajePagoError;

                logger.Error(IMDSerialize.Serialize(67823458104262, $"Error en {metodo}: {ex.Message}(EntCreateOrder entCreateOrder, EntCreateUserAgent entUserAgent)", entCreateOrder, entUserAgent, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Consume servicio de Conekta para consultar información de la Orden
        /// Creado: Cristopher Noh 03/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="orderId">ID de la orden de Conekta</param>
        /// <param name="entUserAgent">Agente de usuario del server</param>
        /// <returns></returns>
        public IMDResponse<string> SGetOrder(string orderId, EntCreateUserAgent entUserAgent)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.SGetOrder);
            logger.Info(IMDSerialize.Serialize(67823458105039, $"Inicia {metodo}(string orderId, EntCreateUserAgent entUserAgent)", orderId, entUserAgent));

            try
            {
                HttpWebRequest peticion = (HttpWebRequest)WebRequest.Create($"{urlServicioConektaCrearOrden}/{orderId}");
                peticion.Accept = $"application/vnd.conekta-v{conketaVersion}+json";
                peticion.UserAgent = $"Conekta/v1 DotNetBindings10/Conekta::{conketaVersion}";
                peticion.Method = "GET";

                byte[] conektaApiKeyBytes = Encoding.UTF8.GetBytes(conketaApiKey);
                string conektaApiKeyBase64 = Convert.ToBase64String(conektaApiKeyBytes);
                string agenteUsuarioSerializado = JsonConvert.SerializeObject(entUserAgent);

                peticion.Headers.Add("Authorization", $"Basic {conektaApiKeyBase64}:");
                peticion.Headers.Add("Accept-Language", conektaLocale);
                peticion.Headers.Add("X-Conekta-Client-User-Agent", agenteUsuarioSerializado);

                HttpWebResponse respuesta = (HttpWebResponse)peticion.GetResponse();
                StreamReader respuestaStream = new StreamReader(respuesta.GetResponseStream());
                string body = respuestaStream.ReadToEnd();

                response.Code = 0;
                response.Message = "Respuesta de servicio obtenida";
                response.Result = body;
            }

            catch (Exception ex)
            {
                response.Code = 67823458105816;
                response.Message = "Error al consultar la orden";

                logger.Error(IMDSerialize.Serialize(67823458105816, $"Error en {metodo}(string orderId, EntCreateUserAgent entUserAgent): {ex.Message}", orderId, entUserAgent, ex, response));
            }
            return response;
        }
    }
}
