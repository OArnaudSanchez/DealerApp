using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IVehiculoService
    {
        Task<PagedList<Vehiculo>> GetVehiculos(VehiculoQueryFilter filters, ResourceLocation resourceLocation);
        Task<Vehiculo> GetVehiculo(int id);
        Task InsertVehiculo(Vehiculo vehiculo);
        Task<bool> UpdateVehiculo(Vehiculo vehiculo);
        Task<bool> DeleteVehiculo(int id);
    }
}