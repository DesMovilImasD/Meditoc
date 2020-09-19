using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes
{
    public class EnumReportes
    {
        public enum Origen { 
            WEB = 1,
            BD,
            APP,
            CALLCENTER,
            ADMINPANEL
        }
    }
}
