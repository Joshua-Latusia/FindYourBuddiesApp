using System;
using System.Diagnostics;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.Devices.PointOfService;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using FindYourBuddiesApp.PageModels;
using Newtonsoft.Json;
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
        private UwpUser user;
        public MapDisplayPage()
        {
            InitializeComponent();
            startRefreshing();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = (UwpUser) e.Parameter;
            MapHandler.DrawUser(MyMap,user,false,false);
            MapHandler.center(MyMap, user.location);
        }

        private void startRefreshing()
        {
            Timer Refresher = new Timer(timerCallback, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
        }

        private async void timerCallback(object state)
        {
            var RefreshRequest = JsonConvert.SerializeObject(new RefreshRequest() {user = user.user});
            
            var packet = new Packet() {PacketType = EPacketType.RefreshRequest, Payload = RefreshRequest};

            TcpClient.DoRequest(packet, ResponseCallback);
        }

        private async void ResponseCallback(Packet packet)
        {
            user.user = JsonConvert.DeserializeObject<User>(packet.Payload);

            //send request for friends

            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                MapHandler.DrawUser(MyMap, user, true, false);
                MapHandler.center(MyMap, user.location);
                Debug.WriteLine("update user");
            });
        }

        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var b = (Button) sender;
        //    var selected = (User) b.DataContext;
        //    var loc = new Geopoint(new BasicGeoposition {Latitude = selected.Position.Latitude, Longitude = selected.Position.Longitude});
        //    Map.Center = loc;
        //}
    }

   
}