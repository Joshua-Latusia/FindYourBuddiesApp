using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using FindYourBuddiesApp.Pages;
using SharedCodePortable;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FindYourBuddiesApp
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private UwpUser user;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = new UwpUser((User)e.Parameter);
            Frame.Navigate(typeof(MapDisplayPage), user);
            Map.IsSelected = true;
        }

        private void MenuListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Map.IsSelected)
            {
                Frame.Navigate(typeof(MapDisplayPage),user);
                BackButton.Visibility = Visibility.Collapsed;
                PageName.Text = "Find your buddies!";
            }
            else if (Friends.IsSelected)
            {
                Frame.Navigate(typeof(FriendsOverviewPage),user);
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
            Frame.Navigate(typeof(MapDisplayPage),user);
            Map.IsSelected = true;
            BackButton.Visibility = Visibility.Collapsed;
            PageName.Text = "Find your buddies!";
        }
    }
}