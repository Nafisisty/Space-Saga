using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NasaSpaceHistoryApi.Models;

namespace NasaSpaceHistoryApi.Services
{
    public class HistoryService
    {
        readonly HistroyRepository histroyRepository = new HistroyRepository();

        public List<Country> GetCountries()
        {
            List<Country> countries = histroyRepository.GetCountries().ToList();
            return countries;
        }

        public List<HistoryView> GetHistory(string latitude, string longitude)
        {
            List<HistoryView> historyView = histroyRepository.GetHistory(latitude, longitude);
            return historyView;
        }

        public List<HistoryView> Search(string keyword)
        {
            List<HistoryView> historyView = histroyRepository.Search(keyword);
            return historyView;             
        }

        public string  Save(HistoryView historyView)
        {
            string msg = histroyRepository.Save(historyView);
            return msg;
        }
       
    }
}