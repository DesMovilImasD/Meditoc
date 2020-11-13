using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;

namespace IMD.Meditoc.CallCenter.Mx.Business.Empresa
{
    public class BusEmpresa
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusEmpresa));
        DatEmpresa datEmpresa;

        public BusEmpresa()
        {
            datEmpresa = new DatEmpresa();
        }

        /// <summary>
        /// Guarda los datos de una empresa
        /// </summary>
        /// <param name="entEmpresa"></param>
        /// <returns></returns>
        public IMDResponse<EntEmpresa> BSaveEmpresa(EntEmpresa entEmpresa)
        {
            IMDResponse<bool> responseValidation = new IMDResponse<bool>();
            IMDResponse<EntEmpresa> response = new IMDResponse<EntEmpresa>();

            string metodo = nameof(this.BSaveEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458383982, $"Inicia {metodo}(EntEmpresa entEmpresa)", entEmpresa));

            try
            {
                responseValidation = BValidaDatos(entEmpresa);
                if (responseValidation.Code != 0)
                {
                    return response = responseValidation.GetResponse<EntEmpresa>();
                }

                IMDResponse<DataTable> dtEmpresa = datEmpresa.DSaveEmpresa(entEmpresa);
                if (dtEmpresa.Code != 0)
                {
                    return dtEmpresa.GetResponse<EntEmpresa>();
                }
                if (dtEmpresa.Result.Rows.Count < 1)
                {
                    response.Code = -76768273456;
                    response.Message = "No ha sido posible generar la empresa/cliente.";
                    return response;
                }

                EntEmpresa oEmpresa = new EntEmpresa();
                foreach (DataRow item in dtEmpresa.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);

                    oEmpresa.iIdEmpresa = dr.ConvertTo<int>("iIdEmpresa");
                    oEmpresa.sNombre = dr.ConvertTo<string>("sNombre");
                    oEmpresa.sFolioEmpresa = dr.ConvertTo<string>("sFolioEmpresa");
                    oEmpresa.sCorreo = dr.ConvertTo<string>("sCorreo");
                    oEmpresa.sFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion").ToString("dd/MM/yyyy");
                    oEmpresa.bActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo"));
                    oEmpresa.bBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja"));
                }

                response.Code = 0;
                response.Message = entEmpresa.iIdEmpresa == 0 ? "La empresa ha sido guardada correctamente." : "La empresa ha sido actualizada correctamente.";
                response.Result = oEmpresa;
            }
            catch (Exception ex)
            {
                response.Code = 67823458384759;
                response.Message = "Ocurrió un error inesperado al intentar guardar los datos de la empresa.";

                logger.Error(IMDSerialize.Serialize(67823458384759, $"Error en {metodo}(EntEmpresa entEmpresa): {ex.Message}", entEmpresa, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Valida los datos para guardar la empresa
        /// </summary>
        /// <param name="entEmpresa"></param>
        /// <returns></returns>
        public IMDResponse<bool> BValidaDatos(EntEmpresa entEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458385536, $"Inicia {metodo}(EntEmpresa entEmpresa)", entEmpresa));

            try
            {
                response.Code = 67823458385536;
                response.Result = false;

                if (string.IsNullOrWhiteSpace(entEmpresa.sNombre))
                {
                    response.Message = "El nombre de la empresa no puede ser vacío.";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(entEmpresa.sCorreo))
                {
                    response.Message = "El correo de la empresa no puede ser vacío.";
                    return response;
                }

                //Verificar si el correo no ha sido registrado en otra empresa
                IMDResponse<List<EntEmpresa>> lstEmpresas = BGetEmpresas(null);

                if (lstEmpresas.Result.Count > 0)
                {
                    if (lstEmpresas.Result.Exists(c => c.sCorreo == entEmpresa.sCorreo) && entEmpresa.iIdEmpresa == 0)
                    {
                        response.Message = "Ya existe una empresa registrada con el correo proporcionado.";
                        return response;
                    }

                }

                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458386313;
                response.Message = "Ocurrió un error inesperado al validar los datos ingresados de la empresa.";

                logger.Error(IMDSerialize.Serialize(67823458386313, $"Error en {metodo}(EntEmpresa entEmpresa): {ex.Message}", entEmpresa, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Obtiene la lista de las empresas solicitadas
        /// </summary>
        /// <param name="iIdEmpresa"></param>
        /// <param name="psCorreo"></param>
        /// <returns></returns>
        public IMDResponse<List<EntEmpresa>> BGetEmpresas(int? iIdEmpresa = null, string psCorreo = null, string psFolioEmpresa = null)
        {
            IMDResponse<List<EntEmpresa>> response = new IMDResponse<List<EntEmpresa>>();

            string metodo = nameof(this.BGetEmpresas);
            logger.Info(IMDSerialize.Serialize(67823458393306, $"Inicia {metodo}(int? iIdEmpresa, string psCorreo = null)", iIdEmpresa, psCorreo));

            try
            {
                IMDResponse<DataTable> dtEmpresa = datEmpresa.DGetEmpresas(iIdEmpresa, psCorreo, psFolioEmpresa);
                List<EntEmpresa> lstEmpresa = new List<EntEmpresa>();
                EntEmpresa entEmpresa;

                if (dtEmpresa.Result.Rows.Count < 1)
                {
                    response = dtEmpresa.GetResponse<List<EntEmpresa>>();
                }

                foreach (DataRow item in dtEmpresa.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    entEmpresa = new EntEmpresa();

                    entEmpresa.iIdEmpresa = dr.ConvertTo<int>("iIdEmpresa");
                    entEmpresa.sNombre = dr.ConvertTo<string>("sNombre");
                    entEmpresa.sFolioEmpresa = dr.ConvertTo<string>("sFolioEmpresa");
                    entEmpresa.sCorreo = dr.ConvertTo<string>("sCorreo");
                    entEmpresa.sFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion").ToShortDateString();
                    entEmpresa.bActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo"));
                    entEmpresa.bBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja"));

                    lstEmpresa.Add(entEmpresa);
                }

                response.Code = 0;
                response.Message = "La lista de empresas ha sido obtenida.";
                response.Result = lstEmpresa;
            }
            catch (Exception ex)
            {
                response.Code = 67823458394083;
                response.Message = "Ocurrió un error inesperado al obtener los datos de la empresa.";

                logger.Error(IMDSerialize.Serialize(67823458394083, $"Error en {metodo}(int? iIdEmpresa, string psCorreo = null): {ex.Message}", iIdEmpresa, psCorreo, ex, response));
            }
            return response;
        }
    }
}