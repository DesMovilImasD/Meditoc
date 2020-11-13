using System;
using System.Collections.Generic;
using CallCenter.Helpers;
using Xamarin.Forms;

namespace CallCenter.Views.ProductList
{

    public partial class ProductListView : ContentView
    {
        public ProductListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// borra un elemento de la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Delete_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var ctx = this.BindingContext as ProductListDTO;
            ProductItemDTO item = ((Button)sender).BindingContext as ProductItemDTO;
            MessagingCenter.Send<ProductListView, ProductItemDTO>(this, GlobalEventSender.PAYMENT_DELETE_PRODUCT, item);
        }

        /// <summary>
        /// continuar al modo de pago.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Continue2Pay_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            MessagingCenter.Send<ProductListView, object>(this, GlobalEventSender.PAYMENT_CONTINUE_TO_PAY, e);
        }

        /// <summary>
        /// Codigo de canjeo de descuento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RedeemCode_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            MessagingCenter.Send<ProductListView, object>(this, GlobalEventSender.PAYMENT_ADD_REDEEM_CODE, e);
        }

        void Decrement_Clicked(System.Object sender, System.EventArgs e)
        {
            var ctx = this.BindingContext as ProductListDTO;
            ProductItemDTO item = ((Button)sender).BindingContext as ProductItemDTO;
            item.Quantity -= item.Quantity == 1 ? 0 : 1;
            item.Render();
            ctx.RenderCost();
        }

        void Increment_Clicked(System.Object sender, System.EventArgs e)
        {
            var ctx = this.BindingContext as ProductListDTO;
            ProductItemDTO item = ((Button)sender).BindingContext as ProductItemDTO;
            item.Quantity += item.Quantity == 300 ? 0 : 1;
            item.Render();
            ctx.RenderCost();
        }

        void ChangeQuantity_Clicked(System.Object sender, System.EventArgs e)
        {
            var ctx = this.BindingContext as ProductListDTO;
            ProductItemDTO item = ((Button)sender).BindingContext as ProductItemDTO;
            MessagingCenter.Send<ProductListView, int>(this, GlobalEventSender.PAYMENT_EDIT_PRODUCT_QUANTITY, item.Id);
        }
    }
}
