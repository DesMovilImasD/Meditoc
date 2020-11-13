using System;
using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public class ShowHidePassEffect: RoutingEffect
    {
        public string EntryText { get; set; }
        public Color TintColor { get; private set; }

        public ShowHidePassEffect() : base("Xamarin.ShowHidePassEffect") {
            

        }
    }


    public static class PassEffect
    {
        public static readonly BindableProperty HideProperty =
        BindableProperty.CreateAttached("Hide", typeof(string), typeof(PassEffect), "hidePass");

        public static readonly BindableProperty ShowProperty =
        BindableProperty.CreateAttached("Show", typeof(string), typeof(PassEffect), "showPass");


        public static string GetHide(BindableObject view)
        {
            return (string)view.GetValue(HideProperty);
        }

        public static void SetHide(BindableObject view, string value)
        {
            view.SetValue(HideProperty, value);
        }

        public static string GetShow(BindableObject view)
        {
            return (string)view.GetValue(ShowProperty);
        }

        public static void SetShow(BindableObject view, string value)
        {
            view.SetValue(ShowProperty, value);
        }

    }
}
