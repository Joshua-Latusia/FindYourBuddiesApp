using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FindYourFriendsServer
{
    class Utils
    {
        public static string HashPass(string pass)
        {
            SHA512CryptoServiceProvider sha512 = new SHA512CryptoServiceProvider();
            return BitConverter.ToString(sha512.ComputeHash(Encoding.Default.GetBytes(pass))).Replace("-", "");
        }

    }
}
