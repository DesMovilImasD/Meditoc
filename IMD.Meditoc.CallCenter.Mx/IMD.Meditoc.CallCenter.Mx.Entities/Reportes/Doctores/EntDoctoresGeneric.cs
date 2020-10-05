using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Doctores
{
    public class EntDoctoresGeneric
    {
        public int iIdColaborador { get; set; }
        public string sColaborador { get; set; }
        public string sTipoColaborador { get; set; }
        public string sEspecialidad { get; set; }
        public string sCedulaProfesional { get; set; }
        public string sTelefono { get; set; }
        public string sCorreo { get; set; }
        public string iNumSala { get; set; }
        public string sDireccionConsultorio { get; set; }
        public string sRFC { get; set; }
        public int iIdConsulta { get; set; }
        public DateTime? dtFechaProgramadaInicio { get; set; }
        public DateTime? dtFechaProgramadaFin { get; set; }
        public DateTime? dtFechaConsultaInicio { get; set; }
        public DateTime? dtFechaConsultaFin { get; set; }
        public string sEstatusConsulta { get; set; }
        public int iIdPaciente { get; set; }
        public int iIdFolioPaciente { get; set; }
        public string sNombrePaciente { get; set; }
        public string sPaternoPaciente { get; set; }
        public string sMaternoPaciente { get; set; }
        public string sTelefonoPaciente { get; set; }
        public string sCorreoPaciente { get; set; }
    }
}
