using System;
using System.Collections.Generic;
using System.Threading;
using CallCenter.Models;
using Xamarin.Forms;

using CallCenter.ViewModels;

namespace CallCenter.Views
{
    using UIControl = IntelliAbb.Xamarin.Controls;

    public partial class vwCOVIDSurvey : ContentPage
    {

        private COVIDSurveyViewModel GetContext
        {
            get{return (COVIDSurveyViewModel)BindingContext;}
        }

        public vwCOVIDSurvey()
        {
            InitializeComponent();
            Configure();
        }

        public void Configure()
        {
            BindingContext = COVIDSurveyViewModel.Create(this);
            NavigationPage.SetHasNavigationBar(this, false);
        }
       
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SurveyAsk model = e.SelectedItem as SurveyAsk;
            GetContext.Selected(model);
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            SurveyAsk model = e.Item as SurveyAsk;
            GetContext.Selected(model);
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetContext.ReloadDataCommand.Execute(null);
            GetContext.LocationCommand.Execute(null);
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        void Checked_Tapped(System.Object sender, Xamarin.Forms.TappedEventArgs e)
        {
            var checkbox = (UIControl.Checkbox)sender;
            var ob = (SurveyAsk)checkbox.BindingContext;
        }

        async void tabMenu_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
