using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    /// <summary>Modelo Base de herencia para los modelos.
    /// </summary>
    public class ChatModel : BaseModel
    {
        //sUIDCliente = psUsuarioUID, sPassword = Settings.sPassLogin
        /// <summary>Campo el cual contiene el password de la cuenta del usuario actual
        /// </summary>
        ///
        public string sUIDCliente { get; set; }
        public string sPassword { get; set; }
    }
}
