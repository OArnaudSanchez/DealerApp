using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Services
{
    public class CombustibleService : ICombustibleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Combustible> _pagedGenerator;
        public CombustibleService(IUnitOfWork unitOfWork, IPagedGenerator<Combustible> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Combustible>> GetCombustibles(CombustibleQueryFilter filters)
        {
            var combustibles = await _unitOfWork.CombustibleRepository.GetAll();
            combustibles = filters.TipoCombustible != null ? combustibles.Where(x => x.TipoCombustible == filters.TipoCombustible) : combustibles;
            combustibles = filters.Descripcion != null ? combustibles.Where(x => x.Descripcion.ToLower().Contains(filters.Descripcion.ToLower())) : combustibles;
            combustibles = filters.Estatus != null ? combustibles.Where(x => x.Estatus == filters.Estatus) : combustibles;
            return _pagedGenerator.GeneratePagedList(combustibles, filters);
        }

        public async Task<Combustible> GetCombustible(int id)
        {
            var currentCombustible = await _unitOfWork.CombustibleRepository.GetById(id);
            return currentCombustible ?? throw new BussinessException("El combustible no existe", 404);
        }

        public async Task InsertCombustible(Combustible combustible)
        {
            await CombustibleValidation(combustible);
            combustible.Id = 0;
            combustible.Estatus = true;
            await _unitOfWork.CombustibleRepository.Add(combustible);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCombustible(Combustible combustible)
        {
            var currentCombustible = await GetCombustible(combustible.Id);
            currentCombustible.Descripcion = combustible.Descripcion;
            combustible.Estatus = combustible.Estatus ?? true;
            _unitOfWork.CombustibleRepository.Update(currentCombustible);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCombustible(int id)
        {
            var currentCombustible = await GetCombustible(id);
            await _unitOfWork.CombustibleRepository.Delete(currentCombustible.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task CombustibleValidation(Combustible combustible)
        {
            var currentCombustible = await _unitOfWork.CombustibleRepository.GetAll();
            if (currentCombustible.Where(x => x.TipoCombustible.ToString().ToLower() == combustible.TipoCombustible.ToString().ToLower()).Any())
            {
                throw new BussinessException("Ya existe este combustible", 400);
            }
        }

    }
}