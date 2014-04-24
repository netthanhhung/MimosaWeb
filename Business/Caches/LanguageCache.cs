using System.Collections.Generic;
using System.Linq;
using Business.Localization;
using Entities;
using MVCCore.Framework;
using MVCCore.Framework.Web.Caching;

namespace Business.Caches
{
    public class LanguageCache : BaseCache<List<Language>>
    {
        public LanguageCache()
            : base(CONSTANTS.CacheMinutes, LanguageService.GetListAll, false)
        { }

        private static LanguageCache Instance
        {
            get { return SingletonBase<LanguageCache>.Instance; }
        }

        public static List<Language> GetList()
        {
            return Instance.Get();
        }

        public static Language Get(int LanguageID)
        {
            return Instance.Get().FirstOrDefault(n => n.Id == LanguageID);
        }

        public static Language Get(string LanguageName)
        {
            return Instance.Get().FirstOrDefault(n => TextUtility.MakeAlias(n.Name) == TextUtility.MakeAlias(LanguageName));
        }
    }
}