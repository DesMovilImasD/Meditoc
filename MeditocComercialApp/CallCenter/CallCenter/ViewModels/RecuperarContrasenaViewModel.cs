using CallCenter.Helpers;
using CallCenter.Models;
using CallCenter.Renderers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CallCenter.ViewModels
{
    public class RecuperarContrasenaViewModel : BaseViewModel
    {
        readonly ICPFeeds cpFeeds;
        CultureInfo culture = new CultureInfo("es-MX");

        MyEntry txtUser;
        Button btnRecuperarContrasena;
        MyEntry txtValidacion;
        Button btnCambiarContrasena;

        public RecuperarContrasenaViewModel(Page page)
           : base(page)
        {
            this.cpFeeds = DependencyService.Get<ICPFeeds>();
        }

        #region[Variables]
        string susuariorecuperacion = string.Empty;
        public const string stextPropertyName = "sUsuarioRecuperacion";
        public string sUsuarioRecuperacion
        {
            get { return susuariorecuperacion; }
            set { SetProperty(ref susuariorecuperacion, value, stextPropertyName); }
        }

        string scodigoconfirmacion = string.Empty;
        public const string sCodigoConfirmacionPropertyName = "sCodigoConfirmacion";
        public string sCodigoConfirmacion
        {
            get { return scodigoconfirmacion; }
            set { SetProperty(ref scodigoconfirmacion, value, sCodigoConfirmacionPropertyName); }
        }
        #endregion

        public RecuperarContrasenaViewModel(Page page, ref MyEntry txtUser, ref Button btnRecuperarContrasena, ref MyEntry txtValidacion, ref Button btnCambiarContrasena) : this(page)
        {
            this.txtUser = txtUser;
            this.btnRecuperarContrasena = btnRecuperarContrasena;
            this.txtValidacion = txtValidacion;
            this.btnCambiarContrasena = btnCambiarContrasena;
        }

        public const string EnviarContrasenaPropertyName = "EnviarContrasena";
        Command loginCommand;
        public Command EnviarContrasena
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new Command(async () => await ExecuteConfirmCommand()));
            }
        }

        public const string RecuperarContrasenaPropertyName = "RecoveryContrasena";
        Command RecuperarCommand;
        public Command RecoveryContrasena
        {
            get
            {
                return RecuperarCommand ??
                    (RecuperarCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }

        private async Task ExecuteConfirmCommand()
        {
            RenewPass objRenewPass = new RenewPass();

            if (Settings.iPaso == 2)
            {
                sUsuarioRecuperacion = Settings.sUserNameLogin;
                Settings.iPaso = 1;
            }

            if (string.IsNullOrEmpty(sUsuarioRecuperacion))
            {
                await page.DisplayAlert("Código de verificación", "Favor de ingresar el usuario.", "Aceptar");
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;
            loginCommand.ChangeCanExecute();
            Settings.sError = "";

            try
            {
                Settings.sUserNameLogin = sUsuarioRecuperacion;

                objRenewPass.sUsuarioLogin = sUsuarioRecuperacion;
                objRenewPass.iPaso = Settings.iPaso;

                if (await cpFeeds.m_Cambio_Contasena(objRenewPass.sUsuarioLogin, ""))
                {
                    Settings.iPaso = 2;
                    await page.DisplayAlert("Código de verificación", "En breve le llegará un email con su código de verificación", "Aceptar");
                    txtUser.IsVisible = false;
                    txtUser.Text = "";
                    btnRecuperarContrasena.Text = "SOLICITAR OTRO CÓDIGO";
                    txtValidacion.IsVisible = true;
                    txtValidacion.Text = "";
                    btnCambiarContrasena.IsVisible = true;
                }
                else
                    if (string.IsNullOrEmpty(Settings.sError))
                    await page.DisplayAlert("Código de verificación", "Por el momento no es posible solicitar un código de verificación, reintente más tarde", "Aceptar");
                else
                    await page.DisplayAlert("Código de verificación", Settings.sError, "Aceptar");

            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Código de verificación", ex.Message, "Aceptar");
            }

            finally
            {
                IsBusy = false;
                loginCommand.ChangeCanExecute();
            }


        }

        private async Task ExecuteLoginCommand()
        {
            RenewPass oRenewPass = new RenewPass();

            if (string.IsNullOrEmpty(sCodigoConfirmacion))
            {
                await page.DisplayAlert("Código de verificación", "Favor de ingresar el código de verificación.", "Aceptar");
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;
            loginCommand.ChangeCanExecute();
            Settings.sError = "";

            try
            {
                oRenewPass.sUsuarioLogin = Settings.sUserNameLogin;
                oRenewPass.sCodigoVerificacion = scodigoconfirmacion;
                oRenewPass.iPaso = Settings.iPaso;

                if (await cpFeeds.m_Cambio_Contasena("",""))
                {
                    Settings.iPaso = 1;
                    await page.DisplayAlert("Cambio de contraseña", "Su nueva contraseña ha sido enviada a su email, favor de verificar.", "Aceptar");
                    //Application.Current.MainPage = new vwLoginPage();
                }
                else
                {
                    if (string.IsNullOrEmpty(Settings.sError))
                        await page.DisplayAlert("Código de verificación", "Por el momento no es posible completar la solicitud de cambio de contraseña, reintente más tarde", "Aceptar");
                    else
                        await page.DisplayAlert("Código de verificación", Settings.sError, "Aceptar");
                }

            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Código de verificación", "Por el momento no es posible completar la solicitud de cambio de contraseña, " +
                    "reintente más tarde. Error: " + ex.Message, "Aceptar");
            }
            finally
            {
                IsBusy = false;
                loginCommand.ChangeCanExecute();
            }


        }

    }
}
