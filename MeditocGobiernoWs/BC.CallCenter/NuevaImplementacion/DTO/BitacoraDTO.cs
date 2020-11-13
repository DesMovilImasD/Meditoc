using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.DTO
{
    public class BitacoraDTO
    {
        public int iIdBitacora { get; set; }
        public int iIdAcceso { get; set; }
        public int iIdEncuesta { get; set; }
        public int iIdLlamada { get; set; }
        public string sEstatus { get; set; }
        public string sMensaje { get; set; }
        public string sCoordenadas { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public DateTime dtFechaBaja { get; set; }
        public bool bBaja { get; set; }
    }
}
