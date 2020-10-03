using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Folio
{
    public class EntFolioVerificarCarga
    {
        public int totalFolios { get; set; }
        public EntProducto entProducto { get; set; }
        public EntEmpresa entEmpresa { get; set; }
        public List<EntFolioUser> lstFolios { get; set; }

        public EntFolioVerificarCarga()
        {
            lstFolios = new List<EntFolioUser>();
            entProducto = new EntProducto();
            entEmpresa = new EntEmpresa();
        }

        //private iceServers: fm.icelink.IceServer[] = [
        //    new fm.icelink.IceServer("stun:turn.frozenmountain.com:3478?transport=udp"),
        //    // NB: The URL "turn:turn.icelink.fm:443" implies that the TURN server supports both UDP and TCP.
        //    // If you want to restrict the network protocol, append "?transport=udp" or "?transport=tcp" to
        //    // the URL, per RFC 7065: https://tools.ietf.org/html/rfc7065#section-3.1.
        //    new fm.icelink.IceServer("turn:turn.frozenmountain.com:80?transport=udp", "test", "pa55w0rd!"),
        //];
    }
}
