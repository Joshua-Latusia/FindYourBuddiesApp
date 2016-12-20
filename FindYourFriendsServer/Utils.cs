using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SharedCodePortable;

namespace FindYourFriendsServer
{
    internal class Utils
    {
        private static readonly string IP_ADRESS = "10.0.0.5";

        public static string HashPass(string pass)
        {
            var sha512 = new SHA512CryptoServiceProvider();
            return BitConverter.ToString(sha512.ComputeHash(Encoding.Default.GetBytes(pass))).Replace("-", "");
        }

        public static Packet SendPacket(Packet p)
        {
            var s = JsonConvert.SerializeObject(p);
            var bytes = Encoding.Default.GetBytes(s);
            var tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(IP_ADRESS, 1337);
            }
            catch (SocketException)
            {
                //Console.ReadKey();
            }


            tcpClient.GetStream().Write(bytes, 0, bytes.Length);
            var outBuffer = new byte[163840];

            var outBufferLen = tcpClient.GetStream().Read(outBuffer, 0, outBuffer.Length);
            var dataBytes = new byte[outBufferLen];

            Array.Copy(outBuffer, dataBytes, outBufferLen);
            var dataString = Encoding.Default.GetString(dataBytes);

            tcpClient.Close();
            return JsonConvert.DeserializeObject<Packet>(dataString);
        }
    }
}