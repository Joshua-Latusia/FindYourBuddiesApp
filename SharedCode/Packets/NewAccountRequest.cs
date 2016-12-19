using System.Collections.Generic;

namespace SharedCodePortable.Packets
{
    public class NewAccountRequest
    {
        public int age;
        public string firstname;
        public List<User> friends;
        public bool isman;
        public string lastname;
        public string password;
        public string username;
    }
}