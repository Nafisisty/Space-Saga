using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Here_History.Models
{
    public class HistoryDataModel
    {
    public int HistoryId { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public string AreaName { get; set; }
    public string CityName { get; set; }
    public string CountryName { get; set; }

    public ObservableCollection<HistoryDataModel> Data { get; set; }
    }

    //public class HistoryDataModelList
    //{
    //    public ObservableCollection<HistoryDataModel> Data { get; set; }
    //}
}
