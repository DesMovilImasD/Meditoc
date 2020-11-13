using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Renderers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupLoad : PopupPage
    {
        public string _message = "";
        public string Message {
            get { return _message; }
            set {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public PopupLoad (string message = "Conectando con un médico...")
		{
			InitializeComponent ();
            Message = message;
		}
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
    }
}