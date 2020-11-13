using CallCenter.Helpers;
using CallCenter.Models;
using CallCenter.Renderers;
using CallCenter.ViewModels;
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
	public partial class vwCambioContrasena : ContentPage
	{
        CambioContrasenaViewModel cambioContrasenaViewModel;
        private PopupLoad _loginPopup;
        private bool isRegister { get; set; } = false;

        public vwCambioContrasena ()
		{
			InitializeComponent ();
            BindingContext = cambioContrasenaViewModel = new CambioContrasenaViewModel(this);
            InitFormulario();
        }

        public void InitFormulario()
        {            
            var tap = new TapGestureRecognizer();

            tap.Tapped +=
               (sender, e) =>
               {
                   OnClick_btnfrmTerminosCondiciones(sender, e);
               };

            txtContrasenaNueva.Placeholder = "Contraseña Nueva";

            txtContrasenaNueva.IsPassword = true;

            btnContrasenaNueva.Text = "Cambiar Contraseña";

            btnContrasenaNueva.Clicked += BtnContrasenaNuevaOnClicked;

            _loginPopup = new PopupLoad();
        }

        private async void BtnContrasenaNuevaOnClicked(object sender, EventArgs eventArgs)
        {

            if (isRegister){return;}
            isRegister = true;

            if (txtContrasenaNueva.Text.Length < 6 || txtContrasenaNueva.Text.Length > 15)
            {
                await DisplayAlert("Aviso", "La contraseña debe de tener de 6 a 15 caracteres", "Aceptar");
                isRegister = false;
                return;
            }

            await PopupNavigation.Instance.PushAsync(_loginPopup);

            RenewPass oRenewPass = new RenewPass();
            oRenewPass.sUsuarioLogin = Settings.sFolio;
            oRenewPass.sPasswordLogin = txtContrasenaNueva.Text;
            
            await cambioContrasenaViewModel.m_CambiarContrasenaCommand(oRenewPass);
           
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
            isRegister = false;
        }

        private async void OnClick_btnfrmTerminosCondiciones(object sender, EventArgs e)
        {
            //stkActivity.IsVisible = true;
            //iaIndicator.IsRunning = true;

            //stkActivity.IsVisible = false;
            //iaIndicator.IsRunning = false;
        }

        private void m_CambioTextoBoton(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (txtContrasenaNueva.Text != "")
            {
                btnContrasenaNueva.Text = "Cambiar Contraseña";
            }
            else
            {
                btnContrasenaNueva.Text = "Continuar";
            }
        }
    }
}