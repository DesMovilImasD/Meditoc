using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Consultas
{
    public class EntConsulta
    {
        public int iIdConsulta { get; set; }
        public int? iIdPaciente { get; set; }
        public int? iIdColaborador { get; set; }
        public int? iIdEstatusConsulta { get; set; }
        public DateTime? dtFechaProgramadaInicio { get; set; }
        public DateTime? dtFechaProgramadaFin { get; set; }
        public DateTime? dtFechaConsultaInicio { get; set; }
        public DateTime? dtFechaConsultaFin { get; set; }
    }
}