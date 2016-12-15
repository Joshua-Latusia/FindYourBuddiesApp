using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Devices.Geolocation;
//using FindYourBuddiesApp.Data;
using SharedCode;

namespace FindYourBuddiesApp.PageModels
{
    public class DisplayMapModel
    {
        public ObservableCollection<User> points { get; set; }

        public DisplayMapModel()
        { 
            points = new ObservableCollection<User>();
            generateGeoPoints();
        }

        public void generateGeoPoints()
        {
            Geopoint p1 = new Geopoint(new BasicGeoposition() {Longitude = 4.797045799999978, Latitude = 51.5819335});
            Geopoint p2 = new Geopoint(new BasicGeoposition() {Longitude = 4.794887000000017, Latitude = 51.5773701});
            Geopoint p3 = new Geopoint(new BasicGeoposition() {Longitude = 4.789343799999983, Latitude = 51.5772772});
            Geopoint p4 = new Geopoint(new BasicGeoposition() {Longitude = 4.789675699999975, Latitude = 51.5855821});
            User user1 = new User("Amphia", p1);
            User user2 = new User("Cas", p2);
            User user3 = new User("Jos", p3);
            User user4 = new User("Avans", p4);
            points.Add(user1);
            points.Add(user2);
            points.Add(user3);
            points.Add(user4);
        }
    }
}