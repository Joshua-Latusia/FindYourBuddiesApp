using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedCode;

namespace FindYourFriendsServer
{
    class UsersFile
    {
        public List<User> Users = new List<User>();

        public List<User> GetUsers() => JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json")).Users;

        public static User GetUser(string username) => JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"))
            .Users.First(a => a.UserName == username);

        public static bool UsernameTaken(string username)
        {
            var userList = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json")).Users;
            foreach (var user in userList)
            {
                if (user.UserName == username)
                    return true;
            }

            return false;
        }

        public static void UpdateUser(User user)
        {
            //TODO think of this
        }

        public static void AddNewUser(User user)
        {
            var usersfile = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"));
            int nextID = usersfile.Users.Select(a => a.UserId).Max() + 1;
            user.UserId = nextID;

            usersfile.Users.Add(user);
            File.WriteAllText("users.json", JsonConvert.SerializeObject(usersfile));
        }

        public static void AddNewUser(string firstname, string lastname, string username, string password, int age,
            bool man)
        {
            var usersfile = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"));
            
            int nextID = usersfile.Users.Select(a => a.UserId).Max() + 1;

            usersfile.Users.Add(new User() {Age = age, FirstName = firstname, IsMan = man, LastName = lastname,PasswordHash = password,UserId = nextID, UserName = username});

            File.WriteAllText("users.json", JsonConvert.SerializeObject(usersfile));
        }


    }
}
