using System.Collections.Generic;
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
    public sealed partial class FriendsOverviewPage : Page
    {
        //TODO remove new 
        public ObservableCollection<User> Friends = new ObservableCollection<User>();
        private UwpUser user;


        public FriendsOverviewPage()
        {
            InitializeComponent();
        }

        // Gets the loged in user and puts this as logedinuser and put all his friends in a list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = (UwpUser)e.Parameter;
            //TODO fix it because friends are now IDS of users instead of friends
            //if (LogedInUser != null) Friends = new ObservableCollection<User>(LogedInUser.Friends);
            if (user != null) FriendList.ItemsSource = user.user.Friends;
        }

        private void FriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO Chage selected friend!
            SelectedFriend.Text = "Ketameme";
        }

        // Navigates to the addfriend Page and also send the LogedInUser to the page so you can add the friend.
        private void AddFriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddFriendPage), user);
        }
    }
}