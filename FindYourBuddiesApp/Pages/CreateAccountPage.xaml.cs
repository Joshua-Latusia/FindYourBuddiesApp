using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindYourBuddiesApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAccountPage : Page
    {
        //TODO set to false when username checking works
        private bool _isUsernameAvailable = true;
        private bool _isPasswordMatching = false;

        public CreateAccountPage()
        {
            this.InitializeComponent();
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
             
            if (FirstNameBox.Text.Length != 0 && LastNameBox.Text.Length != 0 &&
                UsernameBox.Text.Length != 0 && _isUsernameAvailable && 
                PasswordConfirmBox.Text.Length != 0 && _isPasswordMatching &&
                AgeBox.Text.Length != 0)
            {
                int parsedValue;
                if (int.TryParse(AgeBox.Text, out parsedValue))
                {
                    if (parsedValue > 1 && parsedValue < 150)
                    {
                        CreateNewAccount();
                        this.Frame.Navigate(typeof(LoginPage));
                    }
                    else
                    {
                        CreationErrorDialog();
                    }
                }
                else{
                    CreationErrorDialog();
                }
            }
            else
            {
                CreationErrorDialog();
            }
            
        }

        private async void CreationErrorDialog()
        {
            ContentDialog dialog = new ContentDialog()
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
            if (UsernameBox.Text.Length != 0 && PasswordBox.Text.Length != 0)
            {
                var list = new List<string> {UsernameBox.Text, PasswordBox.Text};
                this.Frame.Navigate(typeof(LoginPage),list);
            }
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
