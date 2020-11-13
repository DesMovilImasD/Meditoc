using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CallCenter.Helpers;
using CallCenter.Models;
using CallCenter.Renderers;
using CallCenter.Services;
using CallCenter.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace CallCenter.ViewModels
{
    public class COVIDSurveyViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region -------- [Instance] ---------
        public static COVIDSurveyViewModel Create(vwCOVIDSurvey context) => new COVIDSurveyViewModel(context);
        #endregion

        #region -------- [Properties] --------

        private InternetService oInternetService;
        private vwCOVIDSurvey viewContext { get; set; }
        private double latitude { get; set; } = 0;
        private double longitude { get; set; } = 0;
        private string errorLocation { get; set; } = null;

        private ObservableCollection<SurveyAsk> _DataSource { get; set; } = new ObservableCollection<SurveyAsk>();
        public ObservableCollection<SurveyAsk> DataSource { get { return _DataSource; } }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private bool _isEmptyData = false;
        public bool IsEmptyData
        {
            get { return _isEmptyData; }
            set
            {
                _isEmptyData = value;
                OnPropertyChanged(nameof(IsEmptyData));
            }
        }

        private bool _isLoadedData = false;
        public bool IsLoadedData
        {
            get { return _isLoadedData; }
            set
            {
                _isLoadedData = value;
                OnPropertyChanged(nameof(IsLoadedData));
            }
        }

        #endregion

        #region -------- [Constructor] --------

        public COVIDSurveyViewModel(vwCOVIDSurvey context)
        {
            viewContext = context;
            oInternetService = new InternetService(context);
        }

        #endregion

        #region -------- [Methods] --------

        public void HideAll()
        {
            IsLoadedData = false;
            IsEmptyData = false;
        }

        public void ShowDataButton()
        {
            IsLoadedData = true;
            IsEmptyData = false;
        }

        public void ShowDataError()
        {
            IsLoadedData = false;
            IsEmptyData = true;
        }

        public async Task ReloadData()
        {
            _DataSource.Clear();
            HideAll();
            ICPFeeds Service = DependencyService.Get<ICPFeeds>();
            var response = await Service.m_GetCOVIDSurvey();
            if(response.Count() > 0)
            {
                foreach (SurveyAsk item in response)
                {
                    item.TypeField = TYPE_FIELD.CHECKBOX;
                    _DataSource.Add(item: item);
                }

                String phoneValue = "";
                if (!string.IsNullOrEmpty(Settings.sUserNameLogin))
                {
                    List<string> split_login = Settings.sUserNameLogin
                        .Split("_")
                        .ToList();

                    if (split_login.Count() > 1)
                    {
                        phoneValue = split_login[1];
                    }

                }

                _DataSource.Add(SurveyAsk.BuildTextField("telefono", phoneValue, "Teléfono", true));
                _DataSource.Add(SurveyAsk.BuildTextField("cp","", "Código postal", true));

                ShowDataButton();
                return;
            }
            ShowDataError();
        }

        public async Task<bool> SubmitData()
        {
            ICPFeeds Service = DependencyService.Get<ICPFeeds>();
            var Items = _DataSource.ToList();
            var response = await Service.m_GetCOVIDType(
                asks:Items, // respuestas del formulario
                latitude: latitude, //latitud
                longitude: longitude,//longitud
                error: errorLocation // error al no obtener la ubicacion.
                ); 

            if (response.Status)
            {
                await vwPopupCOVIDSurvey
                    .Show(PopupNavigation.Instance,
                    response.Items,
                    response.Folio);

                    // hay folio hay que llevarlo a la vista de llamada.
                    return true;
            }
            await viewContext.DisplayAlert("Información", response.Msg, "Aceptar");
            return false;
        }

        public void Selected(SurveyAsk model)
        {

            var _model = _DataSource.Where(o => o.Code == model.Code).FirstOrDefault();
            if (_model is null) return;
            if (_model.TypeField == TYPE_FIELD.TEXTFIELD) return;
            _model.Selected = !_model.Selected;

            int index = _DataSource.IndexOf(_model);
            if (index < 0) return;

            _DataSource[index] = _model;
            //_DataSource.Insert(index, _model);

        }

        private async Task GetLocation()
        {
            latitude = 0;
            longitude = 0;
            errorLocation = null;

            bool permisionGranted = await PermissionValidator.LazyCheckLocationPermissions();
            if (permisionGranted)
            {
                LocationResult result = await LocationManager
                .Shared()
                .FindLocation(cached:false);

                if (result.IsSuccess && result.Position != null)
                {
                    latitude = result.Position.Latitude;
                    longitude = result.Position.Longitude;
                    return;
                }
                errorLocation = result.Msg;
                return;
            }
            errorLocation = "Permisos de ubicación no otorgados";
        }

        private async Task<bool>  HandleRequiredProperties()
        {
            var items = DataSource
                .Where(o => o.Required == true)
                .ToList();

            foreach (SurveyAsk item in items)
            {
                if (item.TypeField == TYPE_FIELD.TEXTFIELD)
                {
                    switch (item.Code)
                    {
                        case "cp":
                            if(string.IsNullOrEmpty(item.Ask) || item.Ask.Count() != 5)
                            {
                                await viewContext.DisplayAlert("Información",
                                    "El código postal tiene que tener una logitud de 5 dígitos",
                                    "Aceptar");
                                return false;
                            }
                            
                            break;
                        case "telefono":
                            if (string.IsNullOrEmpty(item.Ask) || item.Ask.Count() != 10)
                            {
                                await viewContext.DisplayAlert("Información",
                                    "El télefono tiene que tener una logitud de 10 dígitos",
                                    "Aceptar");
                                return false;
                            }
                            break;
                        default: break;
                    }
                }
            }
            return true;
        }

        #endregion

        #region -------- [Commands] --------


        public const string ReloadCommandName = "ReloadDataCommand";
        private Command _ReloadCommand;
        public Command ReloadDataCommand
        {
            get
            {
                return _ReloadCommand ??
                    (_ReloadCommand = new Command(async () => {
                        IsRefreshing = true;
                        await ReloadData();
                        IsRefreshing = false;
                    }));
            }
        }

        public const string SubmitCommandName = "SubmitDataCommand";
        private Command _SubmitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _SubmitCommand ??
                    (_SubmitCommand = new Command(async () => {
                        if (!await oInternetService.VerificaInternet())
                            return;

                        if (!await HandleRequiredProperties())
                            return;

                        await PopupNavigation
                        .Instance
                        .PushAsync(new PopupLoad( message: "Espere un momento ..."));

                        bool status = await SubmitData();

                        if (PopupNavigation.Instance.PopupStack.Count() > 0)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }

                        if(status)
                            await viewContext.Navigation.PopAsync();
                        
                    }));
            }
        }

        public const string LocationCommandName = "LocationCommand";
        private Command _LocationCommand;
        public Command LocationCommand
        {
            get
            {
                return _LocationCommand ??
                    (_LocationCommand = new Command(async () => {
                        await GetLocation();
                }));
            }
        }

        #endregion

        #region -------- [INotify PropertyChange] --------

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
