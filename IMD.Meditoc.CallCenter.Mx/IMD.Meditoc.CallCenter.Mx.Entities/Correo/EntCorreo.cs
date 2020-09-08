using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities
{
    public class EntCorreo
    {
        public string sServerMail { get; set; }
        public string sUserMail { get; set; }
        public string sPassMail { get; set; }
        public bool bSSLMail { get; set; }
        public string sAsuntoMail { get; set; }
        public string sMensajeMail { get; set; }
        public int iPortMail { get; set; }
        public bool bAdjuntarFile { get; set; }
        public string sFile { get; set; }
    }
}
