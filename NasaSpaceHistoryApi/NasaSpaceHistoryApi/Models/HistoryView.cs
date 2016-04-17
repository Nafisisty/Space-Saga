using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasaSpaceHistoryApi.Models
{
    public class HistoryView
    {
        public int HistoryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string AreaName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}