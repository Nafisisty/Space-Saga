using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using GalaSoft.MvvmLight;
using Here_History.Models;
using Here_History.Support;
using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json;

namespace Here_History.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="History" /> property's name.
        /// </summary>
        public const string HistoryPropertyName = "HistoryDataModel";

        private ObservableCollection<HistoryDataModel> _history = new ObservableCollection<HistoryDataModel>();

        /// <summary>
        /// Sets and gets the HistoryDataModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<HistoryDataModel> History
        {
            get
            {
                return _history;
            }

            set
            {
                if (_history == value)
                {
                    return;
                }

                _history = value;
                RaisePropertyChanged(HistoryPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="SearchedHistory" /> property's name.
        /// </summary>
        public const string SearchedHistoryPropertyName = "HistoryDataModel";

        private ObservableCollection<HistoryDataModel> _searchedHistory = new ObservableCollection<HistoryDataModel>();

        /// <summary>
        /// Sets and gets the HistoryDataModel property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<HistoryDataModel> SearchedHistory
        {
            get
            {
                return _searchedHistory;
            }

            set
            {
                if (_searchedHistory == value)
                {
                    return;
                }

                _searchedHistory = value;
                RaisePropertyChanged(SearchedHistoryPropertyName);
            }
        }
        
        public MainViewModel()
        {
        }

        public async Task LoadHistoryData(double la, double lo)
        {
            try
            {

            ObservableCollection<HistoryDataModel> histories = (await GetHistory(la.ToString("0.000000"), lo.ToString("0.000000")));
            History.Clear();
            foreach (var historyDataModel in histories)
            {
                _history.Add(historyDataModel);   
            }

            }
            catch (Exception ex)
            {

                Debug.WriteLine("Hey, cant get any data.");
            }
        }

        public async Task<ObservableCollection<HistoryDataModel>> GetHistory(string userLatitude, string userLongitude)
        {

            string page = RestUrl.GetHistoryByGeoPosition(userLatitude, userLongitude);
            //Debug.WriteLine(page);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(page);
            Debug.WriteLine(client.BaseAddress);

            HttpResponseMessage response = await client.GetAsync("");
            string result = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(result);

            var feed = JsonConvert.DeserializeObject<ObservableCollection<HistoryDataModel>>(result);
            return feed;
        }



        public async Task LoadSeachedHistoryData(string searchKeyword)
        {
            try
            {

                ObservableCollection<HistoryDataModel> histories = (await GetSearchedHistory(searchKeyword));
                SearchedHistory.Clear();
                foreach (var historyDataModel in histories)
                {
                    _searchedHistory.Add(historyDataModel);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hey, cant get any data.");
            }
        }

        public async Task<ObservableCollection<HistoryDataModel>> GetSearchedHistory(string searchString)
        {

            string page = RestUrl.GetHistoryByString(searchString);
            //Debug.WriteLine(page);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(page);
            Debug.WriteLine(client.BaseAddress);

            HttpResponseMessage response = await client.GetAsync("");
            string result = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(result);

            var feed = JsonConvert.DeserializeObject<ObservableCollection<HistoryDataModel>>(result);
            return feed;
        }

        public async Task<string> AddHistory(HistoryDataModel historyDataModel)
        {
            string url = "http://spacesaga.azurewebsites.net/api/history/save/";
            string jsonHistoryDataModel = JsonConvert.SerializeObject(historyDataModel);

            var content = new StringContent(jsonHistoryDataModel, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var result = await client.PostAsync(new Uri(url, UriKind.Absolute), content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return "Successfully Added";
            }
            return "Adding Failed";
        }

        public bool CheckNetworkConnection()
        {
            bool ni = NetworkInterface.GetIsNetworkAvailable();
            //Debug.WriteLine("Internet is available --> " + ni);
            return ni;
        }
    }
}