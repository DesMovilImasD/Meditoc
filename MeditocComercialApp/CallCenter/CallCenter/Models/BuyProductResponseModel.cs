using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class BuyProductResponseModel
    {
        public static BuyProductResponseModel Success(BuyProductInfoItemResponseModel item) => new BuyProductResponseModel
        {
            Status = true,
            Result = item,
            Message = ""
        };

        public static BuyProductResponseModel Fail(string messages) => new BuyProductResponseModel
        {
            Status = false,
            Message = messages,
            Result = null
        };

        public bool Status { get; set; }
        public string Message { get; set; }
        public BuyProductInfoItemResponseModel Result { get; set; }

    }

    public class BuyProductInfoItemResponseModel
    {
        public BuyProductInfoItemResponseModel()
        {
            Items = new List<BuyProductItemResponseModel>();
        }

        [JsonProperty("sEmail")]
        public string Email { get; set; }

        [JsonProperty("sNombre")]
        public string Name { get; set; }

        [JsonProperty("sTelefono")]
        public string PhoneNumber { get; set; }

        [JsonProperty("sOrden")]
        public string CardCode { get; set; }

        [JsonProperty("nTotal")]
        public double Total { get; set; }

        [JsonProperty("nTotalPagado")]
        public double TotalPay { get; set; }

        [JsonProperty("sCodigoCupon")]
        public string CouponCode { get; set; }

        [JsonProperty("lstArticulos")]
        public List<BuyProductItemResponseModel> Items { get; set; }

    }


    public class BuyProductItemResponseModel
    {
        [JsonProperty("sFolio")]
        public string Folio { get; set; }

        [JsonProperty("sPass")]
        public string Password { get; set; }

        [JsonProperty("sDescripcion")]
        public string Remarks { get; set; }

        [JsonProperty("nPrecio")]
        public double Cost { get; set; }

        [JsonProperty("nCantidad")]
        public double Quantity { get; set; }
    }

}
