using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallCenter.Renderers;
using CallCenter.Views.HomeSwitch;
using CallCenter.Helpers;


#if __ANDROID__
using Android.Content;
#endif

namespace CallCenter.Views.FakeSplash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FakeSplash : ContentPage
    {
#if __ANDROID__
        private Intent SharedIntent { get; set; }
        public FakeSplash(Intent sharedIntent)
        {
            SharedIntent = sharedIntent;
#else
        public FakeSplash()
        {
#endif
            InitializeComponent();

            IosLogoImage.IsVisible = (Device.iOS == Device.RuntimePlatform);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // habilitar el timer
            Device.StartTimer(TimeSpan.FromSeconds(3.0), TimerElapsed);
            DependencyService.Get<IAppInfo>().SetDarkTheme();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //ocultar el timer
            DependencyService.Get<IAppInfo>().SetLightTheme();
        }

        

        private bool TimerElapsed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //put here your code which updates the view
#if __ANDROID__
                App.Current.MainPage = new MainNavigationPage(new HomeSwitchView(SharedIntent))
                {
                    BarBackgroundColor = System.Drawing.Color.Transparent,
                };
#else
                App.Current.MainPage = new MainNavigationPage(new HomeSwitchView())
                {
                    BarBackgroundColor = System.Drawing.Color.Transparent,
                };
#endif

            });
            //return true to keep timer reccurring
            //return false to stop timer
            return false;
        }

 
        

    }
}
