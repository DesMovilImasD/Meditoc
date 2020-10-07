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
    public class DatEspecialidad
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatEspecialidad));
        private Database database;
        IMDCommonData imdCommonData;
        string spSaveEspecialidad;
        string spGetEspecialidad;

        public DatEspecialidad()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            spSaveEspecialidad = "sva_cat_save_especialidad";
            spGetEspecialidad = "svc_cat_especialidades";
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
    }
}
