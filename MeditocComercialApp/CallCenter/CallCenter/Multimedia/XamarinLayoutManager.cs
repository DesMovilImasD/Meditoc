using FM.IceLink;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CallCenter.Multimedia
{
    public class XamarinLayoutManager : LayoutManager<View>
    {
        public AbsoluteLayout Container { get; private set; }

        private bool InLayout = false;

        public XamarinLayoutManager(AbsoluteLayout container)
            : this(container, null)
        { }

        public XamarinLayoutManager(AbsoluteLayout container, LayoutPreset preset)
            : base(preset)
        {
            Container = container;
            Container.SizeChanged += (sender, e) =>
            {
                if (!InLayout)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem((state) =>
                    {
                        Device.BeginInvokeOnMainThread(Layout);
                    }, null);
                }
            };
            Container.LayoutChanged += (object sender, EventArgs e) =>
            {
                if (!InLayout)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem((state) =>
                    {
                        Device.BeginInvokeOnMainThread(Layout);
                    }, null);
                }
            };
        }

        protected override void AddView(View view)
        {
            Container.Children.Add(view);
        }

        protected override void DispatchToMainThread(Action2<object, object> action, object arg1, object arg2)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                action(arg1, arg2);
            });
        }

        public override void Layout()
        {
            InLayout = true;

            try
            {
                var localVideoControl = GetLocalView();
                var remoteVideoControls = GetRemoteViews();

                var layoutWidth = (int)Container.Width;
                var layoutHeight = (int)Container.Height;

                // Get the new layout.
                var layout = GetLayout(layoutWidth, layoutHeight, remoteVideoControls.Count);
               
                // Apply the local video frame.
                if (localVideoControl != null)
                {
                    var localFrame = layout.LocalFrame;
                    AbsoluteLayout.SetLayoutBounds(localVideoControl, new Xamarin.Forms.Rectangle(localFrame.X, localFrame.Y, localFrame.Width, localFrame.Height));
                    
                    if (Mode == LayoutMode.FloatLocal)
                    {
                        Container.RaiseChild(localVideoControl);
                       
                       
                    }
                }

                // Apply the remote video frames.
                var remoteFrames = layout.RemoteFrames;
                for (int i = 0; i < remoteFrames.Length; i++)
                {
                    var remoteFrame = remoteFrames[i];
                    var remoteVideoControl = (View)remoteVideoControls[i];
                    AbsoluteLayout.SetLayoutBounds(remoteVideoControl, new Xamarin.Forms.Rectangle(remoteFrame.X, remoteFrame.Y, remoteFrame.Width, remoteFrame.Height));

                    if (Mode == LayoutMode.FloatRemote)
                    {
                        Container.RaiseChild(remoteVideoControl);
                    }
                }

                Container.ForceLayout();
            }
            finally
            {
                InLayout = false;
            }
        }

       
        protected override void RemoveView(View view)
        {
            Container.Children.Remove(view);
        }
    }
}
