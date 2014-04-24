using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Business.Caches
{
    public class CONSTANTS
    {
        public static int CacheMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["CacheMinutes"]);
    }
}
