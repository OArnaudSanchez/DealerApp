using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Services
{
    public class ModeloService : IModeloService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Modelo> _pagedGenerator;
        public ModeloService(IUnitOfWork unitOfWork, IPagedGenerator<Modelo> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Modelo>> GetModelos(ModeloQueryFilter filters)
        {
            var modelos = await _unitOfWork.ModeloRepository.GetAll();
            modelos = filters.Nombre != null ? modelos.Where(x => x.Nombre.ToLower() == filters.Nombre.ToLower()) : modelos;
            modelos = filters.Descripcion != null ? modelos.Where(x => x.Descripcion.ToLower().Contains(filters.Descripcion.ToLower())) : modelos;
            modelos = filters.Estatus != null ? modelos.Where(x => x.Estatus == filters.Estatus) : modelos;
            return _pagedGenerator.GeneratePagedList(modelos, filters);

        }

        public async Task<Modelo> GetModelo(int id)
        {
            var currentModelo = await _unitOfWork.ModeloRepository.GetById(id);
            return currentModelo ?? throw new BussinessException("El modelo no existe", 404);
        }

        public async Task InsertModelo(Modelo modelo)
        {
            await ModeloValidation(modelo);
            modelo.Id = 0;
            modelo.Estatus = true;
            await _unitOfWork.ModeloRepository.Add(modelo);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateModelo(Modelo modelo)
        {
            var currentModelo = await GetModelo(modelo.Id);
            currentModelo.Descripcion = modelo.Descripcion;
            currentModelo.Estatus = modelo.Estatus ?? true;
            _unitOfWork.ModeloRepository.Update(currentModelo);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteModelo(int id)
        {
            var currentModelo = await GetModelo(id);
            await _unitOfWork.ModeloRepository.Delete(currentModelo.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task ModeloValidation(Modelo modelo)
        {
            var modelos = await _unitOfWork.ModeloRepository.GetAll();
            if (modelos.Where(x => x.Nombre.ToLower() == modelo.Nombre.ToLower()).Any())
            {
                throw new BussinessException("El modelo ya existe", 400);
            }
        }


    }
}