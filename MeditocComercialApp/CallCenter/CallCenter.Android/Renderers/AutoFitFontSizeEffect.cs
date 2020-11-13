using System;
using System.ComponentModel;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
//using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
//using Android.Widget;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidX.AppCompat.Text;

[assembly: ExportEffect(typeof(CallCenter.Droid.Renderers.AutoFitFontSizeEffect), nameof(AutoFitFontSizeEffect))]
namespace CallCenter.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class AutoFitFontSizeEffect: PlatformEffect
    {
        #region Protected Methods

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }

        protected override void OnAttached()
        {
            try
            {
                
                var textView = this.Control as TextView;
                if (textView == null) return;
                
                if (AutoFitFontSizeEffectParameters.GetMinFontSize(this.Element) == NamedSize.Default &&
                AutoFitFontSizeEffectParameters.GetMaxFontSize(this.Element) == NamedSize.Default)
                    return;

                var min = (int)AutoFitFontSizeEffectParameters.MinFontSizeNumeric(this.Element);
                var max = (int)AutoFitFontSizeEffectParameters.MaxFontSizeNumeric(this.Element);

                if (max <= min) return;
             

                
                TextViewCompat.SetAutoSizeTextTypeWithDefaults(textView, TextViewCompat.AutoSizeTextTypeNone);
                //textView.SetAutoSizeTextTypeUniformWithConfiguration(min, max, 1, (int)ComplexUnitType.Sp);
                //text.SetAutoSizeTextTypeUniformWithConfiguration(min, max, 1, (int)ComplexUnitType.Sp);
            }
            catch (Exception e)
            {

            }
            

            //if (this.Control is AppCompatTextView textView)
            //{
            //    if (AutoFitFontSizeEffectParameters.GetMinFontSize(this.Element) == NamedSize.Default &&
            //        AutoFitFontSizeEffectParameters.GetMaxFontSize(this.Element) == NamedSize.Default)
            //        return;

            //    var min = (int)AutoFitFontSizeEffectParameters.MinFontSizeNumeric(this.Element);
            //    var max = (int)AutoFitFontSizeEffectParameters.MaxFontSizeNumeric(this.Element);

            //    if (max <= min)
            //        return;

            //    //if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            //    //{
            //        textView.SetAutoSizeTextTypeUniformWithConfiguration(min, max, 1, (int)ComplexUnitType.Sp);
            //    //}
            //    //else
            //    //{

            //    //}
                
            //}
        }

        protected override void OnDetached()
        {
        }

        #endregion Protected Methods
    }
}
