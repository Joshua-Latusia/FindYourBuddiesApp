using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using SharedCode;
using SharedCodePortable;

namespace FindYourFriendsServer
{
    internal class Session
    {
        private static readonly Dictionary<string, User> sessions = new Dictionary<string, User>();
        private static readonly Dictionary<string, Timer> SessionTimers = new Dictionary<string, Timer>();

        public static string NewSession(User u)
        {
            var rnd = new byte[5];
            new Random().NextBytes(rnd);
            var token = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(rnd)).Replace("-", "");

            sessions.Add(token, u);
            SessionTimers.Add(token,
                new Timer(SessionExpire, token, TimeSpan.FromMinutes(20), TimeSpan.FromMilliseconds(-1)));
            return token;
        }

        public static bool TryGetAccount(string token, out User u)
        {
            u = null;
            if (!sessions.ContainsKey(token))
                return false;

            u = sessions[token];
            SessionTimers[token].Change(TimeSpan.FromMinutes(20), TimeSpan.FromMilliseconds(-1));
            return true;
        }

        private static void SessionExpire(object state)
        {
            SessionTimers.Remove((string) state);
            sessions.Remove((string) state);
        }
    }
}