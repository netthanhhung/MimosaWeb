using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCCore.Framework.Web.Mvc
{
    /// <summary>
    /// hai.vu
    /// </summary>
    public static class UrlHelperExtension
    {
//        public static string Image(this UrlHelper url)
//        {
//            string path = ConfigurationManager.AppSettings["ImagePath"];
//
//            if (!string.IsNullOrWhiteSpace(path))
//                return url.Content(path);
//            
//            return url.Content(String.Format("~/Content/Images"));
//        }

        public static string appVersion;

        static UrlHelperExtension()
        {
            // use reflection to get svn revision
            if (HttpContext.Current != null)
            {
                var customAttributes =
                    HttpContext.Current.ApplicationInstance.GetType().BaseType.Assembly.GetCustomAttributes(
                        typeof (AssemblyInformationalVersionAttribute), false) ;

                if (customAttributes.Length > 0 && ((AssemblyInformationalVersionAttribute) customAttributes[0]).InformationalVersion.Length > 1)
                    appVersion = "?v=" + ((AssemblyInformationalVersionAttribute)customAttributes[0]).InformationalVersion + "&t=" + DateTime.Now.Ticks;
                else
                    appVersion = "?v=" + DateTime.Now.Ticks;

            }                                

        }

        public static string Image(this UrlHelper url, string imageName)
        {
            string path = ConfigurationManager.AppSettings["ImagePath"];

            if (string.IsNullOrWhiteSpace(path))
                path = "~/Content/Images";
            
            if (string.IsNullOrWhiteSpace(imageName))
                return url.Content(path);

            return url.Content(String.Format("{0}/{1}", path, imageName));
        }

        public static string Script(this UrlHelper url, string fileName)
        {
            string path = ConfigurationManager.AppSettings["ScriptPath"];

            if (string.IsNullOrWhiteSpace(path))
                path = "~/Content/Scripts";
                
            return url.Content(string.Format("{0}/{1}{2}", path, fileName, appVersion));
            
        }
        
      

        public static string Css(this UrlHelper url, string fileName)
        {
            string path = ConfigurationManager.AppSettings["CssPath"];

            if (string.IsNullOrWhiteSpace(path))
                path = "~/Content/Styles";

            return url.Content(String.Format("{0}/{1}{2}", path, fileName, appVersion));
        }
       
        /// <summary>
        /// hai.vu
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaultUrl"></param>
        /// <returns></returns>
        public static string RefererUrl(this UrlHelper url, string defaultUrl = "~")
        {
            if (HttpContext.Current.Request.UrlReferrer != null
                && /* not it self */ !HttpContext.Current.Request.Url.AbsolutePath.ToLower().Equals(HttpContext.Current.Request.UrlReferrer.AbsolutePath.ToLower())
                && /* not login page */ HttpContext.Current.Request.UrlReferrer.PathAndQuery.ToLower().IndexOf("login") < 0
//                && /* not from other domain */ HttpContext.Current.Request.UrlReferrer.Host == HttpContext.Current.Request.Url.Host
                && /* not from other domain */ url.IsLocalUrl(HttpContext.Current.Request.UrlReferrer.ToString())
                
                )

                return HttpContext.Current.Request.UrlReferrer.PathAndQuery;

            return defaultUrl == "~" ? url.Content("~") : defaultUrl;
        }
    }
}
