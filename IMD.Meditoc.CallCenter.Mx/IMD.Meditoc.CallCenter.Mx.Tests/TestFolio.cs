using System;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.Folio;
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
    }
}
