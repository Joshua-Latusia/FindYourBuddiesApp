using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SharedCodePortable;

namespace FindYourFriendsServer
{
    internal class UsersFile
    {
        public List<User> Users = new List<User>();

        public List<User> GetUsers() => JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json")).Users;

        public static User GetUser(string username)
            => JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"))
                .Users.First(a => a.UserName == username);

        public static bool UsernameTaken(string username)
        {
            var userList = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json")).Users;
            foreach (var user in userList)
                if (user.UserName == username)
                    return false;

            return true;
        }

        public static bool ValidateAccount(string username, string password)
        {
            var userList = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json")).Users;
            foreach (var user in userList)
            {
                if (username == user.UserName && password == user.PasswordHash)
                {
                    return true;
                    
                }
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
            var nextID = usersfile.Users.Select(a => a.UserId).Max() + 1;
            user.UserId = nextID;

            usersfile.Users.Add(user);
            File.WriteAllText("users.json", JsonConvert.SerializeObject(usersfile));
        }

        public static void AddNewUser(string firstname, string lastname, string username, string password, int age,
            bool man, List<int> friends  )
        {
            var usersfile = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"));

            var nextID = usersfile.Users.Select(a => a.UserId).Max() + 1;

            usersfile.Users.Add(new User
            {
                Age = age,
                FirstName = firstname,
                IsMan = man,
                LastName = lastname,
                PasswordHash = password,
                UserId = nextID,
                UserName = username,
                Friends = new List<int>()


            });

            File.WriteAllText("users.json", JsonConvert.SerializeObject(usersfile));
        }
    }
}