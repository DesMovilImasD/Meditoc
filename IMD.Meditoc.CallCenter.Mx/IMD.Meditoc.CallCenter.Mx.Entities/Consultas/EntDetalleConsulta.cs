using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Consultas
{
    public class EntDetalleConsulta
    {
        public int? iIdConsulta { get; set; }
        public int? iIdPaciente { get; set; }
        public int? iIdColaborador { get; set; }
        public int? iIdEstatusConsulta { get; set; }
        public DateTime? dtFechaProgramadaInicio { get; set; }
        public string sFechaProgramadaInicio { get; set; }
        public DateTime? dtFechaProgramadaFin { get; set; }
        public string sFechaProgramadaFin { get; set; }
        public DateTime? dtFechaConsultaInicio { get; set; }
        public string sFechaConsultaInicio { get; set; }
        public DateTime? dtFechaConsultaFin { get; set; }
        public string sFechaConsultaFin { get; set; }
        public DateTime? dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
        public string sNombrePaciente { get; set; }
        public string sApellidoPaternoPaciente { get; set; }
        public string sApellidoMaternoPaciente { get; set; }
        public DateTime? dtFechaNacimientoPaciente { get; set; }
        public string sFechaNacimientoPaciente { get; set; }
        public string sCorreoPaciente { get; set; }
        public string sTelefonoPaciente { get; set; }
        public string sTipoSangrePaciente { get; set; }
        public string sSexoPaciente { get; set; }
        public int? iIdFolio { get; set; }
        public string sFolio { get; set; }
        public string sPassword { get; set; }
        public string sOrdenConekta { get; set; }
        public bool bTerminosYCondiciones { get; set; }
        public DateTime? dtFechaVencimiento { get; set; }
        public string sFechaVencimiento { get; set; }
        public int? iIdEmpresa { get; set; }
        public string sNombreEmpresa { get; set; }
        public string sFolioEmpresa { get; set; }
        public string sCorreoEmpresa { get; set; }
        public string sNombreProducto { get; set; }
        public int? iIdTipoProducto { get; set; }
        public string sTipoProducto { get; set; }
        public int? iIdOrigen { get; set; }
        public string sOrigen { get; set; }
        public int? iNumSala { get; set; }
        public string sNombreColaborador { get; set; }
        public string sCorreoColaborador { get; set; }
        public int? iIdEspecialidad { get; set; }
        public string sEspecialidad { get; set; }
        public int? iIdTipoDoctor { get; set; }
        public string sTipoDoctor { get; set; }
        public string sEstatusConsulta { get; set; }
    }
}
