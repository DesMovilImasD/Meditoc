using IMD.Meditoc.CallCenter.Mx.Entities.Empresa;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Entities.Reportes.Ventas
{
    public class EntOrdenReporte
    {
        public string uId { get; set; }
        public string sOrderId { get; set; }
        public double nAmount { get; set; }
        public double nAmountDiscount { get; set; }
        public double nAmountTax { get; set; }
        public double nAmountPaid { get; set; }
        public string sPaymentStatus { get; set; }
        public int iIdCupon { get; set; }
        public string sCodigo { get; set; }
        public EntCustomerInfo customer_info { get; set; }
        public List<EntLineItemReporte> lstItems { get; set; }
        public EntChargeReporte charges { get; set; }
        public DateTime dtRegisterDate { get; set; }
        public string sRegisterDate { get; set; }

    }
}
