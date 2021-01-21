using System.Collections.Generic;
using DealerApp.Core.Common;
using DealerApp.Core.CustomEntities;

namespace DealerApp.Core.Interfaces
{
    public interface IPagedGenerator<T>
    {
        PagedList<T> GeneratePagedList(IEnumerable<T> items, QueryFilter filters);
    }
}