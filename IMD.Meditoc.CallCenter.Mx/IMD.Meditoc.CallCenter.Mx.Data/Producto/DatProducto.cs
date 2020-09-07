using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Data;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace IMD.Meditoc.CallCenter.Mx.Data.Producto
{
    public class DatProducto
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DatProducto));
        private Database database;
        IMDCommonData imdCommonData;
        string saveProducto;
        string ObtenerProductos;        

        public DatProducto()
        {
            imdCommonData = new IMDCommonData();
            string FsConnectionString = "cnxMeditoc";
            database = imdCommonData.DGetDatabase(FsConnectionString, "MeditocComercial", "Meditoc1");

            saveProducto = "sva_meditoc_save_producto";
            ObtenerProductos = "svc_meditoc_ObtenerProductos";
        }


        public IMDResponse<bool> DSaveProducto(EntProducto entProducto)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.DSaveProducto);
            logger.Info(IMDSerialize.Serialize(67823458405738, $"Inicia {metodo}(EntProducto entProducto)", entProducto));


            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(saveProducto))
                {
                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, entProducto.iIdProducto);
                    database.AddInParameter(dbCommand, "piIdTipoProducto", DbType.Int32, entProducto.iIdTipoProducto);
                    database.AddInParameter(dbCommand, "psNombre", DbType.String, entProducto.sNombre);
                    database.AddInParameter(dbCommand, "psNombreCorto", DbType.String, entProducto.sNombreCorto);
                    database.AddInParameter(dbCommand, "psDescripcion", DbType.String, entProducto.sDescripcion);
                    database.AddInParameter(dbCommand, "pfCosto", DbType.Double, entProducto.fCosto);
                    database.AddInParameter(dbCommand, "piMesVigencia", DbType.Int32, entProducto.iMesVigencia);
                    database.AddInParameter(dbCommand, "psIcon", DbType.String, entProducto.sIcon);
                    database.AddInParameter(dbCommand, "pbComercial", DbType.Boolean, entProducto.bComercial);
                    database.AddInParameter(dbCommand, "psPrefijoFolio", DbType.String, entProducto.sPrefijoFolio);
                    database.AddInParameter(dbCommand, "piIdUsuarioMod", DbType.Int32, entProducto.iIdUsuarioMod);
                    database.AddInParameter(dbCommand, "pbActivo", DbType.Boolean, entProducto.bActivo);
                    database.AddInParameter(dbCommand, "pbBaja", DbType.Boolean, entProducto.bBaja);

                    response = imdCommonData.DExecute(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458406515;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458406515, $"Error en {metodo}(EntProducto entProducto): {ex.Message}", entProducto, ex, response));
            }
            return response;
        }

        public IMDResponse<DataTable> DObterProductos(int? iIdProducto)
        {
            IMDResponse<DataTable> response = new IMDResponse<DataTable>();

            string metodo = nameof(this.DObterProductos);
            logger.Info(IMDSerialize.Serialize(67823458407292, $"Inicia {metodo}(int? iIdProducto)", iIdProducto));

            try
            {
                using (DbCommand dbCommand = database.GetStoredProcCommand(ObtenerProductos))
                {

                    database.AddInParameter(dbCommand, "piIdProducto", DbType.Int32, iIdProducto);
                    response = imdCommonData.DExecuteDT(database, dbCommand);
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458408069;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458408069, $"Error en {metodo}(int? iIdProducto): {ex.Message}", iIdProducto, ex, response));
            }
            return response;
        }
    }
}
