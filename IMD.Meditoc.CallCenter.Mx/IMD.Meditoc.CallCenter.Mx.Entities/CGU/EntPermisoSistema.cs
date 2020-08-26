using System.Collections.Generic;

namespace IMD.Meditoc.CallCenter.Mx.Entities.CGU
{
    public class EntPermisoSistema
    {
        public int iIdModulo { get; set; }
        public string sNombre { get; set; }
        public List<EntSubModuloPermiso> lstSubModulo { get; set; }
    }

    public class EntSubModuloPermiso
    {
        public int iIdModulo { get; set; }
        public int iIdSubModulo { get; set; }
        public string sNombre { get; set; }
        public List<EntBotonPermiso> lstBotones { get; set; }
    }

    public class EntBotonPermiso
    {
        public int iIdModulo { get; set; }
        public int iIdSubModulo { get; set; }
        public int iIdBoton { get; set; }
        public string sNombre { get; set; }
    }
}
