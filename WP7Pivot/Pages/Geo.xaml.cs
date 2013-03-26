using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;

namespace WP7Pivot.Pages
{
    public partial class Geo : PivotItem
    {
        GeoCoordinateWatcher watcher;
        public Geo()
        {
            InitializeComponent();
            watcher = new GeoCoordinateWatcher();
            watcher.StatusChanged += watcher_StatusChanged;
            watcher.PositionChanged += watcher_PositionChanged;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (watcher.Status == GeoPositionStatus.NoData)
            {
                watcher.Start();
            }
            else
            {
                watcher.Stop();
                startLocationButton.Content = "StartLocationService";
            }

        }

        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The Location Service is disabled or unsupported.
                    // Check to see whether the user has disabled the Location Service.
                    if (watcher.Permission == GeoPositionPermission.Denied)
                    {
                        // The user has disabled the Location Service on their device.
                        statusTextBlock.Text = "you have this application access to location.";
                    }
                    else
                    {
                        statusTextBlock.Text = "location is not functioning on this device";
                    }
                    break;

                case GeoPositionStatus.Initializing:
                    // The Location Service is initializing.
                    // Disable the Start Location button.
                    startLocationButton.IsEnabled = false;
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data.
                    // Alert the user and enable the Stop Location button.
                    statusTextBlock.Text = "location data is not available.";
                    startLocationButton.Content = "StopLocationService";
                    startLocationButton.IsEnabled = true;
                    break;

                case GeoPositionStatus.Ready:
                    // The Location Service is working and is receiving location data.
                    // Show the current position and enable the Stop Location button.
                    statusTextBlock.Text = "location data is available.";
                    startLocationButton.Content = "StopLocationService";
                    startLocationButton.IsEnabled = true;
                    break;
            }
        }
        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            latitudeTextBlock.Text = "Latitude: " + e.Position.Location.Latitude.ToString("0.000");
            longitudeTextBlock.Text = "Longitude: " + e.Position.Location.Longitude.ToString("0.000");
        }
    }
}
