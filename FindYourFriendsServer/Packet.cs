using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCode
{
    class Packet
    {
        public EPacketType PacketType;
        public string Token;
        public string Payload;
    }

    enum EPacketType
    {
        LoginRequest,
        LoginResponse,
        RefreshRequest,
        RefreshResponse,
        NewAccountRequest,
        SuccesResponse
    }
}
