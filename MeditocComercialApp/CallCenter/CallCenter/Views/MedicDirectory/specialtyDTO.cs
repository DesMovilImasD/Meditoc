using System;
namespace CallCenter.Views.MedicDirectory
{
    public class specialtyDTO
    {

        public int iIdEspecialidad { get; set; }
        public string sNombre { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public string sFechaCreacion { get; set; }
        public int iIdUsuarioMod { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }

        public specialtyDTO()
        {
        }        
    }
}
