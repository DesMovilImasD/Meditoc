#if __ANDROID__
using Android.Content;
using Android.OS;
#endif

using System;
using System.Collections.Generic;
using CallCenter.Helpers;
using CallCenter.Renderers;
using CallCenter.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using CallCenter.Services;
using CallCenter.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Views
{
    public partial class vwFolio : ContentPage
    {
        public FolioViewModel Model;

        readonly ICPFeeds webService;
        private MainPage mainPage;
        public float mainMarginTop { get; set; } = 108;
        public Thickness statusBarHeigth { get; set; } = new Thickness(0, 0, 0, 0);
        private InternetService oInternetService;
        private string PhoneNumber { get; set; } = null;
        private double latitude { get; set; } = 0;
        private double longitude { get; set; } = 0;
        private string errorLocation { get; set; } = null;

#if __ANDROID__
        private Intent ScreenShareIntent;
        public vwFolio(MainPage page, Intent intent)
        {
            ScreenShareIntent = intent;

            statusBarHeigth = new Thickness(0, 40, 0, 0);
            mainMarginTop = 168;
#else
        public vwFolio(MainPage page)
        {
#endif
            InitializeComponent();
            mainPage = page;
            webService = DependencyService.Get<ICPFeeds>();

            Configure();
        }

        //
        public void Configure()
        {
            oInternetService = new InternetService(mainPage);

            // obtenemos el numero de telefono.
            PhoneNumber = "";
            if (!string.IsNullOrEmpty(Settings.sUserNameLogin))
            {
                List<string> split_login = Settings.sUserNameLogin
                    .Split("_")
                    .ToList();

                if (split_login.Count() > 1)
                {
                    PhoneNumber = split_login[1];
                }

            }
        }

        async void COVID_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                await Launcher.TryOpenAsync(Settings.LinkCovID);
            }
            catch(Exception _e)
            {

            }
            
        }


        private async Task GetLocation()
        {
            latitude = 0;
            longitude = 0;
            errorLocation = null;

            bool permisionGranted = await PermissionValidator.LazyCheckLocationPermissions();
            if (permisionGranted)
            {
                LocationResult result = await LocationManager
                .Shared()
                .FindLocation(cached: false);

                if (result.IsSuccess && result.Position != null)
                {
                    latitude = result.Position.Latitude;
                    longitude = result.Position.Longitude;
                    return;
                }
                errorLocation = result.Msg;
                return;
            }
            errorLocation = "Permisos de ubicación no otorgados";
        }


        async void Submit_Tapped(System.Object sender, System.EventArgs e)
        {
            if (!await oInternetService.VerificaInternet())
                return;


            await GetLocation();

            string folio = FolioField.Text;
            if (string.IsNullOrEmpty(folio))
            {
                await DisplayAlert("Información", "El campo de folio no puede estar vacío", "Aceptar");
                return;
            }

            await PopupNavigation
                .Instance
                .PushAsync(new PopupLoad(message: "Espere un momento ..."));

            var request = COVIDRequest.Create(
                folio: folio,
                phone: PhoneNumber,
                cp: null,
                latitude: latitude,
                longitude: longitude,
                error: errorLocation);

            var FolioResponse = await DependencyService
                .Get<ICPFeeds>()
                .m_IsValidCOVID(request);

 
            await PopupNavigation.Instance.PopAsync();
            

            if (FolioResponse.Status)
            {
                //Settings.COVIDFolio = FolioResponse.Folio;
#if __IOS__
                //await Navigation.PushAsync(new vwHomePage());
#else
                //await Navigation.PushAsync(new vwHomePage(ScreenShareIntent));
#endif
            }
            else
            {
                await DisplayAlert("Información", FolioResponse.Msg, "Aceptar");
            }
            
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (!await oInternetService.VerificaInternet())
                return;
            await Navigation.PushAsync(new vwCOVIDSurvey());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

//            if (!string.IsNullOrEmpty(Settings.COVIDFolio))
//            {
//#if __IOS__
//                //await Navigation.PushAsync(new vwHomePage());
//#else
//                //await Navigation.PushAsync(new vwHomePage(ScreenShareIntent));
//#endif
//            }
        }
    }
}
