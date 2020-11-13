using System;
using CallCenter.Helpers;
using CallCenter.Renderers;
using CallCenter.Views.ProductList;
using CallCenter.Views.UserInfo;
using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public class PaymentDataTemplateSelector : DataTemplateSelector
    {
        public PaymentDataTemplateSelector()
        {
            this.ProductList = new DataTemplate(typeof(ProductListView));
            this.UserInfo = new DataTemplate(typeof(UserInfoView));
        }

        /// <summary>
        /// vista de la lista de productos
        /// </summary>
        public DataTemplate ProductList { get; set; }

        /// <summary>
        /// vista de la informacion de usuario.
        /// </summary>
        public DataTemplate UserInfo { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var obj = (PaymentPageInterface)item;
            return obj.Id is 1 ? ProductList : UserInfo;
        }

        

    }
}

