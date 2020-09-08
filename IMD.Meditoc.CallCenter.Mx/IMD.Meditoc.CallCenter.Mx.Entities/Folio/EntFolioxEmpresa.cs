using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioxEmpresa
    {        
        public int iIdEmpresa { get; set; }
        public int iIdOrigen { get; set; }                        

        [JsonProperty(PropertyName = "line_items")]
        public List<line_items> lstLineItems;               
    }    
}
