using System;
using Android.Content;
using CallCenter.Droid.Library;
using CallCenter.Droid.Renderers;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(MasterDetailNavigation), typeof(MasterDetailNavigationRenderer))]
namespace CallCenter.Droid.Renderers
{
    public class MasterDetailNavigationRenderer: NavigationPageRenderer
    {
        IPageController PageController => Element as IPageController;
        MainNavigationPage CustomNavigationPage => Element as MainNavigationPage;

        public MasterDetailNavigationRenderer(Context context)
            : base(context) { }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            //CustomNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            //CustomNavigationPage.IgnoreLayoutChange = false;

            //int containerHeight = b - t;

            //PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));
            //for (var i = 0; i < ChildCount; i++)
            //{
            //    AView child = GetChildAt(i);

            //    if (child is Android.Support.V7.Widget.Toolbar)
            //    {
            //        continue;
            //    }

            //    child.Layout(0, 0, r, b);
            //}

            if (AndroidHelper.contextActivity != null)
            {
                var that = (AndroidHelper.contextActivity as Xamarin.Forms.Platform.Android.FormsAppCompatActivity);
                var toolbar = that.FindViewById<Android.Support.V7.Widget.Toolbar>(AndroidHelper.ToolBarId);
                toolbar.SetNavigationOnClickListener(AndroidHelper.OnClickListener);
            }
        }
    }
}
