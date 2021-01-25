using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedList<Usuario>> GetUsuarios(UsuarioQueryFilter filters, ResourceLocation resourceLocation);
        Task<Usuario> GetUsuario(int id);
        Task<bool> DeleteUsuario(int id);
    }
}