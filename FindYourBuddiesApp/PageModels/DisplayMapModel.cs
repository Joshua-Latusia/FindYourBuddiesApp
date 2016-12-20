using System.Collections.ObjectModel;
using Windows.Devices.Geolocation;
using SharedCode;
using SharedCodePortable;
//using FindYourBuddiesApp.Data;

namespace FindYourBuddiesApp.PageModels
{
    public class DisplayMapModel
    {
        public DisplayMapModel()
        {
            Points = new ObservableCollection<UwpUser>();
            GenerateGeoPoints();
        }

        public ObservableCollection<UwpUser> Points { get; set; }

        public void GenerateGeoPoints()
        {
            var p1 = new Point {Longitude = 4.797045799999978, Latitude = 51.5819335};
            var p2 = new Point {Longitude = 4.794887000000017, Latitude = 51.5773701};
            var p3 = new Point {Longitude = 4.789343799999983, Latitude = 51.5772772};
            var p4 = new Point {Longitude = 4.789675699999975, Latitude = 51.5855821};
            var user1 = new UwpUser(new User("Amphia", p1));
            var user2 = new UwpUser(new User("Cas", p2));
            var user3 = new UwpUser(new User("Jos", p3));
            var user4 = new UwpUser(new User("Avans", p4));
            Points.Add(user1);
            Points.Add(user2);
            Points.Add(user3);
            Points.Add(user4);
        }

       
    }
}