using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Paciente
{
    public class EntPaciente
    {
        public int iIdPaciente { get; set; }
        public int iIdFolio { get; set; }
        public string sFolio { get; set; }
        public int iIdSexo { get; set; }
        public string sSexo { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string sNombre { get; set; }
        public string sApellidoPaterno { get; set; }
        public string sApellidoMaterno { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string sTelefono { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string sCorreo { get; set; }
        public string sTipoSangre { get; set; }
        public string sdtFechaNacimiento { get; set; }
    }
}
