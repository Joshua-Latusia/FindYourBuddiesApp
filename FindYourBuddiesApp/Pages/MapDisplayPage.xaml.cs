using System;
using System.Diagnostics;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
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
        public MapDisplayPage()
        {
            InitializeComponent();
            //startRefreshing();
            StartTimer();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _user = (UwpUser) e.Parameter;
            MapHandler.DrawUser(MyMap,_user,false,false);
            MapHandler.Center(MyMap, _user.Location);
        }

        //private void startRefreshing()
        //{
        //    Timer Refresher = new Timer(timerCallback, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
        //}

        private void StartTimer()
        {
            TimeSpan delay = TimeSpan.FromSeconds(10);

            ThreadPoolTimer.CreateTimer(
                source =>
                {
                    var refreshRequest = JsonConvert.SerializeObject(new RefreshRequest { user = _user.User });

                    var packet = new Packet { PacketType = EPacketType.RefreshRequest, Payload = refreshRequest };

                    TcpClient.DoRequest(packet, ResponseCallback);
                }, delay);
        }

        private async void ResponseCallback(Packet packet)
        {
            if (packet.Payload != null)
            {
                var friends = JsonConvert.DeserializeObject<AllFriendsResponse>(packet.Payload);

                await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    MapHandler.DrawUser(MyMap, _user, true, false);

                    foreach (User u in friends.friends)
                    {
                        UwpUser friend = new UwpUser(u, "");
                        MapHandler.DrawUser(MyMap, friend, true, true);
                    }
                    Debug.WriteLine("update friend");
                });
            }
        }
    }

   
}