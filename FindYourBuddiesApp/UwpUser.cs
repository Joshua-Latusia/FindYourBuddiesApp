using System;
using Windows.Devices.Geolocation;
using SharedCodePortable;

namespace FindYourBuddiesApp
{
    public class UwpUser
    {
        public User user { get; set; }
        public Geopoint location { get; set; }
        private Geolocator locator;
       
        public UwpUser(User user)
        {
            this.user = user;
            location = Utils.PointToGeopoint(user.Position);
            geolocate();
        }

        public UwpUser(User user, string s)
        {
            this.user = user;
            location = Utils.PointToGeopoint(user.Position);
        }

        public async void geolocate()
        {
            GeolocationAccessStatus accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    locator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default, MovementThreshold = 2 };
                    // geolocator = new Geolocator {ReportInterval = 1000};
                    Geoposition pos = await locator.GetGeopositionAsync();
                    location = pos.Coordinate.Point;
                    UpdateLocation();
                    locator.PositionChanged += LocatorOnPositionChanged;
                    break;

            }
        }

        private void LocatorOnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            location = args.Position.Coordinate.Point;
        }

        public void UpdateLocation()
        {
            user.Position = Utils.GeopointToPoint(location);
        }

        

    }
}
