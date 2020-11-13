using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FM.IceLink;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CallCenter.iOS.Renderers.CustomNavigationRenderer))]
namespace CallCenter.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            Forms9Patch.iOS.Settings.Initialize(this);

            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(90, 184, 203);
            LoadApplication(new App());

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UIApplication.SharedApplication.IdleTimerDisabled = true;
            return base.FinishedLaunching(app, options);
        }


        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            ((AggregateException)e.Exception).Handle(ex =>
            {
                Log.Debug("AppDelegate | Xamarin Unobserved Task Exception" + ex);
                return true;
            });
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Debug("AppDelegate | Xamarin Unhandled Exception", ex);
        }
    }

}
