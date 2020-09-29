using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Paciente
{
    public class EntUpdPaciente
    {
        public int iIdPaciente { get; set; }
        public string sNombre { get; set; }
        public string sCorreo { get; set; }
        public string sTelefono { get; set; }
        public string sTipoSangre { get; set; }
        public DateTime? dtFechaNacimiento { get; set; }
        public int? iIdSexo { get; set; }
        public int iIdUsuarioMod { get; set; }
    }
}
