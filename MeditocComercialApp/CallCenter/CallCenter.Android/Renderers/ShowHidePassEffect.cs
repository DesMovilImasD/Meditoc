using System;
using Android.Content.Res;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(CallCenter.Droid.Renderers.ShowHidePassEffect), "ShowHidePassEffect")]
namespace CallCenter.Droid.Renderers
{
    public class ShowHidePassEffect: PlatformEffect
    {
        public ShowHidePassEffect()
        {
        }

        protected override void OnAttached()
        {
            ConfigureControl();
        }

        protected override void OnDetached()
        {
           
        }

        private void ConfigureControl()
        {
            EditText editText = ((EditText)Control);
            editText.SetCompoundDrawablesWithIntrinsicBounds(0, 0, getShowDrawable(), 0);
            

            if(editText.Gravity == GravityFlags.Center)
            {
                editText.CompoundDrawablePadding = -editText.CompoundPaddingRight;
            }

            editText.Touch += (s, e) =>
            {
                var handled = false;
                if (s is EditText && e.Event.Action == MotionEventActions.Up)
                {
                    EditText sEditText = (EditText)s;
                    if(e.Event.RawX >= (sEditText.Right - sEditText.GetCompoundDrawables()[2].Bounds.Width()))
                    {
                        if(sEditText.TransformationMethod == null)
                        {
                            editText.TransformationMethod = PasswordTransformationMethod.Instance;
                            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, getShowDrawable(), 0);
                        }
                        else
                        {
                            editText.TransformationMethod = null;
                            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, getHideDrawable(), 0);
                        }
                        handled = true;
                    }
                }

                e.Handled = handled;
            };

            
        }

        private int getShowDrawable()
        {
            try{
                return (int)typeof(Resource.Drawable).GetField(PassEffect.GetShow(Element)).GetValue(null);
            }
            catch(Exception e)
            {
                return Resource.Drawable.showPass;
            }
        }

        private int getHideDrawable()
        {
            try
            {
                return (int)typeof(Resource.Drawable).GetField(PassEffect.GetHide(Element)).GetValue(null);
            }
            catch (Exception e)
            {
                return Resource.Drawable.hidePass;
            }
        }

    }
    
}
