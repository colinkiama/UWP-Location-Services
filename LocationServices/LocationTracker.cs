using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace LocationServices
{

    public class LocationTracker
    {
        public async Task<Geoposition> GetCurrentPosition()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                   
                    // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
                    Geoposition currentPosition = await geolocator.GetGeopositionAsync();
                    return currentPosition;
                    

                case GeolocationAccessStatus.Denied:
                    Debug.WriteLine("Denied!");
                    break;

                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Unspecified Error");
                    break;
                   
            }
            return null;
        }
    }
}
