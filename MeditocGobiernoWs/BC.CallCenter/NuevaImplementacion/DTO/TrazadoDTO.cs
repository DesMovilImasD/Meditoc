using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.DTO
{
    public class TrazadoDTO
    {
        public int Id { get; set; }
        public string Telefono { get; set; }
        public float Latitud { get; set; }
        public float Longitude { get; set; }
        public string Date { get; set; }
        public bool bGuardado { get; set; }
    }
}
