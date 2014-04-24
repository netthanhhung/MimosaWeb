using System.Configuration;

namespace Mimosa.Models.Search
{
    public interface ISearchFilter
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }        
    }

    public class SearchFilter : ISearchFilter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }        

        public SearchFilter()
        {
            PageIndex = 1;
            PageSize = int.Parse(ConfigurationSettings.AppSettings.Get("PageSize"));
        }
    }
}