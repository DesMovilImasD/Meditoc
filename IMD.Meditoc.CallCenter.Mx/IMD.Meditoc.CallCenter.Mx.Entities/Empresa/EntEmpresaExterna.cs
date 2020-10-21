using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Empresa
{
    public class EntEmpresaExterna
    {
        public int origin { get; set; }
        public EntEmpresaExternaCliente client { get; set; }
        public List<EntEmpresaExternaProducto> products { get; set; }
    }

    public class EntEmpresaExternaCliente
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class EntEmpresaExternaProducto
    {
        public int id { get; set; }
        public int qty { get; set; }
    }
}
