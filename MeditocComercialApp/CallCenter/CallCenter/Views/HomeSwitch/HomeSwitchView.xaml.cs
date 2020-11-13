using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.Linq;
using CallCenter.Views.UniqueOrientation;
using CallCenter.Views.ContractMembership;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using CallCenter.Renderers;
using CallCenter.Helpers;
using CallCenter.Services;
using Xamarin.Essentials;
using CallCenter.Views.MedicDirectory;

#if __ANDROID__
using Android.Content;
#endif

namespace CallCenter.Views.HomeSwitch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeSwitchView : ContentPage
    {
        /// <summary>
        ///  model view relacionado a la vista.
        /// </summary>
        HomeSwitchModel model;
        InternetService _InternetService;

        bool IsClicked { get; set; } = false;
        bool IsPoliciesdUpdate { get; set; } = false;

#if __ANDROID__
        private Intent SharedIntent { get; set; }
        public HomeSwitchView(Intent sharedIntent)
        {
            SharedIntent = sharedIntent;
            model = new HomeSwitchModel(this, sharedIntent);
#else
        public HomeSwitchView()
        {
            model = new HomeSwitchModel(this);
#endif
            InitializeComponent();
            BindingContext = model;
            //stkActivity.SetBinding(IsVisibleProperty, "IsBusy");
            //iaIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            _InternetService = new InternetService(this);
            versionLabel.Text = string.Format(" V{0}", DependencyService.Get<IAppInfo>().GetVersion());
            NavigationPage.SetBackButtonTitle(this, "");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ShowLoading();
            await VersionCheck();
            await HideLoading();
        }

        void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
    

            MenuCollection.SelectedItem = null;
            UpdateSelectionData(e.PreviousSelection, e.CurrentSelection);
        }

       

        async void UpdateSelectionData(IEnumerable<object> previousSelectedContact, IEnumerable<object> currentSelectedContact)
        {

            var selected = currentSelectedContact.FirstOrDefault() as MenuSwitchDTO;
            if (IsClicked) { return; }
            IsClicked = true;

            await ShowLoading();
            if (!await VersionCheck())
            {
                IsClicked = false;
                await HideLoading();
                return;
            }

            if (selected is null)
            {
                IsClicked = false;
                await HideLoading();
                return;
            }

            if (!await _InternetService.VerificaInternet())
            {
                IsClicked = false;
                await HideLoading();
                return;
            }

            //IsBusy = true;
            //Debug.WriteLine("Name: " + selected.Name);
            //Debug.WriteLine("Icon: " + selected.Icon);
            //Debug.WriteLine("Navigate: " + selected.Navigate);

            Page _page = null;
#if __ANDROID__
            switch (selected.Navigate)
            {
                case "LOGIN": {_page = new vwLoginPage(SharedIntent);break;}
                case "ORIENTATION": {_page = new UniqueOrientationView(SharedIntent);break;}
                case "MEMBERSHIP": {_page = new ContractMembershipView(SharedIntent);break;}
                case "DIRECTORY": { _page = new MeditocDirectoryView(SharedIntent); break; }
                case "PHONE": {  OpenDial(); break;}
                default: { break; }
            }
#else
            switch (selected.Navigate)
            {
                case "LOGIN": { _page = new vwLoginPage(); break; }
                case "ORIENTATION": { _page = new UniqueOrientationView(); break; }
                case "MEMBERSHIP": { _page = new ContractMembershipView(); break; }
                case "DIRECTORY": { _page = new MeditocDirectoryView(); break; }
                case "PHONE": { OpenDial(); break; }
                default: { break; }
            }
#endif
            if (_page != null)
            {
                await GetPolicies();
                await Navigation.PushAsync(_page);
            }
            IsClicked = false;
            await HideLoading();
        }

        async Task ShowLoading()
        {
            if(PopupNavigation.Instance.PopupStack.Count() == 0)
            {
                await PopupNavigation
                        .Instance
                        .PushAsync(new PopupLoad(message: "Espere un momento ..."));

            }
        }


        async Task HideLoading()
        {
            if (PopupNavigation.Instance.PopupStack.Count() > 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        async Task<bool> VersionCheck()
        {
            if (await _InternetService.VerificaInternet())
            {
                await model.VerifyStoreVersion();
            }

            return model.AllowNavigation();
        }

        public async void OpenDial()
        {
            try
            {
                await Launcher.TryOpenAsync(new Uri($"tel:{911}"));
            }catch(Exception)
            {

            }
        }

        public async Task GetPolicies()
        {
            if (IsPoliciesdUpdate) return;
            IsPoliciesdUpdate = await DependencyService.Get<ICPFeeds>()
                .GetPolicies();
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if(IsClicked) { return; }
                IsClicked = true;
                await ShowLoading();

                if (!await _InternetService.VerificaInternet())
                {
                    IsClicked = false;
                    await HideLoading();
                    return;
                }

                await GetPolicies();
                await Launcher.TryOpenAsync(Settings.LinkTermsAndConditions);

                await HideLoading();
                IsClicked = false;
            }
            catch(Exception)
            {
                IsClicked = false;
                await HideLoading();
            }
        }
    }
}
