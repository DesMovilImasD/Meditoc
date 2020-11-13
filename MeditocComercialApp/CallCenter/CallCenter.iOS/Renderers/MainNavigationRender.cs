using System;
using CallCenter.iOS.Renderers;
using CallCenter.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

[assembly: ExportRenderer(typeof(MainNavigationPage), typeof(MainNavigationRender))]
namespace CallCenter.iOS.Renderers
{
    public class MainNavigationRender : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            UINavigationBar.Appearance.Translucent = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (Element is Xamarin.Forms.NavigationPage navigationPage)
            {
                if (navigationPage.OnThisPlatform().HideNavigationBarSeparator())
                {
                    if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                    {
                        NavigationBar.StandardAppearance.ShadowColor = null;
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}

