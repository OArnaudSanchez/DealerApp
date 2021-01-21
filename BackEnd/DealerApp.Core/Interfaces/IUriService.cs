using System;

namespace DealerApp.Core.Interfaces
{
    public interface IUriService
    {
        Uri GetPaginationUri(int pageNumber, int pageSize, string route);
    }
}