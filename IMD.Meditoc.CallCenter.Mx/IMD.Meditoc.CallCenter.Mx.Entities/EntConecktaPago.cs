using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities
{
    public class EntConecktaPago
    {
        public string currency { get; set; }
        public int iIdEmpresa { get; set; }
        public int iIdOrigen { get; set; }
        public bool tax { get; set; }
        public int? coupon { get; set; }
        [JsonProperty(PropertyName = "customer_info")]
        public EntPaciente pacienteUnico;

        [JsonProperty(PropertyName = "line_items")]
        public List<line_items> lstLineItems;

        [JsonProperty(PropertyName = "charges")]
        public List<charges> lstCharges;        
    }

    public class payment_method
    {
        public int monthly_installments { get; set; }
        public string type { get; set; }
        public string token_id { get; set; }
        public int expires_at { get; set; }
    }

    public class line_items
    {
        public string name { get; set; }
        public int unit_price { get; set; }
        public int quantity { get; set; }
        public int product_id { get; set; }
        public int monthsExpiration { get; set; }
        public int consecutive { get; set; }
    }

    public class charges
    {

        [JsonProperty(PropertyName = "payment_method")]
        public payment_method payment_;
    }
}
