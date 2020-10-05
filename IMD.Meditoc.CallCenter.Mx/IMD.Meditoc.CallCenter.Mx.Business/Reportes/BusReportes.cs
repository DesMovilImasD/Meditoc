using IMD.Admin.Conekta.Entities.Orders;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Reportes;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using IMD.Meditoc.CallCenter.Mx.Entities.Reportes;
using IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Doctores;
using IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas;
using log4net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.Reportes
{
    public class BusReportes
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusReportes));

        #region Ventas
        /// <summary>
        /// Función: Obtiene los datos para reporte de ventas
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 15/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psFolio">Folio de la venta</param>
        /// <param name="psIdEmpresa">ID de la empresa</param>
        /// <param name="psIdProducto">ID del producto</param>
        /// <param name="psIdTipoProducto">ID del tipo de producto</param>
        /// <param name="psIdOrigen">ID del origen</param>
        /// <param name="psOrderId">Código de la orden</param>
        /// <param name="psStatus">Estatus del pago</param>
        /// <param name="psCupon">Cupon que se aplico</param>
        /// <param name="pdtFechaInicio">Fecha de creación de la orden desde...</param>
        /// <param name="pdtFechaFinal">...a la fecha de creación de la orden</param>
        /// <param name="pdtFechaVencimiento">Fecha de vencimiento del folio</param>
        /// <returns></returns>
        public IMDResponse<EntVentasReporte> BObtenerReporteVentas(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)
        {
            IMDResponse<EntVentasReporte> response = new IMDResponse<EntVentasReporte>();

            string metodo = nameof(this.BObtenerReporteVentas);
            logger.Info(IMDSerialize.Serialize(67823458561915, $"Inicia {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {
                IMDResponse<List<EntFolioGeneric>> respuestaObtenerFolios = this.BObtenerFolios(psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento);
                if (respuestaObtenerFolios.Code != 0)
                {
                    return respuestaObtenerFolios.GetResponse<EntVentasReporte>();
                }

                double dIVA = ConfigurationManager.AppSettings["nIVA"] != null ? Convert.ToDouble(ConfigurationManager.AppSettings["nIVA"]) : 0.16;

                //List<EntFolio> lstFolios = respuestaObtenerFolios.Result.GroupBy(x => x.iIdFolio).Select(x => new EntFolio
                //{
                //    iIdFolio = x.Key,
                //    sFolio = x.Select(y => y.sFolio).First(),
                //    bTerminosYCondiciones = x.Select(y => y.bTerminosYCondiciones).First(),
                //    iConsecutivo = x.Select(y => y.iConsecutivo).First(),
                //    iIdOrigen = x.Select(y => y.iIdOrigen).First(),
                //    sOrigen = x.Select(y => y.sOrigen).First(),
                //    sFechaVencimiento = x.Select(y => y.sFechaVencimiento).First(),
                //    dtFechaVencimiento = x.Select(y => y.dtFechaVencimiento).First(),
                //    entEmpresa = x.Select(e => new EntEmpresa
                //    {
                //        iIdEmpresa = e.iIdEmpresa,
                //        sNombre = e.sEmpresa,
                //        sFolioEmpresa = e.sFolioEmpresa,
                //        sCorreo = e.sCorreo
                //    }).FirstOrDefault(),
                //    entProducto = x.Select(p => new EntProducto
                //    {
                //        iIdProducto = p.iIdProducto,
                //        fCosto = p.fCosto,
                //        sNombre = p.sProducto,
                //        iMesVigencia = p.iMesVigencia,
                //        iIdTipoProducto = p.iIdTipoProducto,
                //        sTipoProducto = p.sTipoProducto
                //    }).FirstOrDefault(),
                //    entOrden = x.Select(o => new EntOrden
                //    {
                //        sOrderId = o.sOrderId,
                //        nAmount = o.nAmount / 100,
                //        nAmountDiscount = o.nAmountDiscount / 100,
                //        nAmountTax = o.nAmountTax / 100,
                //        nAmountPaid = (x.Select(y => y.iIdOrigen).First() == (int)EnumReportes.Origen.WEB || x.Select(y => y.iIdOrigen).First() == (int)EnumReportes.Origen.APP) ? o.nAmountPaid / 100 : o.fCosto + (o.fCosto * dIVA),
                //        sPaymentStatus = o.sPaymentStatus,
                //        sCodigo = o.sCodigo,
                //        iIdCupon = o.iIdCupon,
                //        dtRegisterDate = o.dtRegisterDate,
                //        sRegisterDate = o.dtRegisterDate.ToString("dd/MM/yyy HH:mm:ss"),
                //        customer_info = x.Select(c => new EntCustomerInfo
                //        {
                //            email = c.email,
                //            name = c.name,
                //            phone = c.phone,
                //        }).FirstOrDefault(),
                //        charges = x.Select(ch => new EntChargeReporte
                //        {
                //            sAuthCode = ch.sAuthCode,
                //            sChargeId = ch.sChargeId,
                //            sType = ch.sType
                //        }).FirstOrDefault(),
                //        lstItems = x.Select(i => new EntLineItemReporte
                //        {
                //            iConsecutive = i.iConsecutive,
                //            iQuantity = i.iQuantity,
                //            nUnitPrice = i.nUnitPrice / 100,
                //            sItemId = i.sItemId,
                //            sItemName = i.sItemName,
                //            iIdTitular = i.iIdTitular,
                //            sFolio = i.sNumeroMembresia,
                //        }).OrderBy(i => i.sFolio).ToList()
                //    }).FirstOrDefault()
                //}).ToList();

                //EntVentasReporte entVentasReporte = new EntVentasReporte();
                //int iOrdenes = lstFolios.Where(x => (x.iIdOrigen == (int)EnumReportes.Origen.WEB || x.iIdOrigen == (int)EnumReportes.Origen.APP) && x.entOrden.sPaymentStatus == "paid").Select(x => x.entOrden).Select(x => x.sOrderId).Distinct().Count();
                //iOrdenes += lstFolios.Where(x => x.iIdOrigen != (int)EnumReportes.Origen.WEB && x.iIdOrigen != (int)EnumReportes.Origen.APP).Count();

                //double dTotalPagado = lstFolios.Where(x => (x.iIdOrigen == (int)EnumReportes.Origen.WEB || x.iIdOrigen == (int)EnumReportes.Origen.APP) && x.entOrden.sPaymentStatus == "paid").Select(x => x.entOrden).Select(x => new { x.sOrderId, x.nAmountPaid }).Distinct().Sum(x => x.nAmountPaid);
                //dTotalPagado += lstFolios.Where(x => x.iIdOrigen != (int)EnumReportes.Origen.WEB && x.iIdOrigen != (int)EnumReportes.Origen.APP).Select(x => x.entOrden).Sum(x => x.nAmountPaid);

                //double dDescuento = lstFolios.Where(x => (x.iIdOrigen == (int)EnumReportes.Origen.WEB || x.iIdOrigen == (int)EnumReportes.Origen.APP) && x.entOrden.sPaymentStatus == "paid").Select(x => x.entOrden).Select(x => new { x.sOrderId, x.nAmountDiscount }).Distinct().Sum(x => x.nAmountDiscount);
                //dDescuento += lstFolios.Where(x => x.iIdOrigen != (int)EnumReportes.Origen.WEB && x.iIdOrigen != (int)EnumReportes.Origen.APP).Select(x => x.entOrden).Sum(x => x.nAmountDiscount);

                List<EntFolio> lstFolios = respuestaObtenerFolios.Result.Where(x => x.iIdOrigen != (int)EnumOrigen.Particular).GroupBy(x => x.iIdFolio).Select(x => new EntFolio
                {
                    iIdFolio = x.Key,
                    sFolio = x.Select(y => y.sFolio).First(),
                    bTerminosYCondiciones = x.Select(y => y.bTerminosYCondiciones).First(),
                    iConsecutivo = x.Select(y => y.iConsecutivo).First(),
                    iIdOrigen = x.Select(y => y.iIdOrigen).First(),
                    bConfirmado = x.Select(y => y.bConfirmado).First(),
                    sOrigen = x.Select(y => y.sOrigen).First(),
                    sFechaVencimiento = x.Select(y => y.sFechaVencimiento).First(),
                    dtFechaVencimiento = x.Select(y => y.dtFechaVencimiento).First(),
                    entEmpresa = x.Select(e => new EntEmpresa
                    {
                        iIdEmpresa = e.iIdEmpresa,
                        sNombre = e.sEmpresa,
                        sFolioEmpresa = e.sFolioEmpresa,
                        sCorreo = e.sCorreo
                    }).FirstOrDefault(),
                    entProducto = x.Select(p => new EntProducto
                    {
                        iIdProducto = p.iIdProducto,
                        fCosto = p.fCosto,
                        sNombre = p.sProducto,
                        iMesVigencia = p.iMesVigencia,
                        iIdTipoProducto = p.iIdTipoProducto,
                        sTipoProducto = p.sTipoProducto
                    }).FirstOrDefault(),
                    entOrden = x.Select(o => new EntOrden
                    {
                        sOrderId = o.sOrderId,
                        nAmount = o.nAmount / 100,
                        nAmountDiscount = o.nAmountDiscount / 100,
                        nAmountTax = o.nAmountTax / 100,
                        nAmountPaid = o.nAmountPaid / 100,
                        sPaymentStatus = o.sPaymentStatus,
                        sCodigo = o.sCodigo,
                        iIdCupon = o.iIdCupon,
                        dtRegisterDate = o.dtRegisterDate,
                        sRegisterDate = o.dtRegisterDate.ToString("dd/MM/yyy HH:mm:ss"),
                        customer_info = x.Select(c => new EntCustomerInfo
                        {
                            email = c.email,
                            name = c.name,
                            phone = c.phone,
                        }).FirstOrDefault(),
                        charges = x.Select(ch => new EntChargeReporte
                        {
                            sAuthCode = ch.sAuthCode,
                            sChargeId = ch.sChargeId,
                            sType = ch.sType
                        }).FirstOrDefault(),
                        lstItems = x.Select(i => new EntLineItemReporte
                        {
                            iConsecutive = i.iConsecutive,
                            iQuantity = i.iQuantity,
                            nUnitPrice = i.nUnitPrice / 100,
                            sItemId = i.sItemId,
                            sItemName = i.sItemName,
                            iIdTitular = i.iIdTitular,
                            sFolio = i.sNumeroMembresia,
                        }).OrderBy(i => i.sFolio).ToList()
                    }).FirstOrDefault()
                }).ToList();

                EntVentasReporte entVentasReporte = new EntVentasReporte();
                int iOrdenes = lstFolios.Where(x => x.entOrden.sPaymentStatus == "paid").Select(x => x.entOrden).Select(x => x.sOrderId).Distinct().Count();

                double dTotalPagado = lstFolios.Where(x => x.entOrden.sPaymentStatus == "paid").Select(x => x.entOrden).Select(x => new { x.sOrderId, x.nAmountPaid }).Distinct().Sum(x => x.nAmountPaid);

                double dDescuento = lstFolios.Where(x => x.entOrden.sPaymentStatus == "paid").Select(x => x.entOrden).Select(x => new { x.sOrderId, x.nAmountDiscount }).Distinct().Sum(x => x.nAmountDiscount);

                entVentasReporte.iTotalOrdenes = iOrdenes;
                entVentasReporte.iTotalCuponesAplicados = lstFolios.Select(x => x.entOrden).Select(x => new { x.sOrderId, x.sCodigo, x.sPaymentStatus }).Distinct().Where(x => !string.IsNullOrEmpty(x.sCodigo) && x.sPaymentStatus == "paid").Count();
                entVentasReporte.iTotalFolios = lstFolios.Count;
                entVentasReporte.dTotalVendido = dTotalPagado;
                entVentasReporte.dTotalDescontado = dDescuento;
                entVentasReporte.lstFolios = lstFolios;

                response.Code = 0;
                response.Message = "Folios obtenidos";
                response.Result = entVentasReporte;
            }
            catch (Exception ex)
            {
                response.Code = 67823458562692;
                response.Message = "Ocurrió un error inesperado al consultar los folios en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458562692, $"Error en {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto= null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null): {ex.Message}", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }

        public IMDResponse<EntReporteVenta> BReporteGlobalVentas(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)
        {
            IMDResponse<EntReporteVenta> response = new IMDResponse<EntReporteVenta>();

            string metodo = nameof(this.BReporteGlobalVentas);
            logger.Info(IMDSerialize.Serialize(67823458597657, $"Inicia {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {
                IMDResponse<List<EntFolioGeneric>> respuestaObtenerFolios = this.BObtenerFolios(psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento);
                if (respuestaObtenerFolios.Code != 0)
                {
                    return respuestaObtenerFolios.GetResponse<EntReporteVenta>();
                }

                double dIVA = ConfigurationManager.AppSettings["nIVA"] != null ? Convert.ToDouble(ConfigurationManager.AppSettings["nIVA"]) : 0.16;


                List<EntReporteOrden> ordenes = respuestaObtenerFolios.Result.Where(o => o.iIdOrigen == (int)EnumOrigen.APP || o.iIdOrigen == (int)EnumOrigen.WEB).GroupBy(o => o.sOrderId).Select(o => new EntReporteOrden
                {
                    sOrderId = o.Key,
                    charges = o.Select(c => new EntChargeReporte
                    {
                        sAuthCode = c.sAuthCode,
                        sChargeId = c.sChargeId,
                        sType = c.sType
                    }).FirstOrDefault(),
                    customer_info = o.Select(u => new EntCustomerInfo
                    {
                        email = u.email,
                        name = u.name,
                        phone = u.phone
                    }).FirstOrDefault(),
                    lstProductos = o.GroupBy(p => p.iConsecutive).Select(p => new EntReporteProducto
                    {
                        iConsecutivo = p.Key,
                        iIdProducto = p.Select(r => r.iIdProducto).FirstOrDefault(),
                        iIdTipoProducto = p.Select(r => r.iIdTipoProducto).FirstOrDefault(),
                        iQuantity = p.Select(r => r.iQuantity).FirstOrDefault(),
                        nUnitPrice = p.Select(r => r.nUnitPrice / 100).FirstOrDefault(),
                        sItemId = p.Select(r => r.sItemId).FirstOrDefault(),
                        sNombre = p.Select(r => r.sItemName).FirstOrDefault(),
                        sTipoProducto = p.Select(r => r.sTipoProducto).FirstOrDefault(),
                        lstFolios = p.Select(x => x.sPaymentStatus).First() == "declined" ? new List<EntReporteFolio>() : p.GroupBy(f => f.iIdFolio).Select(f => new EntReporteFolio
                        {
                            iIdFolio = f.Key,
                            sFolio = f.Select(r => r.sFolio).FirstOrDefault(),
                            bTerminosYCondiciones = f.Select(r => r.bTerminosYCondiciones).FirstOrDefault(),
                            sFechaVencimiento = f.Select(r => r.sFechaVencimiento).FirstOrDefault(),
                            bConfirmado = f.Select(r => r.bConfirmado).FirstOrDefault(),
                            sTerminosYCondiciones = f.Select(r => r.bTerminosYCondiciones).FirstOrDefault() ? "SI" : "NO"
                        }).ToList()
                    }).ToList(),
                    iIdCupon = o.Select(r => r.iIdCupon).FirstOrDefault(),
                    nAmount = o.Select(r => r.nAmount / 100).FirstOrDefault(),
                    nAmountDiscount = o.Select(r => r.nAmountDiscount / 100).FirstOrDefault(),
                    nAmountPaid = o.Select(r => r.nAmountPaid / 100).FirstOrDefault(),
                    nAmountTax = o.Select(r => r.nAmountTax / 100).FirstOrDefault(),
                    sCodigo = o.Select(r => r.sCodigo).FirstOrDefault(),
                    sPaymentStatus = o.Select(r => r.sPaymentStatus).FirstOrDefault(),
                    sRegisterDate = o.Select(r => r.sRegisterDate).FirstOrDefault(),
                    iIdOrigen = o.Select(r => r.iIdOrigen).FirstOrDefault(),
                    sOrigen = o.Select(r => r.sOrigen).FirstOrDefault(),
                    uId = o.Select(r => r.uId).FirstOrDefault(),
                }).ToList();

                List<EntReporteOrden> ordenesAdmin = respuestaObtenerFolios.Result.Where(x => x.iIdOrigen == (int)EnumOrigen.PanelAdministrativo || x.iIdOrigen == (int)EnumOrigen.BaseDeDatos).GroupBy(o => o.sOrderId).Select(o => new EntReporteOrden
                {
                    sOrderId = o.Key,
                    iIdEmpresa = o.Select(r => r.iIdEmpresa).FirstOrDefault(),
                    sCorreo = o.Select(r => r.sCorreo).FirstOrDefault(),
                    sFolioEmpresa = o.Select(r => r.sFolioEmpresa).FirstOrDefault(),
                    sNombre = o.Select(r => r.sEmpresa).FirstOrDefault(),
                    charges = o.Select(c => new EntChargeReporte
                    {
                        sAuthCode = c.sAuthCode,
                        sChargeId = c.sChargeId,
                        sType = c.sType
                    }).FirstOrDefault(),
                    customer_info = o.Select(u => new EntCustomerInfo
                    {
                        email = u.email,
                        name = u.name,
                        phone = u.phone
                    }).FirstOrDefault(),
                    lstProductos = o.GroupBy(p => p.iConsecutive).Select(p => new EntReporteProducto
                    {
                        iConsecutivo = p.Key,
                        iIdProducto = p.Select(r => r.iIdProducto).FirstOrDefault(),
                        iIdTipoProducto = p.Select(r => r.iIdTipoProducto).FirstOrDefault(),
                        iQuantity = p.Select(r => r.iQuantity).FirstOrDefault(),
                        nUnitPrice = p.Select(r => r.nUnitPrice / 100).FirstOrDefault(),
                        sItemId = p.Select(r => r.sItemId).FirstOrDefault(),
                        sNombre = p.Select(r => r.sItemName).FirstOrDefault(),
                        sTipoProducto = p.Select(r => r.sTipoProducto).FirstOrDefault(),
                        lstFolios = p.Select(x => x.sPaymentStatus).First() == "declined" ? new List<EntReporteFolio>() : p.GroupBy(f => f.iIdFolio).Select(f => new EntReporteFolio
                        {
                            iIdFolio = f.Key,
                            sFolio = f.Select(r => r.sFolio).FirstOrDefault(),
                            bTerminosYCondiciones = f.Select(r => r.bTerminosYCondiciones).FirstOrDefault(),
                            sFechaVencimiento = f.Select(r => r.sFechaVencimiento).FirstOrDefault(),
                            bConfirmado = f.Select(r => r.bConfirmado).FirstOrDefault(),
                            sTerminosYCondiciones = f.Select(r => r.bTerminosYCondiciones).FirstOrDefault() ? "SI" : "NO"
                        }).ToList()
                    }).ToList(),
                    iIdCupon = o.Select(r => r.iIdCupon).FirstOrDefault(),
                    nAmount = o.Select(r => r.nAmount / 100).FirstOrDefault(),
                    nAmountDiscount = o.Select(r => r.nAmountDiscount / 100).FirstOrDefault(),
                    nAmountPaid = o.Select(r => r.nAmountPaid / 100).FirstOrDefault(),
                    nAmountTax = o.Select(r => r.nAmountTax / 100).FirstOrDefault(),
                    sCodigo = o.Select(r => r.sCodigo).FirstOrDefault(),
                    sPaymentStatus = o.Select(r => r.sPaymentStatus).FirstOrDefault(),
                    sRegisterDate = o.Select(r => r.sRegisterDate).FirstOrDefault(),
                    iIdOrigen = o.Select(r => r.iIdOrigen).FirstOrDefault(),
                    sOrigen = o.Select(r => r.sOrigen).FirstOrDefault(),
                    uId = o.Select(r => r.uId).FirstOrDefault(),
                }).ToList();

                //List<EntReporteEmpresa> empresas = respuestaObtenerFolios.Result.Where(x => x.iIdOrigen == (int)EnumOrigen.PanelAdministrativo).GroupBy(x => x.iIdEmpresa).Select(e => new EntReporteEmpresa
                //{
                //    iIdEmpresa = e.Key,
                //    sCorreo = e.Select(r => r.sCorreo).FirstOrDefault(),
                //    sFolioEmpresa = e.Select(r => r.sFolioEmpresa).FirstOrDefault(),
                //    sNombre = e.Select(r => r.sEmpresa).FirstOrDefault(),
                //    lstProductos = e.GroupBy(f => f.iIdFolio).Select(p => new EntReporteProductoEmpresa
                //    {
                //        iConsecutivo = 1,
                //        iIdProducto = p.Select(r => r.iIdProducto).FirstOrDefault(),
                //        iIdTipoProducto = p.Select(r => r.iIdTipoProducto).FirstOrDefault(),
                //        iQuantity = 1,
                //        nUnitPrice = p.Select(r => r.fCosto).FirstOrDefault(),
                //        sNombre = p.Select(r => r.sProducto).FirstOrDefault(),
                //        sTipoProducto = p.Select(r => r.sTipoProducto).FirstOrDefault(),
                //        entFolio = p.Select(c => new EntReporteFolio
                //        {
                //            iIdFolio = c.iIdFolio,
                //            sFolio = c.sFolio,
                //            bTerminosYCondiciones = c.bTerminosYCondiciones,
                //            iIdOrigen = c.iIdOrigen,
                //            sFechaVencimiento = c.sFechaVencimiento,
                //            sOrigen = c.sOrigen
                //        }).FirstOrDefault()
                //    }).ToList(),
                //}).ToList();

                //for (int i = 0; i < empresas.Count; i++)
                //{
                //    empresas[i].dTotalSinIva = empresas[i].lstProductos.Sum(x => x.nUnitPrice);
                //    empresas[i].dTotalIva = empresas[i].dTotalSinIva * dIVA;
                //    empresas[i].dTotal = empresas[i].dTotalSinIva + empresas[i].dTotalIva;
                //    empresas[i].iTotalFolios = empresas[i].lstProductos.Count;
                //}


                EntReporteVenta entReporteVenta = new EntReporteVenta
                {
                    ResumenOrdenesAdmin = new EntResumenOrdenes
                    {
                        lstOrdenes = ordenesAdmin,
                        dTotalDescontado = ordenesAdmin.Where(x => x.sPaymentStatus != "declined").Sum(x => x.nAmountDiscount),
                        dTotalVendido = ordenesAdmin.Where(x => x.sPaymentStatus != "declined").Sum(x => x.nAmountPaid),
                        iTotalCuponesAplicados = ordenesAdmin.Where(x => !string.IsNullOrEmpty(x.sCodigo) && x.sPaymentStatus != "declined").Count(),
                        iTotalFolios = ordenesAdmin.Sum(x => x.lstProductos.Sum(y => y.lstFolios.Count)),
                        iTotalOrdenes = ordenesAdmin.Where(x => x.sPaymentStatus != "declined").Count(),
                        iTotalOrdenesRechazadas = ordenesAdmin.Where(x => x.sPaymentStatus == "declined").Count()
                    },
                    ResumenOrdenes = new EntResumenOrdenes
                    {
                        lstOrdenes = ordenes,
                        dTotalDescontado = ordenes.Where(x => x.sPaymentStatus != "declined").Sum(x => x.nAmountDiscount),
                        dTotalVendido = ordenes.Where(x => x.sPaymentStatus != "declined").Sum(x => x.nAmountPaid),
                        iTotalCuponesAplicados = ordenes.Where(x => !string.IsNullOrEmpty(x.sCodigo) && x.sPaymentStatus != "declined").Count(),
                        iTotalFolios = ordenes.Sum(x => x.lstProductos.Sum(y => y.lstFolios.Count)),
                        iTotalOrdenes = ordenes.Where(x => x.sPaymentStatus != "declined").Count(),
                        iTotalOrdenesRechazadas = ordenes.Where(x => x.sPaymentStatus == "declined").Count()
                    }
                };

                response.Code = 0;
                response.Message = "Reportes obtenidos";
                response.Result = entReporteVenta;
            }
            catch (Exception ex)
            {
                response.Code = 67823458598434;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458598434, $"Error en {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null): {ex.Message}", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Genera un documento excel con la información de ventas de folios
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 15/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psFolio">Folio de la venta</param>
        /// <param name="psIdEmpresa">ID de la empresa</param>
        /// <param name="psIdProducto">ID del producto</param>
        /// <param name="psIdTipoProducto">ID del tipo de producto</param>
        /// <param name="psIdOrigen">ID del origen</param>
        /// <param name="psOrderId">Código de la orden</param>
        /// <param name="psStatus">Estatus del pago</param>
        /// <param name="psCupon">Cupon que se aplico</param>
        /// <param name="pdtFechaInicio">Fecha de creación de la orden desde...</param>
        /// <param name="pdtFechaFinal">...a la fecha de creación de la orden</param>
        /// <param name="pdtFechaVencimiento">Fecha de vencimiento del folio</param>
        /// <returns></returns>
        public IMDResponse<MemoryStream> BDescargarReporteVentas(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)
        {
            IMDResponse<MemoryStream> response = new IMDResponse<MemoryStream>();

            string metodo = nameof(this.BDescargarReporteVentas);
            logger.Info(IMDSerialize.Serialize(67823458563469, $"Inicia {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto= null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {

                IMDResponse<EntVentasReporte> respuestaObtenerOrdenes = this.BObtenerReporteVentas(psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento);
                if (respuestaObtenerOrdenes.Code != 0)
                {
                    return respuestaObtenerOrdenes.GetResponse<MemoryStream>();
                }
                MemoryStream memoryStream = new MemoryStream();
                using (ExcelPackage package = new ExcelPackage(memoryStream))
                {
                    package.Workbook.Worksheets.Add("Ventas");
                    package.Workbook.Worksheets.Add("Folios");

                    ExcelWorksheet sheetVentas = package.Workbook.Worksheets["Ventas"];
                    ExcelWorksheet sheetFolios = package.Workbook.Worksheets["Folios"];

                    int row = 1;
                    int colLabel = 1;
                    int colValue = 7;
                    string orden = String.Empty;

                    #region Información Gral del Reporte de Ventas
                    sheetVentas.Cells[row, colLabel, row, colValue].Merge = true;
                    sheetVentas.Cells[row, colLabel, row, colValue].Value = "Resumen de Ventas";
                    sheetVentas.Cells[row, colLabel, row, colValue].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheetVentas.Cells[row, colLabel, row, colValue].Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    sheetVentas.Cells[row, colLabel, row, colValue].Style.Font.Color.SetColor(Color.White);
                    sheetVentas.Cells[row, colLabel, row, colValue].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    sheetVentas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetVentas.Cells[row, colLabel, row, colValue - 1].Value = "Total de órdenes:";
                    sheetVentas.Cells[row, colValue].Value = respuestaObtenerOrdenes.Result.iTotalOrdenes;

                    sheetVentas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetVentas.Cells[row, colLabel, row, colValue - 1].Value = "Total de folios:";
                    sheetVentas.Cells[row, colValue].Value = respuestaObtenerOrdenes.Result.iTotalFolios;

                    sheetVentas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetVentas.Cells[row, colLabel, row, colValue - 1].Value = "Total de cupones aplicados:";
                    sheetVentas.Cells[row, colValue].Value = respuestaObtenerOrdenes.Result.iTotalCuponesAplicados;

                    sheetVentas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetVentas.Cells[row, colLabel, row, colValue - 1].Value = "Total descuento por cupón:";
                    sheetVentas.Cells[row, colValue].Value = respuestaObtenerOrdenes.Result.dTotalDescontado;
                    sheetVentas.Cells[row, colValue].Style.Numberformat.Format = "$ #,##0.00";

                    sheetVentas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetVentas.Cells[row, colLabel, row, colValue - 1].Value = "Total de ventas:";
                    sheetVentas.Cells[row, colValue].Value = respuestaObtenerOrdenes.Result.dTotalVendido;
                    sheetVentas.Cells[row, colValue].Style.Numberformat.Format = "$ #,##0.00";

                    sheetVentas.Cells.AutoFitColumns();
                    #endregion

                    List<string> headers = new List<string>
                    {
                        "Folio",
                        "Consecutivo",
                        "Términos y Condiciones",
                        "Fecha de vencimiento",

                        "Empresa",
                        "Correo Empresa",

                        "Producto",
                        "Costo Producto",
                        "Duración de vigencia",
                        "Tipo Producto",

                        "Origen",
                        "Folio confirmado",

                        "Número de Orden",
                        "Fecha de Orden",
                        "Total sin IVA",
                        "IVA",
                        "Descuento",
                        "Total Pagado",
                        "Estatus",
                        "Código de Cupón",
                        "Tipo de Pago",

                        "Nombre de Cliente",
                        "Correo electrónico",
                        "Teléfono de Cliente",

                        "Código de Autorización"
                    };
                    row = 1;

                    this.BReporteHeaders(sheetFolios, headers, ++row);

                    #region Folios con ordenes en conekta
                    respuestaObtenerOrdenes.Result.lstFolios.ForEach(y =>
                    {
                        int col = 0;
                        #region Información General folio
                        sheetFolios.Cells[++row, ++col].Value = y.sFolio;
                        sheetFolios.Cells[row, ++col].Value = y.iConsecutivo;
                        sheetFolios.Cells[row, ++col].Value = y.bTerminosYCondiciones ? "SI" : "NO";
                        sheetFolios.Cells[row, ++col].Value = y.sFechaVencimiento == null ? "Sin asignar" : y.sFechaVencimiento;
                        #endregion

                        #region Empresa
                        sheetFolios.Cells[row, ++col].Value = y.entEmpresa.sNombre;
                        sheetFolios.Cells[row, ++col].Value = y.entEmpresa.sCorreo;
                        #endregion

                        #region Producto
                        sheetFolios.Cells[row, ++col].Value = y.entProducto.sNombre;
                        sheetFolios.Cells[row, ++col].Value = y.entProducto.fCosto.ToString("C2");
                        sheetFolios.Cells[row, ++col].Value = y.entProducto.iMesVigencia == 0 ? "Uso único" : y.entProducto.iMesVigencia == 1 ? "1 mes" : $"{y.entProducto.iMesVigencia} meses";
                        sheetFolios.Cells[row, ++col].Value = y.entProducto.sTipoProducto;
                        #endregion

                        #region Origen
                        sheetFolios.Cells[row, ++col].Value = y.sOrigen;
                        #endregion

                        sheetFolios.Cells[row, ++col].Value = y.bConfirmado ? "SI" : "NO";


                        if (!orden.Equals(y.entOrden.sOrderId))
                        {
                            orden = y.entOrden.sOrderId;
                            int iFolios = respuestaObtenerOrdenes.Result.lstFolios.Select(x => x.entOrden).Where(x => x.sOrderId == orden).Count();
                            int rowStart = row;
                            int rowFinish = row + (iFolios - 1);
                            #region Orden
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.sOrderId;
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.sRegisterDate;
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.nAmount.ToString("C2");
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.nAmountTax.ToString("C2");
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.nAmountDiscount.ToString("C2");
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.nAmountPaid.ToString("C2");
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.sPaymentStatus;
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.sCodigo;
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.charges.sType;
                            #endregion

                            #region customer_info(Cliente)
                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.customer_info.name;

                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.customer_info.email;

                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.customer_info.phone;
                            #endregion

                            sheetFolios.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetFolios.Cells[rowStart, col, rowFinish, col].Value = y.entOrden.charges.sAuthCode;

                            row = rowStart;
                        }
                    });
                    #endregion

                    //#region Folios sin ordenes en conekta
                    //respuestaObtenerOrdenes.Result.lstFolios.Where(x => x.iIdOrigen != (int)EnumReportes.Origen.WEB && x.iIdOrigen != (int)EnumReportes.Origen.APP).ToList().ForEach(y =>
                    //{
                    //    int col = 0;
                    //    #region Información General folio
                    //    sheetFolios.Cells[++row, ++col].Value = y.sFolio;
                    //    sheetFolios.Cells[row, ++col].Value = y.iConsecutivo;
                    //    sheetFolios.Cells[row, ++col].Value = y.bTerminosYCondiciones ? "SI" : "NO";
                    //    sheetFolios.Cells[row, ++col].Value = y.sFechaVencimiento;

                    //    #endregion

                    //    #region Empresa
                    //    sheetFolios.Cells[row, ++col].Value = y.entEmpresa.sNombre;
                    //    sheetFolios.Cells[row, ++col].Value = y.entEmpresa.sCorreo;
                    //    #endregion

                    //    #region Producto
                    //    sheetFolios.Cells[row, ++col].Value = y.entProducto.sNombre;
                    //    sheetFolios.Cells[row, ++col].Value = y.entProducto.fCosto;
                    //    sheetFolios.Cells[row, ++col].Value = y.entProducto.iMesVigencia;
                    //    sheetFolios.Cells[row, ++col].Value = y.entProducto.sTipoProducto;
                    //    #endregion

                    //    #region Origen
                    //    sheetFolios.Cells[row, ++col].Value = y.sOrigen;
                    //    #endregion

                    //    #region Orden
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.sOrderId;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.sRegisterDate;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.nAmount;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.nAmountTax;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.nAmountDiscount;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.nAmountPaid;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.sPaymentStatus;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.sCodigo;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.charges.sType;
                    //    #endregion

                    //    #region customer_info(Cliente)
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.customer_info.name;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.customer_info.email;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.customer_info.phone;
                    //    sheetFolios.Cells[row, ++col].Value = y.entOrden.charges.sAuthCode;
                    //    #endregion
                    //});
                    //#endregion

                    sheetFolios.Cells[sheetFolios.Dimension.Address].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheetFolios.Cells[sheetFolios.Dimension.Address].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheetFolios.Cells[sheetFolios.Dimension.Address].Style.WrapText = true;
                    sheetFolios.Cells[sheetFolios.Dimension.Address].AutoFitColumns();

                    MemoryStream ms = new MemoryStream(package.GetAsByteArray());
                    response.Result = ms;
                }
                response.Code = 0;
                response.Message = "Reporte creado";
            }
            catch (Exception ex)
            {
                response.Code = 67823458564246;
                response.Message = "Ocurrió un error inesperado al generar el reporte de órdenes";

                logger.Error(IMDSerialize.Serialize(67823458564246, $"Error en {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto= null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null): {ex.Message}", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Permite dar estilo a los encabezados de un hoja de excel
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 15/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="worksheet">hoja de excel</param>
        /// <param name="headers">encabezados</param>
        /// <param name="row">fila asignada</param>
        /// <returns></returns>
        public IMDResponse<bool> BReporteHeaders(ExcelWorksheet worksheet, List<string> headers, int row)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BReporteHeaders);
            logger.Info(IMDSerialize.Serialize(67823458169530, $"Inicia {metodo}(ExcelWorksheet worksheet, List<string> headers, int row)", headers, row));

            try
            {
                int col = 0;
                headers.ForEach(x =>
                {
                    worksheet.Cells[row, ++col].Value = x;
                });

                worksheet.Cells[row, 1, row, headers.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[row, 1, row, headers.Count].Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                worksheet.Cells[row, 1, row, headers.Count].Style.Font.Color.SetColor(Color.White);
            }
            catch (Exception ex)
            {
                response.Code = 67823458170307;
                response.Message = "Ocurrió un error inesperado al generar el reporte de ventas";

                logger.Error(IMDSerialize.Serialize(67823458170307, $"Error en {metodo}(ExcelWorksheet worksheet, List<string> headers, int row): {ex.Message}", headers, row, ex, response));
            }
            return response;
        }
        #endregion Ventas

        #region Doctores
        /// <summary>
        /// Función: Obtiene los datos para reporte de doctores
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 17/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psIdColaborador">folio del doctor</param>
        /// <param name="psColaborador">nombre del doctor</param>
        /// <param name="psIdTipoDoctor">id tipo de doctor</param>
        /// <param name="psIdEspecialidad">id de especialidad</param>
        /// <param name="psIdConsulta">id de la consulta</param>
        /// <param name="psIdEstatusConsulta">estatus de la consulta</param>
        /// <param name="psRFC">rfc del doctor</param>
        /// <param name="psNumSala">numero de sala de la consulta</param>
        /// <param name="pdtFechaProgramadaInicio">fecha inicial programada de la consulta</param>
        /// <param name="pdtFechaProgramadaFinal">fecha final programada de la consulta</param>
        /// <param name="pdtFechaConsultaInicio">fecha inicial real de la consulta</param>
        /// <param name="pdtFechaConsultaFin">fecha final real de la consulta</param>
        /// <returns></returns>
        public IMDResponse<EntDoctoresReporte> BObtenerReporteDoctores(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,
            string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFinal = null,
            DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<EntDoctoresReporte> response = new IMDResponse<EntDoctoresReporte>();

            string metodo = nameof(this.BObtenerReporteDoctores);
            logger.Info(IMDSerialize.Serialize(67823458565023, $"Inicia {metodo}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null)", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                IMDResponse<List<EntDoctoresGeneric>> respuestaObtenerConsultas = this.BObtenerConsultasDoctor(psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin);
                if (respuestaObtenerConsultas.Code != 0)
                {
                    return respuestaObtenerConsultas.GetResponse<EntDoctoresReporte>();
                }

                List<EntDoctor> lstDoctores = respuestaObtenerConsultas.Result.GroupBy(x => x.iIdColaborador).Select(x => new EntDoctor
                {
                    iIdDoctor = x.Key,
                    sNombre = x.Select(d => d.sColaborador).First(),
                    sTipoDoctor = x.Select(d => d.sTipoColaborador).First(),
                    sEspecialidad = x.Select(d => d.sEspecialidad).First(),
                    sCedulaProfecional = x.Select(d => d.sCedulaProfesional).First(),
                    sTelefono = x.Select(d => d.sTelefono).First(),
                    sCorreo = x.Select(d => d.sCorreo).First(),
                    iNumSala = x.Select(d => d.iNumSala).First(),
                    sDireccionConsultorio = x.Select(d => d.sDireccionConsultorio).First(),
                    sRFC = x.Select(d => d.sRFC).First(),
                    iTotalConsultas = x.Where(c => c.iIdConsulta != 0).Count(),
                    lstPacientes = x.Select(d => d.iIdPaciente).ToList(),
                    lstConsultas = x.Select(c => new EntConsulta
                    {
                        iIdConsulta = c.iIdConsulta,
                        sEstatusConsulta = c.sEstatusConsulta,
                        dtFechaProgramadaInicio = c.dtFechaProgramadaInicio,
                        dtFechaProgramadaFin = c.dtFechaProgramadaFin,
                        dtFechaConsultaInicio = c.dtFechaConsultaInicio,
                        dtFechaConsultaFin = c.dtFechaConsultaFin,
                        sFechaProgramadaInicio = c.dtFechaProgramadaInicio?.ToString(),
                        sFechaProgramadaFin = c.dtFechaProgramadaFin?.ToString(),
                        sFechaConsultaInicio = c.dtFechaConsultaInicio?.ToString(),
                        sFechaConsultaFin = c.dtFechaConsultaFin?.ToString(),
                        iIdPaciente = c.iIdPaciente,
                        entPaciente = x.Select(p => new EntPaciente
                        {
                            iIdPaciente = p.iIdPaciente,
                            iIdFolio = p.iIdFolioPaciente,
                            sNombre = p.sNombrePaciente,
                            sApellidoPaterno = p.sPaternoPaciente,
                            sApellidoMaterno = p.sMaternoPaciente,
                            sTelefono = p.sTelefonoPaciente,
                            sCorreo = p.sCorreo
                        }).Where(p => p.iIdPaciente == c.iIdPaciente).FirstOrDefault()
                    }).Where(c => c.iIdConsulta != 0).OrderBy(i => i.dtFechaProgramadaInicio).ToList()
                }).ToList();

                List<int> lstPacientes = new List<int>();
                foreach (EntDoctor doctor in lstDoctores)
                {
                    lstPacientes.AddRange(doctor.lstPacientes);
                };

                EntDoctoresReporte entDoctoresReporte = new EntDoctoresReporte();
                entDoctoresReporte.iTotalConsultas = lstDoctores.Sum(x => x.iTotalConsultas);
                entDoctoresReporte.iTotalDoctores = lstDoctores.Count;
                entDoctoresReporte.iTotalPacientes = lstPacientes.Where(x => x != 0).Distinct().Count();
                entDoctoresReporte.lstDoctores = lstDoctores;

                response.Code = 0;
                response.Message = "Doctores obtenidos";
                response.Result = entDoctoresReporte;
            }
            catch (Exception ex)
            {
                response.Code = 67823458565800;
                response.Message = "Ocurrió un error inesperado al consultar la información de los doctores en la base de datos";

                logger.Error(IMDSerialize.Serialize(67823458565800, $"Error en {metodo}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null): {ex.Message}", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Obtiene los datos y convierne en celdas de un archivo excel para general el reporte de doctores
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 15/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psIdColaborador">folio del doctor</param>
        /// <param name="psColaborador">nombre del doctor</param>
        /// <param name="psIdTipoDoctor">id tipo de doctor</param>
        /// <param name="psIdEspecialidad">id de especialidad</param>
        /// <param name="psIdConsulta">id de la consulta</param>
        /// <param name="psIdEstatusConsulta">estatus de la consulta</param>
        /// <param name="psRFC">rfc del doctor</param>
        /// <param name="psNumSala">numero de sala de la consulta</param>
        /// <param name="pdtFechaProgramadaInicio">fecha inicial programada de la consulta</param>
        /// <param name="pdtFechaProgramadaFinal">fecha final programada de la consulta</param>
        /// <param name="pdtFechaConsultaInicio">fecha inicial real de la consulta</param>
        /// <param name="pdtFechaConsultaFin">fecha final real de la consulta</param>
        /// <returns></returns>
        public IMDResponse<MemoryStream> BDescargarReporteDoctores(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,
            string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFinal = null,
            DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<MemoryStream> response = new IMDResponse<MemoryStream>();

            string metodo = nameof(this.BDescargarReporteDoctores);
            logger.Info(IMDSerialize.Serialize(67823458566577, $"Inicia {metodo}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null)", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                IMDResponse<EntDoctoresReporte> respuestaObtenerDoctores = this.BObtenerReporteDoctores(psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin);
                if (respuestaObtenerDoctores.Code != 0)
                {
                    return respuestaObtenerDoctores.GetResponse<MemoryStream>();
                }
                MemoryStream memoryStream = new MemoryStream();
                using (ExcelPackage package = new ExcelPackage(memoryStream))
                {
                    package.Workbook.Worksheets.Add("Consultas");
                    package.Workbook.Worksheets.Add("Doctores");

                    ExcelWorksheet sheetConsultas = package.Workbook.Worksheets["Consultas"];
                    ExcelWorksheet sheetDoctores = package.Workbook.Worksheets["Doctores"];

                    int row = 1;
                    int colLabel = 1;
                    int colValue = 7;
                    int iddoctor = 0;

                    #region Información Gral del Reporte de Consultas
                    sheetConsultas.Cells[row, colLabel, row, colValue].Merge = true;
                    sheetConsultas.Cells[row, colLabel, row, colValue].Value = "Resumen de Consultas";
                    sheetConsultas.Cells[row, colLabel, row, colValue].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheetConsultas.Cells[row, colLabel, row, colValue].Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    sheetConsultas.Cells[row, colLabel, row, colValue].Style.Font.Color.SetColor(Color.White);
                    sheetConsultas.Cells[row, colLabel, row, colValue].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    sheetConsultas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetConsultas.Cells[row, colLabel, row, colValue - 1].Value = "Total de doctores:";
                    sheetConsultas.Cells[row, colValue].Value = respuestaObtenerDoctores.Result.iTotalDoctores;

                    sheetConsultas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetConsultas.Cells[row, colLabel, row, colValue - 1].Value = "Total de pacientes:";
                    sheetConsultas.Cells[row, colValue].Value = respuestaObtenerDoctores.Result.iTotalPacientes;

                    sheetConsultas.Cells[++row, colLabel, row, colValue - 1].Merge = true;
                    sheetConsultas.Cells[row, colLabel, row, colValue - 1].Value = "Total de consultas:";
                    sheetConsultas.Cells[row, colValue].Value = respuestaObtenerDoctores.Result.iTotalConsultas;

                    sheetConsultas.Cells.AutoFitColumns();
                    #endregion

                    List<string> headers = new List<string>
                    {
                        "Folio",
                        "Nombre de Doctor",
                        "Tipo de Doctor",
                        "  Especialidad  ",
                        "Cedula Profesional",
                        "Telefono Doctor",
                        "Correo Eléctronico",
                        "Número de Sala",
                        "Dirección de Consultorio",
                        " RFC ",
                        "Total Consultas",

                        "Folio de Consulta",
                        "Fecha Programada Inicio",
                        "Fecha Programada Fin",
                        "Fecha Consulta Inicio",
                        "Fecha Consulta Fin",
                        "Estatus de Consulta",

                        "Número de Paciente",
                        "Folio Paciente",
                        "Nombre Paciente",
                        "Apellido Paterno",
                        "Apellido Materno",
                        "Telefono Paciente",
                        "Correo Electrónico"
                    };
                    row = 1;

                    this.BReporteHeaders(sheetDoctores, headers, ++row);

                    #region Folios con ordenes en conekta
                    respuestaObtenerDoctores.Result.lstDoctores.ForEach(y =>
                    {
                        int col = 0;
                        if (!iddoctor.Equals(y.iIdDoctor))
                        {
                            iddoctor = y.iIdDoctor;

                            int rowStart = ++row;
                            int rowFinish = row + ((y.iTotalConsultas == 0) ? y.iTotalConsultas : (y.iTotalConsultas - 1));

                            #region Información del doctor
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.iIdDoctor;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sNombre;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sTipoDoctor;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sEspecialidad;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sCedulaProfecional;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sTelefono;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sCorreo;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.iNumSala;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sDireccionConsultorio;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.sRFC;
                            sheetDoctores.Cells[rowStart, ++col, rowFinish, col].Merge = true;
                            sheetDoctores.Cells[rowStart, col, rowFinish, col].Value = y.iTotalConsultas;
                            #endregion

                            row = (y.iTotalConsultas == 0) ? rowStart : (rowStart - 1);
                        }

                        #region Información General folio
                        if (y.iTotalConsultas != 0)
                        {
                            foreach (EntConsulta consulta in y.lstConsultas)
                            {
                                int colFor = col;
                                sheetDoctores.Cells[++row, ++col].Value = consulta.iIdConsulta;
                                sheetDoctores.Cells[row, ++col].Value = consulta.sFechaProgramadaInicio;
                                sheetDoctores.Cells[row, ++col].Value = consulta.sFechaProgramadaFin;
                                sheetDoctores.Cells[row, ++col].Value = consulta.sFechaConsultaInicio;
                                sheetDoctores.Cells[row, ++col].Value = consulta.sFechaConsultaFin;
                                sheetDoctores.Cells[row, ++col].Value = consulta.sEstatusConsulta;

                                sheetDoctores.Cells[row, ++col].Value = consulta.entPaciente.iIdPaciente;
                                sheetDoctores.Cells[row, ++col].Value = consulta.entPaciente.iIdFolio;
                                sheetDoctores.Cells[row, ++col].Value = consulta.entPaciente.sNombre;
                                sheetDoctores.Cells[row, ++col].Value = consulta.entPaciente.sApellidoPaterno;
                                sheetDoctores.Cells[row, ++col].Value = consulta.entPaciente.sApellidoMaterno;
                                sheetDoctores.Cells[row, ++col].Value = consulta.entPaciente.sTelefono;
                                sheetDoctores.Cells[row, ++col].Value = consulta.entPaciente.sCorreo;

                                col = colFor;
                            }
                        }
                        #endregion
                    });
                    #endregion

                    sheetDoctores.Cells[sheetDoctores.Dimension.Address].AutoFitColumns();
                    sheetDoctores.Cells[sheetDoctores.Dimension.Address].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheetDoctores.Cells[sheetDoctores.Dimension.Address].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    sheetDoctores.Cells[sheetDoctores.Dimension.Address].Style.WrapText = true;

                    MemoryStream ms = new MemoryStream(package.GetAsByteArray());
                    response.Result = ms;
                }
                response.Code = 0;
                response.Message = "Reporte creado";
            }
            catch (Exception ex)
            {
                response.Code = 67823458567354;
                response.Message = "Ocurrió un error inesperado al generar el reporte de doctores";

                logger.Error(IMDSerialize.Serialize(67823458567354, $"Error en {metodo}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null): {ex.Message}", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }

        #endregion

        #region Consultas para los usuarios

        /// <summary>
        /// Función: obtiene una lista genérica con los datos de reporte de folios directo de la base de datos
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 14/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psFolio">Folio de la venta</param>
        /// <param name="psIdEmpresa">ID de la empresa</param>
        /// <param name="psIdProducto">ID del producto</param>
        /// <param name="psIdTipoProducto">ID del tipo de producto</param>
        /// <param name="psIdOrigen">ID del origen</param>
        /// <param name="psOrderId">Código de la orden</param>
        /// <param name="psStatus">Estatus del pago</param>
        /// <param name="psCupon">Cupon que se aplico</param>
        /// <param name="pdtFechaInicio">Fecha de creación de la orden desde...</param>
        /// <param name="pdtFechaFinal">...a la fecha de creación de la orden</param>
        /// <param name="pdtFechaVencimiento">Fecha de vencimiento del folio</param>
        /// <returns></returns>
        public IMDResponse<List<EntFolioGeneric>> BObtenerFolios(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)
        {
            IMDResponse<List<EntFolioGeneric>> response = new IMDResponse<List<EntFolioGeneric>>();
            DatReportes datReportes = new DatReportes();

            string metodo = nameof(this.BObtenerFolios);
            logger.Info(IMDSerialize.Serialize(67823458568131, $"Inicia {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null)", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento));

            try
            {
                pdtFechaInicio = pdtFechaInicio?.Date;
                pdtFechaFinal = pdtFechaFinal?.AddDays(1).Date;
                IMDResponse<DataTable> respuestaObtenerOrdenes = datReportes.DObtenerReporteVentas(psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento);
                if (respuestaObtenerOrdenes.Code != 0)
                {
                    return respuestaObtenerOrdenes.GetResponse<List<EntFolioGeneric>>();
                }

                List<EntFolioGeneric> lstFolios = new List<EntFolioGeneric>();
                foreach (DataRow filaItem in respuestaObtenerOrdenes.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(filaItem);
                    EntFolioGeneric entFolio = new EntFolioGeneric
                    {
                        iIdFolio = dr.ConvertTo<int>("iIdFolio"),
                        sFolio = dr.ConvertTo<string>("sFolio"),
                        iConsecutivo = dr.ConvertTo<int>("iConsecutivo"),
                        bConfirmado = Convert.ToBoolean(dr.ConvertTo<int>("bConfirmado")),
                        bTerminosYCondiciones = Convert.ToBoolean(dr.ConvertTo<int>("bTerminosYCondiciones")),
                        dtFechaVencimiento = dr.ConvertTo<DateTime?>("dtFechaVencimiento"),
                        //sFechaVencimiento = dr.ConvertTo<string>("dtFechaVencimiento"),

                        #region empresa
                        iIdEmpresa = dr.ConvertTo<int>("iIdEmpresa"),
                        sEmpresa = dr.ConvertTo<string>("sEmpresa"),
                        sFolioEmpresa = dr.ConvertTo<string>("sFolioEmpresa"),
                        sCorreo = dr.ConvertTo<string>("sCorreo"),
                        #endregion

                        #region producto
                        iIdProducto = dr.ConvertTo<int>("iIdProducto"),
                        fCosto = dr.ConvertTo<double>("fCosto"),
                        sProducto = dr.ConvertTo<string>("sProducto"),
                        iMesVigencia = dr.ConvertTo<int>("iMesVigencia"),
                        iIdTipoProducto = dr.ConvertTo<int>("iIdTipoProducto"),
                        sTipoProducto = dr.ConvertTo<string>("sTipoProducto"),
                        #endregion

                        #region origen
                        iIdOrigen = dr.ConvertTo<int>("iIdOrigen"),
                        sOrigen = dr.ConvertTo<string>("sOrigen"),
                        #endregion

                        #region orden
                        uId = dr.ConvertTo<string>("uId"),
                        sOrderId = dr.ConvertTo<string>("sOrderId"),
                        nAmount = dr.ConvertTo<double>("nAmount"),
                        nAmountDiscount = dr.ConvertTo<double>("nAmountDiscount"),
                        nAmountTax = dr.ConvertTo<double>("nAmountTax"),
                        nAmountPaid = dr.ConvertTo<double>("nAmountPaid"),
                        sPaymentStatus = dr.ConvertTo<string>("sPaymentStatus"),
                        dtRegisterDate = dr.ConvertTo<DateTime>("dtRegisterDate"),
                        //sRegisterDate = dr.ConvertTo<string>("dtRegisterDate"),
                        #endregion

                        #region cupones
                        iIdCupon = dr.ConvertTo<int>("iIdCupon"),
                        sCodigo = dr.ConvertTo<string>("sCodigo"),
                        #endregion

                        #region custom (cliente)
                        name = dr.ConvertTo<string>("sName"),
                        email = dr.ConvertTo<string>("sEmail"),
                        phone = dr.ConvertTo<string>("sPhone"),
                        #endregion

                        #region items (detalles)
                        iConsecutive = dr.ConvertTo<int>("iConsecutive"),
                        sItemId = dr.ConvertTo<string>("sItemId"),
                        sItemName = dr.ConvertTo<string>("sItemName"),
                        nUnitPrice = dr.ConvertTo<double>("nUnitPrice"),
                        iQuantity = dr.ConvertTo<int>("iQuantity"),
                        #endregion

                        #region charges (cargos)
                        sAuthCode = dr.ConvertTo<string>("sAuthCode"),
                        sChargeId = dr.ConvertTo<string>("sChargeId"),
                        sType = dr.ConvertTo<string>("sType"),
                        #endregion

                        #region membresia
                        sNumeroMembresia = dr.ConvertTo<string>("sNumeroMembresia"),
                        iIdTitular = dr.ConvertTo<int>("iIdTitular")
                        #endregion
                    };
                    entFolio.sFechaVencimiento = entFolio.dtFechaVencimiento?.ToString("dd/MM/yyyy HH:mm");
                    entFolio.sRegisterDate = entFolio.dtRegisterDate.ToString("dd/MM/yyyy HH:mm");
                    lstFolios.Add(entFolio);
                }

                response.Code = 0;
                response.Result = lstFolios;
            }
            catch (Exception ex)
            {
                response.Code = 67823458568908;
                response.Message = "Ocurrió un error inesperado al obtener el reporte de ventas";

                logger.Error(IMDSerialize.Serialize(67823458568908, $"Error en {metodo}(string psFolio = null, string psIdEmpresa = null, string psIdProducto = null, string psIdTipoProducto = null, string psIdOrigen = null, string psOrderId = null, string psStatus = null, string psCupon = null, string psTipoPago = null, DateTime? pdtFechaInicio = null, DateTime? pdtFechaFinal = null, DateTime? pdtFechaVencimiento = null): {ex.Message}", psFolio, psIdEmpresa, psIdProducto, psIdTipoProducto, psIdOrigen, psOrderId, psStatus, psCupon, psTipoPago, pdtFechaInicio, pdtFechaFinal, pdtFechaVencimiento, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: obtiene una lista genérica con los datos de reporte de doctores directo de la base de datos
        /// Creado: Anahi Duarte 
        /// Fecha de Creación: 17/09/2020
        /// Modificado:
        /// Fecha de Modificación: 
        /// </summary>
        /// <param name="psIdColaborador">folio del doctor</param>
        /// <param name="psColaborador">nombre del doctor</param>
        /// <param name="psIdTipoDoctor">id tipo de doctor</param>
        /// <param name="psIdEspecialidad">id de especialidad</param>
        /// <param name="psIdConsulta">id de la consulta</param>
        /// <param name="psIdEstatusConsulta">estatus de la consulta</param>
        /// <param name="psRFC">rfc del doctor</param>
        /// <param name="psNumSala">numero de sala de la consulta</param>
        /// <param name="pdtFechaProgramadaInicio">fecha inicial programada de la consulta</param>
        /// <param name="pdtFechaProgramadaFinal">fecha final programada de la consulta</param>
        /// <param name="pdtFechaConsultaInicio">fecha inicial real de la consulta</param>
        /// <param name="pdtFechaConsultaFin">fecha final real de la consulta</param>
        /// <returns></returns>
        private IMDResponse<List<EntDoctoresGeneric>> BObtenerConsultasDoctor(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,
            string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime? pdtFechaProgramadaInicio = null, DateTime? pdtFechaProgramadaFinal = null,
            DateTime? pdtFechaConsultaInicio = null, DateTime? pdtFechaConsultaFin = null)
        {
            IMDResponse<List<EntDoctoresGeneric>> response = new IMDResponse<List<EntDoctoresGeneric>>();
            DatReportes datReportes = new DatReportes();

            string metodo = nameof(this.BObtenerConsultasDoctor);
            logger.Info(IMDSerialize.Serialize(67823458569685, $"Inicia {metodo}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null)", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin));

            try
            {
                pdtFechaProgramadaInicio = pdtFechaProgramadaInicio?.Date;
                pdtFechaProgramadaFinal = pdtFechaProgramadaFinal?.AddDays(1).Date;
                pdtFechaConsultaInicio = pdtFechaConsultaInicio?.Date;
                pdtFechaConsultaFin = pdtFechaConsultaFin?.AddDays(1).Date;

                IMDResponse<DataTable> respuestaObtenerConsultas = datReportes.DObtenerReporteDoctores(psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin);
                if (respuestaObtenerConsultas.Code != 0)
                {
                    return respuestaObtenerConsultas.GetResponse<List<EntDoctoresGeneric>>();
                }

                List<EntDoctoresGeneric> lstConsultas = new List<EntDoctoresGeneric>();
                foreach (DataRow filaItem in respuestaObtenerConsultas.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(filaItem);
                    EntDoctoresGeneric entDoctoresGeneric = new EntDoctoresGeneric
                    {
                        #region doctor
                        iIdColaborador = dr.ConvertTo<int>("iIdColaborador"),
                        sColaborador = dr.ConvertTo<string>("sColaborador"),
                        sTipoColaborador = dr.ConvertTo<string>("sTipoColaborador"),
                        sEspecialidad = dr.ConvertTo<string>("sEspecialidad"),
                        sCedulaProfesional = dr.ConvertTo<string>("sCedulaProfecional"),
                        sTelefono = dr.ConvertTo<string>("sTelefono"),
                        sCorreo = dr.ConvertTo<string>("sCorreo"),
                        iNumSala = dr.ConvertTo<string>("iNumSala"),
                        sDireccionConsultorio = dr.ConvertTo<string>("sDireccionConsultorio"),
                        sRFC = dr.ConvertTo<string>("sRFC"),
                        #endregion

                        #region consulta
                        iIdConsulta = dr.ConvertTo<int>("iIdConsulta"),
                        dtFechaProgramadaInicio = dr.ConvertTo<DateTime?>("dtFechaProgramadaInicio"),
                        dtFechaProgramadaFin = dr.ConvertTo<DateTime?>("dtFechaProgramadaFin"),
                        dtFechaConsultaInicio = dr.ConvertTo<DateTime?>("dtFechaConsultaInicio"),
                        dtFechaConsultaFin = dr.ConvertTo<DateTime?>("dtFechaConsultaFin"),
                        sEstatusConsulta = dr.ConvertTo<string>("sEstatusConsulta"),
                        #endregion

                        #region paciente
                        iIdPaciente = dr.ConvertTo<int>("iIdPaciente"),
                        iIdFolioPaciente = dr.ConvertTo<int>("iIdFolioPaciente"),
                        sNombrePaciente = dr.ConvertTo<string>("sNombrePaciente"),
                        sPaternoPaciente = dr.ConvertTo<string>("sPaternoPaciente"),
                        sMaternoPaciente = dr.ConvertTo<string>("sMaternoPaciente"),
                        sTelefonoPaciente = dr.ConvertTo<string>("sTelefonoPaciente"),
                        sCorreoPaciente = dr.ConvertTo<string>("sCorreoPaciente")
                        #endregion

                    };
                    lstConsultas.Add(entDoctoresGeneric);
                }

                response.Code = 0;
                response.Result = lstConsultas;
            }
            catch (Exception ex)
            {
                response.Code = 67823458570462;
                response.Message = "Ocurrió un error inesperado al obtener la información del reporte de doctores";

                logger.Error(IMDSerialize.Serialize(67823458570462, $"Error en {metodo}(string psIdColaborador = null, string psColaborador = null, string psIdTipoDoctor = null, string psIdEspecialidad = null, string psIdConsulta = null,string psIdEstatusConsulta = null, string psRFC = null, string psNumSala = null, DateTime ? pdtFechaProgramadaInicio = null, DateTime ? pdtFechaProgramadaFinal = null,DateTime ? pdtFechaConsultaInicio = null, DateTime ? pdtFechaConsultaFin = null): {ex.Message}", psIdColaborador, psColaborador, psIdTipoDoctor, psIdEspecialidad, psIdConsulta, psIdEstatusConsulta, psRFC, psNumSala, pdtFechaProgramadaInicio, pdtFechaProgramadaFinal, pdtFechaConsultaInicio, pdtFechaConsultaFin, ex, response));
            }
            return response;
        }
        #endregion
    }
}
