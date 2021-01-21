using System.Collections.Generic;
using DealerApp.Core.Common;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Interfaces;
using Microsoft.Extensions.Options;

namespace DealerApp.Core.Services
{
    public class PagedGenerator<T> : IPagedGenerator<T>
    {
        private readonly PaginationOptions _paginationOptions;
        public PagedGenerator(IOptions<PaginationOptions> paginationOptions)
        {
            _paginationOptions = paginationOptions.Value;
        }
        public PagedList<T> GeneratePagedList(IEnumerable<T> items, QueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            return PagedList<T>.Create(items, filters.PageNumber, filters.PageSize);
        }
    }
}