using System;

using Xamarin.Forms;

namespace CallCenter.Helpers
{
    public class GlobalEventSender 
    {
        public const string PAYMENT_DELETE_PRODUCT = "callcenter.meditoc.comercial.payment.DELETE.product";
        public const string PAYMENT_ADD_REDEEM_CODE = "callcenter.meditoc.comercial.payment.ADD.redeem_code";
        public const string PAYMENT_CONTINUE_TO_PAY = "callcenter.meditoc.comercial.payment.CONTINUE.pay";
        public const string PAYMENT_EDIT_PRODUCT_QUANTITY = "callcenter.meditoc.comercial.payment.EDIT.product_quantity";
        public const string PAYMENT_COMFIRM_PAY = "callcenter.meditoc.comercial.payment.CONFIRM.product";
        public const string VIDEO_CLOSE_BY_MEDIC = "callcenter.meditoc.comercial.video.CLOSE.bymedic";
    }
}

