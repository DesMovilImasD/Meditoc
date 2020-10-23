using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioRequest
    {
        public int iIdRequest { get; set; }
        public string sNumberPhone { get; set; }
        public string sFolio { get; set; }
        public string sPassword { get; set; }
        public DateTime? dtFechaVencimiento { get; set; }
        public DateTime dtRequested { get; set; }
        public DateTime? dtLastRequest { get; set; }
    }
}
