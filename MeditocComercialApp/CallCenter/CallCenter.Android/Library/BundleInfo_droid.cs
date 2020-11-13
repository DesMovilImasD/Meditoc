using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CallCenter.Helpers;
using Xamarin.Essentials;
using XF = Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CallCenter.Droid.Library.BundleInfo_droid))]
namespace CallCenter.Droid.Library
{
    public class BundleInfo_droid : IAppInfo
    {

        WindowManagerFlags _originalFlags;

        /**
         * retorna la version actual de la app
         */
        public string GetVersion()
        {
            var context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        /**
         * retorna la version del build actual de la app
         */
        public int GetBuild()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }

        /**
         * retorna la uri de conexion a la app store
         */
        public string StoreUrl() => string.Format(
            "https://play.google.com/store/apps/details?id={0}&hl=en", GetBundleId());


        /**
         * retorna el bunde id de la aplicacion de android
         */
        public string GetBundleId()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.PackageName;
        }

        /**
         * busca en la tienda la version actual de la aplicacion y
         * la compara con la version actual y retorna un bandera que
         * indica si es necesario actualizar la aplicacion.
         */
        public Task<VersionResult> NeedUpdateApp()
        {
            return Task.Run(async () =>
            {
                try
                {
                    // si es appCurrentVersion 1, significa que aun no hay version en la tienda.
                    double appCurrentVersion = GetVersion().RemoveAllOcurrenceToDouble(".");
                    //if (appCurrentVersion == 1002)
                    //{
                    //    return VersionResult.Done(false);
                    //}
                    var uri = new Uri(StoreUrl());
                    using (var client = new HttpClient())
                    using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "text/html");
                        request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                        request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
                        using (var response = await client.SendAsync(request).ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();
                            var responseHTML = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var rx = new Regex(@"(?<=""htlgb"">)(\d{1,3}\.\d{1,3}\.\d{1,3}\.{0,1}\d{0,4})(?=<\/span>)", RegexOptions.Compiled);
                            MatchCollection matches = rx.Matches(responseHTML);

                            string storeVersion = matches.Count > 0 ? matches[0].Value : "Unknown";
                            if(storeVersion == "Unknown")return VersionResult.Done(false);
                            double appStoreVersion = storeVersion.RemoveAllOcurrenceToDouble(".");
                            //double appCurrentVersion = GetVersion().RemoveAllOcurrenceToDouble(".");

                            bool needUpdate = appCurrentVersion < appStoreVersion;

                            return VersionResult.Done(needUpdate);
                        }
                    }

                }
                catch (Exception e)
                {
                    var error = string.IsNullOrEmpty(e.Message) ?
                    "ha ocurrido un error inesperado" :
                        e.Message;
                    Console.WriteLine(error);
                    return VersionResult.Fail(error);
                }
            });

        }

        /**
         * finaliza la aplicacion android.
         */
        public void CloseApp()
        {
            Android.OS
                .Process
                .KillProcess(Android.OS.Process.MyPid());
        }

        /**
         * url para navegar a la tienda de android
         * para descargar la ultima version de la aplicacion.
         */
        public string GotoStore() => string.Format(
            "https://play.google.com/store/apps/details?id={0}", GetBundleId());

        /**
         * abre la configuracion de la aplicacion.
         */
        public void OpenAppSettings()
        {
            var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            string package_name = GetBundleId();// "my.android.package.name";
            var uri = Android.Net.Uri.FromParts("package", package_name, null);
            intent.SetData(uri);
            Application.Context.StartActivity(intent);
        }

        public void HideStatusBar()
        {
            var activity = (Activity)Platform.CurrentActivity;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        public void ShowStatusBar()
        {
            var activity = (Activity)Platform.CurrentActivity;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
            
        }

        public void SetDarkTheme()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                XF.Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.Visible;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Transparent);
                });
            }
        }

        public void SetLightTheme()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                XF.Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.White);
                });
            }
        }

        Window GetCurrentWindow()
        {
            var window = Platform.CurrentActivity.Window;

            // clear FLAG_TRANSLUCENT_STATUS flag:
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }
    }
}
