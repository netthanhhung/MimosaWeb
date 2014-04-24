using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mimosa.Utility
{
    public class UrlHepler
    {
        public static string GetTitleDetail()
        {
            if (HttpContext.Current.Session["LanguageId"] == null || (int)HttpContext.Current.Session["LanguageId"] == 2)
            {
                return "chi-tiet-phong";
            }

            return "detail-room";
        }
    }
}