using System;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using SharedCode.Packets;
using SharedCodePortable;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindYourBuddiesApp.Pages
{
    /// <summary>
    ///     This Page shows the friends of the loged in user
    /// </summary>
    public sealed partial class FriendsOverviewPage
    {
        
        public ObservableCollection<User> Friends;
        
        // The loged in user.
        private UwpUser _user;


        public FriendsOverviewPage()
        {
            InitializeComponent();
        }

        // Gets the user from previous page and puts this as _user and put all his friends in a list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {           
            _user = (UwpUser)e.Parameter;

            Update();
            Friends = new ObservableCollection<User>();
                     

            if (_user != null) FriendList.ItemsSource = _user.User.Friends;
        }

        private async void UpdateUser(Packet packet)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var response = JsonConvert.DeserializeObject<GetUserResponse>(packet.Payload);
            _user = new UwpUser (response.user);
            });
        }

        private async void UpdateFriends()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RequestAllFriends r = new RequestAllFriends
                {
                    idList = _user.User.Friends
                };

                Packet p = new Packet
                {
                    PacketType = EPacketType.RequestAllFriends,
                    Payload = JsonConvert.SerializeObject(r)
                };

                TcpClient.DoRequest(p, GetFriendsResponseCallback);
            });
        }

        private void Update()
        {
           
                GetUserRequest req = new GetUserRequest { username = _user.User.UserName };

                Packet packet = new Packet
                {
                    PacketType = EPacketType.GetUserRequest,
                    Payload = JsonConvert.SerializeObject(req)
                };

                TcpClient.DoRequest(packet, UpdateUser);

        }

        private void FriendButton_OnClick(object sender, RoutedEventArgs e)
        {

            SelectedFriend.Text = Friends[FriendList.SelectedIndex].UserName;
        }

        // Wat te doen als je alle vrienden terug krijgt
        private void GetFriendsResponseCallback(Packet packet)
        {
            //var response = JsonConvert.DeserializeObject<GetUserResponse>(packet.Payload);

                RequestAllFriends r = new RequestAllFriends
                {
                    idList = _user.User.Friends
                };

                Packet p = new Packet
                {
                    PacketType = EPacketType.RequestAllFriends,
                    Payload = JsonConvert.SerializeObject(r)
                };

                TcpClient.DoRequest(p, ShowFriendsCallBack);

           
        }

        private async void ShowFriendsCallBack(Packet packet)
        {
            var response = JsonConvert.DeserializeObject<AllFriendsResponse>(packet.Payload);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var user in response.friends)
                {
                    Friends.Add(user);
                }

            });
        }

        // Navigates to the addfriend Page and also send the LogedInUser to the page so you can add the friend.
        private void AddFriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddFriendPage), _user);
        }



        // Calls on a webrequest for the current user to get the friends and show them in the overview using databinding and item template.
        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            Friends.Clear();
            UpdateFriends();
        }

        private void FriendList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFriend.Text = Friends[FriendList.SelectedIndex].UserName;
        }
    }
}