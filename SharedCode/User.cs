using System.Collections.Generic;
using SharedCode;

namespace SharedCodePortable
{
    public class User
    {
        //TODO maybe friends should be friend objects without friends list otherwise inf loop???
        public List<User> Friends = new List<User>();
        public int UserId;

        public User()
        {
        }

        public User(string first, string last, string user, string hash, int age, bool man)
        {
            FirstName = first;
            LastName = last;
            UserName = user;
            PasswordHash = hash;
            Age = age;
            IsMan = man;
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