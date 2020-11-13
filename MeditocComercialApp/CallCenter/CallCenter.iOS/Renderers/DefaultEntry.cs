using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CallCenter.iOS.Renderers.DefaultEntry))]
namespace CallCenter.iOS.Renderers
{
    public class DefaultEntry : EntryRenderer
    {
        public DefaultEntry() { }

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