using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Consultas
{
    public class EntHistorialClinico
    {
        public int iIdHistorialClinico { get; set; }
        public int iIdConsulta { get; set; }
        public string sSintomas { get; set; }
        public string sDiagnostico { get; set; }
        public string sTratamiento { get; set; }
        public double fPeso { get; set; }
        public double fAltura { get; set; }
        public string sAlergias { get; set; }
        public string sComentarios { get; set; }
        public DateTime? dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
    }
}
