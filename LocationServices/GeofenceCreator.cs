using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

namespace LocationServices
{
    public class GeofenceCreator
    {
        List<Geofence> geoFences = new List<Geofence>();

        public async void StartUp()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Location Unspecified");
                    break;
                case GeolocationAccessStatus.Allowed:
                    geoFences = (List<Geofence>)GeofenceMonitor.Current.Geofences;
                    break;

                case GeolocationAccessStatus.Denied:
                    Debug.WriteLine("Access Denied!");
                    break;

            }
        }

        public Geofence SetUpGeoFence(double latitude, double longitude)
        {
            string fenceId = "enteredHomeFence";
            BasicGeoposition position;
            position.Latitude = latitude;
            position.Longitude = longitude;
            position.Altitude = 0.0;
            double radius = 60; // in meters


            // Set a circular region for the geofence.
            Geocircle geocircle = new Geocircle(position, radius);

            // Set the monitored states.
            MonitoredGeofenceStates monitoredStates =
                            MonitoredGeofenceStates.Entered;

            // Set how long you need to be in geofence for the enter event to fire.
            TimeSpan dwellTime = TimeSpan.FromSeconds(5);

            // Set how long the geofence should be active.
            TimeSpan duration = TimeSpan.FromDays(1);

            // Set up the start time of the geofence.
            DateTimeOffset startTime = DateTime.Now;

            // Create the geofence.
            Geofence geofence = new Geofence(fenceId, geocircle, monitoredStates, false, dwellTime, startTime, duration);
            GeofenceMonitor.Current.Geofences.Add(geofence);

            return geofence;
        }
    }
}
