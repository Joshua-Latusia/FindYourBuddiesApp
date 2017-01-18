using System;
using System.Collections.Generic;
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
    public sealed partial class CreateAccountPage : Page
    {
        private bool _isPasswordMatching;
        //TODO set to false when username checking works
        private bool _isUsernameAvailable = true;

        public CreateAccountPage()
        {
            InitializeComponent();
        }

        // This page needs connection with the server to either create account or check if username is available
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void UsernameBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var name = "Nan";
            if (UsernameBox.Text != "")
            {
                name = UsernameBox.Text;
            }
            CheckUsernameRequest r = new CheckUsernameRequest()
            {
                username = name
            };

            Packet p = new Packet()
            {
                PacketType = EPacketType.CheckUsernameRequest, Payload =  JsonConvert.SerializeObject(r)
            };

            //TODO if crashing when inserting username comment this line
            TcpClient.DoRequest(p, CheckUsernameRequestCallback);
        }

        private async void CheckUsernameRequestCallback(Packet obj)
        {
            var response = JsonConvert.DeserializeObject<LoginResponse>(obj.Payload);
            if (response.succes)
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    NameAvailableTb.Text = "Name is Available";
                    _isUsernameAvailable = true;
                });
            }
            else
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    NameAvailableTb.Text = "Name is taken";
                    _isUsernameAvailable = false;
                });
            }
        }

        private void PasswordConfirmBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == PasswordConfirmBox.Text)
            {
                PasswordCheckTb.Text = "Passwords are matching";
                _isPasswordMatching = true;
            }
            else
            {
                PasswordCheckTb.Text = "Passwords are not matching";
                _isPasswordMatching = false;
            }
        }

        private void CreateAccount_OnClick(object sender, RoutedEventArgs e)
        {
            if ((FirstNameBox.Text.Length != 0) && (LastNameBox.Text.Length != 0) &&
                (UsernameBox.Text.Length != 0) && _isUsernameAvailable &&
                (PasswordConfirmBox.Text.Length != 0) && _isPasswordMatching &&
                (AgeBox.Text.Length != 0))
            {
                int parsedValue;
                if (int.TryParse(AgeBox.Text, out parsedValue))
                    if ((parsedValue > 1) && (parsedValue < 150))
                    {
                        CreateNewAccount();
                        Frame.Navigate(typeof(LoginPage));
                    }
                    else
                    {
                        CreationErrorDialog();
                    }
                else CreationErrorDialog();
            }
            else
            {
                CreationErrorDialog();
            }
        }

        private async void CreationErrorDialog()
        {
            var dialog = new ContentDialog
            {
                Title = "Account creation failed",
                PrimaryButtonText = "Ok",
                Content = new TextBlock
                {
                    Text = "Some fields are not correct\n",
                    FontSize = 24
                }
            };

            await dialog.ShowAsync();
        }

        private void CreateNewAccount()
        {
            NewAccountRequest r = new NewAccountRequest()
            {
                firstname = FirstNameBox.Text,
                age = Int32.Parse(AgeBox.Text),
                isman = ToggleSwitch.IsOn,
                lastname = LastNameBox.Text,
                username = UsernameBox.Text,
                password = PasswordBox.Text,
                friends = new List<User>()

            };

            Packet p = new Packet()
            { PacketType = EPacketType.NewAccountRequest, Payload = JsonConvert.SerializeObject(r) };

            TcpClient.DoRequest(p, NewAccountCallback);





        }

        private async void NewAccountCallback(Packet obj)
        {
            var response = JsonConvert.DeserializeObject<LoginResponse>(obj.Payload);
            if (response.succes)
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {

                    var dialog = new ContentDialog()
                    {
                        Title = "Created account",
                        MaxWidth = this.ActualWidth
                    };
                    dialog.PrimaryButtonText = "OK";

                    var result = await dialog.ShowAsync();
                });
            }
            else
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "Account creation failed",
                        MaxWidth = this.ActualWidth
                    };
                    dialog.PrimaryButtonText = "OK";

                    var result = await dialog.ShowAsync();
                });
            }
        }

        private void BackToInlog_OnClick(object sender, RoutedEventArgs e)
        {
            if ((UsernameBox.Text.Length != 0) && (PasswordBox.Text.Length != 0))
            {
                var list = new List<string> {UsernameBox.Text, PasswordBox.Text};
                Frame.Navigate(typeof(LoginPage), list);
            }
            Frame.Navigate(typeof(LoginPage));
        }
    }
}