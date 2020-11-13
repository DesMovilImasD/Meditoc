using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    /// <summary>Modelo Base de herencia para los modelos.
    /// </summary>
    public class UserModel : BaseModel
    {
        /// <summary>Campo el cual contiene el Nombre para mostrar de un usuario
        /// </summary>
        public string sNameDisplay { get; set; }

        /// <summary>Campo el cual contiene la URL del Avatar para mostrar de un usuario
        /// </summary>
        public string sURLAvatar { get; set; }

        /// <summary>Campo el cual contiene la URL del Perfil para mostrar de un usuario
        /// </summary>
        public string sURLPerfil { get; set; }

        /// <summary>Campo el cual contiene el rol de un usuario
        /// </summary>
        public string sRol { get; set; }

        /// <summary>Campo el cual contiene los amigos del usuario
        /// </summary>
        public string sFriends { get; set; }

    }
}
