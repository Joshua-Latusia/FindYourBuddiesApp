using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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


        //TODO add Navigation change 
        private void CreateAccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccountPage));
        }

        //TODO add Login stuff
        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            //DO login stuff checks etc.

            Utils.Connect(Utils.IP_ADRESS, Utils.Port);

            LoginButton.Content = "Woop WOop";


            //this.Frame.Navigate(typeof(MainPage));
        }
    }
}