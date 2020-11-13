using CallCenter.Helpers;
using CallCenter.Renderers;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CallCenter.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        readonly ICPFeeds cpFeeds;
        CultureInfo culture = new CultureInfo("es-MX");       
        private PopupLoad _loginPopup;

        private PopupInstructions _PopupInstructions;

        public HomeViewModel(Page page)
            : base(page)
        {
            this.cpFeeds = DependencyService.Get<ICPFeeds>();
            //this._CometChatService = new CometChatService(page);
            Settings.bChatInicializado = false;
            Settings.bClosePopPup = true;
            // _loginPopup = new PopupLoad();

            _PopupInstructions = new PopupInstructions();
        }

        #region [Declaración variables]

        #endregion

        public async Task ExecuteHomeCommand()
        {

            Settings.sError = "";

            if (IsBusy)
                return;

            IsBusy = true;

            if (!Settings.bChatInicializado)
            {
                IsBusy = false;
            }
        }

        public async Task ExecuteLoginCommand()
        {
            Settings.sError = "";

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (!Settings.bChatInicializado)
                    await ExecuteHomeCommand();

            }
            catch (Exception ex)
            {
                Settings.sError = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }

        }

        public async Task ExecuteGrupoChatCommand()
        {
            Settings.sUIDDR = "";
            Settings.sError = "";

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (Settings.sUIDDR != "")
                {
                    if (!Settings.bChatInicializado)
                    {
                        Settings.bClosePopPup = false;
                        await ExecuteHomeCommand();
                    }

                    if (!Settings.bLogueado)
                    {
                        Settings.bClosePopPup = false;
                        await ExecuteLoginCommand();
                    }

                    Settings.bClosePopPup = true;
                }
                else
                    throw new Exception("Por el momento no se encuentra disponible el servicio, reintente más tarde.");

            }
            catch (Exception ex)
            {
                Settings.sError = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }

        }

        public async Task ExecuteChatCommand()
        {
            try
            {
                Settings.sUIDDR = "";
                Settings.sError = "";

                if (IsBusy)
                    return;

                IsBusy = true;
            }
            catch (Exception ex)
            {
                Settings.sError = ex.Message;
            }
            finally
            {
                IsBusy = false;

                if (Settings.sError != "")
                {
                    Settings.bEnProceso = false;
                    if (Settings.bPoPupActivo)
                    {
                        Settings.bPoPupActivo = false;
                        //await PopupNavigation.Instance.PopAsync();
                        Settings.bClicButton = false;
                        await page.DisplayAlert("Información", Settings.sError, "Aceptar");
                    }
                }
            }

        }
        public async Task<bool> SlowAsync()
        {
            await Task.Delay(5000); //or whatever async, some server fetch, this is not blocking the UI, the main thread is released until it is finished .. unless it also contains some synchronous code
            IsBusy = false;
            Settings.bClicButton = false;
            return true;
        }
        public async Task ExecuteVideoChatCommand()
        {
            try
            {
                Settings.sUIDDR = "";
                Settings.sError = "";

                if (IsBusy)
                    return;

                IsBusy = true;
            }
            catch (Exception ex)
            {
                Settings.sError = ex.Message;
            }
            finally
            {
                IsBusy = false;

                if (Settings.sError != "")
                {
                    Settings.bEnProceso = false;
                    if (Settings.bPoPupActivo)
                    {
                        Settings.bPoPupActivo = false;
                        Settings.bClicButton = false;
                        await page.DisplayAlert("Información", Settings.sError, "Aceptar");
                    }
                }
            }

        }

    }
}
