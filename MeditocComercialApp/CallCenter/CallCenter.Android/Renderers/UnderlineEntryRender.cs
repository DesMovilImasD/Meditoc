using System;
using Android.Content;
using Android.Views;
using CallCenter.Droid.Renderers;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(UnderlineEntry), typeof(UnderlineEntryRender))]
namespace CallCenter.Droid.Renderers
{
    public class UnderlineEntryRender : EntryRenderer
    {
        public UnderlineEntryRender(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null) return;
            //Control.TextAlignment = Android.Views.TextAlignment.Center;
            //this.Control.Gravity = GravityFlags.Center;
            //this.Control.SetForegroundGravity(GravityFlags.Center);
        }
        
    }
}

