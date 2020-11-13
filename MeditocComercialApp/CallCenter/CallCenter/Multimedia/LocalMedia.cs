using FM.IceLink;
using Matroska = FM.IceLink.Matroska;
using Opus = FM.IceLink.Opus;
using Vp8 = FM.IceLink.Vp8;
using Vp9 = FM.IceLink.Vp9;
using Yuv = FM.IceLink.Yuv;
using System;
using System.IO;

#if __IOS__
using AVFoundation;
using FM.IceLink.Cocoa;
using UIKit;
#else
using Android.Views;
using Android.Widget;
using Android.Media.Projection;
using FM.IceLink.Android;
#endif

namespace CallCenter.Multimedia
{
#if __IOS__
    public class LocalCameraMedia : LocalMedia<OpenGLView>
#else
    public class LocalCameraMedia : LocalMedia<FrameLayout>
#endif
    {
        private VideoConfig _CameraConfig = new VideoConfig(320, 250, 15);

#if __IOS__
        private AVCapturePreview _preview;
#else
        private FM.IceLink.Android.CameraPreview _preview;
#endif

#if __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalCameraMedia"/> class.
        /// </summary>
        /// <param name="disableAudio">Whether to disable audio.</param>
        /// <param name="disableVideo">Whether to disable video.</param>
        /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
        public LocalCameraMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(disableAudio, disableVideo, aecContext)
        {
            _preview = new AVCapturePreview();
            Initialize();
        }
#else

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalCameraMedia"/> class.
        /// </summary>
        /// <param name="context">The Android context.</param>
        /// <param name="disableAudio">Whether to disable audio.</param>
        /// <param name="disableVideo">Whether to disable video.</param>
        /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
        public LocalCameraMedia(Android.Content.Context context, bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(context, disableAudio, disableVideo, aecContext)
        {
            _preview = new FM.IceLink.Android.CameraPreview(context, LayoutScale.Contain);
            Initialize();
        }
#endif

        /// <summary>
        /// Creates a video source.
        /// </summary>
        protected override VideoSource CreateVideoSource()
        {
#if __IOS__
            return new AVCaptureSource(_preview, _CameraConfig);
#else
            return new FM.IceLink.Android.CameraSource(_preview, _CameraConfig);
#endif
        }

#if __IOS__
        /// <summary>
        /// Creates a view sink.
        /// </summary>
        /// <returns></returns>
        protected override ViewSink<OpenGLView> CreateViewSink()
        {
            return null;
        }

        public UIKit.UIView GetView()
        {
            return _preview;
        }
#else
        /// <summary>
        /// Creates a view sink.
        /// </summary>
        /// <returns></returns>
        protected override ViewSink<FrameLayout> CreateViewSink()
        {
            return null;
        }

        public View GetView()
        {
            return _preview.View;
        }
#endif
    }

#if __IOS__
    public class LocalScreenMedia : LocalMedia<UIImageView>
#else
    public class LocalScreenMedia : LocalMedia<ImageView>
#endif
    {
#if __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalScreenMedia"/> class.
        /// </summary>
        /// <param name="disableAudio">Whether to disable audio.</param>
        /// <param name="disableVideo">Whether to disable video.</param>
        /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
        public LocalScreenMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(disableAudio, disableVideo, aecContext)
        {
            Initialize();
        }
#else
        private MediaProjectionSource projectionSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalScreenMedia"/> class.
        /// </summary>
        /// <param name="context">The Android context.</param>
        /// <param name="disableAudio">Whether to disable audio.</param>
        /// <param name="disableVideo">Whether to disable video.</param>
        /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
        public LocalScreenMedia(MediaProjection projection, Android.Content.Context context, bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(context, disableAudio, disableVideo, aecContext)
        {
            projectionSource = new MediaProjectionSource(projection, context, 3);
            Initialize();
        }
#endif

        /// <summary>
        /// Creates a video source.
        /// </summary>
        /// <returns></returns>
        protected override VideoSource CreateVideoSource()
        {
#if __IOS__
            return new ScreenSource(3);
#else
            return projectionSource;
#endif
        }

#if __IOS__
        /// <summary>
        /// Creates a view sink.
        /// </summary>
        protected override ViewSink<UIImageView> CreateViewSink()
        {
            return new ImageViewSink();
        }
#else
        /// <summary>
        /// Creates a view sink.
        /// </summary>
        protected override ViewSink<ImageView> CreateViewSink()
        {
            return new FM.IceLink.Android.ImageViewSink(context);
        }
#endif
    }

#if __IOS__
    public abstract class LocalMedia<TView> : RtcLocalMedia<TView>
#else
    public abstract class LocalMedia<TView> : RtcLocalMedia<TView>
#endif
    {
#if !__IOS__
        protected Android.Content.Context context;
#endif

#if __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalMedia"/> class.
        /// </summary>
        /// <param name="disableAudio">Whether to disable audio.</param>
        /// <param name="disableVideo">Whether to disable video.</param>
        /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
        public LocalMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(disableAudio, disableVideo, aecContext)
        {
            AVAudioSession.SharedInstance().SetCategory(AVAudioSessionCategory.PlayAndRecord, AVAudioSessionCategoryOptions.AllowBluetooth | AVAudioSessionCategoryOptions.DefaultToSpeaker);
        }
#else
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalMedia"/> class.
        /// </summary>
        /// <param name="context">The Android context.</param>
        /// <param name="disableAudio">Whether to disable audio.</param>
        /// <param name="disableVideo">Whether to disable video.</param>
        /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
        public LocalMedia(Android.Content.Context context, bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(disableAudio, disableVideo, aecContext)
        {
            this.context = context;
        }
#endif

        /// <summary>
        /// Creates an audio recorder.
        /// </summary>
        /// <param name="inputFormat">The input format.</param>
        /// <returns></returns>
        protected override AudioSink CreateAudioRecorder(AudioFormat inputFormat)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, Id + "-local-audio-" + inputFormat.Name.ToLower() + ".mkv");
            return new Matroska.AudioSink(filename);
        }

        /// <summary>
        /// Creates an audio source.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        protected override AudioSource CreateAudioSource(AudioConfig config)
        {
#if __IOS__
            return new AudioUnitSource(config);
#else
            return new FM.IceLink.Android.AudioRecordSource(context, config);
#endif
        }

        /// <summary>
        /// Creates an H.264 encoder.
        /// </summary>
        /// <returns></returns>
        protected override VideoEncoder CreateH264Encoder()
        {
            return null;
        }

        /// <summary>
        /// Creates an image converter.
        /// </summary>
        /// <param name="outputFormat">The output format.</param>
        /// <returns></returns>
        protected override VideoPipe CreateImageConverter(VideoFormat outputFormat)
        {
            return new Yuv.ImageConverter(outputFormat);
        }

        /// <summary>
        /// Creates an Opus encoder.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        protected override AudioEncoder CreateOpusEncoder(AudioConfig config)
        {
            return new Opus.Encoder(config);
        }

        /// <summary>
        /// Creates a video recorder.
        /// </summary>
        /// <param name="inputFormat">The output format.</param>
        /// <returns></returns>
        protected override VideoSink CreateVideoRecorder(VideoFormat inputFormat)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, Id + "-local-video-" + inputFormat.Name.ToLower() + ".mkv");
            return new Matroska.VideoSink(filename);
        }

        /// <summary>
        /// Creates a VP8 encoder.
        /// </summary>
        /// <returns></returns>
        protected override VideoEncoder CreateVp8Encoder()
        {
            return new Vp8.Encoder();
        }

        /// <summary>
        /// Creates a VP9 encoder.
        /// </summary>
        /// <returns></returns>
        protected override VideoEncoder CreateVp9Encoder()
        {
            return new Vp9.Encoder();
        }
    }
}
