using System;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IMD.Meditoc.CallCenter.Mx.Tests
{
    [TestClass]
    public class TestCGU
    {
        [TestMethod]
        public void TSaveModulo()
        {
            EntModulo entModulo = new EntModulo
            {
                bActivo = true,
                bBaja = false,
                iIdModulo = 0,
                iIdUsuarioMod = 1,
                //sNombre = "Configuración"
                sNombre = "Administracion"
            };

            BusModulo busModulo = new BusModulo();
            var res = busModulo.BSaveModulo(entModulo);
        }

        [TestMethod]
        public void TSaveSubModulo()
        {
            EntSubModulo entModulo = new EntSubModulo
            {
                bActivo = true,
                bBaja = false,
                iIdModulo = 2,
                iIdSubModulo = 0,
                iIdUsuarioMod = 1,
                sNombre = "Usuarios"
            };

            BusSubmodulo busSubModulo = new BusSubmodulo();
            var res = busSubModulo.BSaveSubModulo(entModulo);
        }

        [TestMethod]
        public void TSaveBoton()
        {
            EntBoton entBoton = new EntBoton
            {
                bActivo = true,
                bBaja = false,
                iIdBoton = 0,
                iIdModulo = 2,
                iIdSubModulo = 1,
                iIdUsuarioMod = 1,
                sNombre = "Agregar"
            };
            BusBoton busBoton = new BusBoton();
            var res = busBoton.BSaveBoton(entBoton);
        }

        [TestMethod]
        public void TGetPermisos()
        {
            BusPermiso busPermiso = new BusPermiso();
            var res = busPermiso.BObtenerPermisoxPerfil(null);

            var json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }

        [TestMethod]
        public void TRecuperar()
        {
            BusUsuario busUsuario = new BusUsuario();

            var res = busUsuario.BRecuperarPassword("g098@live.com.mx");
            var json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
