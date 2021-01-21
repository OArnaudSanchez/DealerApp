using System.Collections.Generic;
using System.Threading.Tasks;
using DealerApp.Core.Common;

namespace DealerApp.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T Entity);
        bool Update(T Entity);
        Task<bool> Delete(int id);
    }
}