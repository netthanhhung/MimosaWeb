using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Business.Localization;

namespace Mimosa.Utility
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString T(this HtmlHelper html, string format)
        {
            int languageid;
            if (HttpContext.Current.Session["LanguageId"] != null)
            {
                languageid = int.Parse(HttpContext.Current.Session["LanguageId"].ToString());
            }
            else
                languageid = int.Parse(ConfigurationManager.AppSettings["LanguageId"]);

            var resFormat = LanguageService.GetResource(format, languageid);

            return  new MvcHtmlString(resFormat);
        } 
    }
}