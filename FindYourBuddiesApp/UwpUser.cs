using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using SharedCodePortable;

namespace FindYourBuddiesApp
{
    public class UwpUser
    {
        public User user { get; set; }
        public Geopoint location { get; set; }

        public UwpUser(User user)
        {
            this.user = user;
            //location = Utils.PointToGeopoint(user.Position);
        }

        public void UpdateLocation()
        {
            user.Position = Utils.GeopointToPoint(location);
        }
    }
}
