using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    public class ResponseModel
    {
        private string _sMensaje = "";
        private bool _bRespuesta = false;
        private string _sParameter1 = "";

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

        public string sFolio { get; set; }
        /// <summary>
        /// False es tipo 1 y true es tipo 2
        /// </summary>
        public bool bTipoFolio { get; set; }
    }
}
