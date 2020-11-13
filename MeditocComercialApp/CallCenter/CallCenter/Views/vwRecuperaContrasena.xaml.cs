using CallCenter.ViewModels;
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
	public partial class vwRecuperaContrasena : ContentPage
    {
        RecuperarContrasenaViewModel oRecuperarContrasenaViewModel;
        int _iPaso;

        public vwRecuperaContrasena(int iVentana)
        {
            _iPaso = iVentana;
            InitializeComponent ();
            txtUser.IsVisible = true;
            btnRecuperarContrasena.IsVisible = true;
            txtValidacion.IsVisible = false;
            btnCambiarContrasena.IsVisible = false;
            btnBack.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(async () => await OnBack()) });
            BindingContext = oRecuperarContrasenaViewModel = new RecuperarContrasenaViewModel(this, ref txtUser, ref btnRecuperarContrasena, ref txtValidacion, ref btnCambiarContrasena);
            InitFormulario();
        }

        private void InitFormulario()
        {
            Image img = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 35
            };

            var tap = new TapGestureRecognizer();

            tap.Tapped +=
               (sender, e) =>
               {
                   OnClick_btnRegresar(sender, e);
               };
            img.GestureRecognizers.Add(tap);


            txtUser.IsVisible = true;
            btnRecuperarContrasena.IsVisible = true;
            txtValidacion.IsVisible = false;
            btnCambiarContrasena.IsVisible = false;
            txtUser.SetBinding(Entry.TextProperty, RecuperarContrasenaViewModel.stextPropertyName);
            txtValidacion.SetBinding(Entry.TextProperty, RecuperarContrasenaViewModel.sCodigoConfirmacionPropertyName);

            if (_iPaso == 2)
            {
                txtUser.IsVisible = false;
                btnRecuperarContrasena.IsVisible = true;
                txtValidacion.IsVisible = true;
                btnCambiarContrasena.IsVisible = true;

            }

            btnRecuperarContrasena.SetBinding(Button.CommandProperty, RecuperarContrasenaViewModel.EnviarContrasenaPropertyName);
            btnCambiarContrasena.SetBinding(Button.CommandProperty, RecuperarContrasenaViewModel.RecuperarContrasenaPropertyName);


            stkActivity.SetBinding(IsVisibleProperty, "IsBusy");
            iaIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
        }

        private async Task OnBack()
        {
            m_Clear_Settings();
            await Navigation.PopModalAsync();
        }

        private async void OnClick_btnRegresar(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            m_Clear_Settings();

            return base.OnBackButtonPressed();
        }

        private void m_Clear_Settings()
        {
            Settings.sUserNameLogin = "";
            Settings.iPaso = 1;
        }
    }
}