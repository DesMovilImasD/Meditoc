using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using CallCenter.Droid.Renderers;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntrySearchBar), typeof(EntrySearchBarRender))]
namespace CallCenter.Droid.Renderers
{
    public class EntrySearchBarRender:EntryRenderer
    {
        public EntrySearchBarRender(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //this.Control.Background = global::Android.Graphics.Color.Red;
                GradientDrawable gd = new GradientDrawable();
                //gd.SetColor(global::Android.Graphics.Color.Transparent);
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(30);
                //gd.SetStroke(2, Android.Graphics.Color.Rgb(48, 99, 142));
                Control.SetBackgroundDrawable(gd);
                //Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetTextColor(Android.Graphics.Color.Black);
                //Control.SetWidth(15);
                Control.SetMinimumWidth(100);
                Control.SetHintTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Black));


            }

        }

    }
}