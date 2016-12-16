using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode;

namespace FindYourFriendsServer.Packets
{
    class NewAccountRequest
    {
        public string firstname;
        public string lastname;
        public string username;
        public string password;
        public int age;
        public bool isman;
        public List<User> friends;
        //TODO fix with GEOLOCATION and right library
        //public Geopoint location;
    }
}
