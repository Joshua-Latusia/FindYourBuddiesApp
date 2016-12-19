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
            points = new ObservableCollection<User>();
            generateGeoPoints();
        }

        public ObservableCollection<User> points { get; set; }

        public void generateGeoPoints()
        {
            var p1 = new Point {Longitude = 4.797045799999978, Latitude = 51.5819335};
            var p2 = new Point {Longitude = 4.794887000000017, Latitude = 51.5773701};
            var p3 = new Point {Longitude = 4.789343799999983, Latitude = 51.5772772};
            var p4 = new Point {Longitude = 4.789675699999975, Latitude = 51.5855821};
            var user1 = new User("Amphia", p1);
            var user2 = new User("Cas", p2);
            var user3 = new User("Jos", p3);
            var user4 = new User("Avans", p4);
            points.Add(user1);
            points.Add(user2);
            points.Add(user3);
            points.Add(user4);
        }
    }
}