using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntFolioGeneric
    {
        public int iIdFolio { get; set; }
        public string sFolio { get; set; }
        public int iConsecutivo { get; set; }
        public bool bTerminosYCondiciones { get; set; }
        public int iIdOrigen { get; set; }
        public bool bConfirmado { get; set; }
        public string sOrigen { get; set; }
        public DateTime? dtFechaVencimiento { get; set; }
        public string sFechaVencimiento { get; set; }
        public int iIdEmpresa { get; set; }
        public string sEmpresa { get; set; }
        public string sFolioEmpresa { get; set; }
        public string sCorreo { get; set; }
        public int iIdProducto { get; set; }
        public int iIdTipoProducto { get; set; }
        public string sTipoProducto { get; set; }
        public string sProducto { get; set; }
        public string sNombreCorto { get; set; }
        public string sDescripcion { get; set; }
        public double fCosto { get; set; }
        public string sCosto { get; set; }
        public int iMesVigencia { get; set; }
        public string uId { get; set; }
        public string sOrderId { get; set; }
        public double nAmount { get; set; }
        public double nAmountDiscount { get; set; }
        public double nAmountTax { get; set; }
        public double nAmountPaid { get; set; }
        public string sPaymentStatus { get; set; }
        public int iIdCupon { get; set; }
        public string sCodigo { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string sAuthCode { get; set; }
        public string sChargeId { get; set; }
        public string sType { get; set; }
        public int iConsecutive { get; set; }
        public string sItemId { get; set; }
        public string sItemName { get; set; }
        public double nUnitPrice { get; set; }
        public int iQuantity { get; set; }
        public string sNumeroMembresia { get; set; }
        public int iIdTitular { get; set; }
        public DateTime dtRegisterDate { get; set; }
        public string sRegisterDate { get; set; }
    }
}
