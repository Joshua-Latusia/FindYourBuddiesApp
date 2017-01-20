using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Devices.Sensors;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using SharedCode;
using SharedCode.Packets;
using SharedCodePortable;
using SharedCodePortable.Packets;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindYourBuddiesApp.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapDisplayPage : Page
    {
        private UwpUser _user;
        public static DispatcherTimer timer;
        public static bool timerstarted = false;
        public IList<Geofence> geofences;
        public Geofence CurrentGeofence;

        public MapDisplayPage()
        {
            InitializeComponent();
            GeofenceMonitor.Current.GeofenceStateChanged += CurrentOnGeofenceStateChanged;
            //startRefreshing();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _user = (UwpUser)e.Parameter;
            MapHandler.DrawUser(MyMap, _user, false, false);
            MapHandler.Center(MyMap, _user.Location);
            GeofenceMonitor.Current.GeofenceStateChanged += CurrentOnGeofenceStateChanged;

            var refreshRequest = JsonConvert.SerializeObject(new RefreshRequest { user = _user.User });

            var packet = new Packet { PacketType = EPacketType.RefreshRequest, Payload = refreshRequest };

            TcpClient.DoRequest(packet, ResponseCallback);

            StartTimer();
        }

        private async void CurrentOnGeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            var reports = sender.ReadReports();

            await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.High, agileCallback: (async () =>
                {
                   foreach (GeofenceStateChangeReport report in reports)
                    {
                        switch (report.NewState)
                        {
                            case GeofenceState.Removed:

                                break;

                            case GeofenceState.Entered:
                                Debug.WriteLine("entered");
                                string username = report.Geofence.Id;
                                CurrentGeofence = report.Geofence;
                                timer.Stop();
                                var packet = new Packet()
                                {
                                    PacketType = EPacketType.AllFriendsRequest,
                                    Payload =
                                        JsonConvert.SerializeObject(new RequestAllFriends()
                                        {
                                            idList = _user.User.Friends
                                        })
                                };

                                TcpClient.DoRequest(packet, GeofenceResponseCallback);
                               break;

                            case GeofenceState.Exited:
                                Debug.WriteLine("kekekekekekekekekekkeke");
                                break;
                        }
                    }
                }
            ));
        }

        private async void GeofenceResponseCallback(Packet packet)
        {
            List<User> friends = JsonConvert.DeserializeObject<List<User>>(packet.Payload);
            foreach (User u in friends)
            {
                if (u.UserName == CurrentGeofence.Id)
                {
                    string title = "you're in range of" + u.FirstName + " " + u.LastName;
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                    {
                        var dialog = new ContentDialog
                        {
                            Title = title,
                            MaxWidth = ActualWidth
                        };
                        dialog.PrimaryButtonText = "OK";

                        var result = await dialog.ShowAsync();
                    });
                }
            }
        }

        private void AddGeofence(Geopoint location, string title, double radius)
        {
            string fenceKey = title;
            // the geofence is a circular region:
            Geocircle geocircle = new Geocircle(location.Position, radius);

            try
            {
                GeofenceMonitor.Current.Geofences.Add(new Geofence(fenceKey, geocircle, MonitoredGeofenceStates.Entered, false, TimeSpan.FromSeconds(0.1)));

            }
            catch (Exception e)
            {
                Debug.WriteLine("catched" + e);
            }

        }



        //private void startRefreshing()
        //{
        //    Timer Refresher = new Timer(timerCallback, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
        //}


        private void StartTimer()
        {
            timerstarted = true;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += TimerOnTick;
            timer.Start();
        }


        private void TimerOnTick(object sender, object o)
        {
            var refreshRequest = JsonConvert.SerializeObject(new RefreshRequest { user = _user.User });

            var packet = new Packet { PacketType = EPacketType.RefreshRequest, Payload = refreshRequest };

            TcpClient.DoRequest(packet, ResponseCallback);
        }

        // ff de t weer hoofdletter
        //private void Starttimer()
        //{
        //    TimeSpan delay = TimeSpan.FromSeconds(10);

        //    ThreadPoolTimer.CreatePeriodicTimer(
        //        source =>
        //        {
        //            var refreshRequest = JsonConvert.SerializeObject(new RefreshRequest { user = _user.User });

        //            var packet = new Packet { PacketType = EPacketType.RefreshRequest, Payload = refreshRequest };

        //            TcpClient.DoRequest(packet, ResponseCallback);
        //        }, delay);
           
        //}

        private async void ResponseCallback(Packet packet)
        {
            if (packet.Payload != null)
            {
                var friends = JsonConvert.DeserializeObject<AllFriendsResponse>(packet.Payload);

                await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    GeofenceMonitor.Current.Geofences.Clear();
                    MapHandler.DrawUser(MyMap, _user, true, false);

                    foreach (User u in friends.friends)
                    {
                        UwpUser friend = new UwpUser(u, "");
                        AddGeofence(friend.Location, friend.User.UserName, 20);
                        MapHandler.DrawUser(MyMap, friend, true, true);
                    }
                    Debug.WriteLine("update friend");
                });
            }
        }

        private void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            _user.Location = new Geopoint(new BasicGeoposition() {Latitude = 51.5840049, Longitude = 4.7972440999999435 });
            _user.UpdateLocation();
        }

    }


   
}