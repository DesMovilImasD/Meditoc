using System;
using CallCenter.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FolioEntry), typeof(CallCenter.iOS.Renderers.FolioEntry_ios))]
namespace CallCenter.iOS.Renderers
{
    public class FolioEntry_ios: EntryRenderer
    {
        public FolioEntry_ios() {}

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
                Control.Layer.MasksToBounds = true;
                Control.TextColor = UIColor.White;
                Control.BackgroundColor = UIColor.FromRGB(242, 101, 66);

            }
        }
    }
}
