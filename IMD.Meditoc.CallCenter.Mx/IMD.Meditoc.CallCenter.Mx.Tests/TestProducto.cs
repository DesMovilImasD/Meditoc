using System;
using System.Collections.Generic;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Producto;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IMD.Meditoc.CallCenter.Mx.Tests
{
    [TestClass]
    public class TestProducto
    {
        [TestMethod]
        public void TestMethod1()
        {
            BusProducto busProducto = new BusProducto();
            IMDResponse<List<EntProducto>> res = busProducto.BGetProductoEmpresaExterna();

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }

        [TestMethod]
        public void TestMethod2()
        {
            BusProducto busProducto = new BusProducto();
           var res = busProducto.BGetOrientacionesLocutorios();

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
