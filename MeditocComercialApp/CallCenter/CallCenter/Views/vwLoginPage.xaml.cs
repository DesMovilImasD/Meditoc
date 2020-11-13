#if __ANDROID__
using Android.Content;
using Android.OS;
#endif

using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CallCenter.ViewModels;
using CallCenter.Helpers;
using System.Linq;
using CallCenter.Renderers;
using CallCenter.Views.HomeSwitch;

namespace CallCenter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vwLoginPage : CoolContentPage
    {
        LoginViewModel loginViewModel;
        readonly ICPFeeds cpFeeds;

#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }
#endif


#if __IOS__
        public vwLoginPage()
        {
            InitializeComponent(); BindingContext = loginViewModel = new LoginViewModel(this);
#else
        public vwLoginPage(Intent screenshareIntent)
        {
            ScreenshareIntent = screenshareIntent;
            InitializeComponent();
            BindingContext = loginViewModel = new LoginViewModel(this, ScreenshareIntent);
#endif

            InitFormulario();
            this.cpFeeds = DependencyService.Get<ICPFeeds>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loginViewModel.VerifyStoreCommand.Execute(null);
        }

        public void InitFormulario()
        {
            //Settings.COVIDFolio = "";
            stkActivity.SetBinding(IsVisibleProperty, "IsBusy");
            iaIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            txtUserName.SetBinding(Entry.TextProperty, LoginViewModel.UsernamePropertyName);
            txtUserName.Placeholder = "Usuario";

            txtPassword.SetBinding(Entry.TextProperty, LoginViewModel.PasswordPropertyName);
            txtPassword.Placeholder = "Contraseña";
            txtPassword.IsPassword = true;

            if (!string.IsNullOrEmpty(Settings.sPassLogin)) txtPassword.Text = Settings.sPassLogin;
            if (!string.IsNullOrEmpty(Settings.sUserNameLogin)) txtUserName.Text = Settings.sUserNameLogin;

            btnLogin.SetBinding(Button.CommandProperty, LoginViewModel.LoginCommandPropertyName);

            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    GoToHomeSwicth();
                };
            }

            versionLabel.Text = string.Format(" V{0}", DependencyService.Get<IAppInfo>().GetVersion());
        }

        private async Task OnClick_btnfrmRecuperarContrasena()
        {
            ActivityIndicator oActivityIndicator = new ActivityIndicator();
            StackLayout oStackLayout = new StackLayout();
            oActivityIndicator.IsRunning = true;
            oStackLayout.IsVisible = true;

            oActivityIndicator.IsRunning = false;
            oStackLayout.IsVisible = false;

            if (Settings.iPaso == 1)
            {
                await Navigation.PushModalAsync(new vwRecuperaContrasena(1));
            }
            else
            {
                await Navigation.PushModalAsync(new vwRecuperaContrasena(2));
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (!Settings.bLogueado)
                GoToHomeSwicth();
            return false;
        }

        void GoToHomeSwicth()
        {
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
        }


    }
}