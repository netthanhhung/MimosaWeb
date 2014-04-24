using System;
using System.Configuration;
using System.Web.Mvc;

namespace MVCCore.Framework.Web.Mvc
{
    public static class HtmlHelperExtension
    {
        private static UrlHelper Url(this HtmlHelper html)
        {
            return ((WebViewPage)html.ViewDataContainer).Url;
        }

        public static MvcHtmlString Script(this HtmlHelper html, string fileName, bool autoResolveUrl = true)
        {
            if (autoResolveUrl)
                fileName = html.Url().Script(fileName);
            else
            {
                fileName += UrlHelperExtension.appVersion;
            }

            string htmlString = string.Format("<script type='text/javascript' src='{0}'></script>", fileName);

            return MvcHtmlString.Create(htmlString);
        }

        public static MvcHtmlString ScriptAjax(this HtmlHelper html, string relativePath)
        {
            var ajaxRoot = ConfigurationManager.AppSettings["AjaxRoot"];

            return html.Script(new Uri(new Uri(ajaxRoot), relativePath).ToString(), false);
        }


        public static MvcHtmlString Css(this HtmlHelper html, string fileName, bool autoResolveUrl = true)
        {
            if (autoResolveUrl)
                fileName = html.Url().Css(fileName);
            else
            {
                fileName += UrlHelperExtension.appVersion;
            }

            string htmlString = string.Format("<link rel='stylesheet' type='text/css' href='{0}' />", fileName);

            return MvcHtmlString.Create(htmlString);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string fileName, object htmlAttributes = null)
        {
            TagBuilder tagBuilder = new TagBuilder("image");
            tagBuilder.MergeAttribute("src", html.Url().Image(fileName));
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

    }
}