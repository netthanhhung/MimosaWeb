using System.Collections.Generic;
using System.Linq;
using System;
using Business.Caches;
using Dal;
using Entities;

namespace Business.Localization
{
    public static class LanguageService
    {


        public static List<Language> GetListAll()
        {
            using (var context = new MimosaEntities())
            {
                return context.Languages.OrderBy(x => x.Id).ToList();
            }
        }



        public static Language Get(int id)
        {
            using (var context = new MimosaEntities())
            {
                return context.Languages.First(x => x.Id == id);
            }
        }



        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether to empty string will be returned if a resource is not found and default value is set to empty string</param>
        /// <returns>A string representing the requested resource string.</returns>
        public static string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            string result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();

            var resources = LocaleStringResourceCache.Get(resourceKey, languageId);

            if (resources != null)
                result = resources.ResourceValue;

            if (String.IsNullOrEmpty(result))
            {
                if (!String.IsNullOrEmpty(defaultValue))
                {
                    result = defaultValue;
                }
                else
                {
                    if (!returnEmptyIfNotFound)
                    {
                        var strTemp = resourceKey.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        result = strTemp[strTemp.Length - 1];
                    }
                }

            }
            return result;
        }
    }
}
