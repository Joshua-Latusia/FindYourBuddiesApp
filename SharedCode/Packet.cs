namespace SharedCodePortable
{
    public class Packet
    {
        public EPacketType PacketType;
        public string Payload;
        public string Token;
    }

    public enum EPacketType
    {
        LoginRequest,
        LoginResponse,
        RefreshRequest,
        RefreshResponse,
        NewAccountRequest,
        SuccesResponse
    }
}