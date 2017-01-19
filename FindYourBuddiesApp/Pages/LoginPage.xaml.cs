using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

 
        private void CreateAccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccountPage));
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            string user = "Nan";
            string pass = "Nan";
            if (UserNameTb.Text != "" && PasswordTb.Password != "")
            {
                user = UserNameTb.Text;
                pass = PasswordTb.Password;
            }

            //DO login stuff checks etc.

            LoginRequest r = new LoginRequest
            {
                username = user,
                password = pass
            };
            Packet p = new Packet {PacketType = EPacketType.LoginRequest, Payload = JsonConvert.SerializeObject(r)};

            TcpClient.DoRequest(p, LoginCallback);


            //this.Frame.Navigate(typeof(MainPage));
        }

        private async void LoginCallback(Packet obj)
        {
            var response = JsonConvert.DeserializeObject<LoginResponse>(obj.Payload);
            if (response.succes)
            {
                //await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                //{
                    GetUserRequest request = new GetUserRequest {username = response.username};
                    Packet p = new Packet
                    {
                        PacketType = EPacketType.GetUserRequest,
                        Payload = JsonConvert.SerializeObject(request)
                    };

                    TcpClient.DoRequest(p, GetUserResponseCallback);
                //});
            }
            else
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Wrong credentials",
                        MaxWidth = ActualWidth
                    };
                    dialog.PrimaryButtonText = "OK";

                    var result = await dialog.ShowAsync();
                });
            }

        }

        private async void GetUserResponseCallback(Packet packet)
        {
            var response = JsonConvert.DeserializeObject<GetUserResponse>(packet.Payload);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame.Navigate(typeof(MainPage), response.user);
            });
        }
    }
}