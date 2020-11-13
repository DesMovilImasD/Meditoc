using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    /// <summary>Modelo Base de herencia para los modelos.
    /// </summary>
    public class DrModel: UserModel
    {
        /// <summary>Campo el cual contiene el ID del DR, este Id es del CGU.
        /// </summary>
        public int iIdUsuario { get; set; }

        /// <summary>Campo el cual contiene el estatus d ocupación del DR.
        /// </summary>
        public bool bEstado { get; set; }

        /// <summary>Campo el cual contiene el ID del CGU del DR.
        /// </summary>
        public int iIdDRCGU { get; set; }

        /// <summary>Campo el cual contiene el tipo de operacion de comunicación realizada.
        /// </summary>
        public int iTipoAtencion { get; set; }

    }
}
