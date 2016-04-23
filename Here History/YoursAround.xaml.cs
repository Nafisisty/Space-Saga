using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GART.Data;
using GART;
using GART.BaseControls;
using GART.Controls;
using Here_History.Models;

namespace Here_History
{
    public partial class YoursAround : PhoneApplicationPage
    {

        private ObservableCollection<ARItem> locations;
        private GeoPosition<GeoCoordinate> CurrentPosition { get; set; }

        public  YoursAround()
        {
            InitializeComponent();

            App._GeoWatcher.PositionChanged += (o, args) => Dispatcher.BeginInvoke(() =>
        {
        CurrentPosition = App._GeoWatcher.Position;

        locations = new ObservableCollection<ARItem>();

        foreach (var aHistory in App._ViewModelLocator.Main.History)
        {
            double latitude = Convert.ToDouble(aHistory.Latitude);
            double longitude = Convert.ToDouble(aHistory.Longitude);

            locations.Add(new ShortHistory()
            {
                HistoryId = aHistory.HistoryId,
                GeoLocation = new GeoCoordinate(latitude, longitude),
                Content = aHistory.Title,
                Description = GetLocationText(latitude, longitude)
            });
        }

        ardisplay.ARItems = locations;
    });

            App._GeoWatcher.Start();
        }

        private string GetLocationText(double lat, double lon)
        {
            if (CurrentPosition != null && CurrentPosition.Location != null)
            {
                var start = new GeoCoordinate
                      (CurrentPosition.Location.Latitude,
                       CurrentPosition.Location.Longitude);
                var end = new GeoCoordinate(lat, lon);
                var distance = start.GetDistanceTo(end);

                return distance < 1000
                    ? string.Format("{0}m away.", Math.Round((double)distance, 0))
                    : string.Format("{0}km away.", Math.Round((double)distance / 1000, 2));
            }

            return string.Empty;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop AR services
            ardisplay.StopServices();

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Start AR services
            ardisplay.StartServices();

            base.OnNavigatedTo(e);
        }


        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);

            if (ardisplay != null)
            {
                var orientation = ControlOrientation.Default;

                switch (e.Orientation)
                {
                    case PageOrientation.LandscapeLeft:
                        orientation = ControlOrientation.Clockwise270Degrees;
                        ardisplay.Visibility = Visibility.Visible;
                        break;
                    case PageOrientation.LandscapeRight:
                        orientation = ControlOrientation.Clockwise90Degrees;
                        ardisplay.Visibility = Visibility.Visible;
                        break;

                }

                ardisplay.Orientation = orientation;
            }
        }

        private void WorldView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Debug.WriteLine("" + ((ShortHistory)worldView.SelectedItem).Content);
            if(worldView.SelectedIndex == -1 || worldView.SelectedItem == null)
                return;

            ShortHistory selectedHistory = (ShortHistory)worldView.SelectedItem;
            PhoneApplicationService.Current.State["param"] = App._ViewModelLocator.Main.History.FirstOrDefault(x => x.HistoryId == selectedHistory.HistoryId);

            NavigationService.Navigate(new Uri("/HistoryDetail.xaml", UriKind.RelativeOrAbsolute));

            worldView.SelectedIndex = -1;
        }
    }
}