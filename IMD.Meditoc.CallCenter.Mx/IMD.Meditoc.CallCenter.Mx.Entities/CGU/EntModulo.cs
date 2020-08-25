using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.CGU
{
    public class EntModulo
    {
        public int iIdModulo { get; set; }
        public int iIdUsuarioMod { get; set; }
        public string sNombre { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
