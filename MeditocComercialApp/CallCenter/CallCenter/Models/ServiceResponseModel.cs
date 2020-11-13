using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallCenter.Models
{
    public class ServiceResponseModel
    {

        public static ServiceResponseModel Success(List<ServiceItemResponse> services) => new ServiceResponseModel()
        {
            Status = true,
            Items = services,
            Message = string.Empty
        };

        public static ServiceResponseModel Fail(string error) => new ServiceResponseModel()
        {
            Status = false,
            Message = error,
            Items = new List<ServiceItemResponse>()
        };

        public ServiceResponseModel()
        {
            this.Items = new List<ServiceItemResponse>();
        }

        public List<ServiceItemResponse> Items { get; set; }

        public bool Status { get; set; }

        public string Message { get; set; }
    }

    public class ServiceItemResponse
    {
        public ServiceItemResponse()
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

        [JsonProperty("sResumen")]
        public string Remark { get; set; }

        [JsonProperty("iMesVigencia")]
        public int Availability { get; set; }

        [JsonProperty("lstProducto")]
        public List<object> Products { get; set; }

        [JsonProperty("sIcon")]
        public string Icon { get; set; }

        [JsonProperty("sDescripcion")]
        public string Abstract { get; set; }
    }
}
