using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedCode;

namespace FindYourFriendsServer.Packets
{
    class RefreshResponse
    {
        //TODO not sure if this should be response
        // Idea is to refresh the friends list internal in the server before asking for this.
        public User user;
    }
}
