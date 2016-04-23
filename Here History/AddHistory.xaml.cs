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
    public partial class AddHistory : PhoneApplicationPage
    {
        public AddHistory()
        {
            InitializeComponent();
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryDataModel aHistoryDataModel = new HistoryDataModel();
            aHistoryDataModel.Title = titleTextBox.Text;
            aHistoryDataModel.Latitude = latTextBox.Text;
            aHistoryDataModel.Longitude = lonTextBox.Text;
            aHistoryDataModel.Description = descriptionTextBox.Text;
            aHistoryDataModel.Summary = summaryTextBox.Text;
            aHistoryDataModel.AreaName = areaTextBox.Text;
            aHistoryDataModel.CityName = cityTextBox.Text;
            aHistoryDataModel.CountryName = countryTextBox.Text;

            string mesg = await App._ViewModelLocator.Main.AddHistory(aHistoryDataModel);

            MessageBox.Show("" + mesg);
        }
    }
}