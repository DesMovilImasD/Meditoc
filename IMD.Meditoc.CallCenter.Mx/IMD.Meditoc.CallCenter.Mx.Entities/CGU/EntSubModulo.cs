using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.CGU
{
    public class EntSubModulo
    {
        public int iIdModulo { get; set; }
        public int iIdSubModulo { get; set; }
        public string sNombre { get; set; }
        public int iIdUsuarioMod { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
