using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if __ANDROID__
using Android.Content;
#endif

namespace CallCenter.Views.ContractMembership
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractMembershipView : ContentPage
    {
        private ContractMembershipModel modelContext;

#if __ANDROID__
        private Intent SharedIntent { get; set; }
        public ContractMembershipView(Intent sharedIntent)
        {
            SharedIntent = sharedIntent;
            modelContext = new ContractMembershipModel(this, sharedIntent);
#else
        public ContractMembershipView()
        {
            modelContext = new ContractMembershipModel(this);
#endif
            InitializeComponent();
            BindingContext = modelContext;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#12B6CB");
            stkActivity.SetBinding(IsVisibleProperty, "IsBusy");
            iaIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            modelContext.ReloadDataCommand.Execute(null);

        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ContractMembershipDTO item = e.SelectedItem as ContractMembershipDTO;
            modelContext.Selected(item);

        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ContractMembershipDTO item = e.Item as ContractMembershipDTO;
            modelContext.Selected(item);

        }
    }
}
