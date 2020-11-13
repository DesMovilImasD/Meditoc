using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CallCenter.Helpers;
using CallCenter.Renderers;
using CallCenter.Services;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Views.Dialogs.PromotionalCode
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PromotionalCodeView : PopupPage, INotifyPropertyChanged
    {
        public TaskCompletionSource<PromotionalCodeResult> _resultCompletion = null;
        private InternetService internetService { get; set; }

        public double Total { get; set; }
        public static async Task<PromotionalCodeResult> Show(IPopupNavigation navigator, InternetService internetService, double Total)
        {
            
            var view = new PromotionalCodeView(internetService, Total);
            await navigator.PushAsync(view);
            var result = await view.GetResult();
            await navigator.PopAsync();
            return result;
        }

        

        public Task<PromotionalCodeResult> GetResult()
        {
            _resultCompletion = new TaskCompletionSource<PromotionalCodeResult>();
            return _resultCompletion.Task;
        }

        public PromotionalCodeView(InternetService internetService, double Total)
        {
            InitializeComponent();
            BindingContext = this;
            this.internetService = internetService;
            this.Total = Total;
        }

        public void Cancel_Tapped(System.Object sender, System.EventArgs e)
        {
            if (_resultCompletion != null)
            {
                _resultCompletion.SetResult(PromotionalCodeResult.Fail());
                _resultCompletion = null;
            }
        }

        public async void Submit_Tapped(System.Object sender, System.EventArgs e)
        {
            errors.IsVisible = false;
            errors.Text = "";
            buttonsLayout.IsVisible = false;
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;

            if (string.IsNullOrEmpty(Coupon) &&string.IsNullOrWhiteSpace(Coupon))
            {
                errors.IsVisible = true;
                buttonsLayout.IsVisible = true;
                errors.Text = "Es necesario que ingrese un código de descuento";
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsRunning = false;
                return;
            }

            if(!await internetService.VerificaInternet())
            {
                errors.IsVisible = true;
                errors.Text = "Sin conexión a internet";
                buttonsLayout.IsVisible = true;
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsRunning = false;
                return;
            }

            ICPFeeds Service = DependencyService.Get<ICPFeeds>();
            var model = await Service.VerifyCoupon(Coupon);
            if(model is null)
            {
                errors.IsVisible = true;
                buttonsLayout.IsVisible = true;
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsRunning = false;
                errors.Text = "Ocurrio un error al obtener la respuesta del servidor, intente de nuevo.";
                return;
            }

            if (model.Code != "0")
            {
                errors.IsVisible = true;
                buttonsLayout.IsVisible = true;
                loadingIndicator.IsVisible = false;
                loadingIndicator.IsRunning = false;
                errors.Text = model.Message;
                return;
            }

            // verificamos descuento de monto.
            //if(model.Result.CategoryId == 1 && model.Result.QuantityDiscount > Total)
            //{
            //    errors.IsVisible = true;
            //    buttonsLayout.IsVisible = true;
            //    loadingIndicator.IsVisible = false;
            //    loadingIndicator.IsRunning = false;
            //    errors.Text = $"Este cupón no se puede utilizar tiene un descuento de ${model.Result.QuantityDiscount} que es mayor a ${Total}";
            //    return;
            //}

            if (_resultCompletion != null)
            {
                _resultCompletion.SetResult(
                    PromotionalCodeResult.Success(
                        model.Result.Id,
                        model.Result.Code,
                        model.Result.QuantityDiscount ?? 0,
                        model.Result.Remarks,
                        model.Result.DueDate,
                        model.Result.CategoryId,
                        model.Result.PercentageDiscount ?? 0
                        ));
                _resultCompletion = null;
            }

        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        private string _coupon;
        public string Coupon
        {
            get { return _coupon; }
            set
            {
                _coupon = value;
                OnPropertyChanged(nameof(Coupon));
            }
        }
    }

    public struct PromotionalCodeResult
    {
        public bool Status { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public double PercentageDiscount { get; set; }
        public int Type { get; set; }
        public string CodeId { get; set; }
        public string Name { get; set; }
        public DateTime ExpireIn { get; set; }

        public static PromotionalCodeResult Success(
            string code_id,
            string code,
            double discount,
            string name,
            string expire_in,
            int type,
            double percentageDiscount
            ) => new PromotionalCodeResult
        {
            Status = true,
            Code = code,
            Discount = discount,
            CodeId = code_id,
            Name = name,
            PercentageDiscount = percentageDiscount,
            Type = type,
            ExpireIn = DateTime.TryParse(expire_in, out DateTime result) ? result : DateTime.Now
        };

        public static PromotionalCodeResult Fail() => new PromotionalCodeResult
        {
            Status = false,
            Code = "----",
            Discount = 0.0f
        };
    }
}
