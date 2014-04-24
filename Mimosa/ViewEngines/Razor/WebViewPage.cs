#region Using...

using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Business.Localization;

#endregion

namespace Mimosa.ViewEngines.Razor.WebViewPage
{
    public delegate LocalizedString Localizer(string text, params object[] args);

    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {

        //private ILocalizationService _localizationService;
        private Localizer _localizer;
        //private IWorkContext _workContext;

        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
        {
            get
            {
                if (_localizer == null)
                {
                    //null localizer
                    //_localizer = (format, args) => new LocalizedString((args == null || args.Length == 0) ? format : string.Format(format, args));

                    //default localizer
                    _localizer = (format, args) =>
                                     {
                                         int languageId;
                                         if (HttpContext.Current.Session["LanguageId"] != null)
                                         {
                                             languageId = int.Parse(HttpContext.Current.Session["LanguageId"].ToString());
                                         }
                                         else
                                             languageId = int.Parse(ConfigurationManager.AppSettings["LanguageId"]);

                                         var resFormat = LanguageService.GetResource(format, languageId);
                                         if (string.IsNullOrEmpty(resFormat))
                                         {
                                             return new LocalizedString(format);
                                         }
                                         return
                                             new LocalizedString((args == null || args.Length == 0)
                                                                     ? resFormat
                                                                     : string.Format(resFormat, args));
                                     };
                }
                return _localizer;
            }
        }
          
        public HelperResult RenderWrappedSection(string name, object wrapperHtmlAttributes)
        {
            Action<TextWriter> action = delegate(TextWriter tw)
                                {
                                    var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(wrapperHtmlAttributes);
                                    var tagBuilder = new TagBuilder("div");
                                    tagBuilder.MergeAttributes(htmlAttributes);

                                    var section = RenderSection(name, false);
                                    if (section != null)
                                    {
                                        tw.Write(tagBuilder.ToString(TagRenderMode.StartTag));
                                        section.WriteTo(tw);
                                        tw.Write(tagBuilder.ToString(TagRenderMode.EndTag));
                                    }
                                };
            return new HelperResult(action);
        }

        public HelperResult RenderSection(string sectionName, Func<object, HelperResult> defaultContent)
        {
            return IsSectionDefined(sectionName) ? RenderSection(sectionName) : defaultContent(new object());
        }

        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View != null && viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }
 
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}