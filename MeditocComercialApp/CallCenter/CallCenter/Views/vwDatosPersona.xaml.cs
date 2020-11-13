using CallCenter.Helpers;
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
    public partial class vwDatosPersona : ContentPage
    {
        INavigation oInavigation;
        private INavigation navigation;
        //private vwMainPageMaster detail;
        private MasterDetailPage oPage;

        public vwDatosPersona(INavigation poINavigation, MasterDetailPage popage)
        {
            oInavigation = poINavigation;

            InitializeComponent();
            
            lblinstitucion.Text = Settings.sInstitucion;
            //lblNombre.Text = Settings.sUserName;
            lblFolio.Text = Settings.sFolio;
            lblFolio.FontSize = 40;
            oPage = popage;
            /*
              Settings.sUserNameLogin = psUserLogin;
                    Settings.sUserName = sResponseUser.sNombre;
                    Settings.bDoctor = sResponseUser.bDoctor;
                    Settings.sUsuarioUID = sResponseUser.sUIDCliente;
                    Settings.sSexo = sResponseUser.sSexo;
                    Settings.sTelefonoDRs = sResponseUser.sTelefonoDRs;
                    Settings.sInstitucion = sResponseUser.sInstitucion;
                    Settings.iIdUsuario = sResponseUser.iIdUsuario;
                    Settings.sFolio = sResponseUser.sFolio;
             */


        }

        public vwDatosPersona()
        {
        }

        async void OnButtonClickedChangePassword(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new vwCambioContrasena());

            //await label.RelRotateTo(360, 1000);
        }

        protected override bool OnBackButtonPressed()
        {

            //oPage.Detail = new NavigationPage(new vwHomePage() { /*Title = Settings.sFolio*/ }) { BarBackgroundColor = Color.White, BarTextColor = Color.FromHex("#12b6cb") };
            //return true;
            return base.OnBackButtonPressed();
        }

        private async Task OnButtonClickeSoporteTecnico(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new vwContactPage());
        }


    }
}