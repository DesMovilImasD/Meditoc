using System;
using CallCenter.iOS.Renderers;
using CallCenter.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(UnderlinePicker), typeof(UnderlinePickerRenderer))]
namespace CallCenter.iOS.Renderers
{
    public class UnderlinePickerRenderer : PickerRenderer
    {
      

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null || e.NewElement != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
                Control.TextAlignment = UITextAlignment.Center;
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var startingPoint = new CGPoint(x: rect.GetMinX(), y: rect.GetMaxY()-10);
            var endingPoint = new CGPoint(x: rect.GetMaxX(), y: rect.GetMaxY()-10);

            CGContext context = UIGraphics.GetCurrentContext();
            context.SetLineWidth(1);
            UIColor.Clear.SetFill();
            UIColor.FromRGB(177, 177, 177).SetStroke();
            var currentPath = new CGPath();
            currentPath.AddLines(new CGPoint[] { startingPoint, endingPoint });
            context.AddPath(currentPath);
            context.DrawPath(CGPathDrawingMode.Stroke);
            context.SaveState();


        }

    }

    
   
}

