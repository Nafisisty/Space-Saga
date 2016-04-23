using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Here_History.Support
{
     public  static class RestUrl
    {
         private static string mainDomain = "http://spacesaga.azurewebsites.net/api/history/";

         public static string GetHistoryByGeoPosition(string latitude, string longitude)
         {
             return string.Format("{0}GetHistory/{1}/{2}/", mainDomain, latitude, longitude);
         }

         public static string GetHistoryByString(string searchKeyword)
         {
             return string.Format("{0}search/{1}/", mainDomain, searchKeyword);
         }

         public static string PostHistoryByUser()
         {
             return string.Format("{0}save/");
         }
    }
}
