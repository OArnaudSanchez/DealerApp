using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IRolService
    {
        Task<PagedList<Rol>> GetRoles(RolQueryFilter filters);
        Task<Rol> GetRol(int id);
        Task InsertRol(Rol rol);
        Task<bool> UpdateRol(Rol rol);
        Task<bool> DeleteRol(int id);
    }
}