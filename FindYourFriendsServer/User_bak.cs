using System.Collections.Generic;

namespace SharedCode
{
    public class User_bak
    {
        public int UserId;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public bool IsMan { get; set; }
        public Point Location { get; set; }
        public List<int> Friends;
        //public Geopoint Location;

        public User_bak()
        {
        }

        public User_bak(string first, string last, string user, string hash, int age, bool man, List<int> friends )
        {
            FirstName = first;
            LastName = last;
            UserName = user;
            PasswordHash = hash;
            Age = age;
            IsMan = man;
            Friends = friends;
        }

        
    }
}