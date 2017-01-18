using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode;
using SharedCodePortable;

namespace SharedCode.Packets
{
    public class SearchUsernameResponse
    {
        public List<User> users;
        public bool succes;
    }
}
