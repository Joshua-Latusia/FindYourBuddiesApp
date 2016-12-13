using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class LoginPage : Page
    {
        

        public LoginPage()
        {
            this.InitializeComponent();
        }




        //TODO add Navigation change 
        private void CreateAccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateAccountPage));
        }

        //TODO add Login stuff
        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            //DO login stuff checks etc.

            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
