using IMD.Admin.Utilities.Business.Converter;
using IMD.Admin.Utilities.Business.Serialize;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.Pagos.Data;
using IMD.Meditoc.Pagos.Entities.Reporte;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.Pagos.Business
{
    public class BusReportes
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusReportes));

#if DEBUG
        private readonly DatReportes datReportes;
#else
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DatReportes datReportes;
#endif

        public BusReportes()
        {
            datReportes = new DatReportes();
        }

        public IMDResponse<List<EntOrderReporte>> BObtenerReporteOrdenes(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null)
        {
            IMDResponse<List<EntOrderReporte>> response = new IMDResponse<List<EntOrderReporte>>();

            string metodo = nameof(this.BObtenerReporteOrdenes);
            logger.Info(IMDSerialize.Serialize(67823458163314, $"Inicia {metodo}(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null)", psStatus, psType, pdtFechaInicio, pdtFechaFinal));

            try
            {
                IMDResponse<DataTable> respuestaObtenerOrdenes = datReportes.DObtenerReporteOrdenes(psStatus, psType, pdtFechaInicio, pdtFechaFinal);
                if(respuestaObtenerOrdenes.Code != 0)
                {
                     return respuestaObtenerOrdenes.GetResponse<List<EntOrderReporte>>();
                }

                List<EntReporteGeneric> lstReporte = new List<EntReporteGeneric>();
                foreach (DataRow filaItem in respuestaObtenerOrdenes.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(filaItem);
                    EntReporteGeneric entReporteGeneric = new EntReporteGeneric
                    {
                        iConsecutive = dr.ConvertTo<int>("iConsecutive"),
                        iQuantity = dr.ConvertTo<int>("iQuantity"),
                        nUnitPrice = dr.ConvertTo<double>("nUnitPrice"),
                        sItemId = dr.ConvertTo<string>("sItemId"),
                        sItemName = dr.ConvertTo<string>("sItemName"),
                        uId = dr.ConvertTo<Guid>("uId"),
                        dtRegisterDate = dr.ConvertTo<DateTime>("dtRegisterDate"),
                        nAmount = dr.ConvertTo<double>("nAmount"),
                        sAuthCode = dr.ConvertTo<string>("sAuthCode"),
                        sChargeId = dr.ConvertTo<string>("sChargeId"),
                        sEmail = dr.ConvertTo<string>("sEmail"),
                        sName = dr.ConvertTo<string>("sName"),
                        sOrderId = dr.ConvertTo<string>("sOrderId"),
                        sPaymentStatus = dr.ConvertTo<string>("sPaymentStatus"),
                        sPhone = dr.ConvertTo<string>("sPhone"),
                        sType = dr.ConvertTo<string>("sType")
                    };
                    lstReporte.Add(entReporteGeneric);
                }
                List<EntOrderReporte> lstOrder = lstReporte.GroupBy(x => x.uId).Select(x => new EntOrderReporte
                {
                    uId = x.Key,
                    dtRegisterDate = x.Select(y => y.dtRegisterDate).First(),
                    sRegisterDate = x.Select(y => y.dtRegisterDate.ToString("dd/MM/yyy HH:mm:ss")).First(),
                    nAmount = x.Select(y => y.nAmount).First(),
                    sAuthCode = x.Select(y => y.sAuthCode).First(),
                    sChargeId = x.Select(y => y.sChargeId).First(),
                    sEmail = x.Select(y => y.sEmail).First(),
                    sName = x.Select(y => y.sName).First(),
                    sOrderId = x.Select(y => y.sOrderId).First(),
                    sPaymentStatus = x.Select(y => y.sPaymentStatus).First(),
                    sPhone = x.Select(y => y.sPhone).First(),
                    sType = x.Select(y => y.sType).First(),
                    lstItems = x.Select(y => new EntItemReporte
                    {
                        iConsecutive = y.iConsecutive,
                        iQuantity = y.iQuantity,
                        nUnitPrice = y.nUnitPrice,
                        sItemId = y.sItemId,
                        sItemName = y.sItemName
                    }).ToList()
                }).ToList();

                response.Code = 0;
                response.Message = "Órdenes obtenidas";
                response.Result = lstOrder;
            }
            catch (Exception ex)
            {
                response.Code = 67823458164091;
                response.Message = "Ocurrió un error inesperado al consultar las órdenes en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458164091, $"Error en {metodo}(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null): {ex.Message}", psStatus, psType, pdtFechaInicio, pdtFechaFinal, ex, response));
            }
            return response;
        }
    }
}
