using System.Collections.Generic;

namespace CallCenter.Views.Payment
{
    /// <summary>
    ///  modelo de la vista de pago.
    /// </summary>
    public class PaymentDTO
    {
        public PaymentDTO()
        {
            this.Products = new List<PaymentProductDTO>();
            this.Types = new List<PaymentTypeDTO>();
        }

        /// <summary>
        /// informaion del usuario
        /// </summary>
        public PaymenUserDTO UserInfo { get; set; }

        /// <summary>
        /// lista de productos seleccionados al pago.
        /// </summary>
        public List<PaymentProductDTO> Products {get; set;}

        /// <summary>
        /// Canjeo de codigo
        /// </summary>
        public PaymentRedeemCodeDTO RedeemCode { get; set; }

        /// <summary>
        /// tipo de pagos
        /// </summary>
        public List<PaymentTypeDTO> Types { get; set; }
    }

    /// <summary>
    /// Productos para el pago
    /// </summary>
    public class PaymentProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
    }

    /// <summary>
    /// informacion del pago
    /// </summary>
    public class PaymenUserDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int CardPart1 { get; set; }
        public int CardPart2 { get; set; }
        public int CardPart3 { get; set; }
        public int CardMonth { get; set; }
        public int CardYear { get; set; }
        public int CardPin { get; set; }
        public int Type { get; set; }
        
    }

    /// <summary>
    /// Codigo de promocional.
    /// </summary>
    public class PaymentRedeemCodeDTO
    {
        public string Code { get; set; }
        public decimal Discount { get; set; }
    }

    /// <summary>
    /// tipo de pago.
    /// </summary>
    public class PaymentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    
}

