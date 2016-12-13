using Windows.Devices.Geolocation;
using Windows.UI.StartScreen;

namespace FindYourBuddiesApp.Data
{
    public class User
    {
        public Geopoint Position;
        public string Name;

        public User(string name, Geopoint position)
        {
            this.Name = name;
            this.Position = position;
        } 
    }
}