using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.CallCenter
{
    public class EntOnlineMod
    {
        public int iIdColaborador { get; set; }
        public int iIdUsuarioMod { get; set; }
        public bool bOnline { get; set; }
        public bool bOcupado { get; set; }
    }
}
