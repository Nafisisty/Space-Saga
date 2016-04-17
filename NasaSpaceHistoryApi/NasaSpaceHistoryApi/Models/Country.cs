using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasaSpaceHistoryApi.Models
{
    public class Country
    {
        // Scalar Properties
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryHistory { get; set; }

        // Navigation Property
        public List<City> Cities { get; set; }
    }
}