using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Here_History.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Here_History
{
    public partial class CurrentPlaceHistory : PhoneApplicationPage
    {
        public CurrentPlaceHistory()
        {
            InitializeComponent();
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            await App._ViewModelLocator.Main.LoadHistoryData();
        }

        private void historyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(historyListBox.SelectedItem == null)
                return;
            var content = historyListBox.SelectedItem as HistoryDataModel;

            PhoneApplicationService.Current.State["param"] = content;
            NavigationService.Navigate(new Uri("/HistoryDetail.xaml", UriKind.RelativeOrAbsolute));
            historyListBox.SelectedIndex = -1;
        }
    }
}