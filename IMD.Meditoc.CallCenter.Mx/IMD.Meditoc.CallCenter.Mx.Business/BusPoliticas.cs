using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace IMD.Meditoc.CallCenter.Mx.Business
{
    public class BusPoliticas
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPoliticas));

        public IMDResponse<EntPoliticas> ObtenerPoliticas()
        {
            IMDResponse<EntPoliticas> response = new IMDResponse<EntPoliticas>();
            IMDResponse<List<EntIceLinkServer>> lstServers = new IMDResponse<List<EntIceLinkServer>>();

            string metodo = nameof(this.ObtenerPoliticas);
            logger.Info(IMDSerialize.Serialize(67823458380874, $"Inicia {metodo}"));

            try
            {
                EntPoliticas entPoliticas = new EntPoliticas();


                lstServers = BgetIceLinkServer();

                entPoliticas.sAvisoDePrivacidad = ConfigurationManager.AppSettings["sAvisoDePrivacidad"];
                entPoliticas.sTerminosYCondiciones = ConfigurationManager.AppSettings["sTerminosYCondiciones"];
                entPoliticas.sCorreoContacto = ConfigurationManager.AppSettings["sCorreoContacto"];
                entPoliticas.sCorreoSoporte = ConfigurationManager.AppSettings["sCorreoSoporte"];
                entPoliticas.sDireccionEmpresa = ConfigurationManager.AppSettings["sDireccionEmpresa"];
                entPoliticas.sTelefonoEmpresa = ConfigurationManager.AppSettings["sTelefonoEmpresa"];
                entPoliticas.nMaximoDescuento = Convert.ToDouble(ConfigurationManager.AppSettings["nMaximoDescuento"]);
                entPoliticas.nIVA = Convert.ToDouble(ConfigurationManager.AppSettings["nIVA"]);
                entPoliticas.sLlaveDominio = ConfigurationManager.AppSettings["sLlaveDominio"];
                entPoliticas.sLlaveIcelink = ConfigurationManager.AppSettings["sLlaveIcelink"];
                entPoliticas.sConektaPublicKey = ConfigurationManager.AppSettings["sConektaPublicKey"];
                entPoliticas.rutasIceServer = lstServers.Result;

                string sMensualidades = ConfigurationManager.AppSettings["sMensualidades"];
                if (!string.IsNullOrWhiteSpace(sMensualidades))
                {
                    entPoliticas.lstMensualidades = JsonConvert.DeserializeObject<List<EntMensualidad>>(sMensualidades);
                }
                entPoliticas.bTieneMesesSinIntereses = Convert.ToBoolean(ConfigurationManager.AppSettings["bTieneMesesSinIntereses"]);

                response.Message = "Políticas consultadas";
                response.Result = entPoliticas;
            }
            catch (Exception ex)
            {
                response.Code = 67823458381651;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458381651, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntIceLinkServer>> BgetIceLinkServer()
        {
            IMDResponse<List<EntIceLinkServer>> response = new IMDResponse<List<EntIceLinkServer>>();
            List<EntIceLinkServer> iceLinkServers;
            EntIceLinkServer oIceLinkServers;

            string metodo = nameof(this.BgetIceLinkServer);
            logger.Info(IMDSerialize.Serialize(67823458465567, $"Inicia {metodo}"));

            try
            {
                string sIceLinkServer = ConfigurationManager.AppSettings["sIceLinkServers"].ToString();
                iceLinkServers = new List<EntIceLinkServer>();
                string[] oServer = sIceLinkServer.Split('|');

                foreach (var item in oServer)
                {
                    string[] oDatos = item.Split(',');

                    oIceLinkServers = new EntIceLinkServer
                    {
                        sServer = oDatos[0],
                        sUser = oDatos[1],
                        sPassword = oDatos[2]
                    };

                    iceLinkServers.Add(oIceLinkServers);
                }

                response.Code = 0;                
                response.Result = iceLinkServers;                
            }
            catch (Exception ex)
            {
                response.Code = 67823458466344;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458466344, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
