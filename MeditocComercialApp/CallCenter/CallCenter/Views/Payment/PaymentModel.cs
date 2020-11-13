using System;
using CallCenter.ViewModels;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using CallCenter.Services;
using System.Threading.Tasks;
using CallCenter.Helpers;
using CallCenter.Views.ProductList;
using CallCenter.Views.UserInfo;
using System.Collections.Generic;
using System.Linq;
using CallCenter.Views.Dialogs.PromotionalCode;
using Rg.Plugins.Popup.Services;
using Conekta.Xamarin;
using CallCenter.Validation;
using CallCenter.Views.Dialogs.PayNotification;
using CallCenter.Models;
using CallCenter.Renderers;
using CallCenter.Views.Dialogs.EditQuantity;

#if __ANDROID__
using Android.Content;
#endif

namespace CallCenter.Views.Payment
{


    public class PaymentModel : BaseViewModel
    {

        #region -------- [properties] --------

        private readonly ObservableCollection<PaymentPageInterface> source = new ObservableCollection<PaymentPageInterface>();
        public ObservableCollection<PaymentPageInterface> DataSource { get { return source; } }

        /// <summary>
        /// servicio de verificacion de internet
        /// </summary>
        private InternetService internetService;

        /// <summary>
        /// contexto de la vista
        /// </summary>
        private PaymentView view { get; set; }

        /// <summary>
        /// obtiene la posicion actual de la posicion
        /// del carrousel
        /// </summary>
        public int CurrentPosition { get; set; }

        public bool Loading { get; set; } = false;

        /// <summary>
        /// bandera de actualizacion
        /// </summary>
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        /// <summary>
        /// bandera que muestra el indicador del tab1
        /// </summary>
        private bool _showTab1 = true;
        public bool ShowTab1
        {
            get { return _showTab1; }
            set
            {
                _showTab1 = value;
                OnPropertyChanged(nameof(ShowTab1));
            }
        }

        /// <summary>
        /// bandera que muestra el indicador del tab2
        /// </summary>
        private bool _showTab2 = false;
        public bool ShowTab2
        {
            get { return _showTab2; }
            set
            {
                _showTab2 = value;
                OnPropertyChanged(nameof(ShowTab2));
            }
        }

        private bool _SwipeEnable = true;
        public bool SwipeEnable
        {
            get { return _SwipeEnable;  }
            set
            {
                _SwipeEnable = value;
                OnPropertyChanged(nameof(_SwipeEnable));
            }
        }

        #endregion

        #region -------- [Constructor] --------

#if __ANDROID__

        private Intent intent { get; set; }

        public static PaymentModel Create(PaymentView page, Intent intent, List<ProductItemDTO> items) =>
            new PaymentModel(page, intent, items);

        public PaymentModel(PaymentView page,  Intent intent, List<ProductItemDTO> items)
        {
            this.intent = intent;
#else

        public static PaymentModel Create(PaymentView page, List<ProductItemDTO> items) =>
            new PaymentModel(page, items);

        public PaymentModel(PaymentView page, List<ProductItemDTO> items)
        {
#endif
            this.view = page;
            internetService = new InternetService(page);
            BuildDataSource(items);
        }

        #endregion

        #region -------- [commands] --------

        /// <summary>
        /// comando de verificación
        /// </summary>
        public const string VerifyCommandName = "VerifyCommandName";
        private Command _VerifyCommand;
        public Command VerifyCommand
        {
            get
            {
                return _VerifyCommand ??
                    (_VerifyCommand = new Command(async () => {
                        //IsRefreshing = true;
                        //await VerifyRedeemCode();
                        //IsRefreshing = false;
                    }));
            }
        }

        /// <summary>
        /// comando de envio de datos
        /// </summary>
        public const string SubmitCommandName = "SubmitCommandName";
        private Command _SubmitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _SubmitCommand ??
                    (_VerifyCommand = new Command(async () => {

                    }));
            }
        }

        /// <summary>
        /// comano que se envia desde el carousel
        /// </summary>
        public const string PositionChangeName = "PositionChangeName";
        private Command _PositionChangeCommand;
        public Command PositionChangeCommand
        {
            get
            {
                return _PositionChangeCommand ??
                    (_PositionChangeCommand = new Command<int>(PositionChanged));
            }
        }

        #endregion

        #region -------- [Methods] --------

        /// <summary>
        /// cambio de posicion
        /// </summary>
        /// <param name="position"></param>
        void PositionChanged(int position)
        {
            CurrentPosition = position;
            ShowTab1 = position == 0 ? true : false;
            ShowTab2 = !ShowTab1;
        }

        /// <summary>
        /// Carga las vistas.
        /// </summary>
        private void BuildDataSource(List<ProductItemDTO> items)
        {
            var pageProducts = ProductListDTO.Create(1, "DETALLE DE\nCOMPRA", items );
            source.Add(pageProducts);
            var pageUserInfo = UserInfoDTO.Create(2, "INFORMACIÓN\nDE PAGO");
            source.Add(pageUserInfo);
            PositionChanged(0);
        }

        /// <summary>
        /// actualiza el total enla vista de formulario
        /// de esta manera sabra el formulario que necesitara saber si
        /// es necesario modificar el tipo de cobro.
        /// [esto es por que no se encontro una manera de notificarle directamente]
        /// del cambio de precio a la vista del formulario.
        /// </summary>
        /// <returns></returns>
        public bool NeedUpdateMonthyPayType()
        {
            var _products = source.Where(o => o.Id == 1).FirstOrDefault();

            var _form = source.Where(o => o.Id == 2).FirstOrDefault();

            if(_products != null && _form != null)
            {
                var products = _products as ProductListDTO;
                var form = _form as UserInfoDTO;
                var status = (products.Total != form.Total);
                
                return status;
            }

            return false;
        }

        public void UpdateCost()
        {
            var _products = source.Where(o => o.Id == 1).FirstOrDefault();
            var _form = source.Where(o => o.Id == 2).FirstOrDefault();
            if (_products != null && _form != null)
            {
                var products = _products as ProductListDTO;
                var form = _form as UserInfoDTO;
                form.Total = products.Total;
            }
                
        }

        public async void EditQuantity(int id)
        {
            if(Loading) { return; }
            Loading = true;
            var _page = source.Where(o => o.Id == 1).FirstOrDefault();
            if(_page != null)
            {
                var page = _page as ProductListDTO;
                var item = page.Products.Where(o => o.Id == id).FirstOrDefault();
                if(item != null)
                {
                    var result = await EditQuantityView.Show(PopupNavigation.Instance, (int)item.Quantity, 1, 300);
                    if (result.Status)
                    {
                        item.Quantity = (decimal)result.Quantity;
                        item.Render();
                        page.RenderCost();
                    }
                }
            }
            Loading = false;
        }

        /// <summary>
        /// borrar un producto
        /// </summary>
        /// <param name="item"></param>
        public void DeleteProduct(ProductItemDTO item)
        {
            var _page = source.Where(o => o.Id == 1).FirstOrDefault();
            if(_page != null)
            {
                var page = _page as ProductListDTO;
                page.Delete(item);
                page.RenderCost();
            }  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task AddRedeemCode()
        {
            if (Loading) { return; }
            Loading = true;

            var _page = source.Where(o => o.Id == 1).FirstOrDefault();
            if(_page != null)
            {
                var page = _page as ProductListDTO;
                if(page.Products.Count() == 0)
                {
                    await view.DisplayAlert("Información", "Es necesario tener productos para añadir un cupón", "Aceptar");
                    Loading = false;
                    return;
                }
                
                await ShowLoading();
                if(!await internetService.VerificaInternet()){
                    await HideLoading();
                    Loading = false;
                    return;
                }
                await HideLoading();
                var result = await PromotionalCodeView.Show(PopupNavigation.Instance, internetService, page.Total);
                if (result.Status)
                {
                    page.Coupon = new ProductReedemDTO
                    {
                        Code = result.Code,
                        Discount = result.Type == 1 ? result.Discount : result.PercentageDiscount,
                        ExpireIn = result.ExpireIn,
                        Id = result.CodeId,
                        Name = result.Name,
                        Type= result.Type
                    };
                    page.RenderCost();
                }
                Loading = false;                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <param name="Name"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="CVC"></param>
        /// <returns></returns>
        async Task<string> GenerateCardToken(string CardNumber, string Name, int Month, int Year, string CVC)
        {
            var key = Settings.ConektaPublicKey;
            if (Device.RuntimePlatform == Device.iOS)
            {
                string token = await new ConektaTokenizer(
                    key,
                    RuntimePlatform.iOS
                    ).GetTokenAsync(CardNumber, Name, CVC, Year, Month);
                return token;
            }
            else
            {
                string token = await new ConektaTokenizer(
                    key,
                    RuntimePlatform.Android
                    ).GetTokenAsync(CardNumber, Name, CVC, Year, Month);
                return token;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Submit()
        {
            if (Loading) { return; }
            Loading = true;

            await ShowLoading();

            var _form = this.source.Where(o => o.Id == 2).FirstOrDefault();
            if (_form is null)
            {
                await HideLoading();
                Loading = false;
                return;
            }
            var _products = this.source.Where(o => o.Id == 1).FirstOrDefault();
            if (_products is null)
            {
                await HideLoading();
                Loading = false;
                return;
            }

            var form = _form as UserInfoDTO;
            var products = _products as ProductListDTO;

            if (products.Products.Count() == 0)
            {
                await HideLoading();
                await this.view.DisplayAlert(
                "Información",
                "Es necesario añadir por lo menos un producto para comprar",
                "Entendido");
                Loading = false;
                return;
            }

            if (!form.TermsAndConditions)
            {
                await HideLoading();
                await this.view.DisplayAlert(
                    "Información",
                    "Es necesario aceptar los términos y condiciones.",
                    "Entendido");
                Loading = false;
                return;
            }

            if (!await internetService.VerificaInternet())
            {
                await HideLoading();
                Loading = false;
                return;
            }

            // generar codigo de tarjeta
            var cardcode = await GenerateCardToken(
                    form.CardNumber,
                    form.UserName,
                    Convert.ToInt32(form.CardMonth),
                    Convert.ToInt32(form.CardYear),
                    form.CardPin);

            // verificar si se genero el codigo de tarjeta.
            if (string.IsNullOrEmpty(cardcode))
            {
                await HideLoading();
                await this.view.DisplayAlert(
                    "Información",
                    "No pudimos procesar el pago de tu pedido, revisa nuevamente los datos ingresados o intenta con otra tarjeta",
                    "Entendido");
                Loading = false;
                return;
            }

            // solicitud de pago de productos.
            var request = new BuyProductRequestModel
            {
                UserForm = new UserFormRequest
                {
                    Email = form.Email,
                    Name = form.UserName,
                    PhoneNumber = form.PhoneNumber
                },
                Coupon = products.Coupon is null ? null : products.Coupon.Id,
                Products = products.Products.Select(o => new ProductItemRequest
                {
                    Id = o.Id,
                    Quantity = (int)o.Quantity
                }).ToList(),
                Changes = new List<ChargesItemRequest>
                    {
                        ChargesItemRequest.Create(cardcode, (int)form.Type)
                    }
            };

            var service = DependencyService.Get<ICPFeeds>();
            var response = await service.RegisterSubscription(request);
            if (response.Status)
            {
                await HideLoading();
                //Loading = false;

                var multiple = response.Result.Items.Count() > 1;
                var item = response.Result.Items.FirstOrDefault();
                await PayNotificationView.Show(PopupNavigation.Instance, item.Folio, item.Password,  multiple);
                if (!multiple)
                {
                    Settings.sUserNameLogin = item.Folio;
                    Settings.sPassLogin = item.Password;
                }

                await GoToLogin();
                return;
            }
            await HideLoading();
            Loading = false;
            await this.view.DisplayAlert("Información", response.Message, "Entendido");
        }

        async Task ShowLoading()
        {
            if (PopupNavigation.Instance.PopupStack.Count() == 0)
            {
                await PopupNavigation
                        .Instance
                        .PushAsync(new PopupLoad(message: "Espere un momento ..."));

            }
        }


        async Task HideLoading()
        {
            if (PopupNavigation.Instance.PopupStack.Count() > 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        #endregion

        async Task GoToLogin()
        {
#if __ANDROID__
            await view.Navigation.PushAsync(new vwLoginPage(intent));
#else
            await view.Navigation.PushAsync(new vwLoginPage());
#endif
        }

    }
}

