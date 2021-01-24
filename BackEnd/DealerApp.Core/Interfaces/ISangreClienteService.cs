using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface ISangreClienteService
    {
        Task<PagedList<SangreCliente>> GetSangreClientes(SangreClienteQueryFilter filters);
        Task<SangreCliente> GetSangreCliente(int id);
        Task InsertSangreCliente(SangreCliente sangreCliente);
        Task<bool> UpdateSangreCliente(SangreCliente sangreCliente);
        Task<bool> DeleteSangreCliente(int id);
    }
}