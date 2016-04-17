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
using GART.Data;
using GART;
using GART.BaseControls;
using GART.Controls;

namespace Here_History
{
    public partial class YoursAround : PhoneApplicationPage
    {
        private readonly GeoCoordinateWatcher _GeoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
        private ObservableCollection<ARItem> locations;
        private GeoPosition<GeoCoordinate> CurrentPosition { get; set; }

        public YoursAround()
        {
            InitializeComponent();

            _GeoWatcher.PositionChanged += (o, args) => Dispatcher.BeginInvoke(() =>
        {
        CurrentPosition = _GeoWatcher.Position;

        locations = new ObservableCollection<ARItem>
        {
            new ShortHistory()
            {
                GeoLocation = new GeoCoordinate(23.719039, 90.409319), 
                Content = "NorthSouth University Dhaka",
                Description = GetLocationText(23.719039, 90.409319)
            },
            new ShortHistory()
            {
                GeoLocation = new GeoCoordinate(23.810332, 90.412518), 
                Content = "Indpendent University Dhaka",
                Description = GetLocationText(23.810332, 90.412518)
            }
        };

        ardisplay.ARItems = locations;
    });

            _GeoWatcher.Start();
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
    }
}