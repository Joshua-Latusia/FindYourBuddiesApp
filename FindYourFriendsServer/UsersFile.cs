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

        public static List<User> GetUsersContaingString(string username)
        {       

            var userList = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json")).Users;
            //LINQ ---
            return userList.Where(u => u.UserName.Contains(username)).ToList();
            //LINQ ---
     
        }

        public static List<User> GetAllFriendsByID(List<int> ids)
        {
            var userList = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json")).Users;
            List<User> friends = new List<User>();
            foreach (var user in userList)
            {
                if(ids.Contains(user.UserId))
                {
                    friends.Add(user);
                }
            }
            return friends;
        }

        
        public static void RemoveUser(string username)
        {
            int i = 0;
            var usersfile = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"));
            foreach (var user in usersfile.Users)
            {

                if (user.UserName == username)
                {
                    usersfile.Users.RemoveAt(i);
                    File.WriteAllText("users.json", JsonConvert.SerializeObject(usersfile));
                    return;
                }
                i++;
            }

        }

        //TODO test
        public static void AddFriendToUser(string username, string friend)
        {
            if (GetUser(username) != null)
            {
                // get the user from the userfile and the friends id 
                // Adds the friend id to the user and updates the user.
                var usersfile = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"));
                var user  = GetUser(username);
                var friendID = GetUser(friend).UserId;
                //TODO check if friends isnt already in the list

                RemoveUser(username);
                var updatedFile =  JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"));

                if (user.Friends == null)
                {
                    user.Friends = new List<int>();
                }
                user.Friends.Add(friendID);
                updatedFile.Users.Add(user);
                File.WriteAllText("users.json", JsonConvert.SerializeObject(updatedFile));
            }
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
            RemoveUser(user.UserName);
            var userfile = JsonConvert.DeserializeObject<UsersFile>(File.ReadAllText("users.json"));
            userfile.Users.Add(user);
            File.WriteAllText("users.json", JsonConvert.SerializeObject(userfile));
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