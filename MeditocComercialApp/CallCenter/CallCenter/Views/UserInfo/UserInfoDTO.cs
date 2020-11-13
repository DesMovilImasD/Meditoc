using System;
using System.ComponentModel.DataAnnotations;
using CallCenter.Helpers;
using CallCenter.ViewModels;
using Xamarin.Forms;

namespace CallCenter.Views.UserInfo
{
    public class UserInfoDTO :  PaymentPageInterface
    {

        public static UserInfoDTO Create(int id, string name) => new UserInfoDTO
        {
            Id = id,
            Name = name,
            CardMonth = "01"
        };

        public int Id { get; set; }
        public string Name { get; set; }
        public double Total { get; set; } = 0;

        /// <summary>
        /// Email
        /// </summary>
        [
            Required(ErrorMessage = "El campo de correo es necesario"),
            EmailAddress(ErrorMessage = "El formato de correo no es válido")
        ]
        public String Email { get; set; }

        /// <summary>
        /// Nombre completo del usuario
        /// </summary>
        [Required(ErrorMessage = "El campo de nombre de usuario es necesario")]
        public String UserName { get; set; }


        /// <summary>
        /// Número de telefono.
        /// </summary>
        [RegularExpression("^+?[0-9]+$", ErrorMessage = "El télefono debe ser de 10 dígitos")]
        public String PhoneNumber { get; set; }

        /// <summary>
        /// Número de la tarjeta.
        /// </summary>
        [
            Required(ErrorMessage = "El campo de número de tarjeta es requerido"),
            RegularExpression("^[0-9]{13,16}$", ErrorMessage = "El número de tarjeta debe ser de 13 a 16 dígitos")
        ]
        public String CardNumber { get; set; }

        /// <summary>
        /// Mes de la tarjeta
        /// </summary>
        [Required(ErrorMessage = "El campo del mes de la tarjeta es requerido")]
        public String CardMonth { get; set; }

        /// <summary>
        /// Año de la tarjeta
        /// </summary>
        [
            Required(ErrorMessage = "El campo del año de la tarjeta es requerido"),
            RegularExpression("^[0-9]{4}$", ErrorMessage = "El año debe ser de 4 dígitos.")
        ]
        public String CardYear { get; set; }


        /// <summary>
        /// CVC e la tarjeta
        /// </summary>
        [
            Required(ErrorMessage = "El campo de cvc es requerido"),
            RegularExpression("^[0-9]{3,4}$", ErrorMessage = "El cvc debe ser de 3 dígitos.")
        ]
        public String CardPin { get; set; }
        

        /// <summary>
        /// Tipo de tarjeta.
        /// </summary>
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int? Type { get; set; }

        /// <summary>
        /// Bandera terminos y condiciones.
        /// </summary>
        public bool TermsAndConditions { get; set; } = false;

        /// <summary>
        /// Card part #1
        /// </summary>
        public string CardPart1 { get; set; }

        ///// <summary>
        ///// Card part #2
        ///// </summary>
        //public string CardPart2 { get; set; }

        ///// <summary>
        ///// Card part #3
        ///// </summary>
        //public string CardPart3 { get; set; }

        ///// <summary>
        ///// Card part #4
        ///// </summary>
        //public string CardPart4 { get; set; }

    }
}

