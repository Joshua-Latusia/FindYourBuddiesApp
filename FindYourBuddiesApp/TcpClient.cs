using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using SharedCodePortable;

namespace FindYourBuddiesApp
{
    class TcpClient
    {
        private Socket _socket;
        private static string IP = "192.168.1.101";
        private static int Port = 1337;

        public static void DoRequest(Packet p, Action<Packet> responseCallback)
        {
            new TcpClient().SendPacket(p, responseCallback);
        }

        public void SendPacket(Packet p, Action<Packet> responseCallback)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketAsyncEventArgs connectArgs = new SocketAsyncEventArgs
            {
                RemoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port)
            };

            connectArgs.Completed += delegate
            {
                SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();

                var json = JsonConvert.SerializeObject(p);

                var jsonbytes = Encoding.ASCII.GetBytes(json);

                sendArgs.SetBuffer(jsonbytes, 0, jsonbytes.Length);

                //sendArgs.RemoteEndPoint = _socket.RemoteEndPoint;

                sendArgs.Completed += delegate
                {
                    var recvArgs = new SocketAsyncEventArgs {RemoteEndPoint = _socket.RemoteEndPoint};

                    recvArgs.Completed += delegate(object s, SocketAsyncEventArgs e)
                    {
                        var response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                        Disconnect();
                        responseCallback(JsonConvert.DeserializeObject<Packet>(response));

                    };
                    recvArgs.SetBuffer(new byte[68140], 0, 68140);

                    _socket.ReceiveAsync(recvArgs);

                };

                _socket.SendAsync(sendArgs);
            };


            _socket.ConnectAsync(connectArgs);


        }

    

        private void Disconnect()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Dispose();
        }

    }
}
