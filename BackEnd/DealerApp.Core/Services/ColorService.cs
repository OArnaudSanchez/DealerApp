using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Services
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Color> _pagedGenerator;
        public ColorService(IUnitOfWork unitOfWork, IPagedGenerator<Color> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Color>> GetColors(ColorQueryFilter filters)
        {
            var colores = await _unitOfWork.ColorRepository.GetAll();
            colores = filters.Nombre != null ? colores.Where(x => x.Nombre.ToLower() == filters.Nombre.ToLower()) : colores;
            colores = filters.Descripcion != null ? colores.Where(x => x.Descripcion.ToLower().Contains(filters.Descripcion.ToLower())) : colores;
            colores = filters.Estatus != null ? colores.Where(x => x.Estatus == filters.Estatus) : colores;
            return _pagedGenerator.GeneratePagedList(colores, filters);
        }

        public async Task<Color> GetColor(int id)
        {
            var color = await _unitOfWork.ColorRepository.GetById(id);
            return color ?? throw new BussinessException("No se encontro el color", 404);
        }

        public async Task InsertColor(Color color)
        {
            await ColorValidation(color);
            color.Id = 0;
            color.Estatus = true;
            await _unitOfWork.ColorRepository.Add(color);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateColor(Color color)
        {
            var currentColor = await GetColor(color.Id);
            currentColor.Descripcion = color.Descripcion;
            currentColor.Estatus = color.Estatus ?? true;
            _unitOfWork.ColorRepository.Update(currentColor);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteColor(int id)
        {
            var currentColor = await GetColor(id);
            await _unitOfWork.ColorRepository.Delete(currentColor.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task ColorValidation(Color color)
        {
            var currentColores = await _unitOfWork.ColorRepository.GetAll();
            if (currentColores.Where(x => x.Nombre.ToLower() == color.Nombre.ToLower()).Any())
            {
                throw new BussinessException("Ya existe este color", 400);
            }
        }
    }
}