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
    public partial class SearchResult : PhoneApplicationPage
    {
        public SearchResult()
        {
            InitializeComponent();
        }

        private void searchedHistoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchedHistoryListBox.SelectedItem == null)
                return;
            var content = searchedHistoryListBox.SelectedItem as HistoryDataModel;

            PhoneApplicationService.Current.State["param"] = content;
            NavigationService.Navigate(new Uri("/HistoryDetail.xaml", UriKind.RelativeOrAbsolute));
            searchedHistoryListBox.SelectedIndex = -1;
        }
    }
}