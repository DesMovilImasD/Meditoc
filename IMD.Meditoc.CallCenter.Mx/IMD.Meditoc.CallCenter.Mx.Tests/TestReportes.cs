using System;
using IMD.Meditoc.CallCenter.Mx.Business.Reportes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IMD.Meditoc.CallCenter.Mx.Tests
{
    [TestClass]
    public class TestReportes
    {
        [TestMethod]
        public void TestMethod1()
        {
            BusReportes busReportes = new BusReportes();

            var res = busReportes.BReporteGlobalVentas();

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
        [TestMethod]
        public void TestMethod2()
        {
            BusReportes busReportes = new BusReportes();

            var res = busReportes.BObtenerFolios();

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
