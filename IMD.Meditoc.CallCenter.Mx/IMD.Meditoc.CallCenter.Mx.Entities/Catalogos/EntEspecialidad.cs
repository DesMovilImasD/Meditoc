using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Catalogos
{
    public class EntEspecialidad
    {
        public int iIdEspecialidad { get; set; }
        public string sNombre { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
        public int iIdUsuarioMod { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
