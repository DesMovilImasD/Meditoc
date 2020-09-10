using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.Catalogos
{
    public class DatEspecialidad
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatEspecialidad));
        private Database database;
        IMDCommonData imdCommonData;
        string saveEspecialidad;
        string getEspecialidad;

        public DatEspecialidad()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveEspecialidad = "sva_cat_save_especialidad";
            getEspecialidad = "svc_cat_especialidades";
        }

        public IMDResponse<bool> DSaveEspecialidad(int piIdEspecialidad, string psNombre, int piIdUsuarioMod, bool pbActivo, bool pbBaja)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458446919, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveEspecialidad))
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
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458447696, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }


        public IMDResponse<DataTable> DGetEspecialidad(int? piIdEspecialidad = null)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458448473, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(getEspecialidad))
                {
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, piIdEspecialidad);

                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458449250;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458449250, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
