using System;
using IMD.Admin.Conekta.Entities.Orders;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Business.Consulta;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.CallCenter;
using IMD.Meditoc.CallCenter.Mx.Entities.Consultas;
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

        [TestMethod]
        public void TNuevaConsultaFolio()
        {
            EntNuevaConsulta entNuevaConsulta = new EntNuevaConsulta
            {
                sFolio = "VE0000132",
                consulta = new EntConsulta
                {
                    iIdColaborador = 3,
                    dtFechaProgramadaInicio = Convert.ToDateTime("2020-09-18 13:00"),
                    dtFechaProgramadaFin = Convert.ToDateTime("2020-09-18 14:00"),
                },
                customerInfo = new EntCustomerInfo
                {
                    email = "g098@live.com.mx",
                    name = "Cristopher",
                    phone = "9992974446"
                }
            };

            BusFolio busFolio = new BusFolio();

            var res = busFolio.BNuevoFolioEspecialista(entNuevaConsulta);
            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
