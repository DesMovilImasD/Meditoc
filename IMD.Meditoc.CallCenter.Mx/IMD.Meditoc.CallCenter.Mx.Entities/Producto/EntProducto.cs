using System;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Producto
{
    public class EntProducto
    {
        public int iIdProducto { get; set; }
        public int iIdTipoProducto { get; set; }
        public string sTipoProducto { get; set; }
        public int iIdGrupoProducto { get; set; }
        public string sGrupoProducto { get; set; }
        public string sNombre { get; set; }
        public string sNombreCorto { get; set; }
        public string sDescripcion { get; set; }
        public double fCosto { get; set; }
        public string sCosto { get; set; }
        public int iMesVigencia { get; set; }
        public string sIcon { get; set; }
        public bool bComercial { get; set; }
        public string sComercial { get; set; }
        public string sPrefijoFolio { get; set; }
        public int iIdUsuarioMod { get; set; }
        public bool bActivo { get; set; }
        public bool bBaja { get; set; }
    }
}
