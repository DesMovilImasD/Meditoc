using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallCenter.Helpers.FontAwesome;
using CallCenter.Helpers;

namespace CallCenter.Views.Dialogs.PayNotification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayNotificationView : PopupPage, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigator"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<bool> Show(IPopupNavigation navigator, string username, string password, bool multiple = false)
        {
            var view = new PayNotificationView(username, password, multiple);
            await navigator.PushAsync(view);
            var result = await view.GetResult();
            await navigator.PopAsync();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public TaskCompletionSource<bool> _resultCompletion = null;

        private string Username { get; set; }

        private string Password { get; set; }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public PayNotificationView(string username, string password, bool isMultiple)
        {
            InitializeComponent();
            BindingContext = this;
            Username = username;
            Password = password;
            IsMultiple = isMultiple;

            if (!IsMultiple) BuildDialog();
            else BuildDialog2(); ;
        }

        public Task<bool> GetResult()
        {
            _resultCompletion = new TaskCompletionSource<bool>();
            return _resultCompletion.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Submit_Tapped(System.Object sender, System.EventArgs e)
        {
            if (_resultCompletion != null)
            {
                _resultCompletion.SetResult(true);
                _resultCompletion = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// build dialog
        /// </summary>
        public void BuildDialog()
        {
            Line1 = "\n\nTu pago se ha realizado con éxito, \n tus credenciales de acceso son: \n\n";
            LineUser = "Usuario: ";
            LineUserData = $"{this.Username} \n";
            LinePassword = "Contraseña:";
            LinePasswordData = $"{this.Password} \n\n";
            Line2 = "Te llegará un correo electrónico " +
                "con tus credenciales de acceso. " +
                "Si no recibes tus credenciales favor de comunicarte " +
                "con nosotros enviando un correo electrónico a " +
                $"{Settings.SupportEmail} para brindarte soporte";
            IconLabel = FontAwesomeIcons.Check;
        }

        /// <summary>
        /// build dialog2
        /// </summary>
        public void BuildDialog2()
        {
            IconLabel = FontAwesomeIcons.Check;
            Line1 = "\n\nTu pago se ha realizado con éxito, " +
                "te llegará un correo electrónico " +
                "con tus credenciales de acceso \n\n" +
                "Si no recibes tus credenciales favor de comunicarte " +
                "con nosotros enviando un correo electrónico a " +
                $"{Settings.SupportEmail} para brindarte soporte \n\n";
        }

        public string _iconLabel { get; set; } = "";
        public string IconLabel
        {
            get { return _iconLabel; }
            set
            {
                _iconLabel = value;
                OnPropertyChanged(nameof(IconLabel));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string _line1 { get; set; } = "";
        public string Line1
        {
            get { return _line1;  }
            set
            {
                _line1 = value;
                OnPropertyChanged(nameof(Line1));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string _line2 { get; set; } = "";
        public string Line2
        {
            get { return _line2;  }
            set
            {
                _line2 = value;
                OnPropertyChanged(nameof(Line2));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string _LinePassword { get; set; } = "";
        public string LinePassword
        {
            get { return _LinePassword; }
            set
            {
                _LinePassword = value;
                OnPropertyChanged(nameof(LinePassword));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string _linePasswordData { get; set; } = "";
        public string LinePasswordData
        {
            get { return _linePasswordData; }
            set
            {
                _linePasswordData = value;
                OnPropertyChanged(nameof(LinePasswordData));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string _lineUser { get; set; } = "";
        public string LineUser
        {
            get { return _lineUser; }
            set
            {
                _lineUser = value;
                OnPropertyChanged(nameof(LineUser));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string _lineUserData { get; set; } = "";
        public string LineUserData
        {
            get { return _lineUserData; }
            set
            {
                _lineUserData = value;
                OnPropertyChanged(nameof(LineUserData));
            }
        }

        public bool _isMultiple { get; set; } = false;
        public bool IsMultiple
        {
            get { return _isMultiple; }
            set
            {
                _isMultiple = value;
                OnPropertyChanged(nameof(IsMultiple));
            }
        }
    }
}
