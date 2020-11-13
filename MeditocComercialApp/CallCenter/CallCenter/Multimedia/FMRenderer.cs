using Xamarin.Forms;

#if __IOS__
using Xamarin.Forms.Platform.iOS;
#else
using Xamarin.Forms.Platform.Android;
#endif

[assembly: ExportRenderer(typeof(CallCenter.Multimedia.FMView), typeof(CallCenter.Multimedia.FMViewRenderer))]
namespace CallCenter.Multimedia
{
#if __IOS__
    public class FMViewRenderer : ViewRenderer<FMView, UIKit.UIView>
#else
    public class FMViewRenderer : ViewRenderer<FMView, Android.Views.View>
#endif
    {
        protected override void OnElementChanged(ElementChangedEventArgs<FMView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                SetNativeControl(e.NewElement.NativeView);
            }
        }
    }

    public class FMView : View
    {
#if __IOS__
        public UIKit.UIView NativeView { get; private set; }

        public FMView(UIKit.UIView view)
        {
          
            this.NativeView = view;
        }
#else
        public Android.Views.View NativeView { get; private set; }

        public FMView(Android.Views.View view)
        {
            this.NativeView = view;
        }
#endif
    }
}

