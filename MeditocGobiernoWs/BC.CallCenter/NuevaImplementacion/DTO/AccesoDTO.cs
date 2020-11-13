using System;

namespace BC.CallCenter.NuevaImplementacion.DTO
{
    public class AccesoDTO
    {
        public int iIdAcceso { get; set; }
        public string sTelefono { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public DateTime? dtFechaBaja { get; set; }
        public bool bBaja { get; set; }
        public string sNombreDispositivo { get; set; }
    }
}
