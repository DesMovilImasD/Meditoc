using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IMDResponse<bool> BSaveEmpresa(EntEmpresa entEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveEmpresa);
            logger.Info(IMDSerialize.Serialize(67823458383982, $"Inicia {metodo}(EntEmpresa entEmpresa)", entEmpresa));

            try
            {
                response = BValidaDatos(entEmpresa);

                if (!response.Result)
                    return response;

                response = datEmpresa.DSaveEmpresa(entEmpresa);

                if (response.Code != 0)
                {
                    response.Message = "Ocurrio un error al guardar la empresa";
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458384759;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458384759, $"Error en {metodo}(EntEmpresa entEmpresa): {ex.Message}", entEmpresa, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BValidaDatos(EntEmpresa entEmpresa)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458385536, $"Inicia {metodo}(EntEmpresa entEmpresa)", entEmpresa));

            try
            {
                response.Code = 67823458385536;

                if (entEmpresa.sNombre == "")
                {
                    response.Message = "El nombre no puede ser vacio";
                    response.Result = false;
                    return response;
                }

                if (entEmpresa.sCorreo == "")
                {
                    response.Message = "El correo no puede ser vacio";
                    response.Result = false;
                    return response;
                }


                if (entEmpresa.sFolioEmpresa == "")
                {
                    response.Message = "Debe generar un folio para la empresa";
                    response.Result = false;
                    return response;
                }

                response.Code = 0;
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458386313;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458386313, $"Error en {metodo}(EntEmpresa entEmpresa): {ex.Message}", entEmpresa, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntEmpresa>> BGetEmpresas()
        {
            IMDResponse<List<EntEmpresa>> response = new IMDResponse<List<EntEmpresa>>();

            string metodo = nameof(this.BGetEmpresas);
            logger.Info(IMDSerialize.Serialize(67823458393306, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataTable> dtEmpresa = datEmpresa.DGetEmpresas();
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
                    entEmpresa.bActivo = dr.ConvertTo<bool>("bActivo");
                    entEmpresa.bBaja = dr.ConvertTo<bool>("bBaja");

                    lstEmpresa.Add(entEmpresa);
                }

                response.Code = 0;
                response.Message = "Lista de empresas";
                response.Result = lstEmpresa;
            }
            catch (Exception ex)
            {
                response.Code = 67823458394083;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458394083, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
