using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode;

namespace FindYourFriendsServer
{
    class Login
    {
        public static bool CheckLogin(string username, string hash)
        {
            if (!File.Exists("users.json"))
                return false;
            //TODO fixx
            return UsersFile.GetUser(username).PasswordHash == hash;
        }

        public static Tuple<bool, string, User> DoLogin(string username, string hash)
        {
            var a = UsersFile.GetUser(username);
            return Tuple.Create(CheckLogin(username, hash), Session.NewSession(a), a);
        }
    }
}
