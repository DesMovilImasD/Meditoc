#if __ANDROID__
using Android.Content;
using Android.OS;
#endif

using CallCenter.Models;
using CallCenter.Helpers;
using CallCenter.Renderers;
using Rg.Plugins.Popup.Pages;
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
	public partial class vwPopupTerminos : PopupPage
    {
        readonly ICPFeeds cpFeeds;
        private LoginModel oLoginModel;
        private PopupLoad ovwPopupLoad;
#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }
#endif

#if __IOS__
         public vwPopupTerminos ()
		{
#else
        public vwPopupTerminos(Intent screenshareIntent)
        {
            ScreenshareIntent = screenshareIntent;
#endif


            InitializeComponent();
            Initialize();
            this.cpFeeds = DependencyService.Get<ICPFeeds>();

        }
        private void Initialize()
        {
            var tapt = new TapGestureRecognizer();

            tapt.Tapped +=
               (sender, e) =>
               {
                   OnClick_btnTerminosyCondiciones(sender, e);
               };

            var tapp = new TapGestureRecognizer();

            linkTerminos.GestureRecognizers.Add(tapt);

            tapp.Tapped +=
               (sender, e) =>
               {
                   OnClick_btnAvisoPrivacidad(sender, e);
               };

            //linkAvisoPrivacidad.GestureRecognizers.Add(tapp);

        }

        private async void Aceptar_OnClicked(object sender, EventArgs e)
        {
            ovwPopupLoad = new PopupLoad();
            await PopupNavigation.Instance.PushAsync(ovwPopupLoad);

            if (await this.cpFeeds.m_Acepta_Temino_y_condiciones(Settings.sUserNameLogin))
            {
                Settings.bSession = true;
               // if (Device.RuntimePlatform == Device.iOS)
#if __IOS__
                    Application.Current.MainPage = new MainPage();
#else
                    Application.Current.MainPage = new MainPage(ScreenshareIntent);
#endif

//                else
//#if __IOS__
//                Application.Current.MainPage = new MainPage();// { ToolbarItems = { new ToolbarItem { Icon = "meditoc_white.png", Priority = 3, Order = ToolbarItemOrder.Primary } } };
//#else
//                    Application.Current.MainPage = new MainPage();// { ToolbarItems = { new ToolbarItem { Icon = "meditoc_white.png", Priority = 3, Order = ToolbarItemOrder.Primary } } };
//#endif


            }
            else
                await DisplayAlert("Términos y condiciones.", "Hubo un error al aceptar los términos y condiciones, reintente por favor.", "Aceptar");

            await PopupNavigation.Instance.PopAllAsync();
        }

        private void Cancelar_OnClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void OnClick_btnTerminosyCondiciones(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new vwterminosycondiciones(0, true));
            //Application.Current.MainPage = new vwterminosycondiciones(0, true);
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnClick_btnAvisoPrivacidad(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new vwterminosycondiciones(1, true));
            await PopupNavigation.Instance.PopAsync();
        }
    }
}