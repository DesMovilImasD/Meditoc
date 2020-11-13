using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class BuyProductRequestModel
    {
        public BuyProductRequestModel()
        {
            this.Products = new List<ProductItemRequest>();
            this.Changes = new List<ChargesItemRequest>();
        }

        //[JsonProperty("tax")]
        //public bool Tax { get; set; } = true;

        [JsonProperty("currency")]
        public string Currency { get; set; } = "MXN";

        public int iIdOrigen { get; set; } = 3;

        [JsonProperty("coupon")]
        public string Coupon { get; set; } = null;

        [JsonProperty("customer_info")]
        public UserFormRequest UserForm { get; set; }

        [JsonProperty("line_items")]
        public List<ProductItemRequest> Products { get; set; }

        [JsonProperty("charges")]
        public List<ChargesItemRequest> Changes { get; set; }
    }

    public class UserFormRequest
    {

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }
    }

    public class ProductItemRequest
    {
        [JsonProperty("product_id")]
        public int Id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

    }

    public class ChargesItemRequest
    {

        public static ChargesItemRequest Create(string token, int Months) => new ChargesItemRequest
        {
            payment = new CardPaymentItemRequest
            {
                Token = token,
                MonthlyInstallments= Months
            }
        };

        [JsonProperty("payment_method")]
        public CardPaymentItemRequest payment { get; set; }
    }

    public class CardPaymentItemRequest
    {
        [JsonProperty("monthly_installments")]
        public int MonthlyInstallments { get; set; } = 1;

        [JsonProperty("type")]
        public string Type { get; set; } = "card";

        [JsonProperty("token_id")]
        public string Token { get; set; }

        [JsonProperty("expires_at")]
        public int ExpiredAt { get; set; } = 0;
        
    }
}
