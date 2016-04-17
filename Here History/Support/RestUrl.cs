using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Here_History.Support
{
     public  static class RestUrl
    {
         private static string mainDomain = "http://192.168.1.15/space/api/History/";

         public static string GetHistoryByGeoPosition(string latitude, string longitude)
         {
             return string.Format("{0}GetHistory/{1}/{2}/", mainDomain, latitude, longitude);
         }
    }
}
