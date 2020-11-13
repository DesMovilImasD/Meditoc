using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CallCenter.Helpers;
using Xamarin.Forms;
using Conekta.Xamarin;
using System.Threading.Tasks;
using CallCenter.Validation;
using Xamarin.Essentials;
using CallCenter.Renderers;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using CallCenter.Models;
using Newtonsoft.Json;
using System.Linq;

namespace CallCenter.Views.UserInfo
{
    public class MonthlyPayment
    {
        public static MonthlyPayment Create(string title, int value) => new MonthlyPayment
        {
            Title = title,
            Code = value
        };

        public string Title { get; set; }
        public int Code { get; set; }



    }

    public partial class UserInfoView : ContentView, INotifyPropertyChanged
    {
        const string ONE_MONTH = "Una sola exhibición";
        const string THREE_MONTH = "3 meses sin intereses";
        const string SIX_MONTH = "6 meses sin intereses";
        const string NINE_MONTH = "9 meses sin intereses";
        const string TWELVE_MONTH = "12 meses sin intereses";


        private List<MonthlyPayment> _monthlyPayments { get; set; } = new List<MonthlyPayment>();
        private List<PoliciesMonthlyPayments> _months { get; set; }

        public int _TermsHeight = 22;
        public int TermsHeight
        {
            get
            {
                return _TermsHeight;
            }
            set
            {
                _TermsHeight = value;
                OnPropertyChanged(nameof(TermsHeight));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UserInfoView()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            try{
                _months = JsonConvert.DeserializeObject<List<PoliciesMonthlyPayments>>(Settings.MonthlyPayments);
            }
            catch (Exception e)
            {
                _months = new List<PoliciesMonthlyPayments>();
            }
            AdjustMonthyPayment();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Submit_Clicked(System.Object sender, System.EventArgs e)
        {
            var status = ValidationHelper.IsFormValid(BindingContext, this);
            MessagingCenter.Send<UserInfoView, object>(this, GlobalEventSender.PAYMENT_COMFIRM_PAY, status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UnderlineEntry_PropertyChanged(System.Object sender, System.EventArgs e)
        {
            var model = BindingContext as UserInfoDTO;
            if(model != null)
            {
                model.CardNumber = $"{model.CardPart1}".Replace("-", "");//{model.CardPart2}{model.CardPart3}{model.CardPart4}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UnderlineEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var model = BindingContext as UserInfoDTO;
            if (model != null)
            {
                if(emailField == sender)
                {
                    if (model.Email.Contains(" "))
                    {
                        model.Email = model.Email.Replace(" ", "");
                        emailField.Text = model.Email;
                    }
                    
                    ValidationHelper.IsValidProperty(
                        model,
                        this,
                        model.Email,
                        nameof(model.Email));
                }

                if(nameField == sender)
                {
                    ValidationHelper.IsValidProperty(
                        model,
                        this,
                        model.UserName,
                        nameof(model.UserName));
                }

                if(cardPart1Field == sender)
                {
                    ValidationHelper.IsValidProperty(
                        model,
                        this,
                        model.CardNumber,
                        nameof(model.CardNumber));
                }


                if(yearField == sender)
                {
                    ValidationHelper.IsValidProperty(
                        model,
                        this,
                        model.CardYear,
                        nameof(model.CardYear));
                }

                if(pinField == sender)
                {
                    ValidationHelper.IsValidProperty(
                        model,
                        this,
                        model.CardPin,
                        nameof(model.CardPin));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(Settings.LinkTermsAndConditions))
                {
                    await Launcher.TryOpenAsync(Settings.LinkTermsAndConditions);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AwesomeCheckbox_IsCheckedChanged(System.Object sender, System.EventArgs e)
        {
            var check = (IntelliAbb.Xamarin.Controls.Checkbox)sender;
            var model = BindingContext as UserInfoDTO;
            if(model != null && check != null){
                model.TermsAndConditions = check.IsChecked;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void AdjustMonthyPayment()
        {
            var model = this.BindingContext as UserInfoDTO;
            if (model is null) return;
            _monthlyPayments.Clear();
            _monthlyPayments.Add(MonthlyPayment.Create(ONE_MONTH, 1));

            if (Settings.HasMonthsWithoutInterest)
            {
                var items = _months
                           .Where(o => o.minPurchase <= model.Total)
                           .Select(o => MonthlyPayment.Create(o.Remarks, o.Months)).ToList();
                _monthlyPayments.AddRange(items);

            }
            
            TypePicker.ItemsSource = _monthlyPayments;
            TypePicker.ItemDisplayBinding = new Binding("Title");
            TypePicker.SelectedItem = Settings.HasMonthsWithoutInterest ? null : _monthlyPayments.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TypePicker_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var picker = (UnderlinePicker)sender;
            var item = picker.SelectedItem as MonthlyPayment;
            var model = BindingContext as UserInfoDTO;
            if (model != null)
            {
                var value = (item == null) ? null : (int?)item.Code;
                var status = model.Type != value;
                model.Type = value;
                if (status){
                    ValidationHelper.IsValidProperty(
                        model,
                        this,
                        model.Type,
                        nameof(model.Type));
                }
                
            }
        }




    }
}
