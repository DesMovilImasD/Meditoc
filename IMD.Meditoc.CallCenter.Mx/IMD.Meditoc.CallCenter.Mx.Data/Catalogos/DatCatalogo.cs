using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace IMD.Meditoc.CallCenter.Mx.Data.Catalogos
{
    public class DatCatalogo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatCatalogo));
        private Database database;
        IMDCommonData imdCommonData;
        string spGetCatalogos;
        string spSaveEspecialidad;
        string spGetEspecialidad;
        string spGetEspecialidadFiltrado;

        public DatCatalogo()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spGetCatalogos = "svc_get_catalogos";
            spSaveEspecialidad = "sva_cat_save_especialidad";
            spGetEspecialidad = "svc_cat_especialidades";
            spGetEspecialidadFiltrado = "svc_cat_especialidades_filtro";
        }

        public IMDResponse<DataSet> DGetCatalogos()
        {
            IMDResponse<DataSet> response = new IMDResponse<DataSet>();

            string metodo = nameof(this.DGetCatalogos);
            logger.Info(IMDSerialize.Serialize(67823458642723, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetCatalogos))
                {
                    response = imdCommonData.DExecuteDS(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458643500;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar los catalogos del sistema.";

                logger.Error(IMDSerialize.Serialize(67823458643500, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> DSaveEspecialidad(int piIdEspecialidad, string psNombre, int piIdUsuarioMod, bool pbActivo, bool pbBaja)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458446919, $"Inicia {metodo}(int piIdEspecialidad, string psNombre, int piIdUsuarioMod, bool pbActivo, bool pbBaja)", piIdEspecialidad, psNombre, piIdUsuarioMod, pbActivo, pbBaja));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spSaveEspecialidad))
                {
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, piIdEspecialidad);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, psNombre);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, piIdUsuarioMod);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, pbActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, pbBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458447696;
                response.Message = "Ocurrió un error inesperado en la base de datos al guardar la especialidad.";

                logger.Error(IMDSerialize.Serialize(67823458447696, $"Error en {metodo}(int piIdEspecialidad, string psNombre, int piIdUsuarioMod, bool pbActivo, bool pbBaja): {ex.Message}", piIdEspecialidad, psNombre, piIdUsuarioMod, pbActivo, pbBaja, ex, response));
            }
            return response;
        }


        public IMDResponse<DataTable> DGetEspecialidad(int? piIdEspecialidad = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458448473, $"Inicia {metodo}(int? piIdEspecialidad = null)", piIdEspecialidad));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetEspecialidad))
                {
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, piIdEspecialidad);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458449250;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar las especialidades.";

                logger.Error(IMDSerialize.Serialize(67823458449250, $"Error en {metodo}(int? piIdEspecialidad = null): {ex.Message}", piIdEspecialidad, ex, response));
            }
            return response;
        }


        public IMDResponse<DataTable> DGetEspecialidadFiltrado(int? piIdEspecialidad = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458448473, $"Inicia {metodo}(int? piIdEspecialidad = null)", piIdEspecialidad));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(spGetEspecialidadFiltrado))
                {
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, piIdEspecialidad);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458449250;
                response.Message = "Ocurrió un error inesperado en la base de datos al consultar las especialidades.";

                logger.Error(IMDSerialize.Serialize(67823458449250, $"Error en {metodo}(int? piIdEspecialidad = null): {ex.Message}", piIdEspecialidad, ex, response));
            }
            return response;
        }
    }
}
