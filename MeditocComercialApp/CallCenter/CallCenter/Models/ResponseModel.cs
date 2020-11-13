using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class ResponseModel
    {
        private string _sMensaje = "";
        private bool _bRespuesta = false;
        private string _sParameter1 = "";
        private string _sFolio = null;

        /// <summary>Campo el cual contiene el mensaje de la respuesta.
        /// </summary>
        public string sMensaje
        {
            get { return _sMensaje; }
            set { _sMensaje = value; }
        }

        /// <summary>Campo el cual contiene la bandera de la respuesta.
        /// </summary>
        public bool bRespuesta
        {
            get { return _bRespuesta; }
            set { _bRespuesta = value; }
        }

        public string sParameter1
        {
            get { return _sParameter1; }
            set { _sParameter1 = value; }
        }

        public string sFolio
        {
            get { return _sFolio; }
            set { _sFolio = value; }
        }
    }

    public class ResponseObjectModel<T>
    {
        [JsonProperty("sMensaje")]
        public string Message { get; set; }

        [JsonProperty("bRespuesta")]
        public bool Status { get; set; }

        [JsonProperty("sParameter1")]
        public T Data { get; set; }

        [JsonProperty("sFolio")]
        public string Folio { get; set; }

    }
}
