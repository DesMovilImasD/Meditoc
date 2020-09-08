﻿using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business;
using IMD.Meditoc.CallCenter.Mx.Entities;
using log4net;
using System;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Controllers
{
    public class PoliticasController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PoliticasController));

        [HttpGet]
        [Route("Api/Politicas/Get/Politicas")]
        public IMDResponse<EntPoliticas> GetPoliticas()
        {
            IMDResponse<EntPoliticas> response = new IMDResponse<EntPoliticas>();

            string metodo = nameof(this.GetPoliticas);
            logger.Info(IMDSerialize.Serialize(67823458379320, $"Inicia {metodo}"));

            try
            {
                BusPoliticas busPoliticas = new BusPoliticas();

                response = busPoliticas.ObtenerPoliticas();
            }
            catch (Exception ex)
            {
                response.Code = 67823458380097;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458380097, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}