using System;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class CouponResponseModel
    {
        [JsonProperty("Code")]
        public  string Code { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Result")]
        public CouponResultModel Result { get; set; }
    }

    public class CouponResultModel
    {
        [JsonProperty("fiIdCupon")]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("fiIdCuponCategoria")]
        public int CategoryId {get; set;}

        [JsonProperty("fsDescripcion")]
        public string Remarks { get; set; }

        [JsonProperty("fsDescripcionCategoria")]
        public string RemarksCategory { get; set; }

        [JsonProperty("fsCodigo")]
        public string Code { get; set; }

        [JsonProperty("fnMontoDescuento")]
        public double? QuantityDiscount { get; set; }

        [JsonProperty("fnPorcentajeDescuento")]
        public double? PercentageDiscount { get; set; }

        [JsonProperty("fiMesBono")]
        public object BonusMonth  { get; set; }

        [JsonProperty("fiTotalLanzamiento")]
        public int TotalLauch { get; set; }

        [JsonProperty("fiTotalCanjeado")]
        public int TotalRedeemed { get; set; }

        [JsonProperty("fdtFechaVencimiento")]
        public string DueDate { get; set; }

        [JsonProperty("fbActivo")]
        public bool IsActive { get; set; }

        [JsonProperty("fbBaja")]
        public bool IsDown { get; set; }

    }
}
