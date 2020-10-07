using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace IMD.Meditoc.CallCenter.Mx.Web.Tokens
{
    public class MeditocAuthentication : AuthorizeAttribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get { return false; } }
        public async Task AuthenticateAsync(HttpAuthenticationContext httpAuthenticationContext, CancellationToken cancellationToken)
        {
            HttpRequestMessage httpRequestMessage = httpAuthenticationContext.Request;

            AuthenticationHeaderValue authenticationHeaderValue = httpRequestMessage.Headers.Authorization;

            HttpRequestHeaders headers = httpRequestMessage.Headers;

            try
            {
                if (headers.Where(x => x.Key == "AppKey" || x.Key == "appkey").Count() != 1)
                {
                    SetHttpUnauthorized(httpAuthenticationContext);
                    return;
                }

                if (headers.Where(x => x.Key == "AppToken" || x.Key == "apptoken").Count() != 1)
                {
                    SetHttpUnauthorized(httpAuthenticationContext);
                    return;
                }


                string appKey = headers.GetValues("AppKey").FirstOrDefault();
                string appToken = headers.GetValues("AppToken").FirstOrDefault();

                if (string.IsNullOrWhiteSpace(appKey) || string.IsNullOrWhiteSpace(appToken))
                {
                    appKey = headers.GetValues("appkey").FirstOrDefault();
                    appToken = headers.GetValues("apptoken").FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(appKey) || string.IsNullOrWhiteSpace(appToken))
                    {
                        SetHttpUnauthorized(httpAuthenticationContext);
                        return;
                    }

                }

                IMDEndec iMDEndec = new IMDEndec();

                IMDResponse<string> resAppKey = iMDEndec.BDecrypt(appKey, "M3dit0cAppKeyV4l", "MeditocK");
                if (resAppKey.Code != 0)
                {
                    SetHttpUnauthorized(httpAuthenticationContext);
                    return;
                }

                IMDResponse<string> resAppToken = iMDEndec.BDecrypt(appToken, "M3dit0cAppToken8", "MeditocT");
                if (resAppKey.Code != 0)
                {
                    SetHttpUnauthorized(httpAuthenticationContext);
                    return;
                }

                if (resAppKey.Result != "MeditocAppKeyAuthenti0WebOK")
                {
                    SetHttpUnauthorized(httpAuthenticationContext);
                    return;
                }

                if (resAppToken.Result != "IMD.Meditoc.CallCenterSTthenticacion2020WebOK")
                {
                    SetHttpUnauthorized(httpAuthenticationContext);
                    return;
                }

            }
            catch (Exception)
            {
                SetHttpUnauthorized(httpAuthenticationContext);
                return;
            }

            GenericIdentity identity = new GenericIdentity("Meditoc");
            string[] rol = { "Manager" };
            GenericPrincipal principal = new GenericPrincipal(identity, rol);

            httpAuthenticationContext.Principal = principal;

        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext httpAuthenticationChallengeContext, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage = await httpAuthenticationChallengeContext.Result.ExecuteAsync(cancellationToken);

            httpAuthenticationChallengeContext.Result = new ResponseMessageResult(httpResponseMessage);
        }

        public void SetHttpUnauthorized(HttpAuthenticationContext httpAuthenticationContext)
        {
            HttpRequestMessage httpRequestMessage = httpAuthenticationContext.Request;
            httpAuthenticationContext.ErrorResult = new MeditocFailureResult("No autorizado", httpRequestMessage);
        }
    }
}