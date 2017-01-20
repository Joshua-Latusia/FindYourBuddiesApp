﻿using System;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
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
    public sealed partial class AddFriendPage : Page
    {

        // public User LogedInUser;
        private UwpUser _user;
        
        public ObservableCollection<User> MatchingUsers;
        public AddFriendPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //LogedInUser = (User) e.Parameter;
            _user = (UwpUser)e.Parameter;
            MatchingUsers = new ObservableCollection<User>();
          
        }


        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FriendsOverviewPage), _user);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (UserNameTb.Text != "")
            {
                string user = UserNameTb.Text;
                
                SearchUsernameRequest r = new SearchUsernameRequest
                {
                    username = user
                };

                Packet p = new Packet
                {
                    PacketType = EPacketType.SearchUsernameRequest, Payload = JsonConvert.SerializeObject(r)
                };

                TcpClient.DoRequest(p, SearchUserCallback);
            }
        }

        private async void SearchUserCallback(Packet obj)
        {
            var response = JsonConvert.DeserializeObject<SearchUsernameResponse>(obj.Payload);
            if (response.succes)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    //MatchingUsers = new ObservableCollection<User>(response.users);
                    MatchingUsers.Clear();
                    foreach (var user in response.users)
                    {
                        MatchingUsers.Add(user);
                    }
                });
            }
        }



        private async void AddFriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultsList.SelectedIndex >= 0)
            {
                if (MatchingUsers[ResultsList.SelectedIndex] != null)
                {
                    AddFriendRequest r = new AddFriendRequest
                    {
                        logedinUser = _user.User.UserName,
                        friendUsername = MatchingUsers[ResultsList.SelectedIndex].UserName
                    };
                    Packet p = new Packet
                    {
                        PacketType = EPacketType.AddFriendRequest,
                        Payload = JsonConvert.SerializeObject(r)
                    };

                    TcpClient.DoRequest(p, AddFriendCallBack);
                    
                }
            }
            else
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                 {
                     var dialog = new ContentDialog
                     {
                         Title = "No user selected",
                         MaxWidth = ActualWidth
                     };
                     dialog.PrimaryButtonText = "OK";

                     var result = await dialog.ShowAsync();
                 });
            }
        }

        private async void AddFriendCallBack(Packet obj)
        {
            var response = JsonConvert.DeserializeObject<SuccesResponse>(obj.Payload);
            if (response.succes)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Friend added",
                        MaxWidth = ActualWidth
                    };
                    dialog.PrimaryButtonText = "OK";

                    var result = await dialog.ShowAsync();
                });
            }
        }
    }
}