using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioReporte
    {
        public int iIdFolio { get; set; }
        public int iIdEmpresa { get; set; }
        public string sNombreEmpresa { get; set; }
        public string sFolioEmpresa { get; set; }
        public string sCorreoEmpresa { get; set; }
        public int iIdProducto { get; set; }
        public int iIdTipoProducto { get; set; }
        public string sTipoProducto { get; set; }
        public string sNombreProducto { get; set; }
        public int iMesVigenciaProducto { get; set; }
        public string sIcon { get; set; }
        public int iConsecutivo { get; set; }
        public int iIdOrigen { get; set; }
        public int iIdPaciente { get; set; }
        public string sCorreoPaciente { get; set; }
        public string sNombrePaciente { get; set; }
        public string sTelefonoPaciente { get; set; }
        public string sOrigen { get; set; }
        public string sFolio { get; set; }
        public string sPassword { get; set; }
        public string sOrdenConekta { get; set; }
        public bool bTerminosYCondiciones { get; set; }
        public DateTime? dtFechaVencimiento { get; set; }
        public string sFechaVencimiento { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
