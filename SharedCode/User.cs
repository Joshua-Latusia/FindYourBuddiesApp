﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace SharedCode
{
    public class User
    {
        public int UserId;
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public bool IsMan { get; set; }
        public List<User> Friends = new List<User>();

        public Geopoint Location;

        public User()
        {

        }

        public User(string first , string last, string user, string hash, int age, bool man)
        {
            FirstName = first;
            LastName = last;
            UserName = user;
            PasswordHash = hash;
            Age = age;
            IsMan = man;
        }

        
    }
}