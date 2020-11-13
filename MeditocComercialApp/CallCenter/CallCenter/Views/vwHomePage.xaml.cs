#if __ANDROID__
using Android.Content;
using Android.OS;
#endif


using CallCenter.Helpers;
using CallCenter.Models;
using CallCenter.Multimedia;
using CallCenter.Renderers;
using CallCenter.Services;
using CallCenter.ViewModels;
using CallCenter.Views.HomeSwitch;
using CallCenter.Views.MedicDirectory;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vwHomePage : ContentPage
    {
        static private bool loaded;

        public Thickness statusBarHeigth { get; set; } = new Thickness(0, 0, 0, 0);
        public float mainMarginTop { get; set; } = 108;

        readonly ICPFeeds cpFeeds;
        //private CometChatService oCometChatService;
        private HomeViewModel _HomeViewModel;
        private PopupLoad _loginPopup;
        MainPage oMainPage;
        private PopupInstructions _popupInstructions;
        //private ICallService oCallService;
        private InternetService oInternetService;

        private bool isVideoCall { get; set; } = false;

#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }

#endif

#if __IOS__
        public vwHomePage(MainPage pmainPage)
        {
#else
        public vwHomePage(MainPage pmainPage, Intent screenshareIntent)
        {
            ScreenshareIntent = screenshareIntent;
#endif

            oMainPage = pmainPage;
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = _HomeViewModel = new HomeViewModel(this);

            InitFormulario();
            this.cpFeeds = DependencyService.Get<ICPFeeds>();
            //this.oCometChatService = new CometChatService(this);
            this.oInternetService = new InternetService(this);
            Settings.bClicButton = false;
            _loginPopup = new PopupLoad();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                // handle the tap

                oMainPage.IsPresented = true;
                //Settings.COVIDFolio = "";
                await Navigation.PopAsync();
            };
            tabMenu.GestureRecognizers.Add(tapGestureRecognizer);

#if __ANDROID__
            statusBarHeigth = new Thickness(0, 40, 0, 0);
            mainMarginTop = 168;
            Console.WriteLine("__ANDROID__ is defined");
#endif

        }
        private void InitFormulario()
        {
            Settings.bEnProceso = false;
            stkActivity.SetBinding(IsVisibleProperty, "IsBusy");
            iaIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");

            string sSexo = Settings.sSexo;
            string sUsername = Settings.sUserName;

            if (sSexo == "M")
            {
                sImage1.IsVisible = true;
                sImage2.IsVisible = false;
            }

            if (sSexo == "F")
            {
                sImage1.IsVisible = false;
                sImage2.IsVisible = true;

            }

            Settings.bPoPupActivo = false;
            Settings.bClosePopPup = true;
        }

        protected override void OnAppearing()
        {
            Multimedia.Context.GenerateCertificate();

            base.OnAppearing();

            if (!loaded)
            {
                try
                {
#if __IOS__
                    string prefix = "CallCenter.iOS";
#else
                    string prefix = "CallCenter.Droid";
#endif

                    FM.IceLink.License.SetKey(Settings.IceLinkKey);

                }
                catch (Exception)
                {
                    DisplayAlert("Invalid License Key", "Please check your license key", "Ok");
                }

                loaded = true;
            }

            if (Settings.bCancelaDoctor)
            {
                Settings.bCancelaDoctor = false;
                // si es orientacion unica redireccionar a la vista principal
                if (Settings.ProductType == 2)
                {
                    GoToHomeSwicth();
                }
            }
        }

        public async void LaunchChatTapped(object sender, EventArgs args)
        {
            await ExecuteCommand("CHAT");
        }

        public async void LauchCallTapped(object sender, EventArgs args)
        {

            await ExecuteCommand("CALL");
        }

        public async void LaunchVideoTapped(object sender, EventArgs args)
        {

            //string NAVIGATION = Settings.IsFolio ? "FOLIO" : "VIDEOCALL";
            await ExecuteCommand("VIDEOCALL");
        }

        public async Task ExecuteCommand(string psComand)
        {
            if (!await oInternetService.VerificaInternet()) return;

            switch (psComand)
            {

                case "CHAT":
                    newResponseModel<medicSpecialityDTO> oResponseModel = new newResponseModel<medicSpecialityDTO>();

                    oResponseModel = await cpFeeds.m_SolicitaMedico(Settings.bEsAgendada, Settings.iIdUsuario, Settings.dtFechaVencimiento);
                    if (oResponseModel.Code != 0 && oResponseModel.Result.iNumSala != "0")
                    {
                        if (await VerificaPermisosAudio())
                        {

                            //Multimedia.Context.Instance.SessionId = oResponseModel.sParameter1;
                            Multimedia.Context.Instance.SessionId = oResponseModel.Result.iNumSala;
                            Multimedia.Context.Instance.Name = Settings.sUserName;
                            Multimedia.Context.Instance.IsMedicConnected = false;

                            Multimedia.Context.Instance.EnableScreenShare = false;
                            Multimedia.Context.Instance.EnableAudioReceive = false;
                            Multimedia.Context.Instance.EnableAudioSend = false;
                            Multimedia.Context.Instance.EnableVideoReceive = false;
                            Multimedia.Context.Instance.EnableVideoSend = false;

                            await Navigation.PushModalAsync(new Chat(false));
                        }
                    }
                    else
                    {
                        await DisplayAlert("Info", oResponseModel.Message, "Aceptar");
                        if (PopupNavigation.PopupStack.Count > 0)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }

                    isVideoCall = false;

                    break;
                case "VIDEOCALL":

                    if (isVideoCall) { return; }
                    isVideoCall = true;

                    if (!await VerificaPermisosAudio()) return;

                    await PopupNavigation.Instance.PushAsync(_loginPopup);

                    newResponseModel<medicSpecialityDTO> oResponseModel_Video = new newResponseModel<medicSpecialityDTO>();

                    oResponseModel_Video = await cpFeeds.m_SolicitaMedico(Settings.bEsAgendada, Settings.iIdUsuario, Settings.dtFechaVencimiento);

                    if (oResponseModel_Video.Code == 0 && oResponseModel_Video.Result.iNumSala != "0")
                    {
                        string folio = Settings.sFolio;// string.IsNullOrEmpty(Settings.COVIDFolio) ? Settings.sFolio : Settings.COVIDFolio;

                        //Multimedia.Context.Instance.SessionId = oResponseModel_Video.sParameter1;
                        Multimedia.Context.Instance.SessionId = oResponseModel_Video.Result.iNumSala;
                        Multimedia.Context.Instance.Name = Settings.sUserName + "(" + folio + ")";
                        Multimedia.Context.Instance.IsMedicConnected = false;
                        Multimedia.Context.Instance.Videollamada_init = false;

                        Multimedia.Context.Instance.EnableScreenShare = false;

                        Multimedia.Context.Instance.EnableAudioReceive = true;
                        Multimedia.Context.Instance.EnableAudioSend = true;
                        Multimedia.Context.Instance.EnableVideoReceive = true;
                        Multimedia.Context.Instance.EnableVideoSend = true;


                        await Navigation.PushAsync(new Chat(true)
                        {
                            Title = "Folio: " + folio,
                            BarBackgroundColor = Color.FromHex("#FFFFFF")
                        });
                    }
                    else
                    {
                        await DisplayAlert("Info", oResponseModel_Video.Message, "Aceptar");

                        if (PopupNavigation.Instance.PopupStack.Count > 0)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }

                    isVideoCall = false;
                    break;

                case "CALL":

                    Settings.bPoPupActivo = true;

                    if (!string.IsNullOrEmpty(Settings.sTelefonoDRs))
                    {
                        Settings.sError = "";
                        Device.OpenUri(new Uri("tel:" + Settings.sTelefonoDRs + ""));

                        Settings.bPoPupActivo = false;
                    }
                    else
                    {
                        Settings.bPoPupActivo = false;
                        await DisplayAlert("Llamada", "Por el momento no hay servicio de llamadas, cierre sesión e intente nuevamente.", "Aceptar");
                    }
                    Settings.bEnProceso = false;
                    break;
            }

        }

        private async Task<bool> VerificaPermisosAudio()
        {
            return await PermissionValidator.CheckVideoCallPermissions();
        }

        protected override bool OnBackButtonPressed()
        {
            Settings.bChatInicializado = false;
            Settings.bSession = false;
            return base.OnBackButtonPressed();
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
