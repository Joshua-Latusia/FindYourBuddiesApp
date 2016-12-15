using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace SharedCode
{
    public class User
    {
        private Geopoint _location;
        private string Name;

        public User(string name, Geopoint location)
        {
            this.Name = name;
            this._location = location;
        }

        public Geopoint Position { get; set; }
    }
}
