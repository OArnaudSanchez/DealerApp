using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IClienteService
    {
        Task<PagedList<Cliente>> GetClientes(ClienteQueryFilter filters, ResourceLocation resourceLocation);
        Task<Cliente> GetCliente(int id);
        Task InsertCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task<bool> DeleteCliente(int id);
    }
}