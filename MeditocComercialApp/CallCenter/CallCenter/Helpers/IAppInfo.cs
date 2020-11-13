using System;
using System.Threading.Tasks;

namespace CallCenter.Helpers
{
    /**
     * interface utilizado para manejar
     * ciertas variables o objetos nativos.
     */
    public interface IAppInfo
    {
        // obtiene la version de la app
        string GetVersion();

        // obtiene la ultima version del build de la app
        int GetBuild();

        // obtiene la url de la tienda de la version de la app
        string StoreUrl();

        // obtiene el bundle identifier de la app
        string GetBundleId();

        // busca si en la tienda existe una nueva actualizacion de la app
        Task<VersionResult> NeedUpdateApp();

        // finaliza la aplicacion.
        void CloseApp();

        // lanzamos la tienda correspondiente
        string GotoStore();

        // lanzamos la configuracion de la aplicacion.
        void OpenAppSettings();

        // escode el status bar
        void HideStatusBar();

        // muestra el status bar
        void ShowStatusBar();

        void SetLightTheme();
        void SetDarkTheme();
    }


    /**
     * structura de version result,
     * utilizada para retornar una respuesta del versionamiento
     */
    public struct VersionResult
    {
        // variable que indica si el proceso fue exitoso
        public bool isSuccess;

        // variable que indica si es necesario actualizar la app
        public bool needUpdate;

        // variable que muestra errores
        public string error;

        // intance de la clase para generar una salida de error
        public static VersionResult Fail(string error) => new VersionResult
        {
            isSuccess = false,
            needUpdate = false,
            error = error
        };

        // instance de la clase para generar una salida correcta.
        public static VersionResult Done(bool needUpdate) => new VersionResult
        {
            isSuccess =true,
            needUpdate = needUpdate,
            error = ""
        };
    }
}
