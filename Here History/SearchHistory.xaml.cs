using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Here_History
{
    public partial class SearchHistory : PhoneApplicationPage
    {
        public SearchHistory()
        {
            InitializeComponent();
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            await App._ViewModelLocator.Main.LoadSeachedHistoryData(searchTextBox.Text);

            NavigationService.Navigate(new Uri("/SearchResult.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}