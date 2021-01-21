using System.Collections.Generic;
using DealerApp.Core.CustomEntities;

namespace DealerApp.Core.Interfaces
{
    public interface IOrderItems<T>
    {
        IEnumerable<T> GetItemsOrdered(IEnumerable<T> items, ResourceLocation resourceLocation);
    }
}