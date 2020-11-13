using FM.IceLink;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FM.IceLink.WebSync4;
using CallCenter.Helpers;
using CallCenter.Models;
using Newtonsoft.Json;


#if __IOS__
using CoreFoundation;
using FM.IceLink.Cocoa.Helpers;
#else
using Android.Views;
using Android.Media.Projection;
#endif


namespace CallCenter.Multimedia
{
    public class Context
    {
        // This flag determines the signalling mode used.
        // Note that Manual and Auto signalling do not Interop.
        private static bool SIGNAL_MANUALLY = false;
        private Signalling _signalling;

        public event EventHandler<MessageReceivedArgs> MessageReceived;
        public event Action1<string> PeerJoined;
        public event Action1<string> PeerLeft;
        private static Future<DtlsCertificate> _certificatePromise;
        private static DtlsCertificate _certificate;

        private string _webSyncServerUrl = "https://v4.websync.fm/websync.ashx"; // WebSync On-Demand

        /// <summary>
        /// Notas: segun la documentacion recomienda utilizar los puertos 3478
        /// y 80,con trasnport udp, ya que esta configuraciion hace que el flujo de
        /// video no se sea transmitido de nuevo en caso de perdida.
        /// </summary>
        private IceServer[] _iceServers { get; set; }

        private XamarinLayoutManager LayoutManager = null;

        public LocalCameraMedia LocalCameraMedia = null;
        private LocalScreenMedia LocalScreenMedia = null;
        private AecContext _AecContext = null;

        public INavigation oNavigation = null;

        public bool Videollamada_init =false;

        public Boolean IsMedicConnected = false;

        private RemoteMediaCollection RemoteMedias = new RemoteMediaCollection();

#if __ANDROID__
        public MediaProjection MediaProjection { get; set; }
#endif

        #region Singleton
        private static Context _context;
        public static Context Instance
        {
            get
            {
                if (_context == null)
                {
                    _context = new Context();
                }

                return _context;
            }
        }
        #endregion

        #region Properties
        public string SessionId { get; set; }

        public string Name { get; set; }

        public bool EnableScreenShare { get; set; }

        public bool EnableDataChannel { get; set; }

        public bool EnableAudioSend { get; set; }

        public bool EnableAudioReceive { get; set; }

        public bool EnableVideoSend { get; set; }

        public bool EnableVideoReceive { get; set; }
        #endregion

        private void OnMessageReceived(MessageReceivedArgs args)
        {
            var handler = MessageReceived;

            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPeerJoined(string peer)
        {
            var handler = PeerJoined;

            if (handler != null)
            {
                handler(peer);
            }
        }
        private void OnPeerLeft(string peer)
        {
            var handler = PeerLeft;

            if (handler != null)
            {
                handler(peer);
            }
        }

        private Context()
        {
            // Log to the console.
            Log.Provider = new ConsoleLogProvider(LogLevel.Debug);
            Log.LogLevel = LogLevel.Debug;

            this.EnableAudioReceive = true;
            this.EnableAudioSend = true;
            this.EnableVideoReceive = true;
            this.EnableVideoSend = true;
        }

        #region Toggles
        public void ToggleRecordAudio(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                if (LocalCameraMedia != null)
                {
                    LocalCameraMedia.ToggleAudioRecording();
                }
            }
            else
            {
                var remoteMedia = RemoteMedias.GetById(id);
                if (remoteMedia != null)
                {
                    ((RemoteMedia)remoteMedia).ToggleAudioRecording();
                }
            }
        }

        public void ToggleRecordVideo(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                if (LocalCameraMedia != null)
                {
                    LocalCameraMedia.ToggleVideoRecording();
                }
            }
            else
            {
                var remoteMedia = RemoteMedias.GetById(id);
                if (remoteMedia != null)
                {
                    ((RemoteMedia)remoteMedia).ToggleVideoRecording();
                }
            }
        }

        public void ToggleAudioMute(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                if (LocalCameraMedia != null)
                {
                    LocalCameraMedia.AudioMuted = !LocalCameraMedia.AudioMuted;
                }
            }
            else
            {
                var remoteMedia = RemoteMedias.GetById(id);
                if (remoteMedia != null)
                {
                    remoteMedia.AudioMuted = !remoteMedia.AudioMuted;
                }
            }
        }

        public void ToggleVideoMute(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                if (LocalCameraMedia != null)
                {
                    LocalCameraMedia.VideoMuted = !LocalCameraMedia.VideoMuted;
                }
            }
            else
            {
                var remoteMedia = RemoteMedias.GetById(id);
                if (remoteMedia != null)
                {
                    remoteMedia.VideoMuted = !remoteMedia.VideoMuted;
                }
            }
        }
        #endregion

        public static void GenerateCertificate()
        {
            Promise<DtlsCertificate> promise = new Promise<DtlsCertificate>();
            _certificatePromise = promise;

            ManagedThread.Dispatch(() =>
            {
                promise.Resolve(DtlsCertificate.GenerateCertificate());
            });
        }

        public void WriteLine(string message)
        {
            _signalling.WriteLine(message);
        }

#if __IOS__
        public Future<FM.IceLink.LocalMedia> StartLocalMedia(AbsoluteLayout container)
#else
        public Future<FM.IceLink.LocalMedia> StartLocalMedia(Android.Content.Context context, AbsoluteLayout container)
#endif
        {
            return _certificatePromise.Then<FM.IceLink.LocalMedia>((cert) =>
            {
                _certificate = cert;

                if (!EnableAudioSend && !EnableVideoSend)
                {
                    LayoutManager = new XamarinLayoutManager(container);

                    return Promise<FM.IceLink.LocalMedia>.ResolveNow<FM.IceLink.LocalMedia>(null);
                }
                else
                {
                    Xamarin.Forms.View localView;
                    // Set up the local media.
                    if (!EnableScreenShare)
                    {
#if __IOS__
                        LocalCameraMedia = new LocalCameraMedia(!EnableAudioSend, !EnableVideoSend, null);
#else

                        if (EnableAudioSend)
                        {
                            _AecContext = new AecContext();
                            LocalCameraMedia = new LocalCameraMedia(context, !EnableAudioSend, !EnableVideoSend, _AecContext);
                        }
                        else
                        {
                            LocalCameraMedia = new LocalCameraMedia(context, !EnableAudioSend, !EnableVideoSend, null);

                        }
#endif
                        localView = new FMView(((LocalCameraMedia)LocalCameraMedia).GetView());
                    }
                    else
                    {
#if __IOS__
                        LocalScreenMedia = new LocalScreenMedia(!EnableAudioSend, !EnableVideoSend, null);
#else
                        if (EnableAudioSend)
                        {
                            _AecContext = new AecContext();
                            LocalScreenMedia = new LocalScreenMedia(MediaProjection, context, !EnableAudioSend, !EnableVideoSend, _AecContext);

                        }
                        else
                        {
                            LocalScreenMedia = new LocalScreenMedia(MediaProjection, context, !EnableAudioSend, !EnableVideoSend, null);
                        }
#endif
                        localView = new FMView(LocalScreenMedia.View);
                    }

                    // Set up the layout manager.
                    LayoutManager = new XamarinLayoutManager(container);

                    // Add the local preview to the layout.
                    if (localView != null)
                    {
                        LayoutManager.SetLocalView(localView);

                        // TODO Context menu
                        //LocalMedia.View.ContextMenu = videoChat.LocalContextMenu;
                    }

                    // Start the local media.
                    if (!EnableScreenShare)
                    {
                        return LocalCameraMedia.Start();
                    }
                    else
                    {
                        return LocalScreenMedia.Start();
                    }
                }
            });
        }

        public Future<FM.IceLink.LocalMedia> StopLocalMedia()
        {
            return Promise<FM.IceLink.LocalMedia>.WrapPromise<FM.IceLink.LocalMedia>(() =>
            {
                if (LocalCameraMedia == null && LocalScreenMedia == null)
                {
                    return Promise<FM.IceLink.LocalMedia>.ResolveNow<FM.IceLink.LocalMedia>(null);
                }

                // Stop the local media.
                if (LocalScreenMedia != null)
                {
                    return LocalScreenMedia.Stop();
                }
                else
                {
                    return LocalCameraMedia.Stop();
                }
            }).Then((o) =>
            {
                // Tear down the layout manager.
                var layoutManager = LayoutManager;
                if (layoutManager != null)
                {
                    layoutManager.RemoveRemoteViews();
                    layoutManager.UnsetLocalView();
                    LayoutManager = null;
                }

                // Tear down the local camera media.
                if (LocalCameraMedia != null)
                {
                    LocalCameraMedia.Destroy(); // LocalCameraMedia.Destroy() will also destroy AecContext.
                    LocalCameraMedia = null;
                }

                // Tear down the local screen media.
                if (LocalScreenMedia != null)
                {
                    LocalScreenMedia.Destroy(); // LocalScreenMedia.Destroy() will also destroy AecContext.
                    LocalScreenMedia = null;
                }
            });
        }

        public Future<object> JoinAsync()
        {
            if (SIGNAL_MANUALLY)
            {
                _signalling = ManualSignalling();
            }
            else
            {
                _signalling = AutoSignalling();
            }

            return _signalling.JoinAsync();
        }

        public AutoSignalling AutoSignalling()
        {
            return new AutoSignalling(_webSyncServerUrl, Name, SessionId, new Function1<PeerClient, Connection>((remoteClient) =>
            {
                return Connection(remoteClient);
            }), new Action2<string, string>((n, m) =>
            {
                OnMessageReceived(new MessageReceivedArgs(n, m));
            }));
        }

        public ManualSignalling ManualSignalling()
        {
            return new ManualSignalling(_webSyncServerUrl, Name, SessionId, new Function1<PeerClient, Connection>((remoteClient) =>
            {
                return Connection(remoteClient);
            }), new Action2<string, string>((n, m) =>
            {
                OnMessageReceived(new MessageReceivedArgs(n, m));
            }));
        }

        private Connection Connection(PeerClient remoteClient)
        {
            string peerName = "Unknown";
            FM.WebSync.Record r;
            if (remoteClient.BoundRecords != null && remoteClient.BoundRecords.TryGetValue("userName", out r))
            {
                if (!String.IsNullOrEmpty(r.ValueJson))
                {
                    peerName = r.ValueJson.Trim('"');
                }
            }

            // Create remote media to manage the remote audio/video tracks.
            RemoteMedia remoteMedia = null;

#if __IOS__
            Utilities.DispatchSync(() =>
            {
                remoteMedia = new RemoteMedia(false, false, null);
            });
#else
            FM.IceLink.Android.Utility.DispatchToMainThread(() =>
            {
                remoteMedia = new RemoteMedia(Android.App.Application.Context, false, false, null);
            }, true);
#endif

            // Initialize the audio/video streams for the connection with the
            // local media outputs (sender) and the remote media inputs (receiver).
            AudioStream audioStream = null;
            VideoStream videoStream = null;
            if (LocalCameraMedia != null)
            {
                audioStream = new AudioStream(LocalCameraMedia, remoteMedia)
                {
                    LocalSend = EnableAudioSend,
                    LocalReceive = EnableAudioReceive
                };

                videoStream = new VideoStream(LocalCameraMedia, remoteMedia)
                {
                    LocalSend = EnableVideoSend,
                    LocalReceive = EnableVideoReceive
                };
            }

            if (LocalScreenMedia != null)
            {
                audioStream = new AudioStream(LocalScreenMedia, remoteMedia)
                {
                    LocalSend = EnableAudioSend,
                    LocalReceive = EnableAudioReceive
                };

                videoStream = new VideoStream(LocalScreenMedia, remoteMedia)
                {
                    LocalSend = EnableVideoSend,
                    LocalReceive = EnableVideoReceive
                };
            }

            // Initialize the connection with the streams.
            Connection connection = new Connection(new Stream[] { audioStream, videoStream });

            // Provide STUN/TURN servers to assist with NAT traversal.
            try
            {
                var items = JsonConvert.DeserializeObject<List<LoginModel.LoginIceServer>>(Settings.IceLinkServers);
                List<IceServer> servers = new List<IceServer>();
                foreach(var item in items)
                {
                    if(string.IsNullOrEmpty(item.sPassword) || string.IsNullOrEmpty(item.sUser))
                    {
                        servers.Add(new IceServer(item.sServer));
                    }else
                    {
                        servers.Add(new IceServer(item.sServer, item.sUser, item.sPassword));
                    }
                }
                _iceServers = servers.ToArray();
                
            }
            catch (Exception e)
            {
                _iceServers = new[]
                {
                    //new IceServer("stun:turn.v3.icelink.fm:3478?transport=udp"),
                    //new IceServer("stun:turn.v3.icelink.fm:80?transport=udp", "santiagoms12@gmail.com", "Meditocicelink1!")

                    //otros servidores de icelink
                    //new IceServer("stun:turn.frozenmountain.com:3478?transport=udp"),
                    new IceServer("turn:turn.frozenmountain.com:80?transport=udp", "test", "pa55w0rd!"),

                    //NB: url "turn:turn.icelink.fm:443" implies that the relay server supports both TCP and UDP
                    //if you want to restrict the network protocol, use "turn:turn.icelink.fm:443?transport=udp"
                    //or "turn:turn.icelink.fm:443?transport=tcp". For further info, refer to RFC 7065 3.1 URI Scheme Syntax
                    //new IceServer("turn:turn.frozenmountain.com:80?transport=udp", "test", "pa55w0rd!"),
                    //new IceServer("turns:turn.frozenmountain.com:443", "test", "pa55w0rd!")
                };
            }
            
            
            connection.IceServers = _iceServers;
          

            // Add the remote view to the layout.
            var remoteView = remoteMedia.View;
            if (remoteView != null)
            {
                LayoutManager.AddRemoteView(remoteMedia.Id, new FMView(remoteMedia.View));
            }

            connection.OnStateChange += (c) =>
            {
                if (c.State == ConnectionState.Connected)
                {
                    OnPeerJoined(peerName);
                }
                // Remove the remote view from the layout.
                else if (c.State == ConnectionState.Closing || c.State == ConnectionState.Failing)
                {
                    var layoutManager = LayoutManager;
                    if (layoutManager != null)
                    {
                        layoutManager.RemoveRemoteView(remoteMedia.Id);
                    }
                    RemoteMedias.Remove(remoteMedia);
                    remoteMedia.Destroy();
                }
                else if (c.State == ConnectionState.Failed)
                {
                    OnPeerLeft(peerName);
                    if (!SIGNAL_MANUALLY)
                        _signalling.Reconnect(remoteClient, c);
                }
            };

            // Track the remote media in a collection.
            RemoteMedias.Add(remoteMedia);

            connection.LocalDtlsCertificate = _certificate;

            return connection;
        }

        public Future<object> LeaveAsync()
        {
            try
            {
                //if (!Settings.bCancelaDoctor)
                //{
                //    _signalling.WriteLine("FINALIZARCONSULTA");
                //}
                return _signalling.LeaveAsync();
            }
            catch {
                return null;
            }
        }
    }
}

