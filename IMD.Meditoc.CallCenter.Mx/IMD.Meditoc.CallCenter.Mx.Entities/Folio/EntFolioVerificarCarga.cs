using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioVerificarCarga
    {
        public string producto { get; set; }
        public int totalFolios { get; set; }
        public List<EntFolioUser> lstFolios { get; set; }

        public EntFolioVerificarCarga()
        {
            lstFolios = new List<EntFolioUser>();
        }
    }
}
