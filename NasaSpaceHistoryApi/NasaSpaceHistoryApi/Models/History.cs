using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasaSpaceHistoryApi.Models
{
    public class History
    {
        // Scalar Properties
        public int HistoryId { get; set; }      
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }

        public int AreaId { get; set; }   
        // Navigation Property
        public Area Area { get; set; }
       
    }
}