﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Doctores
{
    public class EntReporteDoctores
    {
        public int iTotalDoctores { get; set; }
        public int iTotalConsultas { get; set; }
        public int iTotalPacientes { get; set; }
        public List<EntDoctorReporte> lstDoctores { get; set; }
    }
}
