using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Colaborador
{
    public class EntDirectorio
    {
        public int iTotalPaginas { get; set; }
        public List<EntColaboradorDirectorio> lstColaboradores { get; set; }

        public EntDirectorio()
        {
            lstColaboradores = new List<EntColaboradorDirectorio>();
        }
    }
}
