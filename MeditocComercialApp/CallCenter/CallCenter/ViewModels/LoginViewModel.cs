#if __ANDROID__
using Android.Content;
using Android.OS;
#endif

using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Globalization;
using CallCenter.Helpers;
using CallCenter.Views;
using Rg.Plugins.Popup.Services;
using CallCenter.Services;


namespace CallCenter.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        readonly ICPFeeds cpFeeds;
        CultureInfo culture = new CultureInfo("es-MX");
        private vwPopupTerminos ovwPopupTerminos;
        private InternetService oInternetService;

        //private double Latitude { get; set; } = 0;
        //private double Longitude { get; set; } = 0;

        private bool _showLogo = false;
        public bool ShowLogo
        {
            get { return _showLogo; }
            set
            {
                _showLogo = value;
                OnPropertyChanged(nameof(ShowLogo));
            }
        }

        // indica si esta permitido el login
        // esta estrictamente relacionado con la verificacion de la version
        // de la tienda, en caso que no exita una actualizacion
        // se permitira el login, en caso contrario no.
        private bool allowLogin { get; set; } = false;

#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }
#endif

#if __IOS__
         public LoginViewModel(Page page) : base(page)
        {
            ovwPopupTerminos = new vwPopupTerminos();
#else
        public LoginViewModel(Page page, Intent screenshareIntent) : base(page)
        {
            ScreenshareIntent = screenshareIntent;
            ovwPopupTerminos = new vwPopupTerminos(ScreenshareIntent);
#endif

            if(Device.RuntimePlatform == Device.iOS){
                ShowLogo = true;
            }

            this.cpFeeds = DependencyService.Get<ICPFeeds>();
            Settings.bChatInicializado = false;
            oInternetService = new InternetService(page);

            // ejecutamos la verificacion de la tienda.
            // cuando se crea el modelo.
            //Task.Run(async () => {
            //    IsBusy = true;
            //    if(await oInternetService.VerificaInternet())
            //    {
            //        await VerifyStoreVersion();
            //    }
            //    IsBusy = false;
            //});
        }

        #region [Declaración variables]
        string username = string.Empty;
        public const string UsernamePropertyName = "UserName";
        public string UserName
        {
            get { return username; }
            set { SetProperty(ref username, value, UsernamePropertyName); }
        }

        string password = string.Empty;
        public const string PasswordPropertyName = "Password";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value, PasswordPropertyName); }
        }
        #endregion

        public const string LoginCommandPropertyName = "LoginCommand";

        Command loginCommand;
        Command verifyStoreCommand;

        public Command LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }

        public Command VerifyStoreCommand
        {
            get
            {
                return verifyStoreCommand ??
                    (verifyStoreCommand = new Command(async () => await VerifyStoreVersion()));
            }
        }

        private async Task ExecuteLoginCommand()
        {
            try
            {
                if (!await oInternetService.VerificaInternet())
                    return;

                if (IsBusy) return;
                IsBusy = true;
                loginCommand.ChangeCanExecute();
                if (UserName != "" && Password != "")
                {
                    if (!allowLogin)
                    {
                        // verificar de nuevo la version de la tienda
                        // si se establecio la conexion de manera correcta permitir el login
                        // de otra manera seguir mostrando error.
                        await VerifyStoreVersion();
                        if (!this.allowLogin) { return; }
                    }

                    if (await Login("ENTRADA"))
                    {
                        if (Settings.bTerminoYcondiciones)
                        {
                            Settings.bSession = true;
#if __IOS__
                            Application.Current.MainPage = new MainPage();
#else
                            Application.Current.MainPage = new MainPage(ScreenshareIntent);
#endif
                        }
                        else
                        {
                            IsBusy = false;
                            await PopupNavigation.Instance.PushAsync(ovwPopupTerminos);
                        }
                    }
                    else
                    {
                        Settings.bSession = false;
                        if (Settings.sError == "")
                        {
                            await page.DisplayAlert("Acceso no permitido", "Verifique sus datos", "Aceptar");
                        }
                        else
                        {
                            await page.DisplayAlert("Acceso no permitido", Settings.sError, "Aceptar");
                        }
                    }
                }
                else
                {
                    IsBusy = false;
                    await page.DisplayAlert("Alerta", "Favor de escribir su usuario y contraseña", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Acceso no permitido", ex.Message, "Aceptar");
            }
            finally
            {
                IsBusy = false;
                loginCommand.ChangeCanExecute();
            }

        }


        public async Task<bool> Login(string sTipoLogin)
        {
            bool bLogin = false;
            switch (sTipoLogin)
            {
                case "ENTRADA":
                    bLogin = await cpFeeds.m_Login(
                        psUsername: this.UserName,
                        psPassword: this.Password,
                        psTipoCheck: sTipoLogin);
                    break;
                default:
                    bLogin = false;
                    await page.DisplayAlert("Información", "Inicio incorrecto.", "ok");
                    break;
            }

            return bLogin;
        }

        /**
         * Verifica si existe una nueva version en la tienda disponible para
         * realizar la actualizacion de la aplicacion.
         * y cambia la bandera allowLogin para que se permita el login
         * en caso que sea necesario.
         */
        private async Task VerifyStoreVersion()
        {
            IAppInfo appInfo = DependencyService.Get<IAppInfo>();

            VersionResult result = await appInfo.NeedUpdateApp();

            if (result.isSuccess)
            {

                // si no es necesario actualizar permitimos el
                // login de manera tradicional
                // en caso contrario mostramos mensaje de error.
                if (!result.needUpdate)
                {
                    allowLogin = true;
                    return;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await page.DisplayAlert("Nueva versión disponible",
                        "Es necesario actualizar su aplicación para disfrutar de las nuevas características.",
                        "Actualizar");

                    await Xamarin.Essentials.Launcher.TryOpenAsync(new Uri(appInfo.GotoStore()));
                    appInfo.CloseApp();

                });
            }
            else
            {
                await page.DisplayAlert("Error", result.error, "Cerrar");
            }
        }
    }
}

