using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Colaborador
{
    public class EntColaboradorDirectorio
    {
        public int iIdColaborador { get; set; }
        public int iIdEspecialidad { get; set; }
        public string sEspecialidad { get; set; }
        public string sNombre { get; set; }
        public string sCedulaProfecional { get; set; }
        public string sTelefono { get; set; }
        public string sCorreo { get; set; }
        public string sFoto { get; set; }
        public string sDireccionConsultorio { get; set; }
        public string sRFC { get; set; }
        public string sURL { get; set; }
        public string sMaps { get; set; }
    }
}
