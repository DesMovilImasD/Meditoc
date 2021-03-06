﻿using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SurveyEntry), typeof(CallCenter.Droid.Renderers.SurveyEntry_droid))]
namespace CallCenter.Droid.Renderers
{
    public class SurveyEntry_droid: EntryRenderer
    {
        public SurveyEntry_droid(Context context):base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                //gd.SetCornerRadius(20);
                //gd.SetColor(global::Android.Graphics.Color.Transparent);

                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(20);
                gd.SetStroke(2, Android.Graphics.Color.White);

                this.Control.Background = gd; // .SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.DarkGray));
            }
        }
    }
}
