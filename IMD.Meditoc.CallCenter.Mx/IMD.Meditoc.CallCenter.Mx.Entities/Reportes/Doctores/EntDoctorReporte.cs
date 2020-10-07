using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Doctores
{
    public class EntDoctorReporte
    {
        public int iIdDoctor { get; set; }
        public string sNombre { get; set; }
        public string sTipoDoctor { get; set; }
        public string sEspecialidad { get; set; }
        public string sCedulaProfecional { get; set; }
        public string sTelefono { get; set; }
        public string sCorreo { get; set; }
        public string iNumSala { get; set; }
        public string sDireccionConsultorio { get; set; }
        public string sRFC { get; set; }
        public int iTotalConsultas { get; set; }
        public List<int> lstPacientes { get; set; }
        public List<EntConsultaReporte> lstConsultas { get; set; }
    }
}
