using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc.Html;
using NasaSpaceHistoryApi.Models;

namespace NasaSpaceHistoryApi.Services
{
    public class HistroyRepository
    {
        NasaSpaceHistoryDbContext db = new NasaSpaceHistoryDbContext();

        public List<Country> GetCountries()
        {
            List<Country> countries = db.Countries.ToList();
            return countries;
        }
        public List<HistoryView> GetHistory(string lat, string lon)
        {
            var latitude = Convert.ToDouble(lat);
            var longitude = Convert.ToDouble(lon);
            //var data = db.History.Include(m => m.Area).Include(m => m.Area.City).Include(m => m.Area.City.Country).Select(n => new {HistoryId = n.HistoryId, AreaName = n.Area.AreaName, CityName = n.Area.City.CityName, CountryName = n.Area.City.Country.CountryName});

            List<HistoryView> data = db.History.Include(m => m.Area).Include(m => m.Area.City).Include(m => m.Area.City.Country).Select(n => new HistoryView(){ HistoryId = n.HistoryId, Latitude = n.Latitude, Longitude = n.Longitude, Title = n.Title, Description = n.Description, Summary = n.Summary, AreaName = n.Area.AreaName, CityName = n.Area.City.CityName, CountryName = n.Area.City.Country.CountryName }).ToList();

            List<HistoryView> historiesInRange = data.FindAll(x => (IsHistoryInRange(x.Latitude, x.Longitude, latitude, longitude)));
            
            return historiesInRange;
        }

        private bool IsHistoryInRange(double la, double lo, double latitude, double longitude)
        {
            return (Math.Pow((la - latitude), 2) + Math.Pow((lo - longitude), 2) <= Math.Pow(0.02, 2));
        }
   
    }
}