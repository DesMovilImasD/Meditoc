using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Producto
{
    public class EntOrientacionIVA
    {
        public double dIva { get; set; }
        public List<EntProducto> lstProductos { get; set; }

        public EntOrientacionIVA()
        {
            lstProductos = new List<EntProducto>();
        }
    }
}
