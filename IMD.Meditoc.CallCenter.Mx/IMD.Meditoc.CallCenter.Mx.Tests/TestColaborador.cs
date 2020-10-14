using System;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Colaborador;
using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IMD.Meditoc.CallCenter.Mx.Tests
{
    [TestClass]
    public class TestColaborador
    {
        [TestMethod]
        public void TestMethod1()
        {
            BusColaborador busColaborador = new BusColaborador();

            IMDResponse<EntColaboradorStatus> res = busColaborador.BGetColaboradorStatus(16);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
