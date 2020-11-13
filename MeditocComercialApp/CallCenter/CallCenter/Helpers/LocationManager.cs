using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CallCenter.Helpers
{
    #region -------- [Location manager] --------

    /// <summary>
    /// Location manager class
    /// </summary>
    public class LocationManager
    {

        private static LocationManager Instance { get; set; }

        public static LocationManager Shared()
        {
            if (Instance == null) Instance = new LocationManager();
            return Instance;
        }
 
        private LocationManager(){}

        /// <summary>
        /// variable que almacena la ultima ubicacion encontrada.
        /// </summary>
        private Location _lastPosition { get; set; }
        public Location LastPosition {
            get { return _lastPosition; }
            private set { _lastPosition = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cached"></param>
        /// <param name="accuracy"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<LocationResult> FindLocation(bool cached = true)
        {
            try
            {
                LocationResult result;

                if (cached)
                {
                    result = await GetCachedPosition();
                    LastPosition = result.Position;
                    return result;
                }

                result = await GetGPSPosition();
                LastPosition = result.Position;
                return result;
            }
            catch(FeatureNotSupportedException e)
            {
                // handle no support on device
                var err = string.IsNullOrEmpty(e.Message) ?
                    "No se pudo obtener la ubicación: característica no soportada" :
                    e.Message;
                return LocationResult.Fail(err);
            }
            catch(FeatureNotEnabledException e) { 
                // 
            var err = string.IsNullOrEmpty(e.Message) ?
                    "No se pudo obtener la ubicación: característica no habilitada" :
                    e.Message;
                return LocationResult.Fail(err);
            }
            catch(PermissionException e)
            {
                // handle permision exception
                var err = string.IsNullOrEmpty(e.Message) ?
                    "No se pudo obtener la ubicación: Permisos no otorgados" :
                    e.Message;
                return LocationResult.Fail(err);

            }
            catch(Exception e)
            {
                // unable get position.
                var err = string.IsNullOrEmpty(e.Message) ?
                    "No se pudo obtener la ubicación: Error desconocido" :
                    e.Message;
                return LocationResult.Fail(err);
            }
         
        }


        /// <summary>
        /// en caso que la ubicacion ya se haya encontrado retorna la posicion cacheada
        /// en caso que no exista la intenta buscar con el gps.
        /// </summary>
        /// <param name="geolocator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        private async Task<LocationResult> GetCachedPosition( )
        {
            var position = await Geolocation.GetLastKnownLocationAsync();
            if (position is null)
            {
                return await GetGPSPosition();
            }
            return LocationResult.Done(position: position);
        }

        /// <summary>
        /// intenta buscar la ubicacion actual
        /// utilizando el gps.
        /// </summary>
        /// <param name="geolocator"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        private async Task<LocationResult> GetGPSPosition()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var position = await Geolocation.GetLocationAsync(request);
            return (position is null) ?
                LocationResult.Fail(msg: "No se pudo obtener la ubicación del gps") :
                LocationResult.Done(position: position);
        }

    }
    #endregion

    #region -------- [Location result] --------

    /// <summary>
    /// location manager result
    /// </summary>
    public struct LocationResult
    {
        public bool IsSuccess;
        public string Msg;
        public Location Position;

        public static LocationResult Done(Location position, string msg = null) => new LocationResult
        {
            IsSuccess = true,
            Msg = msg,
            Position = position
        };

        public static LocationResult Fail(string msg ) => new LocationResult
        {
            IsSuccess = false,
            Msg = msg,
            Position = null
        };

    }

    #endregion
}
