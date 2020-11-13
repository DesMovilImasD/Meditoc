using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if __ANDROID__
using Android.Content;
#endif


namespace CallCenter.Views.UniqueOrientation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UniqueOrientationView : ContentPage
    {
        private UniqueOrientationModel modelContext;

#if __ANDROID__
        private Intent SharedIntent { get; set; }
        public UniqueOrientationView(Intent sharedIntent)
        {
            SharedIntent = sharedIntent;
            modelContext = new UniqueOrientationModel(this, sharedIntent);
#else
        public UniqueOrientationView()
        {
            modelContext = new UniqueOrientationModel(this);
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
            UniqueOrientationDTO item = e.SelectedItem as UniqueOrientationDTO;
            modelContext.Selected(item);

        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            UniqueOrientationDTO item = e.Item as UniqueOrientationDTO;
            modelContext.Selected(item);

        }

    }
}
