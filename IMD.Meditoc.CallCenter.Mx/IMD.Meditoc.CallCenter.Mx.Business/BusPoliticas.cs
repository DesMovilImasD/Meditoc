using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business
{
    public class BusPoliticas
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPoliticas));

        public IMDResponse<EntPoliticas> ObtenerPoliticas()
        {
            IMDResponse<EntPoliticas> response = new IMDResponse<EntPoliticas>();

            string metodo = nameof(this.ObtenerPoliticas);
            logger.Info(IMDSerialize.Serialize(67823458380874, $"Inicia {metodo}"));

            try
            {
                EntPoliticas entPoliticas = new EntPoliticas();

                entPoliticas.sAvisoDePrivacidad = ConfigurationManager.AppSettings["sAvisoDePrivacidad"];
                entPoliticas.sTerminosYCondiciones = ConfigurationManager.AppSettings["sTerminosYCondiciones"];
                entPoliticas.sCorreoContacto = ConfigurationManager.AppSettings["sCorreoContacto"];
                entPoliticas.sCorreoSoporte = ConfigurationManager.AppSettings["sCorreoSoporte"];
                entPoliticas.sDireccionEmpresa = ConfigurationManager.AppSettings["sDireccionEmpresa"];
                entPoliticas.sTelefonoEmpresa = ConfigurationManager.AppSettings["sTelefonoEmpresa"];
                entPoliticas.nMaximoDescuento = Convert.ToDouble(ConfigurationManager.AppSettings["nMaximoDescuento"]);
                entPoliticas.nIVA = Convert.ToDouble(ConfigurationManager.AppSettings["nIVA"]);

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
    }
}
