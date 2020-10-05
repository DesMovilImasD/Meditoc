using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes
{
    public class EntConsulta
    {
        public int iIdConsulta { get; set; }
        public DateTime? dtFechaProgramadaInicio { get; set; }
        public DateTime? dtFechaProgramadaFin { get; set; }
        public DateTime? dtFechaConsultaInicio { get; set; }
        public DateTime? dtFechaConsultaFin { get; set; }
        public string sFechaProgramadaInicio { get; set; }
        public string sFechaProgramadaFin { get; set; }
        public string sFechaConsultaInicio { get; set; }
        public string sFechaConsultaFin { get; set; }
        public string sEstatusConsulta { get; set; }
        public int iIdPaciente { get; set; }
        public EntPaciente entPaciente { get; set; }
    }
}
