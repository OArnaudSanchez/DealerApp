using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Interfaces
{
    public interface IContratoService
    {
        Task<PagedList<Contrato>> GetContratos(ContratoQueryFilter filters);
        Task<Contrato> GetContrato(int id);
        Task InsertContrato(Contrato contrato);
        Task<bool> UpdateContrato(Contrato contrato);
        Task<bool> DeleteContrato(int id);
    }
}