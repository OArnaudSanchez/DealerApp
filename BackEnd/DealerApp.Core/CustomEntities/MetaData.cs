using DealerApp.Core.Common;
using DealerApp.Core.Interfaces;
namespace DealerApp.Core.CustomEntities
{
    public class MetaData
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }


        public MetaData BuildMeta<T>(PagedList<T> items, QueryFilter filters, string path,
        IUriService _uriService)
        {
            return new MetaData()
            {
                TotalCount = items.TotalCount,
                PageSize = items.PageSize,
                CurrentPage = items.CurrentPage,
                TotalPages = items.TotalPages,
                HasNextPage = items.HasNextPage,
                HasPreviousPage = items.HasPreviousPage,
                NextPageUrl = filters.PageNumber >= 1 && filters.PageNumber < items.TotalPages
                ? _uriService.GetPaginationUri(filters.PageNumber + 1, filters.PageSize, path).ToString()
                : null,
                PreviousPageUrl = filters.PageNumber - 1 >= 1 && filters.PageNumber <= items.TotalPages
                ? _uriService.GetPaginationUri(filters.PageNumber - 1, filters.PageSize, path).ToString()
                : null
            };
        }
    }
}