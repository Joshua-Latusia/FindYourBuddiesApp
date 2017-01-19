using System;
using Windows.Devices.Geolocation;
using SharedCodePortable;

namespace FindYourBuddiesApp
{
    public class UwpUser
    {
        public User User { get; set; }
        public Geopoint Location { get; set; }
        private Geolocator _locator;
       
        public UwpUser(User user)
        {
            this.User = user;
            Location = Utils.PointToGeopoint(user.Position);
            Geolocate();
        }

        public UwpUser(User user, string s)
        {
            this.User = user;
            Location = Utils.PointToGeopoint(user.Position);
        }

        public async void Geolocate()
        {
            GeolocationAccessStatus accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    _locator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default, MovementThreshold = 2 };
                    // geolocator = new Geolocator {ReportInterval = 1000};
                    Geoposition pos = await _locator.GetGeopositionAsync();
                    Location = pos.Coordinate.Point;
                    UpdateLocation();
                    _locator.PositionChanged += LocatorOnPositionChanged;
                    break;

            }
        }

        private void LocatorOnPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Location = args.Position.Coordinate.Point;
        }

        public void UpdateLocation()
        {
            User.Position = Utils.GeopointToPoint(Location);
        }

        

    }
}
