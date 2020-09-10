using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioFV
    {
        public int iIdEmpresa { get; set; }
        public int iIdUsuario { get; set; }
        public DateTime dtFechaVencimiento { get; set; }
        public List<EntFolioFVItem> lstFolios { get; set; }
    }

    public class EntFolioFVItem
    {
        public int iIdFolio { get; set; }
    }
}
