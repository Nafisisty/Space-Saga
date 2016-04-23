using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Sql;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Web.Mvc.Html;
using Microsoft.Ajax.Utilities;
using NasaSpaceHistoryApi.Models;

namespace NasaSpaceHistoryApi.Services
{
    public class HistroyRepository
    {
        //public List<Country> GetCountries()
        //{
        //    var nasaSpaceHistoryDbContext = new NasaSpaceHistoryDbContext();
        //    List<Country> countries = nasaSpaceHistoryDbContext.Countries.ToList();
        //    return countries;
        //}
        //public List<HistoryView> GetHistory(string lat, string lon)
        //{
           //var nasaSpaceHistoryDbContext = new NasaSpaceHistoryDbContext();
           
           // const double increase = .02;
           // const double decrease = .02;
          
           // double increasedLtitude = Convert.ToDouble(latitude) + increase;  
           // double decreaseedLtitude = Convert.ToDouble(latitude) - decrease;
           // double increasedLongitude = Convert.ToDouble(longitude) + increase;
           // double decreaseedLongitude = Convert.ToDouble(longitude) - decrease;

           // List<History> histories = nasaSpaceHistoryDbContext.History.ToList();

           // List<History> historiess = (from history in histories
           //                             where ((history.Latitude <= increasedLtitude && history.Latitude >= decreaseedLtitude) && (history.Longitude <= increasedLongitude && history.Longitude >= decreaseedLongitude))
           //                             select history).ToList();

           //List<int> areaIdList = (from item in histories
           //                         select item.AreaId).ToList();

           //List<Area> areas = nasaSpaceHistoryDbContext.Areas.Where(area => areaIdList.Contains(area.AreaId)).ToList();

           //List<int> cityIdList = (from item in areas
           //                        select item.CityId).ToList();
           //List<City> Cities = nasaSpaceHistoryDbContext.Cities.Where(city => cityIdList.Contains(city.CityId)).ToList();

           //List<int> countryIdList = (from item in Cities
           //                           select item.CountryId).ToList();

           //List<Country> Countries = nasaSpaceHistoryDbContext.Countries.Where(country => countryIdList.Contains(country.CountryId)).ToList();

           //List<HistoryView> historyView = histories.Select(x => new HistoryView
           //{
           //    Latitude = x.Latitude,
           //    Longitude = x.Longitude,
           //    HistoryId = x.HistoryId,
           //    Description = x.Description,
           //    Summary = x.Summary,
           //    Title = x.Title,
           //    AreaName = (from value in areas
           //                     where (value.AreaId == x.AreaId)
           //                     select value.AreaName).SingleOrDefault(),

           //    CityName =(from value in Cities
           //               join arrr in areas on value.CityId equals arrr.CityId
           //               where  arrr.AreaId==x.AreaId
           //               select value.CityName).SingleOrDefault(),

           //    CountryName = (from value in Countries
           //                   join cts in Cities on value.CountryId equals cts.CountryId
           //                   where cts.CityName == (from data in Cities
           //                                          join arrr in areas on data.CityId equals arrr.CityId
           //                                          where arrr.AreaId == x.AreaId
           //                                          select data.CityName).SingleOrDefault()
           //                   select value.CountryName).SingleOrDefault()
  
               
           //}).ToList();

           //return historyView;    
        //}
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
        
            List<HistoryView> data = db.History.Include(m => m.Area).Include(m => m.Area.City).Include(m => m.Area.City.Country).Select(n => new HistoryView() { HistoryId = n.HistoryId, Latitude = n.Latitude, Longitude = n.Longitude, Title = n.Title, Description = n.Description, Summary = n.Summary, AreaName = n.Area.AreaName, CityName = n.Area.City.CityName, CountryName = n.Area.City.Country.CountryName }).ToList();
            List<HistoryView> historiesInRange = data.FindAll(x => (IsHistoryInRange(x.Latitude, x.Longitude, latitude, longitude)));

            return historiesInRange;
        }

        private bool IsHistoryInRange(double la, double lo, double latitude, double longitude)
        {
            return (Math.Pow((la - latitude), 2) + Math.Pow((lo - longitude), 2) <= Math.Pow(0.02, 2));
        }

        public List<HistoryView> Search(string keyword)
        {
           List<HistoryView> data = db.History.Include(m => m.Area).Include(m => m.Area.City).Include(m => m.Area.City.Country).Select(n => new HistoryView() { HistoryId = n.HistoryId, Latitude = n.Latitude, Longitude = n.Longitude, Title = n.Title, Description = n.Description, Summary = n.Summary, AreaName = n.Area.AreaName, CityName = n.Area.City.CityName, CountryName = n.Area.City.Country.CountryName }).ToList();
           List<HistoryView> matches = data.Where(history => history.CountryName.StartsWith(keyword, StringComparison.OrdinalIgnoreCase) || history.CityName.StartsWith(keyword, StringComparison.OrdinalIgnoreCase) || history.AreaName.StartsWith(keyword, StringComparison.OrdinalIgnoreCase) || history.Title.StartsWith(keyword, StringComparison.OrdinalIgnoreCase) || history.Description.StartsWith(keyword, StringComparison.OrdinalIgnoreCase) || history.Summary.StartsWith(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
           return matches;
        }

        public string Save(HistoryView historyView)
        {
            string msg="";
            Country aCountry = new Country();
            aCountry.CountryName = historyView.CountryName;
            int countCountry = db.Countries.Count(c => c.CountryName == historyView.CountryName);
            if (countCountry < 1)
            {
                db.Countries.Add(aCountry);
                db.SaveChanges();
            }
            Country country = db.Countries.FirstOrDefault(con => con.CountryName == aCountry.CountryName);

            City aCity = new City();
            aCity.CityName = historyView.CityName;
            aCity.CountryId = country.CountryId;
            int countCity = db.Cities.Count(c => c.CityName == historyView.CityName);
            if (countCity < 1)
            {
                db.Cities.Add(aCity);
                db.SaveChanges();
            }

            City city = db.Cities.FirstOrDefault(con => con.CityName == aCity.CityName);

            Area aArea = new Area();
            aArea.AreaName = historyView.AreaName;
            aArea.CityId = city.CityId;

            int countArea = db.Areas.Count(a => a.AreaName == historyView.AreaName);
            if (countArea < 1)
            {
                db.Areas.Add(aArea);
                db.SaveChanges();
            }

            Area area = db.Areas.FirstOrDefault(a =>a.AreaName == aArea.AreaName);

            History aHistory = new History();
            aHistory.Longitude = historyView.Longitude;
            aHistory.Latitude = historyView.Latitude;
            aHistory.Summary = historyView.Summary;
            aHistory.Description = historyView.Description;
            aHistory.Title = historyView.Title;
            aHistory.AreaId = area.AreaId;

            db.History.Add(aHistory);
            try
            {
               db.SaveChanges();
                msg = "Data Inserted Successfully";
               return msg;
            }
            catch(Exception ex)
            {
                 msg = ex.Message;
                 return msg;
            }   
        }
    }
}