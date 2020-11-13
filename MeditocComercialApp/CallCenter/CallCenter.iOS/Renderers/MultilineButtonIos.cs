using System;
using CallCenter.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MultilineButton), typeof(CallCenter.iOS.Renderers.MultilineButtonIos))]
namespace CallCenter.iOS.Renderers
{
    public class MultilineButtonIos: ButtonRenderer
    {
        public MultilineButtonIos()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TitleEdgeInsets = new UIEdgeInsets(4, 4, 4, 4);
                Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                Control.TitleLabel.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}
