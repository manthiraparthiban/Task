using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApp.CommonFunctions
{
    public static class CommonFunction
    {
        public static String objApiUrl = ConfigurationManager.AppSettings["APIURL"].ToString();
    }
}