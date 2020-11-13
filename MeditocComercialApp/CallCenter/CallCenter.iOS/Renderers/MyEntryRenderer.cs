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

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace CallCenter.iOS.Renderers
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.BackgroundColor = UIColor.FromRGB(48, 99, 142);
              
                //Control.BorderStyle = UITextBorderStyle.Line;
            }
        }
    }
}