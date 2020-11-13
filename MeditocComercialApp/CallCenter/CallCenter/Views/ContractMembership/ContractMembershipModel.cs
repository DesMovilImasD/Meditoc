using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CallCenter.Helpers;
using CallCenter.Helpers.FontAwesome;
using CallCenter.Renderers;
using CallCenter.Services;
using CallCenter.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using CallCenter.Views.ProductList;
using CallCenter.Views.Payment;

#if __ANDROID__
using Android.Content;
#endif

namespace CallCenter.Views.ContractMembership
{
    public class ContractMembershipModel : BaseViewModel
    {
#region -------- [properties] -------

        private readonly ObservableCollection<ContractMembershipDTO> source = new ObservableCollection<ContractMembershipDTO>();
        public ObservableCollection<ContractMembershipDTO> DataSource { get { return source; } }

        private InternetService oInternetService;
        private ContractMembershipView ViewContext { get; set; }

        private bool IsSubmit { get; set; } = false;

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

#region -------- [constructor] -------
#if __ANDROID__
        private Intent intent;
        public ContractMembershipModel(ContractMembershipView page, Intent _intent) : base(page)
        {
            intent = _intent;
#else
        public ContractMembershipModel(ContractMembershipView page): base(page)
        {
#endif
            ViewContext = page;
            oInternetService = new InternetService(page);
        }
#endregion

#region -------- [methods] -------
        private void BuildDataSource()
        {
            source.Add(new ContractMembershipDTO
            {
                Id = 1,
                Icon = FontAwesomeIcons.IdCard,
                Description = "Desde el primer día contará con servicio de orientación médica, nutricional y psicológica por 6 meses, tendrá respuesta inmediata y llamadas ilimitadas de orientación médica.",
                Cost = $"$800.00",
                Title = "6 MESES"
            });

            source.Add(new ContractMembershipDTO
            {
                Id = 2,
                Icon = FontAwesomeIcons.IdCard,
                Description = "Desde el primer día contará con servicio de orientación médica, nutricional y psicológica los 365 días del año, tendrá respuesta inmediata y llamadas ilimitadas de orientación médica.",
                Cost = $"$1,450.00",
                Title = "1 AÑO"
            });

            


        }

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
            source.Clear();
            HideAll();
            ICPFeeds Service = DependencyService.Get<ICPFeeds>();
            var response = await Service.GetMembership();

            if (response.Status)
            {
                foreach (var e in response.Items)
                {
                    var icon = e.Icon.Replace(@"\u", @"");
                    source.Add(item: new ContractMembershipDTO
                    {
                        Id = e.Id,
                        Icon = icon.ToUnicode(FontAwesomeIcons.IdCard), 
                        Title = e.Name.ToUpper(),
                        Description = e.Abstract,
                        Cost = e.Cost.ToString("$0.00"),
                        RealCost = e.Cost
                    });
                }
                ShowDataButton();
            }
            else
            {
                ShowDataError();
            }
        }

        /// <summary>
        /// Manejo de las contrataciones.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SubmitData()
        {
            if (source.Where(o => o.Selected == true).Count() == 0)
            {
                await ViewContext.DisplayAlert("Información", "Es necesario seleccionar por lo menos una membresía", "Aceptar");
                return false;
            }
            return true;
        }

        public void Selected(ContractMembershipDTO model)
        {

            var _model = source.Where(o => o.Id == model.Id).FirstOrDefault();
            if (_model is null) return;
            _model.Selected = !_model.Selected;
            _model.render();
            int index = source.IndexOf(_model);
            if (index < 0) return;

            source[index] = _model;

        }

#endregion

#region -------- [commands] --------

        public const string ReloadCommandName = "ReloadDataCommand";
        private Command _ReloadCommand;
        public Command ReloadDataCommand
        {
            get
            {
                return _ReloadCommand ??
                    (_ReloadCommand = new Command(async () => {
                        if (!await oInternetService.VerificaInternet())
                        {
                            IsRefreshing = false;
                            source.Clear();
                            HideAll();
                            ShowDataError();
                            return;
                        }
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

                        if (IsSubmit) { return; }
                        IsSubmit = true;

                        await PopupNavigation
                        .Instance
                        .PushAsync(new PopupLoad(message: "Espere un momento ..."));

                        if (!await oInternetService.VerificaInternet())
                        {
                            if (PopupNavigation.Instance.PopupStack.Count() > 0)
                            {
                                await PopupNavigation.Instance.PopAsync();
                            }
                            IsSubmit = false;
                            return;
                        }

                        bool status = await SubmitData();

                        if (PopupNavigation.Instance.PopupStack.Count() > 0)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }

                        if (status)
                        {
                            var items = source.Where(o => o.Selected == true).Select(o => new ProductItemDTO
                            {
                                Id = o.Id,
                                Cost = o.RealCost,
                                Name = o.Title,
                                Quantity = 1
                            }).ToList();

#if __ANDROID__
                            await page.Navigation.PushAsync(new PaymentView(intent, items));
#else
                            await page.Navigation.PushAsync(new PaymentView(items));
#endif

                        }
                        IsSubmit = false;
                    }));
            }
        }

#endregion
    }
}

