﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindYourFriendsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("Users.json"))
                File.Create("Users.json").Close();

            //UsersFile.AddNewUser("Admin","Admin","Admin",Utils.HashPass("Admin"),18,true);
            //Console.WriteLine("File created");

            // PUT THIS IN USERS JSON INCASE ITS BROKEN
            // ->    {Users:[]}

            TcpServer.Start();
        }
    }
}
