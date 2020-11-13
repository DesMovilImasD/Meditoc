#if __ANDROID__
using Android.Content;
using Android.OS;
#endif


using CallCenter.Helpers;
using CallCenter.Models;
using CallCenter.Renderers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class vwterminosycondiciones : ContentPage
	{
        //sUrl[0] Términos y [1] Aviso
        private string[] sUrl = {"https://docs.google.com/viewer?url=http://148.240.238.149/WS/Files/Términos_y_Condiciones.pdf",
        "https://docs.google.com/viewer?url=http://148.240.238.149/WS/Files/Aviso_de_Privacidad.pdf"};

        private int iVista = 0;
        private bool bInicio = false, bBack = false;
        private string sTextBoton = "Continuar";
        private LoginModel oLoginModel;
        private ICPFeeds cpFeeds;
        private PopupLoad ovwPopupLoad;

        private MasterDetailPage oPage;
#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }
#endif
        public vwterminosycondiciones ()
		{
			InitializeComponent ();
        }

#if __IOS__
          public vwterminosycondiciones(MainPage vwMainPage, int iVista)
          {
#else
        public vwterminosycondiciones(MainPage vwMainPage, int iVista, Intent screenshareIntent)
        {
            ScreenshareIntent = screenshareIntent;
#endif



            bBack = true;
            oPage = vwMainPage;
            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = sUrl[iVista],
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            this.Content = new StackLayout
            {
                Children = { webView }
            };
        }

        public vwterminosycondiciones(int iVista, bool bInicio)
        {
            this.iVista = iVista;
            this.bInicio = bInicio;
            InitForm();
        }

        private void InitForm()
        {

            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = sUrl[iVista],
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Button btnNext = new Button
            {
                Text = sTextBoton,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#00adc1"),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            btnNext.Clicked += OnClickSiguiente;

            Button btnCerrar = new Button
            {
                Text = "Cerrar",
                TextColor = Color.White,
                BackgroundColor = Color.Gray,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            btnCerrar.Clicked += OnClickCerrar;

            StackLayout stkBotones = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { btnCerrar, btnNext }
            };

            if (this.bInicio)
                this.Content = new StackLayout
                {
                    Children = { webView, btnNext }
                };
            else
                this.Content = new StackLayout
                {
                    Children = { webView, stkBotones }
                };

        }

        private async void OnClickSiguiente(object sender, EventArgs args)
        {
            try
            {
                ovwPopupLoad = new PopupLoad();
                await PopupNavigation.Instance.PushAsync(ovwPopupLoad);

                if (this.bInicio)
                {
                    if (this.iVista == 1)
                        this.iVista = 0;
                    else
                        this.iVista = 1;

                    this.sTextBoton = "Aceptar";
                    this.bInicio = false;
                    InitForm();
                }
                else
                {
                    if (this.sTextBoton == "Aceptar")
                    {
                        oLoginModel = new LoginModel();
                        oLoginModel.sUIDCliente = Settings.sUsuarioUID;
                        oLoginModel.bAceptoTerminoCondicion = true;
                        this.cpFeeds = DependencyService.Get<ICPFeeds>();
                        if (await this.cpFeeds.m_Acepta_Temino_y_condiciones(Settings.sUserNameLogin))
                        {
                            Settings.bSession = true;

#if __IOS__
                            Application.Current.MainPage = new MainPage();
#else
                            Application.Current.MainPage = new MainPage(ScreenshareIntent);
#endif
                            //if (Device.RuntimePlatform == Device.iOS)
                            //    Application.Current.MainPage = new MainPage();
                            //else
                            //    Application.Current.MainPage = new MainPage();// { ToolbarItems = { new ToolbarItem { Icon = "meditoc_white.png", Priority = 3, Order = ToolbarItemOrder.Primary } } };

                        }
                        else
                            await DisplayAlert("Términos y condiciones.", "Hubo un error al aceptar los términos y condiciones, reintente por favor.", "Aceptar");

                    }
                    else
                        InitForm();
                }

                await PopupNavigation.Instance.PopAsync();
            }
            catch { }
        }

        private void OnClickCerrar(object sender, EventArgs args)
        {
#if __IOS__
             Application.Current.MainPage = new vwLoginPage();
#else
            Application.Current.MainPage = new vwLoginPage(ScreenshareIntent);
#endif

        }

        protected override bool OnBackButtonPressed()
        {
            if (bBack)
            {
                //oPage.Detail = new NavigationPage(new vwHomePage() { /*Title = Settings.sFolio*/ }) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#12b6cb") };
                return true;//base.OnBackButtonPressed();
            }
            else
                return base.OnBackButtonPressed();
        }
    }

}