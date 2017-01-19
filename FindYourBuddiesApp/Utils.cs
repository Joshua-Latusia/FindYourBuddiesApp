using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Windows.Devices.Geolocation;
using Newtonsoft.Json;
using SharedCode;
using SharedCodePortable;

namespace FindYourBuddiesApp
{
    internal class Utils
    {
        private const int TimeoutMilliseconds = 5000;
        public static string IpAdress = "10.0.0.5";
        public static int Port = 1337;
        private static Socket _socket;
        private static readonly ManualResetEvent ClientDone = new ManualResetEvent(false);



       
        public static string Connect(string hostName, int portNumber)
        {
            var result = string.Empty;
            var hostEntry = new DnsEndPoint(hostName, portNumber);

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = hostEntry;

            socketEventArg.Completed +=
                delegate(object s, SocketAsyncEventArgs e)
                {
                    result = e.SocketError.ToString();
                    ClientDone.Set();
                };

            ClientDone.Reset();
            _socket.ConnectAsync(socketEventArg);
            ClientDone.WaitOne(TimeoutMilliseconds);
            return result;
        }

        public static Geopoint PointToGeopoint(Point point)
        {
            if (point == null)
            {
                return new Geopoint(new BasicGeoposition {Latitude = 51.5840049, Longitude = 4.7972440999999435 });
            }
            return new Geopoint(new BasicGeoposition { Latitude = point.Latitude, Longitude = point.Longitude });
        }

        public static Point GeopointToPoint(Geopoint geopoint)
        {
            return new Point(geopoint.Position.Longitude, geopoint.Position.Latitude);
        }


        public static string SendPacket(Packet p)
        {
            var response = "Error";
            if (_socket != null)
            {
                var socketEventArgs = new SocketAsyncEventArgs();

                socketEventArgs.RemoteEndPoint = _socket.RemoteEndPoint;
                socketEventArgs.UserToken = null;

                socketEventArgs.Completed += delegate(object s, SocketAsyncEventArgs e)
                {
                    response = e.SocketError.ToString();

                    ClientDone.Set();
                };

                var data = JsonConvert.SerializeObject(p);

                var payload = Encoding.UTF8.GetBytes(data);
                socketEventArgs.SetBuffer(payload, 0, payload.Length);

                ClientDone.Reset();

                _socket.SendAsync(socketEventArgs);

                ClientDone.WaitOne(TimeoutMilliseconds);


                return response;
            }

            return response;
        }

        public static string Receive()
        {
            var response = "Time Out";

            if (_socket != null)
            {
                var socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;

                socketEventArg.SetBuffer(new byte[68140], 0, 68140);

                socketEventArg.Completed += delegate(object s, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                    {
                        response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);

                        //test

                        //response = response.Trim('\0');
                    }

                    ClientDone.Set();
                };

                ClientDone.Reset();

                _socket.ReceiveAsync(socketEventArg);

                ClientDone.WaitOne(TimeoutMilliseconds);
            }


            return response;
        }
    }
}