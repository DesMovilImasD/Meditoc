using Newtonsoft.Json;
using System.Collections.Generic;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioxEmpresa
    {        
        public int iIdEmpresa { get; set; }
        public int iIdOrigen { get; set; }
        public string uid { get; set; }
        public int iIdUsuarioMod { get; set; }

        [JsonProperty(PropertyName = "line_items")]
        public List<line_items> lstLineItems;               
    }    
}
