using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#if __ANDROID__
using Android.Content;
#endif
using CallCenter.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using P42.Utils;
using Xamarin.Forms.OpenWhatsApp;
using Rg.Plugins.Popup.Services;
using System.Linq;
using CallCenter.Helpers.FontAwesome;

namespace CallCenter.Views.MedicDirectory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicSpecialityView : ContentPage
    {        
        private string _sNombreEspecialidad;
        private medicSpecialityModel oModel;

#if __ANDROID__
        private Intent ScreenshareIntent { get; set; }
#endif


        public string sNombreEspecialidad
        {
            get { return _sNombreEspecialidad; }
            set
            {
                _sNombreEspecialidad = value;
                OnPropertyChanged();
            }
        }
        public List<EntDirectorio> lstDoctoresEspecialidad { get; set; }
                
#if __ANDROID__


        public MedicSpecialityView(Intent screenshareIntent)
        {
            ScreenshareIntent = screenshareIntent;
            oModel = new medicSpecialityModel(this, screenshareIntent);
# else
            public MedicSpecialityView()
            {
                oModel = new medicSpecialityModel(this);
#endif

            InitializeComponent();

            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromHex("#12B6CB");
            NavigationPage.SetBackButtonTitle(this, "");

            relativePrincipal.Children.Add(
   txtNombreEspecialidad,
   Constraint.RelativeToParent((parent) =>
   {
       return (relativePrincipal.Width / 3) + txtNombreEspecialidad.WidthRequest / 2;
   }));


            lstDoctoresEspecialidad = Task.Run(() => oModel.CargarDatosByEspecialidad(Settings.iIdEspecialidad)).Result; ;
            lstDoctoresView.ItemsSource = lstDoctoresEspecialidad;

            iconSearch.Text = FontAwesomeIcons.Search;
            txtNombreEspecialidad.Text = Settings.sEspecialidad.ToUpper();

            txtBuscarEspecialidad.Completed += txtBuscarDoctor_Search;

            if (lstDoctoresEspecialidad.Count == 0)
            {                
                DisplayAlert("Información", "La especialidad no cuenta con médicos registrados", "OK");
            }
            

        }

        private void openLocation(object sender, EventArgs args)
        {
            try
            {
                var urlMaps = args.GetPropertyValue("Parameter");

                if (urlMaps != null)
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Launcher.OpenAsync(urlMaps.ToString());
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        Launcher.OpenAsync(urlMaps.ToString());
                    }
                    else if (Device.RuntimePlatform == Device.UWP)
                    {
                        Launcher.OpenAsync(urlMaps.ToString());
                    }
                }
                else
                {
                    DisplayAlert("Información", "No se cuenta con información registrada de la ubicación", "OK");
                }
            }
            catch (UriFormatException e)
            {
                DisplayAlert("Información", "Formato de url incorrecto", "OK");
                //Settings.sError = "Formato de url incorrecto";

            }
            catch (Exception e)
            {
                //Settings.sError = "";
                DisplayAlert("Información", "Ocurrio un error", "OK");
            }
        }


        private void openPhone(object sender, EventArgs args)
        {
            try
            {
                var cellPhoneNumber = args.GetPropertyValue("Parameter");

                if (cellPhoneNumber != null)
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        PhoneDialer.Open(cellPhoneNumber.ToString());
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        PhoneDialer.Open(cellPhoneNumber.ToString());
                    }
                    else if (Device.RuntimePlatform == Device.UWP)
                    {
                        PhoneDialer.Open(cellPhoneNumber.ToString());
                    }

                }
                else
                {
                    DisplayAlert("Información", "No se cuenta con un número valido", "OK");
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                DisplayAlert("Información", "No se cuenta con un número valido", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Información", "Ocurrio un error", "OK");
            }
        }

        private void openWhatsApp(object sender, EventArgs args)
        {
            try
            {
                var cellPhoneNumber = args.GetPropertyValue("Parameter");

                if (cellPhoneNumber.ToString().Replace(" ", "").Length == 10)
                {
                    Chat.Open("+52" + cellPhoneNumber.ToString());

                }
                else
                {
                    DisplayAlert("Información", "El número proporcionado no es valido o no se cuenta con información del médico", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Información", "Ocurrio un error", "OK");
            }
        }

        private void searchDoctorEspecialidad(object sender, EventArgs args)
        {
            try
            {
                string sBuscador = txtBuscarEspecialidad.Text;

                BuscarDoctoresEspecialidad(sBuscador);
            }
            catch (Exception ex)
            {
                DisplayAlert("Información", "Ocurrio un error", "OK");
            }
        }

        private void txtBuscarDoctor_Search(object sender, EventArgs args)
        {
            try
            {
                string sBuscador = txtBuscarEspecialidad.Text;

                BuscarDoctoresEspecialidad(sBuscador);
            }
            catch (Exception ex)
            {
                DisplayAlert("Información", "Ocurrio un error", "OK");
            }
        }

        private void BuscarDoctoresEspecialidad(String sBuscador)
        {
            try
            {
                if (sBuscador.Trim() == null || sBuscador.Trim() == "")
                {

                    lstDoctoresView.ItemsSource = Task.Run(() => oModel.CargarDatosByEspecialidad(Settings.iIdEspecialidad)).Result;

                    throw new Exception();
                }

                lstDoctoresView.ItemsSource = Task.Run(() => oModel.CargarDatosByEspecialidad(Settings.iIdEspecialidad, sBuscador))
                    .Result;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
