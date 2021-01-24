using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IModeloService
    {
        Task<PagedList<Modelo>> GetModelos(ModeloQueryFilter filters);
        Task<Modelo> GetModelo(int id);
        Task InsertModelo(Modelo modelo);
        Task<bool> UpdateModelo(Modelo modelo);
        Task<bool> DeleteModelo(int id);
    }
}