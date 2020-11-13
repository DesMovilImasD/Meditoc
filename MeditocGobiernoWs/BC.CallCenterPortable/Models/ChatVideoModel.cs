using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    /// <summary>Modelo Base de herencia para los modelos.
    /// </summary>
    public class ChatVideoModel : BaseModel
    {
        /// <summary>Campo el cual contiene el canal para la video llamada.
        /// </summary>
        public string sCanal { get; set; }

        /// <summary>Campo el cual contiene el password de la cuenta del usuario actual
        /// </summary>
        public string sPassword { get; set; }
    }
}
