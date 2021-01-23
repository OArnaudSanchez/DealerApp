using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IColorService
    {
        Task<PagedList<Color>> GetColors(ColorQueryFilter filters);
        Task<Color> GetColor(int id);
        Task InsertColor(Color color);
        Task<bool> UpdateColor(Color color);
        Task<bool> DeleteColor(int id);
    }
}