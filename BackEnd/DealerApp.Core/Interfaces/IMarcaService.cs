using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IMarcaService
    {
        Task<PagedList<Marca>> GetMarcas(MarcaQueryFilter filters, ResourceLocation resourceLocation);
        Task<Marca> GetMarca(int id);
        Task InsertMarca(Marca marca);
        Task<bool> UpdateMarca(Marca marca);
        Task<bool> DeleteMarca(int id);
    }
}