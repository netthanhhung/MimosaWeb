using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dal;
using Entities;

namespace Business.Localization
{
    public static class LocaleStringResourceService
    {


        public static List<LocaleStringResource> GetListAll()
        {
            using (var context = new MimosaEntities())
            {
                return context.LocaleStringResources.OrderBy(x => x.ResourceName).ToList();
            }
        }


        public static LocaleStringResource Get(int id)
        {
            using (var context = new MimosaEntities())
            {
                return context.LocaleStringResources.Include(x => x.Language).First(x => x.Id == id);
            }
        }



    }
}
