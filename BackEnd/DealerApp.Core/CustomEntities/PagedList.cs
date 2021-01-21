using System;
using System.Collections.Generic;
using System.Linq;

namespace DealerApp.Core.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PagedList(List<T> items, int countPage, int pageNumber, int pageSize)
        {
            TotalCount = countPage;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(countPage / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber = 0, int pageSize = 0)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber -1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}