using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using NasaSpaceHistoryApi.Models;
using NasaSpaceHistoryApi.Services;

namespace NasaSpaceHistoryApi.Controllers
{
    [RoutePrefix("api/History")]
    public class HistoryController : ApiController
    {
        readonly HistoryService historyService = new HistoryService();

        [Route("Countries")]
        public List<Country> GetCountries()
        {
            List<Country> countries = historyService.GetCountries();
            return countries;
        }

        // GET api/History/GetHistory/123.456/321.654 
        [HttpGet]
        [Route("GetHistory/{Latitude:double}/{Longitude:double}")]
        public List<HistoryView> GetHistory(string latitude, string longitude)      
        {
           List<HistoryView> historyView = historyService.GetHistory(latitude, longitude);

           return historyView;
        }

        // GET api/history/Search/abcxyz
        [HttpGet]
        [Route("Search/{keyword}")]
        public List<HistoryView> Search(string keyword)
        {
            List<HistoryView> historyView = historyService.Search(keyword);

            return historyView;
        }

        // POST api/history
        [HttpPost]
        [Route("Save")]
        public string Save( HistoryView historyView)
        {
            string msg="Invalid Data";
            if (historyView ==null)
            {

                return msg;
            }
            msg = historyService.Save(historyView);
            return msg;
        }

        // PUT api/history/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/history/5
        public void Delete(int id)
        {
        }
    }
}
