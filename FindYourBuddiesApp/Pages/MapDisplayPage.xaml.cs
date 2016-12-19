using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FindYourBuddiesApp.PageModels;
using SharedCodePortable;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindYourBuddiesApp.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapDisplayPage : Page
    {
        private readonly DisplayMapModel model;

        public MapDisplayPage()
        {
            InitializeComponent();
            model = new DisplayMapModel();
            DataContext = model;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var b = (Button) sender;
            var selected = (User) b.DataContext;
            var loc = new Geopoint(new BasicGeoposition {Latitude = selected.Position.Latitude, Longitude = selected.Position.Longitude});
            Map.Center = loc;
        }
    }
}