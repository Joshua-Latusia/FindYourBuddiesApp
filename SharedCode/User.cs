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
        public Geopoint Position;
        public string Name;

        public User(string name, Geopoint position)
        {
            this.Name = name;
            this.Position = position;
        }
    }
}
