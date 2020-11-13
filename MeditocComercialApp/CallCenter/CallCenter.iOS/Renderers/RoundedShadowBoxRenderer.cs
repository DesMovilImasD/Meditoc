using System;
using System.Diagnostics;
using CallCenter.iOS.Renderers;
using CallCenter.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedShadowBoxView), typeof(RoundedShadowBoxRenderer))]
namespace CallCenter.iOS.Renderers
{
    
    public class RoundedShadowBoxRenderer: ViewRenderer
    {

        public RoundedShadowBoxRenderer() { }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (this.Element is null) return;
            this.Element.PropertyChanged += (sender, el) =>
            {
                try
                {
                    if(NativeView != null)
                    {
                        NativeView.SetNeedsDisplay();
                        NativeView.SetNeedsLayout();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Rounded Shadow Render warning: {ex.Message}");
                }
            };
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            this.LayoutIfNeeded();
            RoundedShadowBoxView view = (RoundedShadowBoxView)Element;

            //rcv.HasShadow = false;      
            view.Padding = new Thickness(0, 0, 0, 0);
            //this.BackgroundColor = rcv.FillColor.ToUIColor();      
            this.ClipsToBounds = true;
            this.Layer.BackgroundColor = view.FillColor.ToCGColor();
            this.Layer.MasksToBounds = true;
            this.Layer.CornerRadius = (nfloat)view.RoundedCornerRadius;
            if (view.MakeCircle)
            {
                this.Layer.CornerRadius = (int)(Math.Min(Element.Width, Element.Height) / 2);
            }
            this.Layer.BorderWidth = 0;
            if (view.BorderWidth > 0 && view.BorderColor.A > 0.0)
            {
                this.Layer.BorderWidth = view.BorderWidth;
                this.Layer.BorderColor = new UIColor(
                    (nfloat)view.BorderColor.R,
                    (nfloat)view.BorderColor.G,
                    (nfloat)view.BorderColor.B,
                    (nfloat)view.BorderColor.A).CGColor;
            }

        }
    }
}
