using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FriendsOverviewPage : Page
    {
        //TODO remove new 
        public ObservableCollection<User> Friends;
        private UwpUser user;


        public FriendsOverviewPage()
        {
            InitializeComponent();
        }

        // Gets the loged in user and puts this as logedinuser and put all his friends in a list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            user = (UwpUser)e.Parameter;

            Update();


            //TODO fix it because friends are now IDS of users instead of friends
            Friends = new ObservableCollection<User>();
            //if (LogedInUser != null) Friends = new ObservableCollection<User>(LogedInUser.Friends);

            UpdateFriends();
            

            if (user != null) FriendList.ItemsSource = user.user.Friends;
        }

        private async void UpdateUser(Packet packet)
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var response = JsonConvert.DeserializeObject<GetUserResponse>(packet.Payload);
            user = new UwpUser (response.user);
            });
        }

        private async void UpdateFriends()
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                RequestAllFriends r = new RequestAllFriends()
                {
                    idList = user.user.Friends
                };

                Packet p = new Packet()
                {
                    PacketType = EPacketType.RequestAllFriends,
                    Payload = JsonConvert.SerializeObject(r)
                };

                TcpClient.DoRequest(p, GetFriendsResponseCallback);
            });
        }

        private async void Update()
        {
           
                GetUserRequest req = new GetUserRequest()
                { username = user.user.UserName };

                Packet packet = new Packet()
                {
                    PacketType = EPacketType.GetUserRequest,
                    Payload = JsonConvert.SerializeObject(req)
                };

                TcpClient.DoRequest(packet, UpdateUser);

        }

        private void FriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO Chage selected friend!
            SelectedFriend.Text = "Ketameme";
        }

        // Wat te doen als je alle vrienden terug krijgt
        private async void GetFriendsResponseCallback(Packet packet)
        {
            var response = JsonConvert.DeserializeObject<GetUserResponse>(packet.Payload);

                RequestAllFriends r = new RequestAllFriends()
                {
                    idList = user.user.Friends
                };

                Packet p = new Packet()
                {
                    PacketType = EPacketType.RequestAllFriends,
                    Payload = JsonConvert.SerializeObject(r)
                };

                TcpClient.DoRequest(p, ShowFriendsCallBack);

           
        }

        private async void ShowFriendsCallBack(Packet packet)
        {
            var response = JsonConvert.DeserializeObject<AllFriendsResponse>(packet.Payload);
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
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
            Frame.Navigate(typeof(AddFriendPage), user);
        }
    }
}