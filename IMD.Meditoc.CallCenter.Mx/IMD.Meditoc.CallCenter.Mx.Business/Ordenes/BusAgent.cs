using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using log4net;
using System;
using System.Configuration;

namespace IMD.Meditoc.CallCenter.Mx.Business.Ordenes
{
    public class BusAgent
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusAgent));

        private string version;

        public BusAgent()
        {
            version = ConfigurationManager.AppSettings["CONEKTA_VERSION"];
        }

        /// <summary>
        /// Función: Obtiene información del userAgent del server
        /// Creado: Cristopher Noh 03/07/2020
        /// Modificado:
        /// </summary>
        /// <returns>Datos userAgent</returns>
        public IMDResponse<EntCreateUserAgent> BGetUserAgent()
        {
            IMDResponse<EntCreateUserAgent> response = new IMDResponse<EntCreateUserAgent>();

            string metodo = nameof(this.BGetUserAgent);
            logger.Info(IMDSerialize.Serialize(67823458106593, $"Inicia {metodo}()"));

            try
            {
                EntCreateUserAgent entUserAgent = new EntCreateUserAgent();
                entUserAgent.bindings_version = version;
                entUserAgent.lang = ".net";
                entUserAgent.lang_version = typeof(string).Assembly.ImageRuntimeVersion;
                entUserAgent.publisher = "conekta";
                entUserAgent.uname = Environment.OSVersion.ToString();

                response.Code = 0;
                response.Message = "UserAgent success";
                response.Result = entUserAgent;
            }
            catch (Exception ex)
            {
                response.Code = 67823458107370;
                response.Message = "Error al procesar la información.";

                logger.Error(IMDSerialize.Serialize(67823458107370, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}