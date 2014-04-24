using System.Collections.Generic;
using System.Linq;
using Business.Localization;
using Entities;
using MVCCore.Framework;
using MVCCore.Framework.Web.Caching;

namespace Business.Caches
{
    public class LocaleStringResourceCache : BaseCache<List<LocaleStringResource>>
    {
        public LocaleStringResourceCache()
            : base(CONSTANTS.CacheMinutes, LocaleStringResourceService.GetListAll, false)
        { }

        private static LocaleStringResourceCache Instance
        {
            get { return SingletonBase<LocaleStringResourceCache>.Instance; }
        }

        public static List<LocaleStringResource> GetList()
        {
            return Instance.Get();
        }

        public static LocaleStringResource Get(int LocaleStringResourceID)
        {
            return Instance.Get().FirstOrDefault(n => n.Id == LocaleStringResourceID);
        }

        public static LocaleStringResource Get(string LocaleStringResourceName, int LanguageId)
        {
            return Instance.Get().FirstOrDefault(n => n.ResourceName.Trim().ToLower() == LocaleStringResourceName.Trim().ToLower() && n.LanguageId == LanguageId);
        } 
    }
}