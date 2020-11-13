using FM.IceLink;
using Matroska = FM.IceLink.Matroska;
using Opus = FM.IceLink.Opus;
using Pcma = FM.IceLink.Pcma;
using Pcmu = FM.IceLink.Pcmu;
using Vp8 = FM.IceLink.Vp8;
using Vp9 = FM.IceLink.Vp9;
using Yuv = FM.IceLink.Yuv;
using System;
using System.IO;

#if __IOS__
using FM.IceLink.Cocoa;
#else
using Android.Widget;
#endif

namespace CallCenter.Multimedia
{
#if __IOS__
    public class RemoteMedia : RtcRemoteMedia<OpenGLView>
#else
    public class RemoteMedia : RtcRemoteMedia<FrameLayout>
#endif
    {
#if !__IOS__
        private Android.Content.Context context;
#endif

#if __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteMedia"/> class.
        /// </summary>
        /// <param name="disableAudio">if set to <c>true</c> [disable audio].</param>
        /// <param name="disableVideo">if set to <c>true</c> [disable video].</param>
        /// <param name="aecContext">The aec context.</param>
        public RemoteMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(disableAudio, disableVideo, aecContext)
        {
            Initialize();
        }
#else
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteMedia"/> class.
        /// </summary>
        /// <param name="disableAudio">if set to <c>true</c> [disable audio].</param>
        /// <param name="disableVideo">if set to <c>true</c> [disable video].</param>
        /// <param name="aecContext">The aec context.</param>
        public RemoteMedia(Android.Content.Context context, bool disableAudio, bool disableVideo, AecContext aecContext)
            : base(disableAudio, disableVideo, aecContext)
        {
            this.context = context;
            Initialize();
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
            string filename = Path.Combine(path, Id + "-remote-audio-" + inputFormat.Name.ToLower() + ".mkv");
            return new Matroska.AudioSink(filename);
        }

        /// <summary>
        /// Creates an audio sink.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        protected override AudioSink CreateAudioSink(AudioConfig config)
        {
#if __IOS__
            return new AudioUnitSink(config);
#else
            return new FM.IceLink.Android.AudioTrackSink(config);
#endif
        }

        /// <summary>
        /// Creates a pcma decoder.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        protected override AudioDecoder CreatePcmaDecoder(AudioConfig config)
        {
            return new Pcma.Decoder(config);
        }

        /// <summary>
        /// Creates a pcmu decoder.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        protected override AudioDecoder CreatePcmuDecoder(AudioConfig config)
        {
            return new Pcmu.Decoder(config);
        }

        /// <summary>
        /// Creates an H.264 encoder.
        /// </summary>
        /// <returns></returns>
        protected override VideoDecoder CreateH264Decoder()
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
        protected override AudioDecoder CreateOpusDecoder(AudioConfig config)
        {
            return new Opus.Decoder(config);
        }

        /// <summary>
        /// Creates a video recorder.
        /// </summary>
        /// <param name="inputFormat">The output format.</param>
        /// <returns></returns>
        protected override VideoSink CreateVideoRecorder(VideoFormat inputFormat)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, Id + "-remote-video-" + inputFormat.Name.ToLower() + ".mkv");
            return new Matroska.VideoSink(filename);
        }

#if __IOS__
        /// <summary>
        /// Creates a view sink.
        /// </summary>
        protected override ViewSink<OpenGLView> CreateViewSink()
        {
            return new OpenGLSink();
        }
#else
        /// <summary>
        /// Creates a view sink.
        /// </summary>
        protected override ViewSink<FrameLayout> CreateViewSink()
        {
            return new FM.IceLink.Android.OpenGLSink(context);
        }
#endif

        /// <summary>
        /// Creates a VP8 encoder.
        /// </summary>
        /// <returns></returns>
        protected override VideoDecoder CreateVp8Decoder()
        {
            return new Vp8.Decoder();
        }

        /// <summary>
        /// Creates a VP9 encoder.
        /// </summary>
        /// <returns></returns>
        protected override VideoDecoder CreateVp9Decoder()
        {
            //Experimental
            return null; //new Vp9.Decoder();
        }
    }
}
