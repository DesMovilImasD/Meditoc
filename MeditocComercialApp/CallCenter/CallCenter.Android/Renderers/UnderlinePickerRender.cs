using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Views;
using CallCenter.Droid.Renderers;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(UnderlinePicker), typeof(UnderlinePickerRender))]
namespace CallCenter.Droid.Renderers
{
    public class UnderlinePickerRender : PickerRenderer
    {
        public UnderlinePickerRender(Context context): base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control == null || e.NewElement == null) return;
            //Control.Gravity = GravityFlags.CenterHorizontal;
        }
    }
}

