#if __IOS__
#else
using FM.IceLink;
using FM.IceLink.Android;

namespace CallCenter.Multimedia
{
    public class AecContext : FM.IceLink.AecContext
    {
        protected override AecPipe CreateProcessor()
        {
            AudioConfig config = new AudioConfig(48000, 2);
            return new FM.IceLink.AudioProcessing.AecProcessor(config, AudioTrackSink.getBufferDelay(config) + AudioRecordSource.getBufferDelay(config));
        }

        protected override AudioSink CreateOutputMixerSink(AudioConfig config)
        {
            return new AudioTrackSink(config);
        }
    }
}
#endif