using System;
using System.Collections.Generic;

namespace Mimosa.Models.Search
{

    public class SearchResult
    {
        public SearchResult(ISearchFilter filter)
        {
            Filter = filter;
        }

        public int TotalRecords { get; set; }

        public ISearchFilter Filter;

        public int TotalPages
        {
            get
            {
                if (Filter.PageSize == 0)
                    return 0;
                return (int)Math.Ceiling(((decimal)TotalRecords / Filter.PageSize));
            }
        }
    }

    public class SearchResult<TResult> : SearchResult
    {
        public List<TResult> List;

        public SearchResult(ISearchFilter filter)
            : base(filter)
        {
        }
    }

    public class SearchResult2
    {
        protected SearchResult2(ISearchFilter filter)
        {
            Filter = filter;
        }

        public int TotalRecords { get; set; }

        public ISearchFilter Filter { get; private set; }

        public int TotalPages
        {
            get
            {
                if (Filter.PageSize == 0)
                    return 0;
                return (int)Math.Ceiling(((decimal)TotalRecords / Filter.PageSize));
            }
        }
    }

    public class SearchResult2<TResult, TFilter> : SearchResult2 where TFilter : ISearchFilter
    {
        public List<TResult> List { get; private set; }

        public new TFilter Filter
        {
            get { return (TFilter) base.Filter; }
        }

        public SearchResult2(List<TResult> resultList, TFilter filter) : base(filter)
        {
            List = resultList;            
        }
    }



}