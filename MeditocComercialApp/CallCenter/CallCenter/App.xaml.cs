#if __ANDROID__
using Android.Content;
#endif

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallCenter.Views;
using FM.IceLink;
using CallCenter.Views.HomeSwitch;
using CallCenter.Renderers;
using CallCenter.Views.FakeSplash;

[assembly: ExportFont("CircularStd-Medium.ttf", Alias = "CircularStd-Medium")]

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CallCenter
{
    public partial class App : Application
    {

#if __IOS__
        public App()
        {
#else
        public App(Intent screenshareIntent)
        {
#endif
            InitializeComponent();
#if __IOS__
            
            MainPage = new MainNavigationPage(new FakeSplash())
            {
                BarBackgroundColor = System.Drawing.Color.Transparent,    
            };
#else
            MainPage = new MainNavigationPage(new FakeSplash(screenshareIntent))
            {
                BarBackgroundColor = System.Drawing.Color.Transparent
            };
#endif


            // MainPage = new MainPage();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            if (Xamarin.Forms.Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsGestureEnabled = false;
            }

            else if (Xamarin.Forms.Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsGestureEnabled = false;
            }
        }
        // This will handled any unhandled Android Exceptions
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Debug("Application | Xamarin Unhandled Exception", ex);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


    }
}
