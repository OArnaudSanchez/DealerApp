using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface ICombustibleService
    {
        Task<PagedList<Combustible>> GetCombustibles(CombustibleQueryFilter filters);
        Task<Combustible> GetCombustible(int id);
        Task InsertCombustible(Combustible combustible);
        Task<bool> UpdateCombustible(Combustible combustible);
        Task<bool> DeleteCombustible(int id);
    }
}