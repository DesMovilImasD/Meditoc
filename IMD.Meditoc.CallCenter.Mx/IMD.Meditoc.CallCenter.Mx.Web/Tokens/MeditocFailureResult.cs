using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace IMD.Meditoc.CallCenter.Mx.Web.Tokens
{
    public class MeditocFailureResult : IHttpActionResult
    {
        public string FcReasonPhrase;
        public HttpRequestMessage FHttpRequestMessage { get; set; }
        public MeditocFailureResult(string pcReasonPhrase, HttpRequestMessage pHttpRequestMessage)
        {
            FcReasonPhrase = pcReasonPhrase;
            FHttpRequestMessage = pHttpRequestMessage;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
        public HttpResponseMessage Execute()
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            httpResponseMessage.RequestMessage = FHttpRequestMessage;
            httpResponseMessage.ReasonPhrase = FcReasonPhrase;

            return httpResponseMessage;
        }
    }
}