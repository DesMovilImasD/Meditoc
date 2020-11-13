using System;
using System.Linq;
using CallCenter.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(CallCenter.iOS.Renderers.ShowHidePassEffect), "ShowHidePassEffect")]
namespace CallCenter.iOS.Renderers
{
    public class ShowHidePassEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ConfigureControl();
        }

        protected override void OnDetached()
        {

        }

        private void ConfigureControl()
        {
            if (Control != null)
            {
                UITextField vUpdatedEntry = (UITextField)Control;
                var buttonRect = UIButton.FromType(UIButtonType.Custom);
                buttonRect.SetImage(new UIImage(PassEffect.GetShow(Element)), UIControlState.Normal);
                buttonRect.TouchUpInside += (object sender, EventArgs e1) =>
                {
                    if (vUpdatedEntry.SecureTextEntry)
                    {
                        vUpdatedEntry.SecureTextEntry = false;
                        buttonRect.SetImage(new UIImage(PassEffect.GetHide(Element)), UIControlState.Normal);
                    }
                    else
                    {
                        vUpdatedEntry.SecureTextEntry = true;
                        buttonRect.SetImage(new UIImage(PassEffect.GetShow(Element)), UIControlState.Normal);
                    }
                };

                vUpdatedEntry.ShouldChangeCharacters += (textField, range, replacementString) =>
                {
                    string text = vUpdatedEntry.Text;
                    var result = text.Substring(0, (int)range.Location) + replacementString + text.Substring((int)range.Location + (int)range.Length);
                    vUpdatedEntry.Text = result;
                    return false;
                };


                buttonRect.Frame = new CoreGraphics.CGRect(-10.0f, 0.0f, 30.0f, 30.0f);
                buttonRect.ContentMode = UIViewContentMode.Right;

                UIView paddingViewRight = new UIView(new System.Drawing.RectangleF(0.0f, 0.0f, 30.0f, 30.0f));
                paddingViewRight.Add(buttonRect);
                paddingViewRight.ContentMode = UIViewContentMode.BottomRight;


                vUpdatedEntry.RightView = paddingViewRight;
                vUpdatedEntry.RightViewMode = UITextFieldViewMode.WhileEditing;

                Control.Layer.CornerRadius = 4;
                Control.Layer.BorderColor = new CoreGraphics.CGColor(255, 255, 255);
                Control.Layer.MasksToBounds = true;
                // = UITextAlignment.Left;
            }

        }

    }
}
