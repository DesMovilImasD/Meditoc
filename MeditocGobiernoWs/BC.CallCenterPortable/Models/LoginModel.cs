using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    public class LoginModel : BaseModel
    {
        /// <summary>Campo el cual contiene el usuario con el cual se realizara la
        /// autentificación.
        /// </summary>
        public string sUsuarioLogin { get; set; }

        /// <summary>
        /// Propiedad para declarar su longitud en un plano geografico
        /// </summary>
        public string sLongitud { get; set; }

        /// <summary>
        /// Propiedad para declarar su latitud en un plano geografico
        /// </summary>
        public string sLatitud { get; set; }

        /// <summary>Campo el cual contiene el password con el que se esta intentando autentificar.
        /// </summary>
        public string sPasswordLogin { get; set; }

        /// <summary>Campo el cual contiene el nombre del usuario si este se autentifico correctamente.
        /// </summary>
        public string sNombre { get; set; }

        /// <summary>Campo el cual contiene la bandera que indica si un usuario autentificado es Doctor.
        /// </summary>
        public bool bDoctor { get; set; }

        /// <summary>Campo el cual contiene el sexo del usuario autentificado.
        /// </summary>
        public string sSexo { get; set; }

        /// <summary>Campo el cual indica el numero de paso en el cual esta el proceso.
        /// </summary>
        public int iPaso { get; set; }

        /// <summary>Campo el cual indica el número de telefono para marcar a los DRs.
        /// </summary>
        public string sTelefonoDRs { get; set; }

        /// <summary>Campo el cual indica la institución del usuario.
        /// </summary>
        public int iIdUsuario { get; set; }

        /// <summary>Campo el cual indica la institución del usuario.
        /// </summary>
        public string sInstitucion { get; set; }

        /// <summary>Campo el cual indica la contraseña anterior "PAssword a cambiar".
        /// </summary>
        public string sPassOld { get; set; }

        /// <summary>Campo el cual indica la aceptación de los terminos y condiciones.
        /// </summary>
        private bool _bAceptoTerminoCondicion = false;
        public bool bAceptoTerminoCondicion
        {
            get { return _bAceptoTerminoCondicion; }
            set { _bAceptoTerminoCondicion = value; }
        }

        public string sLlaveIcelink { get; set; } = ConfigurationManager.AppSettings["sLlaveIcelink"];
        public string sLlaveDominio { get; set; } = ConfigurationManager.AppSettings["sLlaveDominio"];

        /// <summary>Campo el cual indica el tipo de operación.
        /// </summary>
        private bool _bInsert = false;
        public bool bInsert
        {
            get { return _bInsert; }
            set { _bInsert = value; }
        }

        public string sTerminosYCondiciones { get; set; } = ConfigurationManager.AppSettings["sTerminosYCondiciones"];
        public string sAvisoDePrivacidad { get; set; } = ConfigurationManager.AppSettings["sAvisoDePrivacidad"];
        public string sLigaCovID { get; set; } = ConfigurationManager.AppSettings["sLigaCovID"];


    }
}
