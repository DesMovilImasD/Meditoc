using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Colaborador;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Data.Colaborador
{
    public class DatColaborador
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatColaborador));
        private Database database;
        IMDCommonData imdCommonData;
        string saveColaborador;

        public DatColaborador()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveColaborador = "sva_meditoc_save_colaborador";
        }

        public IMDResponse<bool> DSaveColaborador(EntCreateColaborador entCreateColaborador)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveColaborador);
            logger.Info(IMDSerialize.Serialize(67823458456243, $"Inicia {metodo}"));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveColaborador))
                {
                    database.AddInParameter(dbCommand, "piIdColaborador", DbType.Int32, entCreateColaborador.iIdColaborador);
                    database.AddInParameter(dbCommand, "piIdTipoDoctor", DbType.Int32, entCreateColaborador.iIdTipoDoctor);
                    database.AddInParameter(dbCommand, "piIdEspecialidad", DbType.Int32, entCreateColaborador.iIdEspecialidad);
                    database.AddInParameter(dbCommand, "piIdUsuarioCGU", DbType.Int32, entCreateColaborador.iIdUsuarioCGU);
                    database.AddInParameter(dbCommand, "piIdTipoCuenta", DbType.Int32, entCreateColaborador.iIdTipoCuenta);
                    database.AddInParameter(dbCommand, "piNumSala", DbType.Int32, entCreateColaborador.iNumSala);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entCreateColaborador.sNombreDirectorio);
                    database.AddInParameter(dbCommand, "psCedulaProfecional", DbType.String, entCreateColaborador.sCedulaProfecional);
                    database.AddInParameter(dbCommand, "psTelefono", DbType.String, entCreateColaborador.sTelefonoDirectorio);
                    database.AddInParameter(dbCommand, "psCorreo", DbType.String, entCreateColaborador.sCorreoDirectorio);
                    database.AddInParameter(dbCommand, "psDireccionConsultorio", DbType.String, entCreateColaborador.sDireccionConsultorio);
                    database.AddInParameter(dbCommand, "psRFC", DbType.String, entCreateColaborador.sRFC);
                    database.AddInParameter(dbCommand, "psURL", DbType.String, entCreateColaborador.sURL);
                    database.AddInParameter(dbCommand, "psMaps", DbType.String, entCreateColaborador.sMaps);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entCreateColaborador.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entCreateColaborador.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458457020;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458457020, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
