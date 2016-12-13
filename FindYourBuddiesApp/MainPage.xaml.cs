using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FindYourBuddiesApp.Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FindYourBuddiesApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Frame.Navigate(typeof(MapDisplayPage));
            Map.IsSelected = true;
        }

        private void MenuListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Map.IsSelected)
            {
                Frame.Navigate(typeof(MapDisplayPage));
                BackButton.Visibility = Visibility.Collapsed;
                PageName.Text = "Find your buddies!";
            }
            else if (Friends.IsSelected)
            {
                Frame.Navigate(typeof(FriendsOverviewPage));
                BackButton.Visibility = Visibility.Visible;
                PageName.Text = "All your buddies!";
            }
            else if (Meeting.IsSelected)
            {
                // TODO maybe implement this page
            }
            else if (Settings.IsSelected)
            {
                // TODO maybe implement this page
            }
            else if (Help.IsSelected)
            {
                // TODO maybe implement this page
            }
        }

       

        private void HamburgerButton_OnClick(object sender, RoutedEventArgs e)
        {
            HamburgerSplitview.IsPaneOpen = !HamburgerSplitview.IsPaneOpen;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapDisplayPage));
            Map.IsSelected = true;
            BackButton.Visibility = Visibility.Collapsed;
            PageName.Text = "Find your buddies!";
        }

    }
}
