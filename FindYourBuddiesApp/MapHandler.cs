using System;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace FindYourBuddiesApp
{
    public class MapHandler
    {
        public static IRandomAccessStreamReference UserImage = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/PlayerPin.png"));
        public static IRandomAccessStreamReference FriendImage = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/BuddyIcon4.png"));

        public static void DrawUser(MapControl map, UwpUser user, bool redraw, bool friend)
        {
            if (redraw)
            {
                foreach (MapElement m in map.MapElements)
                {
                    if (m is MapIcon)
                    {
                        MapIcon icon = (MapIcon) m;
                        if (icon.Title == user.User.UserName)
                        {
                            map.MapElements.Remove(icon);
                            break;
                        }
                    }
                }
            }

            MapIcon userIcon = null;
            if(friend){ userIcon = new MapIcon { Location = user.Location, Title = user.User.UserName, Image = FriendImage}; }
            else      { userIcon = new MapIcon { Location = user.Location, Title = user.User.UserName, Image = UserImage}; }

            map.MapElements.Add(userIcon);
        }

        public static async void DrawRoute(MapControl map, UwpUser user, UwpUser friend)
        {
            map.Routes.Clear();
            MapRouteFinderResult result = await MapRouteFinder.GetWalkingRouteAsync(user.Location, friend.Location);
            MapRouteView route = new MapRouteView(result.Route);
            route.RouteColor = Colors.Blue;
            map.Routes.Add(route);
        }

        public static void Center(MapControl map, Geopoint location)
        {
            map.Center = location;
        }

    }
}