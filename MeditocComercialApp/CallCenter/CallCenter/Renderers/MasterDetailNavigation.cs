using System;

using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public class MasterDetailNavigation : NavigationPage
    {
        public MasterDetailNavigation():base()
        {
            
        }

        public MasterDetailNavigation(Page root) : base(root)
        {

        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}

