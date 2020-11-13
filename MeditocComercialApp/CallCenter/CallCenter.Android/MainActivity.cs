using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Android.Media.Projection;
using Android.Content;
using CallCenter.Renderers;
using System.Linq;
using CallCenter.Droid.Library;
using AndroidX.AppCompat.Widget;

namespace CallCenter.Droid
{
    [Activity(Label = "Meditoc 360",  Theme = "@style/MainTheme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity :  global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static MainActivity _context;
        public static bool IsMediaProjection = false;
        public static MainActivity Instance
        {
            get
            {
                if (_context == null)
                {
                    _context = new MainActivity();
                }

                return _context;
            }
        }

        /**
         * on create bundle
         */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                _context = this;

                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

                base.OnCreate(savedInstanceState);
                global::Xamarin.Forms.Forms.SetFlags("IndicatorView_Experimental");
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);                

                MediaProjectionManager manager = GetSystemService(MediaProjectionService).JavaCast<MediaProjectionManager>();
                Intent screenCaptureIntent = manager.CreateScreenCaptureIntent();

                //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
                Forms9Patch.Droid.Settings.Initialize(this);

                LoadApplication(new App(screenCaptureIntent));

                this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                
            }
            catch {

            }
        }


        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            Toolbar toolbar
              = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            AndroidHelper.contextActivity = this;
            AndroidHelper.CurrentApp = App.Current;
            AndroidHelper.ToolBarId = Resource.Id.toolbar;
            AndroidHelper.OnClickListener = new AndroidHelper.BackButtonClickListener();
            toolbar.SetNavigationOnClickListener(AndroidHelper.OnClickListener);
        }

        /**
         * solicitud de permisos
         */
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            //PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /**
         * verificacion del soporte de icelink android
         */
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 42 && FM.IceLink.Android.Utility.IsSDKVersionSupported(BuildVersionCodes.Lollipop))
            {
                if (data == null)
                {
                    Alert("Must allow screen sharing before the chat can start.");
                }
                else
                {
                    MediaProjectionManager manager = GetSystemService(MediaProjectionService).JavaCast<MediaProjectionManager>();
                    Multimedia.Context.Instance.MediaProjection = manager.GetMediaProjection((int)resultCode, data);
                    IsMediaProjection = true;
                }
            }
        }

        /**
         * 
         */
        public void Alert(String format, params object[] args)
        {
            string text = string.Format(format, args);
            Activity self = this;
            self.RunOnUiThread(() =>
            {
                if (!IsFinishing)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(self);
                    alert.SetMessage(text);
                    alert.SetPositiveButton("OK", (sender, arg) => { alert.Show(); });
                }
            });
        }

        /**
         * 
         */
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // check if the current item id 
            // is equals to the back button id
            if (item.ItemId == 16908332) // xam forms nav bar back button id
            {
                if(Xamarin.Forms.Application.
                    Current.
                    MainPage.
                    Navigation.
                    NavigationStack.
                    LastOrDefault().GetType() != typeof(ICoolContentPage))
                {
                    // if its not subscribed then go ahead 
                    // with the default back button action
                    return base.OnOptionsItemSelected(item);
                }

                // retrieve the current xamarin 
                // forms page instance
                var currentpage = (ICoolContentPage)Xamarin.Forms.Application.Current.
                     MainPage.Navigation.NavigationStack.LastOrDefault();

                // check if the page has subscribed to the custom back button event
                if (currentpage?.CustomBackButtonAction != null)
                {
                    // invoke the Custom back button action
                    currentpage?.CustomBackButtonAction.Invoke();
                    // and disable the default back button action
                    return false;
                }

                // if its not subscribed then go ahead 
                // with the default back button action
                return base.OnOptionsItemSelected(item);
            }
            else
            {
                // since its not the back button 
                //click, pass the event to the base
                return base.OnOptionsItemSelected(item);
            }
        }

        /**
         * 
         */
        public override void OnBackPressed()
        {
            // this is really not necessary, but in Android user has both Nav bar back button 
            // and physical back button, so its safe to cover the both events

            if(Xamarin.Forms.Application.Current.GetType() == typeof(ICoolContentPage))
            {
                var currentpage = (ICoolContentPage)Xamarin.Forms.Application.Current.
                MainPage.Navigation.NavigationStack.LastOrDefault();

                if (currentpage?.CustomBackButtonAction != null)
                {
                    currentpage?.CustomBackButtonAction.Invoke();
                    return;
                }
            }
            base.OnBackPressed();
        }
    }
}