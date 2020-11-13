using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    public class CuestionarioModel
    {
        public string sFolio { get; set; }
        public string sLongitud { get; set; }
        public string sLatitud { get; set; }
        public string sError { get; set; }
        public string sNumero { get; set; }
        public List<PreguntasModel> lstPreguntas { get; set; }
    }
}
