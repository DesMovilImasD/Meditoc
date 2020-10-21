using System;
using System.Collections.Generic;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Correo;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IMD.Meditoc.CallCenter.Mx.Tests
{
    [TestClass]
    public class TestFolio
    {
        [TestMethod]
        public void LoginApp()
        {
            string folio = "VC0016001";
            string pass = "pVC13422";

            BusFolio busFolio = new BusFolio();
            IMDResponse<EntFolio> res = busFolio.BLoginApp(folio, pass);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }

        [TestMethod]
        public void ReenviarCorreo()
        {
            string order = "ord_2oWfa4A6DPweWLKre";

            BusCorreo busCorreo = new BusCorreo();
            var res = busCorreo.BReenviarCorreo(order);
        }

        [TestMethod]
        public void TFolioExterno()
        {
            EntEmpresaExterna entEmpresaExterna = new EntEmpresaExterna
            {
                client = new EntEmpresaExternaCliente
                {
                    email = "g098@live.com.mx",
                    name = "Cristopher"
                },
                origin = (int)EnumOrigen.Locutorios,
                products = new List<EntEmpresaExternaProducto>
                {
                    new EntEmpresaExternaProducto
                    {
                        id = 39,
                        qty = 1
                    }
                }
            };

            string req = JsonConvert.SerializeObject(entEmpresaExterna, Formatting.Indented);

            BusFolio busFolio = new BusFolio();

            var res = busFolio.BNuevoFolioEmpresaExterna(entEmpresaExterna);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }

        [TestMethod]
        public void TFolioLocutorio()
        {
            EntFolioLocutorios entFolio = new EntFolioLocutorios
            {
                iIdOrigen = (int)EnumOrigen.Locutorios,
                iIdProducto = 40,
                sNombre = "CristopherGe",
                sTelefono = "+52 999 297 44 46"
            };

            string req = JsonConvert.SerializeObject(entFolio, Formatting.Indented);

            BusFolio busFolio = new BusFolio();

            var res = busFolio.BNuevoFolioLocutorios(entFolio);

            string json = JsonConvert.SerializeObject(res, Formatting.Indented);
        }
    }
}
