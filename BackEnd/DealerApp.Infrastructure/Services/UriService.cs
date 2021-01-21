using System;
using DealerApp.Core.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace DealerApp.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPaginationUri(int pageNumber, int pageSize, string route)
        {
            var endPointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(endPointUri.ToString(), "PageNumber", pageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", pageSize.ToString());
            return new Uri(modifiedUri);
        }

    }
}