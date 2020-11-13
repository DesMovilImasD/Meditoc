using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
#if __ANDROID__
using Android.Content;
#endif
using CallCenter.Helpers;
using Xamarin.Forms;
using CallCenter.Renderers;
using System.Linq;
using CallCenter.Helpers.FontAwesome;

namespace CallCenter.Views.MedicDirectory
{
    public partial class MeditocDirectoryView : ContentPage
    {
        readonly ICPFeeds cpFeeds;
        private MedicDirectoryModel oModel;
        public IList<specialtyDTO> lstEspecialidades { get; private set; }
#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }
        public MeditocDirectoryView(Intent screenshareIntent)
        {
            
            ScreenshareIntent = screenshareIntent;
            oModel = new MedicDirectoryModel(this, screenshareIntent);
#else
        public MeditocDirectoryView()
        {
            oModel = new MedicDirectoryModel(this);
#endif
            InitializeComponent();
            BindingContext = oModel;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#12B6CB");
            NavigationPage.SetBackButtonTitle(this, "");


            this.cpFeeds = DependencyService.Get<ICPFeeds>();
            
            relativePrincipal.Children.Add(
            txtEspecialidad,
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Width /3.3;
            }));

            lstEspecialidades = new List<specialtyDTO>();

            lstEspecialidades = Task.Run(() => oModel.CargarDatos()).Result;

            iconSearch.Text = FontAwesomeIcons.Search;

            txtBuscarEspecialidad.Completed += txtEspecialidad_Enter;

            BindingContext = this;

        }

        private void searchEspecialidades(object sender, EventArgs args)
        {
            try
            {
                string sEspecialidad = txtBuscarEspecialidad.Text;

                buscarDirectorio(sEspecialidad);
            }
            catch (Exception ex)
            {
                DisplayAlert("Información", "El cambo de busqueda es vacio", "OK");
            }
        }

        public void txtEspecialidad_Enter(object sender, EventArgs e)
        {
            try
            {
                string sEspecialidad = txtBuscarEspecialidad.Text;

                buscarDirectorio(sEspecialidad);
            }
            catch (Exception ex)
            {
                DisplayAlert("Información", "El cambo de busqueda es vacio", "OK");
            }
        }

        private void buscarDirectorio(string sEspecialidad)
        {
            if (sEspecialidad.Trim() == null || sEspecialidad.Trim() == "")
            {

                lstEspe.ItemsSource = Task.Run(() => oModel.CargarDatos()).Result;
                throw new Exception();
            }

            lstEspe.ItemsSource = Task.Run(() => oModel.CargarDatos())
                .Result
                .Where(r => r.sNombre.Contains(sEspecialidad.ToUpper()));
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            specialtyDTO item = e.Item as specialtyDTO;


            await ShowLoading();
            Settings.iIdEspecialidad = item.iIdEspecialidad;
            Settings.sEspecialidad = item.sNombre;

#if __ANDROID__
            await this.Navigation.PushAsync(new MedicSpecialityView(ScreenshareIntent));
#else
            await this.Navigation.PushAsync(new MedicSpecialityView());
#endif

            await HideLoading();
        }


        async Task ShowLoading()
        {
            if (PopupNavigation.Instance.PopupStack.Count() == 0)
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

    }
}
