using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SharedCodePortable;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindYourBuddiesApp.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddFriendPage : Page
    {

        public User LogedInUser;
        //TODO remove new bla bla and put this in the searchrequest button.
        public ObservableCollection<User> MatchingUsers = new ObservableCollection<User>();
        public AddFriendPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LogedInUser = (User) e.Parameter;
        }


        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FriendsOverviewPage), LogedInUser);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO send request with the requested username
        }

        private void AddFriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO adds the clickedPerson
        }
    }
}