using System.Collections.Generic;
using SharedCode;

namespace SharedCodePortable
{
    public class User
    {
        // Friends are the ID of the friends.
        public List<int> Friends;
        public int UserId;

        public User()
        {
        }

        public User(string first, string last, string user, string hash, int age, bool man, List<int> friends )
        {
            FirstName = first;
            LastName = last;
            UserName = user;
            PasswordHash = hash;
            Age = age;
            IsMan = man;
            Friends = friends;


        }

        public User(string v, Point location)
        {
            this.UserName = v;
            this.Position = location;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public bool IsMan { get; set; }
        public Point Position { get; set; }
    }
}