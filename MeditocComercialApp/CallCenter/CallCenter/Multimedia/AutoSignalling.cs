using FM.IceLink;
using FM.IceLink.WebSync4;
using System;

namespace CallCenter.Multimedia
{
    public class AutoSignalling : Signalling
    {
        public AutoSignalling(string serverUrl, string name, string sessionId, Function1<PeerClient, Connection> createConnection, Action2<string, string> onReceivedText)
            : base(serverUrl, name, sessionId, createConnection, onReceivedText)
        { }

        protected override void DefineChannels()
        {
            SessionChannel = $"/{SessionId}";
            MetadataChannel = $"{SessionChannel}/metadata";
        }

        /// <summary>
        /// Handles subscription to the user and session channels and promise resolution/rejection. </summary>
        /// <param name="promise"> The connection promise created by JoinAsync method. </param>
        protected override void DoJoinAsync(Promise<object> promise)
        {
            BindUserMetadata(UserIdKey, UserId).Then(new Function1<Object, Future<Object>>((o) =>
            {
                return BindUserMetadata(UserNameKey, UserName);
            })).Then(new Function1<Object, Future<Object>>((o) =>
            {
                return SubscribeToSessionChannel();
            }))
            .Then((o) =>
            {
                if (promise.State == FutureState.Pending)
                {
                    promise.Resolve(o);
                }
            })
            .Fail((e) =>
            {
                if (promise.State == FutureState.Pending)
                {
                    promise.Reject(e);
                }
            });
        }

        private Future<object> SubscribeToSessionChannel()
        {
            Promise<object> promise = new Promise<object>();
            try
            {
                ClientExtensions.JoinConference(Client, new JoinConferenceArgs(SessionChannel)
                {
                    OnSuccess = (o) =>
                    {
                        promise.Resolve(o);
                    },
                    OnFailure = (e) =>
                    {
                        promise.Reject(e.Exception);
                    },
                    OnRemoteClient = (remoteClient) =>
                    {
                        Connection connection = CreateConnection(remoteClient);
                        Connections.Add(connection);
                        return connection;
                    }
                });
            }
            catch (Exception e)
            {
                promise.Reject(e);
            }

            return promise;
        }

        public override void Reconnect(PeerClient remoteClient, Connection connection)
        {
            try
            {
                ClientExtensions.ReconnectRemoteClient(Client, remoteClient, connection);
            }
            catch (Exception e)
            {

            }
        }

        public override void RenegotiateSessionChannel(Connection connection)
        {
            try
            {
                ClientExtensions.Renegotiate(Client, SessionChannel, connection);
            }
            catch (Exception e)
            {


            }
        }
    }
}
