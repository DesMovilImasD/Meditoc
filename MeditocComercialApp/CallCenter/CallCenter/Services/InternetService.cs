using System.Threading.Tasks;
using CallCenter.Helpers;
using Xamarin.Forms;
using Plugin.Connectivity;

namespace CallCenter.Services
{
    public class InternetService
    {
        public Page oPage;

        public InternetService(Page pPage)
        {
            this.oPage = pPage;
        }

        public async Task<bool> VerificaInternet()
        {
            bool bResultado = false;
            try
            {
                var host = "google.com/";

                var sNetworkStatus = CrossConnectivity.Current.IsConnected ? "Connected" : "No Connection";

                // here you will get Connected if you are connected to network 
                if (sNetworkStatus == "Connected")
                {
                    //bool isReachable = CrossConnectivity.Current.IsReachable(host, 5000).Result;
                    var isLocalReachable = await CrossConnectivity.Current.IsRemoteReachable("http://google.com");
                    //IsRemoteReachable(host);
                    if (!isLocalReachable)
                    //if (isReachable == false)
                    {

                        await oPage.DisplayAlert("Información", "Favor de verificar la conexión a internet e intente nuevamente.", "Aceptar");
                        Settings.bEnProceso = false;
                        Settings.bClicButton = false;
                        if (Settings.bPoPupActivo)
                        {
                            Settings.bPoPupActivo = false;
                            //await PopupNavigation.Instance.PopAsync();

                        }

                    }
                    else
                        bResultado = true;
                }
                else
                {

                    await oPage.DisplayAlert("Información", "Actualmente no está conectado a internet, favor de verificar la conexión e intente nuevamente.", "Aceptar");
                    Settings.bEnProceso = false;
                    Settings.bClicButton = false;
                    if (Settings.bPoPupActivo)
                    {
                        Settings.bPoPupActivo = false;
                        //await PopupNavigation.Instance.PopAsync();

                    }
                }
            }
            catch
            {

                await oPage.DisplayAlert("Información", "Favor de verificar la conexión a internet e intente nuevamente.", "Aceptar");
                Settings.bEnProceso = false;
                if (Settings.bPoPupActivo)
                {
                    Settings.bPoPupActivo = false;
                    //await PopupNavigation.Instance.PopAsync();
                    Settings.bClicButton = false;
                }

            }

            return bResultado;
        }
    }

}
