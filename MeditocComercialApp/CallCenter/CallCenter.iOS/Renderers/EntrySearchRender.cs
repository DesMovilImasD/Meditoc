using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CallCenter.iOS.Renderers;
using CallCenter.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntrySearchBar), typeof(EntrySearchRender))]
namespace CallCenter.iOS.Renderers
{
    class EntrySearchRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGB(255, 255, 255);
            }
        }
    }
}