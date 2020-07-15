using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.Pagos.Data;
using IMD.Meditoc.Pagos.Entities.Reporte;
using log4net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
                IMDResponse<List<EntReporteGeneric>> respuestaObtenerOrdenes = this.BObtenerRegistros(psStatus, psType, pdtFechaInicio, pdtFechaFinal);
                if (respuestaObtenerOrdenes.Code != 0)
                {
                    return respuestaObtenerOrdenes.GetResponse<List<EntOrderReporte>>();
                }

                List<EntOrderReporte> lstOrder = respuestaObtenerOrdenes.Result.GroupBy(x => x.uId).Select(x => new EntOrderReporte
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
                    }).OrderBy(y => y.iConsecutive).ToList()
                }).OrderBy(x => x.dtRegisterDate).ToList();

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
        public IMDResponse<MemoryStream> BDescargarReporte(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null)
        {
            IMDResponse<MemoryStream> response = new IMDResponse<MemoryStream>();

            string metodo = nameof(this.BDescargarReporte);
            logger.Info(IMDSerialize.Serialize(67823458166422, $"Inicia {metodo}"));

            try
            {
                IMDResponse<List<EntReporteGeneric>> respuestaObtenerOrdenes = this.BObtenerRegistros(psStatus, psType, pdtFechaInicio, pdtFechaFinal);
                if (respuestaObtenerOrdenes.Code != 0)
                {
                    return respuestaObtenerOrdenes.GetResponse<MemoryStream>();
                }
                MemoryStream memoryStream = new MemoryStream();
                using (ExcelPackage package = new ExcelPackage(memoryStream))
                {
                    package.Workbook.Worksheets.Add("Ordenes Conekta");

                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Ordenes Conekta"];

                    List<string> headers = new List<string>
                    {
                        "uId",
                        "Id Orden",
                        "Monto Total",
                        "Status",
                        "Tipo de Pago",
                        "Consecutivo",
                        "Id Artículo",
                        "Nombre Artículo",
                        "Precio Unitario",
                        "Cantidad",
                        "Subtotal",
                        "Nombre Cliente",
                        "Email",
                        "Teléfono",
                        "Código de Autorización",
                        "Fecha de Orden"
                    };

                    this.BReporteHeaders(worksheet, headers);

                    int row = 1;
                    respuestaObtenerOrdenes.Result.OrderBy(x => x.dtRegisterDate).ThenBy(x => x.iConsecutive).ToList().ForEach(x =>
                    {
                        worksheet.Cells[++row, 1].Value = x.uId;
                        worksheet.Cells[row, 2].Value = x.sOrderId;
                        worksheet.Cells[row, 3].Value = x.nAmount / 100;
                        worksheet.Cells[row, 4].Value = x.sPaymentStatus;
                        worksheet.Cells[row, 5].Value = x.sType;
                        worksheet.Cells[row, 6].Value = x.iConsecutive;
                        worksheet.Cells[row, 7].Value = x.sItemId;
                        worksheet.Cells[row, 8].Value = x.sItemName;
                        worksheet.Cells[row, 9].Value = x.nUnitPrice / 100;
                        worksheet.Cells[row, 10].Value = x.iQuantity;
                        worksheet.Cells[row, 11].Value = x.iQuantity * x.nUnitPrice / 100;
                        worksheet.Cells[row, 12].Value = x.sName;
                        worksheet.Cells[row, 13].Value = x.sEmail;
                        worksheet.Cells[row, 14].Value = x.sPhone;
                        worksheet.Cells[row, 15].Value = x.sAuthCode;
                        worksheet.Cells[row, 16].Value = x.dtRegisterDate.ToString("dd/MM/yyy HH:mm:ss");
                    });

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    MemoryStream ms = new MemoryStream(package.GetAsByteArray());
                    response.Result = ms;
                }
                response.Code = 0;
                response.Message = "Reporte creado";
            }
            catch (Exception ex)
            {
                response.Code = 67823458167199;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458167199, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        private IMDResponse<List<EntReporteGeneric>> BObtenerRegistros(string psStatus = null, string psType = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null)
        {
            IMDResponse<List<EntReporteGeneric>> response = new IMDResponse<List<EntReporteGeneric>>();

            string metodo = nameof(this.BObtenerRegistros);
            logger.Info(IMDSerialize.Serialize(67823458167976, $"Inicia {metodo}"));

            try
            {
                if (pdtFechaFinal != null)
                {
                    pdtFechaFinal = Convert.ToDateTime(pdtFechaFinal).AddDays(1);
                }
                IMDResponse<DataTable> respuestaObtenerOrdenes = datReportes.DObtenerReporteOrdenes(psStatus, psType, pdtFechaInicio, pdtFechaFinal);
                if (respuestaObtenerOrdenes.Code != 0)
                {
                    return respuestaObtenerOrdenes.GetResponse<List<EntReporteGeneric>>();
                }

                List<EntReporteGeneric> lstReporte = new List<EntReporteGeneric>();
                foreach (DataRow filaItem in respuestaObtenerOrdenes.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(filaItem);
                    EntReporteGeneric entReporteGeneric = new EntReporteGeneric
                    {
                        uId = dr.ConvertTo<Guid>("uId"),
                        sOrderId = dr.ConvertTo<string>("sOrderId"),
                        nAmount = dr.ConvertTo<double>("nAmount"),
                        sPaymentStatus = dr.ConvertTo<string>("sPaymentStatus"),
                        sType = dr.ConvertTo<string>("sType"),
                        iConsecutive = dr.ConvertTo<int>("iConsecutive"),
                        sItemId = dr.ConvertTo<string>("sItemId"),
                        sItemName = dr.ConvertTo<string>("sItemName"),
                        iQuantity = dr.ConvertTo<int>("iQuantity"),
                        nUnitPrice = dr.ConvertTo<double>("nUnitPrice"),
                        sName = dr.ConvertTo<string>("sName"),
                        sEmail = dr.ConvertTo<string>("sEmail"),
                        sPhone = dr.ConvertTo<string>("sPhone"),
                        sAuthCode = dr.ConvertTo<string>("sAuthCode"),
                        sChargeId = dr.ConvertTo<string>("sChargeId"),
                        dtRegisterDate = dr.ConvertTo<DateTime>("dtRegisterDate")
                    };
                    lstReporte.Add(entReporteGeneric);
                }

                response.Code = 0;
                response.Result = lstReporte;
            }
            catch (Exception ex)
            {
                response.Code = 67823458168753;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458168753, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BReporteHeaders(ExcelWorksheet worksheet, List<string> headers)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BReporteHeaders);
            logger.Info(IMDSerialize.Serialize(67823458169530, $"Inicia {metodo}"));

            try
            {
                int col = 0;
                headers.ForEach(x =>
                {
                    worksheet.Cells[1, ++col].Value = x;
                });

                worksheet.Cells[1, 1, 1, headers.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 1, 1, headers.Count].Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                worksheet.Cells[1, 1, 1, headers.Count].Style.Font.Color.SetColor(Color.White);
            }
            catch (Exception ex)
            {
                response.Code = 67823458170307;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458170307, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
