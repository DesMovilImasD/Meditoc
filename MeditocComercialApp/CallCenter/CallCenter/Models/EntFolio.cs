using System;
namespace CallCenter.Models
{
    public class EntFolio
    {
        public int iIdFolio { get; set; }
        public int iIdEmpresa { get; set; }
        public int iIdProducto { get; set; }
        public int iConsecutivo { get; set; }
        public int iIdOrigen { get; set; }
        public int iIdPaciente { get; set; }
        public string sFolio { get; set; }
        public string sPassword { get; set; }
        public string sOrdenConekta { get; set; }
        public bool bTerminosYCondiciones { get; set; }
        public DateTime dtFechaVencimiento { get; set; }
        public int iIdUsuarioMod { get; set; }
        public bool bEsAgendada { get; set; }
    }
}
