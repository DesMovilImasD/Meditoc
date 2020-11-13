using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class LoginModel 
    {
        [JsonProperty("sFolio")]
        public string sUIDCliente { get; set; }

        /// <summary>
        /// rutas del servidor de icelink
        /// </summary>
        //public List<LoginIceServer> rutasIceServer { get; set; }

        /// <summary>Campo el cual indica la aceptación de los terminos y condiciones.
        /// </summary>
        private bool _bAceptoTerminoCondicion = false;

        [JsonProperty("bTerminosYCondiciones")]
        public bool bAceptoTerminoCondicion
        {
            get { return _bAceptoTerminoCondicion; }
            set { _bAceptoTerminoCondicion = value; }
        }

        /// <summary>
        /// tipo de producto
        /// 1 = membresia
        /// 2 = orientacion unica
        /// </summary>
        [JsonProperty("iTipoProducto")]
        public int ProductType { get; set; }        


        public class LoginIceServer
        {
            public string sPassword { get; set; }
            public string sServer { get; set; }
            public string sUser { get; set; }
        }

    }
}

