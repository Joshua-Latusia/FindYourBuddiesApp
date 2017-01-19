﻿using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Foundation.Metadata;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace FindYourBuddiesApp
{
    public class MapHandler
    {
        public static IRandomAccessStreamReference userImage = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/user.png"));
        public static IRandomAccessStreamReference friendImage = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/friend.png"));

        public static void DrawUser(MapControl map, UwpUser user, bool redraw, bool friend)
        {
            if (redraw)
            {
                foreach (MapElement m in map.MapElements)
                {
                    if (m is MapIcon)
                    {
                        MapIcon icon = (MapIcon) m;
                        if (icon.Title == user.user.UserName)
                        {
                            map.MapElements.Remove(icon);
                            break;
                        }
                    }
                }
            }

            MapIcon userIcon = null;
            if(friend){ userIcon = new MapIcon() { Location = user.location, Title = user.user.UserName, Image = friendImage}; }
            else      { userIcon = new MapIcon() { Location = user.location, Title = user.user.UserName, Image = userImage}; }

            map.MapElements.Add(userIcon);
        }

        public static async void drawRoute(MapControl map, UwpUser user, UwpUser friend)
        {
            map.Routes.Clear();
            MapRouteFinderResult result = await MapRouteFinder.GetWalkingRouteAsync(user.location, friend.location);
            MapRouteView route = new MapRouteView(result.Route);
            route.RouteColor = Colors.Blue;
            map.Routes.Add(route);
        }

    }
}