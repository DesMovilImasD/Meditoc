using System;
using CallCenter.iOS.Renderers;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

[assembly: ExportRenderer(typeof(MasterDetailNavigation), typeof(MasterDetailNavigationRenderer))]
namespace CallCenter.iOS.Renderers
{
    public class MasterDetailNavigationRenderer: NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
