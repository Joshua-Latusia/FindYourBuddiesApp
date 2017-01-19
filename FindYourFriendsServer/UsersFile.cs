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

        public List<User> GetUsers() => JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));

        public static User GetUser(string username)
            => JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"))
               .First(a => a.UserName == username);


        public static List<User> GetUsersFile()
        {
            return JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));
        }


        // Gets Json with all users, checks if @param username exists in the userlist Json.
        public static bool IsUsernameAvaialable(string username)
        {
            var userList = GetUsersFile();
            // LINQ ---
            return userList.All(user => user.UserName != username);
            // LINQ ---
        }

        // Returns a list with all the users in users.json containing @param username
        public static List<User> GetUsersContaingString(string username)
        {

            var userList = GetUsersFile();
            //LINQ ---
            return userList.Where(u => u.UserName.Contains(username)).ToList();
            //LINQ ---
     
        }

        // Returns a list with all the users who contain @param ids as their userID
        public static List<User> GetAllFriendsById(List<int> ids)
        {
            var userList = GetUsersFile();
            return userList.Where(user => ids.Contains(user.UserId)).ToList();
        }

        
        public static void RemoveUser(string username)
        {
            var i = 0;
            var usersfile = GetUsersFile();
            foreach (var user in usersfile)
            {

                if (user.UserName == username)
                {
                    usersfile.RemoveAt(i);
                    File.WriteAllText("users.json", JsonConvert.SerializeObject(usersfile));
                    return;
                }
                i++;
            }

        }

        // Adds a friend_id to an given user.
        // Get the user with @param username.
        //
        public static void AddFriendToUser(string username, string friend)
        {
            if (GetUser(username) != null)
            {
                // get the user from the userfile and the friends id 
                // Adds the friend id to the user and updates the user.
                var user  = GetUser(username);
                var friendID = GetUser(friend).UserId;

                // if the user already has the friend ID or its his own ID and then it doens't add anything
                if (user.Friends.Contains(friendID) || user.UserId == friendID)
                    return;

                RemoveUser(username);
                var updatedFile =  JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));

                if (user.Friends == null)
                {
                    user.Friends = new List<int>();
                }
                user.Friends.Add(friendID);
                updatedFile.Add(user);
                File.WriteAllText("users.json", JsonConvert.SerializeObject(updatedFile));
            }
        }

        public static bool ValidateAccount(string username, string password)
        {
            var userList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));
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
            var userfile = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));
            userfile.Add(user);
            File.WriteAllText("users.json", JsonConvert.SerializeObject(userfile));
        }

        public static void AddNewUser(User user)
        {
            var usersfile = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));
            var nextID = usersfile.Select(a => a.UserId).Max() + 1;
            user.UserId = nextID;

            usersfile.Add(user);
            File.WriteAllText("users.json", JsonConvert.SerializeObject(usersfile));
        }

        public static void AddNewUser(string firstname, string lastname, string username, string password, int age,
            bool man, List<int> friends  )
        {
            var usersfile = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));

            var nextID = usersfile.Select(a => a.UserId).Max() + 1;

            usersfile.Add(new User
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