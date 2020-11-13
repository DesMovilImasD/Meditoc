using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Views.Dialogs.EditQuantity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditQuantityView : PopupPage, INotifyPropertyChanged
    {
        public TaskCompletionSource<EditQuantityResponse> _resultCompletion = null;
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int CurrentValue { get; set; }

        public static async Task<EditQuantityResponse> Show(IPopupNavigation navigator, int Current, int Min = 1, int Max = 300 )
        {
            var view = new EditQuantityView(Current, Min, Max);
            await navigator.PushAsync(view);
            var result = await view.GetResult();
            await navigator.PopAsync();
            return result;
        }

        public Task<EditQuantityResponse> GetResult()
        {
            _resultCompletion = new TaskCompletionSource<EditQuantityResponse>();
            return _resultCompletion.Task;
        }

        public EditQuantityView(int Current, int Min, int Max)
        {
            InitializeComponent();
            BindingContext = this;
            MinValue = Min;
            MaxValue = Max;
            Quantity = Current;
        }

        public void Cancel_Tapped(System.Object sender, System.EventArgs e)
        {
            if (_resultCompletion != null)
            {
                _resultCompletion.SetResult(EditQuantityResponse.Fail());
                _resultCompletion = null;
            }
        }

        public void Submit_Tapped(System.Object sender, System.EventArgs e)
        {
            errors.IsVisible = false;
            errors.Text = "";
            if (Quantity < MinValue )
            {
                errors.IsVisible = true;
                errors.Text = $"La cantidad no puede ser menor a {MinValue}";
                return;
            }

            if (_resultCompletion != null)
            {
                _resultCompletion.SetResult(EditQuantityResponse.Success(Quantity));
                _resultCompletion = null;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }


    }

    public struct EditQuantityResponse
    {
        public bool Status { get; set; }
        public int? Quantity { get; set; }

        public static EditQuantityResponse Success(int Quantity) => new EditQuantityResponse
        {
            Quantity = Quantity,
            Status = true
        };

        public static EditQuantityResponse Fail() => new EditQuantityResponse
        {
            Status = false,
            Quantity = null
        };
    }
}
