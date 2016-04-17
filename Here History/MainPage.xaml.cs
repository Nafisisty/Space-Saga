using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Here_History.Resources;
using System.Diagnostics;
using Windows.Devices.Geolocation;

namespace Here_History
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void currentPlaceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var locator = new Geolocator();
                locator.DesiredAccuracyInMeters = 50;

                var position = await locator.GetGeopositionAsync();

                Geoposition geoposition = await locator.GetGeopositionAsync(
                TimeSpan.FromMinutes(1),
                TimeSpan.FromSeconds(30)
                );

                var la = geoposition.Coordinate.Latitude.ToString("0.000000");
                var lo = geoposition.Coordinate.Longitude.ToString("0.000000");

                Debug.WriteLine("latitude : " + la + " longtitude : " + lo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("this is catch");
            }

            NavigationService.Navigate(new Uri("/CurrentPlaceHistory.xaml", UriKind.RelativeOrAbsolute));
        }

        private void augmentedRealityButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/YoursAround.xaml", UriKind.RelativeOrAbsolute));
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}