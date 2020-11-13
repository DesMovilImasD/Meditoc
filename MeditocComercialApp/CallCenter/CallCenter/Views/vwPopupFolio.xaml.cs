using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using CallCenter.Helpers;
using CallCenter.Models;

namespace CallCenter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vwPopupFolio : PopupPage
    {
        /**
         * Feed service
         */ 
        private readonly ICPFeeds cpFeedService;

        /**
         * Completion task
         */
        public TaskCompletionSource<FolioResult> _resultCompletion = null;

        /**
         * intancia de la clase
         * retorana un folio result, muestra el modal.
         */
        public static async Task<FolioResult> Show(IPopupNavigation navigator)
        {
            var view = new vwPopupFolio();
            await navigator.PushAsync(view);
            var result = await view.GetResult();
            await navigator.PopAsync();
            return result;
        }

        /**
         * constructor
         */
        public vwPopupFolio()
        {
            InitializeComponent();
            BindingContext = this;
            cpFeedService = DependencyService.Get<ICPFeeds>();
            Loading(false);
        }

        /**
         * lanza el formulario para 
         */
        public ICommand ClickToForm => new Command<string>(
            async (url) => await Launcher.OpenAsync(url));

        /**
         * dissmiss al popup
         */
        void Cancel_Tapped(System.Object sender, System.EventArgs e)
        {
            if (_resultCompletion != null)
            {
                _resultCompletion.SetResult(FolioResult.Fail());
                _resultCompletion = null;
            }
        }

        /**
         * ejecutar proceso de validacion del folio
         */
        async void Submit_Tapped(System.Object sender, System.EventArgs e)
        {
            // verificamos si el folio cumple con lo minimo para ser enviado
            if (!await VerifyFolio()) { return; }

            // obtenemos el folio del textField.
            string Folio = FolioField.Text;

            // mostramos el loading.
            Loading(true);

            //string FolioLogin = string.Format("{0}_{1}", Settings.sUsuarioUID, Folio);
            string UserRequest = Settings.sUsuarioUID;

            // solicitamos un medico disponible.
            ResponseModel model = await cpFeedService.m_SolicitaMedico(UserRequest, Folio);
            if (!string.IsNullOrEmpty(model.sParameter1))
            {
                if (_resultCompletion != null)
                {
                    _resultCompletion.SetResult(
                        FolioResult.Done(Message: model.sMensaje,
                                         SessionId: model.sParameter1,
                                         Folio: model.sFolio));

                    _resultCompletion = null;
                }
            }
            else
            {
                Loading(false);
                await DisplayAlert("Info", model.sMensaje, "Aceptar");
            }
        }

        /**
         * crear la tarea para completar.
         * utilizada para obtener la respuesta del modal.
         */
        public Task<FolioResult> GetResult()
        {
            _resultCompletion = new TaskCompletionSource<FolioResult>();
            return _resultCompletion.Task;
        }

        /**
         * manejo del loading en el modal.
         */
        public void Loading(bool status)
        {
            buttonsLayout.IsVisible = !status;
            loadingLayout.IsVisible = status;
            FolioField.IsEnabled = !status;
            remarkLabel.IsVisible = !status;
        }

        /**
         * verifica el campo de folio.
         */
        public async Task<bool> VerifyFolio()
        {
            if(string.IsNullOrEmpty( FolioField.Text) || string.IsNullOrEmpty(FolioField.Text))
            {
                await DisplayAlert("", "Es necesario proporcionar un folio", "Ok");
                return false;
            }

            return true;
        }
    }

    /**
     * resultado del objeto folio result.
     */
    public struct FolioResult
    {
        // si el proceso fue correcto
        public bool isSuccess;

        // mensaje del server
        public string Message;

        // id de la sesion
        public string SessionId;

        public string Folio;

        // instancia de la estructura cuando se obtiene un mensaje correcto
        public static FolioResult Done(string Message, string SessionId, string Folio) => new FolioResult
        {
            isSuccess = true,
            Message = Message,
            SessionId = SessionId,
            Folio = Folio
        };

        // instancia de la estructura cuando se obtiene un mensaje fallido.
        public static FolioResult Fail() => new FolioResult
        {
            isSuccess = false,
            Message = "",
            SessionId = "",
            Folio = ""
        };
    }

    
}
