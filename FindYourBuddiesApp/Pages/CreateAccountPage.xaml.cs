using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
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
        private readonly bool _isUsernameAvailable = true;

        public CreateAccountPage()
        {
            InitializeComponent();
        }

        // This page needs connection with the server to either create account or check if username is available
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dynamic inlogRequest = new LoginRequest {username = "Admin", password = "Admin"};
            var packet = new Packet
            {
                PacketType = EPacketType.LoginRequest,
                Token = "",
                Payload = JsonConvert.SerializeObject(inlogRequest)
            };
        }

        private void UsernameBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO check if name is available with server
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
            //TODO create acount here
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