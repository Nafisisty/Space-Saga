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
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Here_History
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void currentPlaceButton_Click(object sender, RoutedEventArgs e)
        {   
            NavigationService.Navigate(new Uri("/CurrentPlaceHistory.xaml", UriKind.RelativeOrAbsolute));
        }

        private void augmentedRealityButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/YoursAround.xaml", UriKind.RelativeOrAbsolute));
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchHistory.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Geoposition geoposition;
                geoposition = await GetCurrentLocation();

                var la = geoposition.Coordinate.Latitude.ToString("0.000000");
                var lo = geoposition.Coordinate.Longitude.ToString("0.000000");

                Debug.WriteLine("latitude : " + la + " longtitude : " + lo);

                if (App._ViewModelLocator.Main.CheckNetworkConnection())
                {
                    await
                        App._ViewModelLocator.Main.LoadHistoryData(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
                }
                else
                {
                    MessageBox.Show("There is no internet connection found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("this is catch");
            }

            loading_page.Visibility = Visibility.Collapsed;
        }

        //Method for collecting Current location information of the user.
        private static async Task<Geoposition> GetCurrentLocation()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;

            Geoposition geoposition = await locator.GetGeopositionAsync(
                TimeSpan.FromMinutes(1),
                TimeSpan.FromSeconds(30)
                );
            return geoposition;
        }

        private void Add_appbarbtn_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddHistory.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}