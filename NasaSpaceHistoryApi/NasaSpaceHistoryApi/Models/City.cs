using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasaSpaceHistoryApi.Models
{
    public class City
    {
        // Scalar Properties
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityHistory { get; set; }

        public int CountryId { get; set; }
        // Navigation Property
        public Country Country { get; set; }
        public List<Area> Areas { get; set; }
    }
}