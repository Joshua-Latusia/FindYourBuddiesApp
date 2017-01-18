using System;
using System.IO;
using SharedCodePortable;

namespace FindYourFriendsServer
{
    internal class Login
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