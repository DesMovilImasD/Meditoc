using System.Collections.Generic;

namespace IMD.Meditoc.CallCenter.Mx.Entities
{
    public class EntPoliticas
    {
        public string sTerminosYCondiciones { get; set; }
        public string sAvisoDePrivacidad { get; set; }
        public string sCorreoContacto { get; set; }
        public string sCorreoSoporte { get; set; }
        public string sDireccionEmpresa { get; set; }
        public string sTelefonoEmpresa { get; set; }
        public double nMaximoDescuento { get; set; }
        public double nIVA { get; set; }
        public string sLlaveIcelink { get; set; }
        public string sLlaveDominio { get; set; }
        public string sConektaPublicKey { get; set; }
        public bool bTieneMesesSinIntereses { get; set; }
        public List<EntMensualidad> lstMensualidades { get; set; }
        public List<EntIceLinkServer> rutasIceServer { get; set; }

        public EntPoliticas()
        {
            lstMensualidades = new List<EntMensualidad>();
        }
    }

    public class EntIceLinkServer
    {
        public string sServer { get; set; }
        public string sUser { get; set; }
        public string sPassword { get; set; }
    }
}
