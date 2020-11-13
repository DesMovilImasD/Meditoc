using CallCenter.Helpers;
using CallCenter.Models;
using CallCenter.Renderers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CallCenter.ViewModels
{
    class CambioContrasenaViewModel : BaseViewModel
    {
        readonly ICPFeeds cpFeeds;
        CultureInfo culture = new CultureInfo("es-MX");
        private PopupLoad _loginPopup;

        public CambioContrasenaViewModel(Page page)
           : base(page)
        {
            this.cpFeeds = DependencyService.Get<ICPFeeds>();
            this._loginPopup = new PopupLoad();
        }

        public async Task m_CambiarContrasenaCommand(RenewPass oRenewPass)
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {                
                if (await cpFeeds.m_Cambio_Contasena(oRenewPass.sUsuarioLogin, oRenewPass.sPasswordLogin))
                {
                    await page.DisplayAlert("Confirmación", "La contraseña ha sido modificada exitosamente.", "Aceptar");
                   
                }
                else
                {
                    if (string.IsNullOrEmpty(Settings.sError))
                        await page.DisplayAlert("Información", "Ocurrio un problema al cambiar la contraseña.", "Aceptar");
                    else
                        await page.DisplayAlert("Información", Settings.sError, "Aceptar");
                }

            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Información", ex.Message, "Aceptar");
            }

            finally
            {
                IsBusy = false;
                // await PopupNavigation.Instance.PopAsync();
                
            }
        }

        string scontrasenanueva = string.Empty;
        public const string sContrasenaNuevaPropertyName = "sContrasenaNueva";
        public string sContrasenaNueva
        {
            get { return scontrasenanueva; }
            set { SetProperty(ref scontrasenanueva, value, sContrasenaNuevaPropertyName); }
        }

        string bterminocondicion = string.Empty;
        public const string bTerminoCondicionPropertyName = "sTerminoCondicion";
        public string sTerminoCondicion
        {
            get { return bterminocondicion; }
            set { SetProperty(ref bterminocondicion, value, bTerminoCondicionPropertyName); }
        }
    }
}