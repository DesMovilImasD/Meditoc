using System;
using CallCenter.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SurveyEntry), typeof(CallCenter.iOS.Renderers.SurveyEntry_ios))]
namespace CallCenter.iOS.Renderers
{
    
    public class SurveyEntry_ios: EntryRenderer
    {
        public SurveyEntry_ios()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
                Control.Layer.MasksToBounds = true;
                Control.TextColor = UIColor.DarkGray;
                Control.BackgroundColor = UIColor.White;

            }
        }
    }
}
