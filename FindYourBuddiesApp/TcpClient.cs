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
        private static string _ip = "127.0.0.1";
        private static int _port = 1337;

        public static void DoRequest(Packet p, Action<Packet> responseCallback)
        {
            new TcpClient().SendPacket(p, responseCallback);
        }

        public void SendPacket(Packet p, Action<Packet> responseCallback)
        {
            using (_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                SocketAsyncEventArgs connectArgs = new SocketAsyncEventArgs
                {
                    RemoteEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port)
                };

                connectArgs.Completed += delegate (object cs, SocketAsyncEventArgs a)
                {
                    SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();

                    var json = JsonConvert.SerializeObject(p);

                    var jsonbytes = Encoding.ASCII.GetBytes(json);

                    sendArgs.SetBuffer(jsonbytes, 0, jsonbytes.Length);

                    //sendArgs.RemoteEndPoint = _socket.RemoteEndPoint;

                    sendArgs.Completed += delegate (object ss, SocketAsyncEventArgs se)
                    {
                        var recvArgs = new SocketAsyncEventArgs {RemoteEndPoint = _socket.RemoteEndPoint};

                        recvArgs.Completed += delegate(object rs, SocketAsyncEventArgs re)
                        {
                            var response = Encoding.UTF8.GetString(re.Buffer, re.Offset, re.BytesTransferred);
                            responseCallback(JsonConvert.DeserializeObject<Packet>(response));
                        };
                        recvArgs.SetBuffer(new byte[68140], 0, 68140);

                        ((Socket)ss).ReceiveAsync(recvArgs);

                    };

                    ((Socket)cs).SendAsync(sendArgs);
                };


                _socket.ConnectAsync(connectArgs);

            }
        }

    

        private void Disconnect()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Dispose();
        }

    }
}
