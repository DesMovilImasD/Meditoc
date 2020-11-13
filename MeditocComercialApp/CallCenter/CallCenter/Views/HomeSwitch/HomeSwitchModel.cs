using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CallCenter.ViewModels;
using Xamarin.Forms;
using CallCenter.Helpers.FontAwesome;
using CallCenter.Helpers;
using System.Threading.Tasks;

#if __ANDROID__
using Android.Content;
#endif

namespace CallCenter.Views.HomeSwitch
{
    public class HomeSwitchModel : BaseViewModel
    {
        readonly IList<MenuSwitchDTO> source;
        bool AllowNavigate { get; set; } = false;
        public ObservableCollection<MenuSwitchDTO> MenuItems { get; private set; }

#if __ANDROID__
        private Intent intent;
        public HomeSwitchModel(Page page, Intent _intent):base(page)
        {
            intent = _intent;
#else
        public HomeSwitchModel(Page page):base(page)
        {
#endif
            source = new List<MenuSwitchDTO>();
            BuildMenuSwitch();

        }

        Command verifyStoreCommand;
        public Command VerifyStoreCommand
        {
            get
            {
                return verifyStoreCommand ??
                    (verifyStoreCommand = new Command(async () =>
                    {
                        await VerifyStoreVersion();
                    }));
            }
        }

        /**
         * Verifica si existe una nueva version en la tienda disponible para
         * realizar la actualizacion de la aplicacion.
         * y cambia la bandera allowLogin para que se permita el login
         * en caso que sea necesario.
         */
        public async Task VerifyStoreVersion()
        {
            IAppInfo appInfo = DependencyService.Get<IAppInfo>();

            VersionResult result = await appInfo.NeedUpdateApp();

            if (result.isSuccess)
            {

                // si no es necesario actualizar permitimos el
                // login de manera tradicional
                // en caso contrario mostramos mensaje de error.
                if (!result.needUpdate)
                {
                    AllowNavigate = true;
                    return;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await page.DisplayAlert("Nueva versión disponible",
                        "Es necesario actualizar su aplicación para disfrutar de las nuevas características.",
                        "Actualizar");

                    await Xamarin.Essentials.Launcher.TryOpenAsync(new Uri(appInfo.GotoStore()));
                    appInfo.CloseApp();

                });
            }
            else
            {
                await page.DisplayAlert("Error", result.error, "Cerrar");
            }
        }


        public bool AllowNavigation()
        {
            return AllowNavigate;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildMenuSwitch()
        {
            
            source.Add(new MenuSwitchDTO
            {
                Icon = FontAwesomeIcons.User,
                Name = "ACCESO\nMIEMBROS",
                Navigate = "LOGIN"
            });

            source.Add(new MenuSwitchDTO
            {
                Icon = FontAwesomeIcons.ClinicMedical,
                Name = "ORIENTACIONES\nPAGO ÚNICO",
                Navigate = "ORIENTATION"
            });

            source.Add(new MenuSwitchDTO
            {
                Icon = FontAwesomeIcons.ShoppingCart,
                Name = "CONTRATAR\nMEMBRESIA",
                Navigate = "MEMBERSHIP"
            });

            source.Add(new MenuSwitchDTO
            {
                Icon = FontAwesomeIcons.PhoneAlt,
                Name = "EMERGENCIAS\n911",
                Navigate = "PHONE"
            });

            source.Add(new MenuSwitchDTO
            {
                Icon = FontAwesomeIcons.AddressBook,
                Name = "DIRECTORIO\nMÉDICO",
                Navigate = "DIRECTORY"
            });

            MenuItems = new ObservableCollection<MenuSwitchDTO>(source);
        }
    }
}

