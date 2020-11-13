#if __ANDROID__
using Android.Content;
using Android.OS;
#endif

using CallCenter.Helpers;
using CallCenter.Models;
using CallCenter.Renderers;
using CallCenter.Views.HomeSwitch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }
#endif

#if __IOS__
        public MainPage()
        {
#else
        public MainPage(Intent screenshareIntent)
        {
            ScreenshareIntent = screenshareIntent;
#endif
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            // MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
#if __IOS__
            Detail = new MasterDetailNavigation(new vwHomePage(this) { /*Title = Settings.sFolio*/ }) {BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#0f4d6d") };
#else
            Detail = new MasterDetailNavigation(new vwHomePage(this, ScreenshareIntent) { /*Title = Settings.sFolio*/ }) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#0f4d6d") };
#endif

        }

        public async Task NavigateFromMenu(int id)
        {
            string[] sUrl = {
                Settings.LinkTermsAndConditions,
                Settings.LinkPrivacity,
                Settings.LinkCovID
            };


            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case -1:
                        clearSettings();
#if __IOS__
                        App.Current.MainPage = new MainNavigationPage(new HomeSwitchView())
                        {
                            BarBackgroundColor = System.Drawing.Color.Transparent,    
                        };
#else
                        App.Current.MainPage = new MainNavigationPage(new HomeSwitchView(ScreenshareIntent))
                        {
                            BarBackgroundColor = System.Drawing.Color.Transparent
                        };
#endif

                        break;
                    case 0: 
                        Detail = new NavigationPage(new vwDatosPersona(this.Navigation, this) {/* Title = Settings.sFolio*/ }) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#12b6cb") }; ;
                        
                        //try
                        //{
                        //    Device.OpenUri(new Uri(sUrl[2]));
                        //}
                        //catch (Exception e)
                        //{
                        //    throw e;
                        //}
                        break;
                    case 1:
                        // string UriPDF = "https://yourURL.com/etc";
                        try
                        {
                            Device.OpenUri(new Uri(sUrl[0]));
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        //Detail = new NavigationPage(new vwterminosycondiciones(this, 0) { /*Title = Settings.sFolio*/ }) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#12b6cb") }; ;
                        break;
                    case 2:
                        //string UriPDF = "https://yourURL.com/etc";
                        try
                        {
                            Device.OpenUri(new Uri(sUrl[1]));
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        //Detail = new NavigationPage(new vwterminosycondiciones(this, 1) { /*Title = Settings.sFolio*/ }) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#12b6cb") }; ;
                        break;
                    case 3:
#if __IOS__
                        Detail = new NavigationPage(new vwHomePage(this) {/* Title = Settings.sFolio */}) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#0f4d6d") }; ;
#else
                        Detail = new NavigationPage(new vwHomePage(this, ScreenshareIntent) {/* Title = Settings.sFolio */}) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#0f4d6d") }; ;
#endif


                        break;
                }
            }
            IsPresented = false;

            //MasterPage.ListView.SelectedItem = null;
            //var newPage = MenuPages[id];

            //if (newPage != null && Detail != newPage)
            //{
            //    Detail = newPage;

            //    if (Device.RuntimePlatform == Device.Android)
            //        await Task.Delay(100);

            //    IsPresented = false;
            //}
        }

        public void clearSettings()
        {

            Settings.sUserNameLogin = "";
            Settings.sPassLogin = "";
            Settings.sUserName = "";
            Settings.bDoctor = false;
            Settings.sUsuarioUID = "";
            Settings.sSexo = "";
            Settings.sTelefonoDRs = "";
            Settings.sInstitucion = "";
            Settings.iIdUsuario = 0;
            Settings.sFolio = "";
            Settings.bTerminoYcondiciones = false;

        }
        public void MenuButton_Clicked(object sender, EventArgs e)
        {
            IsPresented = true;
            // DisplayAlert("Clic", "clkc", "");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.iOS)
            {
                IsGestureEnabled = false;
            }
        }
    }
}