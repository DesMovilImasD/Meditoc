using System;
using System.Threading.Tasks;

namespace CallCenter.Helpers
{
    #region -------- [ALIAS] --------
    using PERM = Xamarin.Essentials.Permissions;
    using PERM_STATUS = Xamarin.Essentials.PermissionStatus;
    using APP = Xamarin.Forms.Application;
    using DEVICE = Xamarin.Forms.Device;
    using SERVICE = Xamarin.Forms.DependencyService;
    #endregion

    #region -------- [PERMISION VALIDATOR] --------
    public static class PermissionValidator
    {
        /**
         * VERIFICA LOS PERMISOS DE LA VIDEOLLAMADA
         */
        public static async Task<bool> CheckVideoCallPermissions()
        {
            try
            {
                // -------------------------------------------------------------
                // SE VERIFICAN LOS PERMISOS DE LA CAMARA.
                // -------------------------------------------------------------
                PERM_STATUS CameraStatus = await PERM.CheckStatusAsync<PERM.Camera>();
                if (CameraStatus != PERM_STATUS.Granted)
                {
                    CameraStatus = await PERM.RequestAsync<PERM.Camera>();
                    if (CameraStatus != PERM_STATUS.Granted)
                    {
                        string title = $"El Permiso de camara";
                        string question = $"Para usar la aplicación el permiso de la camara es requerido. Por favor acceda a configuración y habilite el permiso para la aplicación.";
                        string positive = "Configuración";
                        string negative = "Quizás después";

                        Task<bool> task = APP.Current?.MainPage?
                            .DisplayAlert(title, question, positive, negative);
                        if (task == null)
                            return false;

                        var result = await task;
                        if (result)
                        {
                            SERVICE.Get<IAppInfo>().OpenAppSettings();
                        }
                        return false;
                    }
                }
                // -------------------------------------------------------------
                // SI YA SE OTORGO PERMISOS DE LA CAMARA
                // SE VERIFICAN LOS PERMISOS DEL MICROFONO.
                // -------------------------------------------------------------
                PERM_STATUS MicrophoneStatus = await PERM.CheckStatusAsync<PERM.Microphone>();
                if (MicrophoneStatus != PERM_STATUS.Granted)
                {
                    MicrophoneStatus = await PERM.RequestAsync<PERM.Microphone>();
                    if (MicrophoneStatus != PERM_STATUS.Granted)
                    {
                        string title = $"Permiso de microfono";
                        string question = $"Para usar la aplicación el permiso del microfono es requerido. Por favor acceda a configuración y habilite el permiso para la aplicación.";
                        string positive = "Configuración";
                        string negative = "Quizás después";

                        Task<bool> task = APP.Current?.MainPage?
                            .DisplayAlert(title, question, positive, negative);
                        if (task == null)
                            return false;

                        var result = await task;
                        if (result)
                        {
                            SERVICE.Get<IAppInfo>().OpenAppSettings();
                        }

                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /**
         * VERIFICA LOS PERMISOS DE LA UBICACION
         */
        public static async Task<bool> CheckLocationPermissions()
        {
            try
            {
                PERM_STATUS locationStatus = await PERM.CheckStatusAsync<PERM.LocationAlways>();
                if (locationStatus != PERM_STATUS.Granted)
                {
                    locationStatus = await PERM.RequestAsync<PERM.LocationAlways>();
                    if (locationStatus != PERM_STATUS.Granted)
                    {
                        var title = $"Permiso de ubicación";
                        var question = $"Para usar la aplicación el permiso de la ubicación es requerido";
                        var positive = "Configuración";
                        var negative = "Quizás después";

                        Task<bool> task = APP.Current?.MainPage?
                            .DisplayAlert(title, question, positive, negative);
                        if (task == null)
                            return false;

                        var result = await task;
                        if (result)
                        {
                            SERVICE.Get<IAppInfo>().OpenAppSettings();
                        }
                        return false;
                    }
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public static async Task<bool> LazyCheckLocationPermissions()
        {
            try
            {
                PERM_STATUS locationStatus = await PERM.CheckStatusAsync<PERM.LocationAlways>();
                return (locationStatus == PERM_STATUS.Granted);
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
    #endregion
}
