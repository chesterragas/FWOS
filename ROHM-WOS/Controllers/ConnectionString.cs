using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Controllers
{
    public class ConnectionString
    {
        public static string WOSDB
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ROHM_WOSDB"].ConnectionString;
            }
        }
        public static string EmailCredentials
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["EmailCredentials"].ConnectionString;
            }
        }

      
    }
}