using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public partial class MainNavigationPage : NavigationPage
    {
        public MainNavigationPage() : base()
        {
            InitializeComponent();
        }

        public MainNavigationPage(Page root): base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if(!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}
