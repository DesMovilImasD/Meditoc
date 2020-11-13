using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallCenter.Helpers;
using CallCenter.Views.ProductList;
using CallCenter.Views.Dialogs.PayNotification;
using Rg.Plugins.Popup.Services;
using CallCenter.Views.Dialogs.PromotionalCode;
using CallCenter.Views.UserInfo;
using System.Linq;

#if __ANDROID__
using Android.Content;
#endif

namespace CallCenter.Views.Payment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentView : ContentPage
    {
        private PaymentModel modelContext { get; set; }

        #region -------- [Constructor ios/android] --------

#if __ANDROID__
        public Intent intent { get; set; }

        public PaymentView(Intent _intent, List<ProductItemDTO> items)
        {
            intent = _intent;
            modelContext = new PaymentModel(this, _intent, items);
#else
        public PaymentView(List<ProductItemDTO> items)
        {
            modelContext = new PaymentModel(this, items);
#endif
            InitializeComponent();
            BindingContext = modelContext;
        }

        #endregion

        #region ------- [View Cycle] --------

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            Subscribe();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            UnSubscribe();
        }

        
        #endregion

        #region -------- [Events] --------


        private void Subscribe()
        {
            MessagingCenter.Subscribe<ProductListView,ProductItemDTO>(
                this, GlobalEventSender.PAYMENT_DELETE_PRODUCT, async (sender, arg) =>
                {
                    bool choose = await DisplayAlert(
                        "Eliminar producto",
                        "¿Continuar?",
                        "Continuar",
                        "Cancelar");
                    if (choose)
                    {
                        modelContext.DeleteProduct(arg);
                    }
                });

            MessagingCenter.Subscribe<ProductListView,object>(
                this, GlobalEventSender.PAYMENT_ADD_REDEEM_CODE, async (sender, arg) =>
                {
                    await modelContext.AddRedeemCode();
                });

            MessagingCenter.Subscribe<ProductListView, object>(
                this, GlobalEventSender.PAYMENT_CONTINUE_TO_PAY,  (sender, arg) =>
                {
                    Carousel.ScrollTo(1);
                });

            MessagingCenter.Subscribe<UserInfoView,object>(
                this, GlobalEventSender.PAYMENT_COMFIRM_PAY, async (sender, arg) =>
                {
                    if ((bool)arg)
                    {
                        await modelContext.Submit();
                        return;
                    }

                    await DisplayAlert(
                        "Alerta",
                        "Verifique los datos proporcionados",
                        "Entendido");
                });

            MessagingCenter.Subscribe<ProductListView, int>(this, GlobalEventSender.PAYMENT_EDIT_PRODUCT_QUANTITY, (sender, arg) =>
            {
                modelContext.EditQuantity((int)arg);
            });
        }

        private void UnSubscribe()
        {
            MessagingCenter.Unsubscribe<ProductListView, int>(this, GlobalEventSender.PAYMENT_EDIT_PRODUCT_QUANTITY);
            MessagingCenter.Unsubscribe<ProductListView, ProductItemDTO>(this, GlobalEventSender.PAYMENT_DELETE_PRODUCT);
            MessagingCenter.Unsubscribe<ProductListView, object>(this, GlobalEventSender.PAYMENT_ADD_REDEEM_CODE);
            MessagingCenter.Unsubscribe<ProductListView, object>(this, GlobalEventSender.PAYMENT_CONTINUE_TO_PAY);
            MessagingCenter.Unsubscribe<UserInfoView, object>(this, GlobalEventSender.PAYMENT_COMFIRM_PAY);
        }

        void Carousel_CurrentItemChanged(System.Object sender, Xamarin.Forms.CurrentItemChangedEventArgs e)
        {
            if(e.CurrentItem.GetType() == typeof(UserInfoDTO))
            {
                var view = (CarouselView)sender;
                var subViews = view.VisibleViews
                    .ToList()
                    .Where(o => o != null && o.GetType() == typeof(UserInfoView) && o.BindingContext == e.CurrentItem)
                    .ToList();

                foreach(var sb  in subViews)
                {
                    if(sb.BindingContext.GetType() == typeof(UserInfoDTO))
                    {
                        if (modelContext.NeedUpdateMonthyPayType())
                        {
                            modelContext.UpdateCost();
                            var item = (sb as UserInfoView);
                            item.AdjustMonthyPayment();
                        }
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
           base.OnBackButtonPressed();
           return modelContext.Loading;
        }

        #endregion
    }
}
