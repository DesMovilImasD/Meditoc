using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class PoliciesResponseModel
    {

        [JsonProperty("sAvisoDePrivacidad")]
        public string PrivacityLink { get; set; }

        [JsonProperty("sTerminosYCondiciones")]
        public string TermsAndConditionsLink { get; set; }

        [JsonProperty("nMaximoDescuento")]
        public double MaxDiscount { get; set; }

        [JsonProperty("sCorreoContacto")]
        public string Contact { get; set; }

        [JsonProperty("sCorreoSoporte")]
        public string Support { get; set; }

        [JsonProperty("sDireccionEmpresa")]
        public string CompanyAddess { get; set; }

        [JsonProperty("sTelefonoEmpresa")]
        public string CompanyPhone { get; set; }

        [JsonProperty("nIVA")]
        public double IVA { get; set; }

        [JsonProperty("sConektaPublicKey")]
        public string ConektaPublicKey { get; set; }

        [JsonProperty("bTieneMesesSinIntereses")]
        public bool HasMonthsWithoutInterest { get; set; } = false;

        [JsonProperty("lstMensualidades")]
        public List<PoliciesMonthlyPayments> MonthlyPayments { get; set; }

        [JsonProperty("sLlaveIcelink")]
        public string keyIceLink { get; set; }

        [JsonProperty("sLlaveDominio")]
        public string keyDomainIceLink { get; set; }

        public List<EntIceLink> rutasIceServer { get; set; }
    }

    public class PoliciesMonthlyPayments
    {
        [JsonProperty("compra_minima")]
        public double minPurchase { get; set; }

        [JsonProperty("descripcion")]
        public string Remarks { get; set; }

        [JsonProperty("meses")]
        public int Months { get; set; }
    }

    public class EntIceLink
    {
        public string sPassword { get; set; }
        public string sServer { get; set; }
        public string sUser { get; set; }
    }
}
