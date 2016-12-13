using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SharedCode;
using FindYourBuddiesApp.PageModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FindYourBuddiesApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapDisplayPage : Page
    {
        private DisplayMapModel model;
        public MapDisplayPage()
        {
            this.InitializeComponent();
            model = new DisplayMapModel();
            DataContext = model;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            var selected = (User)b.DataContext;
            Map.Center = selected.Position;
        }
    }
}
