using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class MembershipResponseModel
    {

        public static MembershipResponseModel Success(List<MembershipItemResponse> memberships) => new MembershipResponseModel()
        {
            Status = true,
            Items = memberships,
            Message = string.Empty
        };

        public static MembershipResponseModel Fail(string error) => new MembershipResponseModel()
        {
            Status = false,
            Message = error,
            Items = new List<MembershipItemResponse>()
        };

        public MembershipResponseModel()
        {
            this.Items = new List<MembershipItemResponse>();
        }

        public List<MembershipItemResponse> Items { get; set; }

        public bool Status { get; set; }

        public string Message { get; set; }
    }

    public class MembershipItemResponse
    {
        public MembershipItemResponse()
        {
            Products = new List<object>();
        }

        [JsonProperty("iIdProducto")]
        public int Id { get; set; }

        [JsonProperty("sNombre")]
        public string Name { get; set; }

        [JsonProperty("sNombreCorto")]
        public string SmallName { get; set; }

        [JsonProperty("fCosto")]
        public double Cost { get; set; }

        [JsonProperty("sResume")]
        public string Remark { get; set; }

        [JsonProperty("iMesVigencia")]
        public int Availability { get; set; }

        [JsonProperty("sIcon")]
        public string Icon { get; set; }

        [JsonProperty("sDescripcion")]
        public string Abstract { get; set; }

        [JsonProperty("lstProducto")]
        public List<object> Products { get; set; }
    }
}
