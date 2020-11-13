using System;
using Android.Content;
using Android.Graphics;
using CallCenter.Droid.Renderers;
using CallCenter.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedShadowBoxView), typeof(RoundedShadowBoxRenderer))]
namespace CallCenter.Droid.Renderers
{
    public class RoundedShadowBoxRenderer: ViewRenderer
    {
        public RoundedShadowBoxRenderer(Context context):base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            //if (element.HasShadow)
            //{
            Elevation = 100;
            TranslationZ = 50;
            SetZ(150);
            //}
        }

        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {

            if (Element is null) { return false; }
            RoundedShadowBoxView view = (RoundedShadowBoxView)Element;
            this.SetClipChildren(true);
            view.Padding = new Thickness(0, 0, 0, 0);
            int radius = (int)(view.RoundedCornerRadius);
            if (view.MakeCircle)
            {
                radius = Math.Min(Width, Height) / 2;
            }
            radius *= 2;

            try
            {
                var path = new Path();
                path.AddRoundRect(new RectF(0, 0, Width, Height), new float[] {
                    radius,
                    radius,
                    radius,
                    radius,
                    radius,
                    radius,
                    radius,
                    radius
                }, Path.Direction.Ccw);
                canvas.ClipPath(path);
                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                if (view.BorderWidth > 0)
                {
                    // Draw a filled circle.      
                    var paint = new Paint();
                    paint.AntiAlias = true;
                    paint.StrokeWidth = view.BorderWidth;
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = view.BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }
                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}
