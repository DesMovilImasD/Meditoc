using System;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class FolioResponse
    {
        [JsonProperty("bRespuesta")]
        public bool Status { get; set; }

        [JsonProperty("bTipoFolio")]
        public bool IsType2 { get; set; }

        [JsonProperty("sFolio")]
        public string Folio { get; set; }

        [JsonProperty("sMensaje")]
        public string Msg { get; set; }

        [JsonProperty("sParameter1")]
        public string Data { get; set; }
        
    }

    public class COVIDRequest
    {
        [JsonProperty("sFolio")]
        public string COVID { get; set; }

        [JsonProperty("sNumero")]
        public string Phone { get; set; }

        [JsonProperty("sCp")]
        public string CP { get; set; } = null;

        [JsonProperty("sLatitud")]
        public double Latitude { get; set; } = 0;

        [JsonProperty("sLongitud")]
        public double Longitude { get; set; } = 0;

        [JsonProperty("sError")]
        public string LocationError { get; set; } = null;

        
        public static COVIDRequest Create(string folio,
            string phone,
            string cp,
            double latitude,
            double longitude,
            string error = null) {
            return new COVIDRequest
            {
                COVID = folio,
                CP = cp,
                Latitude = latitude,
                Longitude = longitude,
                LocationError = error,
                Phone = phone
            };
        }
    }
}
