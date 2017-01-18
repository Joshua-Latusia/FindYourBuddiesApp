using System;
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
        private const int TIMEOUT_MILLISECONDS = 5000;
        public static string IP_ADRESS = "10.0.0.5";
        public static int Port = 1337;
        private static Socket Socket;
        private static readonly ManualResetEvent _clientDone = new ManualResetEvent(false);



        // geeen idee wtf dit doet
        public static string Connect(string hostName, int portNumber)
        {
            var result = string.Empty;
            var hostEntry = new DnsEndPoint(hostName, portNumber);

            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = hostEntry;

            socketEventArg.Completed +=
                delegate(object s, SocketAsyncEventArgs e)
                {
                    result = e.SocketError.ToString();
                    _clientDone.Set();
                };

            _clientDone.Reset();
            Socket.ConnectAsync(socketEventArg);
            _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
            return result;
        }

        public static Geopoint PointToGeopoint(Point point)
        {
            if (point == null)
            {
                return new Geopoint(new BasicGeoposition() {Latitude = 4.7972440999999435, Longitude = 51.5840049 });
            }
            else
            {
                return new Geopoint(new BasicGeoposition() { Latitude = point.Latitude, Longitude = point.Longitude });
            }          
        }

        public static Point GeopointToPoint(Geopoint geopoint)
        {
            return new Point(geopoint.Position.Longitude, geopoint.Position.Latitude);
        }


        public static string SendPacket(Packet p)
        {
            var response = "Error";
            if (Socket != null)
            {
                var socketEventArgs = new SocketAsyncEventArgs();

                socketEventArgs.RemoteEndPoint = Socket.RemoteEndPoint;
                socketEventArgs.UserToken = null;

                socketEventArgs.Completed += delegate(object s, SocketAsyncEventArgs e)
                {
                    response = e.SocketError.ToString();

                    _clientDone.Set();
                };

                var data = JsonConvert.SerializeObject(p);

                var payload = Encoding.UTF8.GetBytes(data);
                socketEventArgs.SetBuffer(payload, 0, payload.Length);

                _clientDone.Reset();

                Socket.SendAsync(socketEventArgs);

                _clientDone.WaitOne(TIMEOUT_MILLISECONDS);


                return response;
            }

            return response;
        }

        public static string Receive()
        {
            var response = "Time Out";

            if (Socket != null)
            {
                var socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = Socket.RemoteEndPoint;

                socketEventArg.SetBuffer(new byte[68140], 0, 68140);

                socketEventArg.Completed += delegate(object s, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                    {
                        response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);

                        //test

                        //response = response.Trim('\0');
                    }

                    _clientDone.Set();
                };

                _clientDone.Reset();

                Socket.ReceiveAsync(socketEventArg);

                _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
            }


            return response;
        }
    }
}