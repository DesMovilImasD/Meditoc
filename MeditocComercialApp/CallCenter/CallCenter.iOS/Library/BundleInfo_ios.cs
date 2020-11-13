using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CallCenter.Helpers;
using Foundation;
using Newtonsoft.Json;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CallCenter.iOS.Library.BundleInfo_ios))]
namespace CallCenter.iOS.Library
{
    //definicion de alias.
    using DIC_OBJ = Dictionary<string, object>;
    using LIST_OBJ = List<object>;

    public class BundleInfo_ios: IAppInfo
    {

        /**
         * retorna la version actual de la app
         */
        public string GetVersion() => NSBundle.MainBundle
            .ObjectForInfoDictionary("CFBundleShortVersionString")
            .ToString();

        /**
         * retorna la version del build actual de la app
         */
        public int GetBuild() => int.Parse(NSBundle.MainBundle
            .ObjectForInfoDictionary("CFBundleVersion")
            .ToString());

        /**
         * retorna la uri de conexion a la app store
         */
        public string StoreUrl() => string.Format(
            "https://itunes.apple.com/lookup?bundleId={0}", GetBundleId());
        

        /**
         * retorna el bunde id de la aplicacion de ios
         */
        public string GetBundleId() => NSBundle.MainBundle
            .ObjectForInfoDictionary("CFBundleIdentifier")
            .ToString();



        /**
         * busca en la tienda la version actual de la aplicacion y
         * compara con la version actual y retorna si es necesario actualizar
         * la aplicacion.
         */
        public Task<VersionResult> NeedUpdateApp()
        {
            return Task.Run( () => {
                
                try
                {
                    // si es appCurrentVersion 1, significa que aun no hay version en la tienda.
                    string version = GetVersion();
                    double appCurrentVersion = version.RemoveAllOcurrenceToDouble(token: ".");
                    //if (appCurrentVersion == 10.4)
                    //{
                    //    return VersionResult.Done(false);
                    //}
                    using (var webClient = new System.Net.WebClient())
                    {
                        string url = StoreUrl();
                        string jsonString = webClient.DownloadString(url);
                        
                        var lookup = JsonConvert
                            .DeserializeObject<DIC_OBJ>(jsonString);


                        if (lookup != null
                            && lookup.Count >= 1
                            && lookup["resultCount"] != null
                            )
                        {
                            
                            if (Convert.ToInt32(lookup["resultCount"].ToString()) <= 0)
                            {
                                return VersionResult.Done(false);
                            }

                            var results = JsonConvert
                                .DeserializeObject<LIST_OBJ>(lookup[@"results"]
                                .ToString());


                            if (results != null && results.Count > 0)
                            {
                                var values = JsonConvert
                                    .DeserializeObject<DIC_OBJ>(results[0]
                                    .ToString());

                                string storeVersion = values.ContainsKey("version") ?
                                    values["version"].ToString() :
                                    string.Empty;

                                double appStoreVersion = storeVersion.RemoveAllOcurrenceToDouble(token: ".");
                                //double appCurrentVersion = GetVersion().RemoveFirstOcurrenceToDouble(token: ".");

                                bool needUpdate = appCurrentVersion < appStoreVersion;
                                return VersionResult.Done(needUpdate);
                            }
                        }
                    }
                    return VersionResult.Fail("No se ha podido procesar la respuesta del servidor.");
                }
                catch (Exception ex)
                {
                    var error = string.IsNullOrEmpty(ex.Message) ?
                        "ha ocurrido un error inesperado":
                        ex.Message;

                    Console.WriteLine(error);
                    return VersionResult.Fail(error);
                }

            });
          
        }

        /**
         * finaliza la aplicacion.
         */
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }

        /**
         * url para navegar a la tienda de ios
         * para descargar la ultima version de la aplicacion.
         */
        public string GotoStore() => string.Format(
            "https://apps.apple.com/app/meditoc-360/id{0}", "1521078394");

        /**
         * abre la configuracion de la aplicacion.
         */
        public void OpenAppSettings()
        {
            var url = new NSUrl($"app-settings:");
            UIApplication.SharedApplication.OpenUrl(url);
        }

        public void HideStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void ShowStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }

        public void SetDarkTheme()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
                GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
            });
        }

        public void SetLightTheme()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, false);
                GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
            });
        }

        UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;
            return vc;
        }
    }
}
