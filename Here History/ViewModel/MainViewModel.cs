using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Here_History.Models;
using Here_History.Support;
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
        
        public MainViewModel()
        {
        }

        public async Task LoadHistoryData()
        {
            History.Clear();

            ObservableCollection<HistoryDataModel> histories = (await GetHistory());
            foreach (var historyDataModel in histories)
            {
                _history.Add(historyDataModel);   
            }
        }

        public async Task<ObservableCollection<HistoryDataModel>> GetHistory()
        {
            string page = RestUrl.GetHistoryByGeoPosition("111.111", "222.222");
            //Debug.WriteLine(page);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(page);
            Debug.WriteLine(client.BaseAddress);

            HttpResponseMessage response = await client.GetAsync("");
            string result = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(result);

            var feed = JsonConvert.DeserializeObject<ObservableCollection<HistoryDataModel>>(result);
            return feed;

            //HistoryDataModel historyList = new HistoryDataModel()
            //{
            //    Data = new ObservableCollection<HistoryDataModel>()
            //    {
            //        new HistoryDataModel()
            //        {
            //            HistoryId = 1,
            //            Title = "Independent University Bangladesh",
            //            Description = "This is a private university. It is situated at bashundhara. Nasa Space App Challange Bootcamp was held here.",
            //            Latitude = "23.810332",
            //            Longitude = "90.412518",
            //            CountryName = "Bangladesh",
            //            CityName = "Dhaka",
            //            AreaName = "Bashundhara",
            //            Summary = "private university",
            //        },
            //        new HistoryDataModel()
            //        {
            //            HistoryId = 2,
            //            Title = "NorthSouth University",
            //            Description = "This is a private university. It is situated at bashundhara. This is so much expensive. Rich people children study here.",
            //            Latitude = "23.719039",
            //            Longitude = "90.409319",
            //            CountryName = "Bangladesh",
            //            CityName = "Dhaka",
            //            AreaName = "Bashundhara",
            //            Summary = "Private university for rich people's children.",
            //        }

            //        //new HistoryDataModel()
            //        //{
            //        //    HistoryId = 3,
            //        //    Title = "",
            //        //    Description = "",
            //        //    Lattitude = "",
            //        //    Longitude = "",
            //        //    CountryName = "",
            //        //    CityName = "",
            //        //    VenueId = "",
            //        //    Summary = "",
            //        //},
            //        //new HistoryDataModel()
            //        //{
            //        //    HistoryId = 4,
            //        //    Title = "",
            //        //    Description = "",
            //        //    Lattitude = "",
            //        //    Longitude = "",
            //        //    CountryName = "",
            //        //    CityName = "",
            //        //    VenueId = "",
            //        //    Summary = "",
            //        //},
            //        //new HistoryDataModel()
            //        //{
            //        //    HistoryId = 5,
            //        //    Title = "",
            //        //    Description = "",
            //        //    Lattitude = "",
            //        //    Longitude = "",
            //        //    CountryName = "",
            //        //    CityName = "",
            //        //    VenueId = "",
            //        //    Summary = "",
            //        //},
            //        //new HistoryDataModel()
            //        //{
            //        //    HistoryId = 6,
            //        //    Title = "",
            //        //    Description = "",
            //        //    Lattitude = "",
            //        //    Longitude = "",
            //        //    CountryName = "",
            //        //    CityName = "",
            //        //    VenueId = "",
            //        //    Summary = "",
            //        //}
                
            //    }
            //};

            //return historyList;
        }
    }
}