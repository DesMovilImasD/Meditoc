using System;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Business.Consulta;
using IMD.Meditoc.CallCenter.Mx.Entities.CallCenter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IMD.Meditoc.CallCenter.Mx.Tests
{
    [TestClass]
    public class TestCallCenter
    {
        [TestMethod]
        public void BCallCenter()
        {
            int iIdColaborador = 3;
            string sFolio = "VM0000080";

            BusCallCenter busCallCenter = new BusCallCenter();

            IMDResponse<EntCallCenter> res = busCallCenter.BCallCenterStartWithFolio(iIdColaborador, sFolio);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }

        [TestMethod]
        public void TIniciarConsulta()
        {
            int iIdConsulta = 1;
            int iIdColaborador = 3;
            int iIdUsuarioMod = 3;

            BusCallCenter busCallCenter = new BusCallCenter();

            IMDResponse<bool> res = busCallCenter.BIniciarConsulta(iIdConsulta, iIdColaborador, iIdUsuarioMod);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }

        [TestMethod]
        public void TFinalizarConsulta()
        {
            int iIdConsulta = 1;
            int iIdColaborador = 3;
            int iIdUsuarioMod = 3;

            BusCallCenter busCallCenter = new BusCallCenter();

            IMDResponse<bool> res = busCallCenter.BFinalizarConsulta(iIdConsulta, iIdColaborador, iIdUsuarioMod);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
