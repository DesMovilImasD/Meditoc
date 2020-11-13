using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.DTO
{
    public class LlamadaDTO
    {
        public int iIdLlamada { get; set; }
        public int iIdAcceso { get; set; }
        public int iIdDoctor { get; set; }
        public string sNombreDoctor { get; set; } = "";
        public DateTime dtFechaCreacion { get; set; }
        public DateTime dtFechaBaja { get; set; }
        public bool bBaja { get; set; }

    }
}
