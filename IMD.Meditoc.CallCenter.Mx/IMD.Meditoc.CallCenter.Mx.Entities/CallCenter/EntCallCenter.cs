using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.CallCenter
{
    public class EntCallCenter
    {
        public EntColaborador entColaborador { get; set; }
        public EntFolioReporte entFolio { get; set; }
        public EntPaciente entPaciente { get; set; }
        public EntConsulta entConsulta { get; set; }
        public List<EntHistorialClinico> lstHistorialClinico { get; set; }

        public EntCallCenter()
        {
            //entColaborador = new EntColaborador();
            //entFolio = new EntFolioReporte();
            //entPaciente = new EntPaciente();
            //entConsulta = new EntConsulta();
            lstHistorialClinico = new List<EntHistorialClinico>();
        }
    }
}
