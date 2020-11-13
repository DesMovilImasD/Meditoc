using System;
using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public class RoundedShadowBoxView: Grid
    {
        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create("FillColor", typeof(Color), typeof(RoundedShadowBoxView), Color.White);
        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public static readonly BindableProperty RoundedCornerRadiusProperty =
            BindableProperty.Create("RoundedCornerRadius", typeof(double), typeof(RoundedShadowBoxView), 3.0);
        public double RoundedCornerRadius
        {
            get { return (double)GetValue(RoundedCornerRadiusProperty); }
            set { SetValue(RoundedCornerRadiusProperty, value); }
        }

        public static readonly BindableProperty MakeCircleProperty =
            BindableProperty.Create("MakeCircle", typeof(Boolean), typeof(RoundedShadowBoxView), false);
        public Boolean MakeCircle
        {
            get { return (Boolean)GetValue(MakeCircleProperty); }
            set { SetValue(MakeCircleProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create("BorderColor", typeof(Color), typeof(RoundedShadowBoxView), Color.Transparent);
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create("BorderWidth", typeof(int), typeof(RoundedShadowBoxView), 1);
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }
    }
}
