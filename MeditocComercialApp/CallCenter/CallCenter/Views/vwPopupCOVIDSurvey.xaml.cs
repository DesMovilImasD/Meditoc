using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallCenter.Helpers;
using Plugin.Toast.Abstractions;
using Plugin.Toast;

namespace CallCenter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vwPopupCOVIDSurvey : PopupPage, INotifyPropertyChanged
    {
        private List<string> PHRASES { get; set; }
        private string FOLIO { get; set; }
        ToastLength toastLength = ToastLength.Long;


        private string _title1 = "";
        public string Title1 {
            get { return _title1; }
            set {
                _title1 = value;
                OnPropertyChanged(nameof(Title1));
            } }

        private string _subtitle1 = "";
        public string Subtitle1 {
            get {return _subtitle1; }
            set {
                _subtitle1 = value;
                OnPropertyChanged(nameof(Subtitle1));
            } }

        private string _subtitle2 = "";
        public string Subtitle2 {
            get { return _subtitle2; }
            set {
                _subtitle2 = value;
                OnPropertyChanged(nameof(Subtitle2));
            } }

        private string _subtitle3 = "";
        public string Subtitle3 {
            get { return _subtitle3; }
            set {
                _subtitle3 = value;
                OnPropertyChanged(nameof(Subtitle3));
            } }

        private string _subtitle4 = "";
        public string Subtitle4 {
            get { return _subtitle4; }
            set {
                _subtitle4 = value;
                OnPropertyChanged(nameof(Subtitle4));
            } }

        private string _subtitle5 = "";
        public string Subtitle5
        {
            get { return _subtitle5; }
            set
            {
                _subtitle5 = value;
                OnPropertyChanged(nameof(Subtitle5));
            }
        }

        /**
        * Completion task
        */
        public TaskCompletionSource<bool> _resultCompletion = null;

        /**
         * intancia de la clase
         * retorana un folio result, muestra el modal.
         */
        public static async Task<bool> Show(IPopupNavigation navigator, List<string> phrases, string FOLIO)
        {
            var view = new vwPopupCOVIDSurvey(phrases, FOLIO);
            await navigator.PushAsync(view);
            var result = await view.GetResult();
            await navigator.PopAsync();
            return true;
        }

        /**
         * constructor
         */
        public vwPopupCOVIDSurvey(List<string> phrases, string folio)
        {
            InitializeComponent();
            BindingContext = this;
            PHRASES = phrases;
            FOLIO = folio;
            BuildDialog();
        }

        /**
         * dissmiss al popup
         */
        void Cancel_Tapped(System.Object sender, System.EventArgs e)
        {
            if (_resultCompletion != null)
            {
                _resultCompletion.SetResult(!string.IsNullOrEmpty(FOLIO));
                _resultCompletion = null;
            }
        }

        /**
         * retornar el resultado default
         */
        public Task<bool> GetResult()
        {
            _resultCompletion = new TaskCompletionSource<bool>();
            return _resultCompletion.Task;
        }


        /**
         * construye el dialogo con la respuesta del servidor.
         */
        public void BuildDialog()
        {
            if(PHRASES != null && PHRASES.Count() > 0)
            {
                int loop = 0;
                foreach(string item in PHRASES)
                {
                    loop++;
                    switch (loop){
                        case 1:
                            Title1 = item;
                            break;
                        case 2:
                            Subtitle1 = item;
                            break;
                        case 3:
                            Subtitle2 = item;
                            break;
                        case 4:
                            Subtitle3 = item;
                            break;
                        case 5:
                            Subtitle4 = item;
                            break;
                        case 6:
                            Subtitle5 = item;
                            break;
                        default:
                            break;
                    }
                    
                }
            }
        }

        /**
         * mostrar dialogo con folio.
         */
        public void DialogFolio(string folio)
        {
            Title1 = "";
            Subtitle1 = "Muchas gracias por contestar el cuestionario. Su folio es:";
            Subtitle2 = string.Format(" {0} ", folio);
            Subtitle3 = "Es importante guardarlo ya que se le solicitará durante la consulta";
            Subtitle4 = "";
            Subtitle5 = "Podrá acceder a nuestro servicio de consulta en línea con un médico.";
        }

        /**
         * mostrar dialogo sin folio
         */
        public void DialogWithoutFolio()
        {
            Title1 = "¡Muchas gracias por sus respuestas!";
            Subtitle1 = "Con base a ellas se concluyó que usted no tiene la sintomatología para sospechas la presencia de COVID-19.";
            Subtitle2 = "";
            Subtitle3 = "";
            Subtitle4 = "En caso de presentar nuevos sintomas por favor conteste estas preguntas nuevamente. Continua con las medidas preventivas y manténganse informado a través de los canales oficiales";
            Subtitle5 = "¡prevenir es tarea de todos!";
        }

        /**
         * on properti change.
         */
        protected override void OnPropertyChanged(string propertyName = null)
        {

            base.OnPropertyChanged(propertyName);
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Settings.COVIDFolio))
            //{
            //    await Clipboard.SetTextAsync(Settings.COVIDFolio);
            //    CrossToastPopUp.Current.ShowCustomToast(string.Format("Folio copiado {0} ", Settings.COVIDFolio), "#595959", "#FFFFFF", toastLength);
            //}

        }
    }
}
