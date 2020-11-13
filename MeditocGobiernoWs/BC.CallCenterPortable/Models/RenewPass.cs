using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    public class RenewPass : BaseModel
    {
        /// <summary>Campo el cual contiene el usuario con el cual se realizara la
        /// autentificación.
        /// </summary>
        public string sUsuarioLogin { get; set; }

        /// <summary>Campo el cual contiene el password con el que se esta intentando autentificar.
        /// </summary>
        public string sPasswordLogin { get; set; }

        /// <summary>Campo el cual contiene el código de verificación.
        /// </summary>
        public string sCodigoVerificacion { get; set; }

        /// <summary>Campo el cual contiene el Tipo de paso a realizar.
        /// </summary>
        public int iPaso { get; set; } //1=solicitud, 2 validacion

        /// <summary>Campo el cual contiene el id del usuario.
        /// </summary>
        public int iIdUsuario { get; set; }

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

    }
}
