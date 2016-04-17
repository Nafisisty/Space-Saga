using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasaSpaceHistoryApi.Models
{
    public class Area
    {
        // Scalar Properties
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string AreaHistory { get; set; }

        public int CityId { get; set; }

        // Navigation Property
        public City City { get; set; }
        public List<History> History { get; set; }
    }
}