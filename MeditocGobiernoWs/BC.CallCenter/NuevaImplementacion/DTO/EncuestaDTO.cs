using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.DTO
{
    class EncuestaDTO
    {
        public int iIdEncuesta { get; set; }
        public int iIdAcceso { get; set; }
        public string sTipoFolio { get; set; }
        public string sFolio { get; set; }
        public string sNumero { get; set; }
        public string sCP { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public DateTime dtFechaBaja { get; set; }
        public bool bBaja { get; set; }
    }
}
