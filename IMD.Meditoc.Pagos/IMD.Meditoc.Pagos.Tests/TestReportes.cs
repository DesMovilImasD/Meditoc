using System;
using System.Collections.Generic;
using System.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.Pagos.Business;
using IMD.Meditoc.Pagos.Data;
using IMD.Meditoc.Pagos.Entities.Reporte;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IMD.Meditoc.Pagos.Tests
{
    [TestClass]
    public class TestReportes
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TestReportes));

        [TestMethod]
        public void TestMethod1()
        {
            DatReportes datPagos = new DatReportes();
            IMDResponse<DataTable> res = datPagos.DObtenerReporteOrdenes();
        }

        [TestMethod]
        public void TestMethod2()
        {
            BusReportes busReportes = new BusReportes();
            IMDResponse<List<EntOrderReporte>> res = busReportes.BObtenerReporteOrdenes();
            var json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
