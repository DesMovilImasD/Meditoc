using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using CallCenter.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Security;

namespace CallCenter.Services
{
    public class wsConnexion
    {
        public string Metodo = string.Empty;
        public string Parametros = string.Empty;
        public string Respuesta = string.Empty;

        public string sURL = "";

        public wsConnexion()
        {
            this.sURL = Settings.sUrl;
        }

        public void SetUrl(String url)
        {
            this.sURL = url;
        }

        public async Task<string> GetDataRestAsync(object data, string controller, string method)
        {
            string sRespuesta = "";
            try
            {
                string urlbase = String.Format(sURL + "/" + controller + "/" + method);

                //System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                //   (sender, cert, chain, sslPolicyErrors) =>
                //   {
                //       if (cert != null) System.Diagnostics.Debug.WriteLine(cert);
                //       return true;
                //   };
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (HttpClient client = new HttpClient(clientHandler))
                {

                    client.DefaultRequestHeaders.Add("AppKey", Settings.AppKey);
                    client.DefaultRequestHeaders.Add("AppToken", Settings.AppToken);
                    client.Timeout = TimeSpan.FromSeconds(60);


                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string json = JsonConvert.SerializeObject(data);

                    try
                    {
                        using (
                            HttpResponseMessage res = await client.PostAsync(urlbase, new StringContent(json, Encoding.UTF8, "application/json")))
                        {
                            sRespuesta = res.Content.ReadAsStringAsync().Result;
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Hay problemas de conexión con el servicio, reintente más tarde.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sRespuesta;

        }


        /// <summary>
        /// metodo generico para solicitar datos al server.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(string url)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var uri = new Uri(url);
                //var json = JsonConvert.SerializeObject(body);
                using (var client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Add("AppKey", Settings.AppKey);
                    client.DefaultRequestHeaders.Add("AppToken", Settings.AppToken);

                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    string str = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    T model = JsonConvert.DeserializeObject<T>(str);
                    return model;
                }                
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public async Task<string> GetImages(string url)
        {
            string model = "";
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var uri = new Uri(url);

                using (var client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Add("AppKey", Settings.AppKey);
                    client.DefaultRequestHeaders.Add("AppToken", Settings.AppToken);

                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                     model = await response.Content.ReadAsStringAsync().ConfigureAwait(false);                    

                }
            }
            catch (Exception e)
            {
                //return default(T);
            }
            return model;
        }
    }

}


