using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace CallCenter.iOS.Renderers
{
   public class CustomNavigationRenderer : NavigationRenderer
    {
        public override void SetViewControllers(UIViewController[] controllers, bool animated)
        {
            base.SetViewControllers(controllers, animated);

            foreach (var controller in controllers)
            {
                ((UINavigationController)controller).InteractivePopGestureRecognizer.Enabled = false;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (InteractivePopGestureRecognizer != null)
            {
                InteractivePopGestureRecognizer.Enabled = false;
            }
        }
    }
}