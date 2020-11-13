using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DefaultEntry), typeof(CallCenter.Droid.Renderers.DefaultEntry))]
namespace CallCenter.Droid.Renderers
{
    public class DefaultEntry : EntryRenderer
    {
        public DefaultEntry(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //if (Control != null)
            //{
            //    GradientDrawable gd = new GradientDrawable();
            //    //gd.SetCornerRadius(20);
            //    //gd.SetColor(global::Android.Graphics.Color.Transparent);

            //    gd.SetColor(Android.Graphics.Color.Rgb(242, 101, 66));
            //    gd.SetCornerRadius(20);
            //    gd.SetStroke(2, Android.Graphics.Color.Rgb(242, 101, 66));

            //    this.Control.Background = gd; // .SetBackgroundDrawable(gd);
            //    this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            //    Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            //}
        }
    }
}