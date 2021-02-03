using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Services
{
    public class MarcaService : IMarcaService, IOrderItems<Marca>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Marca> _pagedGenerator;
        public MarcaService(IUnitOfWork unitOfWork, IPagedGenerator<Marca> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Marca>> GetMarcas(MarcaQueryFilter filters, ResourceLocation resourceLocation)
        {
            var marcas = await _unitOfWork.MarcaRepository.GetAll();
            marcas = filters.Nombre != null ? marcas.Where(x => x.Nombre.ToLower() == filters.Nombre.ToLower()) : marcas;
            marcas = filters.Descripcion != null ? marcas.Where(x => x.Descripcion.ToLower().Contains(filters.Descripcion.ToLower())) : marcas;
            marcas = filters.Lanzamiento != null ? marcas.Where(x => x.Lanzamiento == filters.Lanzamiento.ToString()) : marcas;
            marcas = filters.Estatus != null ? marcas.Where(x => x.Estatus == filters.Estatus) : marcas;
            return _pagedGenerator.GeneratePagedList(GetItemsOrdered(marcas, resourceLocation), filters);
        }

        public async Task<Marca> GetMarca(int id)
        {
            var currentMarca = await _unitOfWork.MarcaRepository.GetById(id);
            return currentMarca ?? throw new BussinessException("Marca no Encontrada", 404);
        }

        public async Task InsertMarca(Marca marca)
        {
            await MarcaValidation(marca);
            marca.Id = 0;
            marca.Estatus = true;
            await _unitOfWork.MarcaRepository.Add(marca);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateMarca(Marca marca)
        {
            var currentMarca = await GetMarca(marca.Id);
            currentMarca.Descripcion = marca.Descripcion;
            currentMarca.Foto = marca.Foto;
            marca.Estatus = marca.Estatus ?? true;
            _unitOfWork.MarcaRepository.Update(currentMarca);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMarca(int id)
        {
            var currentMarca = await GetMarca(id);
            await _unitOfWork.MarcaRepository.Delete(currentMarca.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task MarcaValidation(Marca marca)
        {
            var marcas = await _unitOfWork.MarcaRepository.GetAll();
            if (marcas.Where(x => x.Nombre.ToLower() == marca.Nombre.ToLower()).Any())
            {
                throw new BussinessException("La marca ya existe", 400);
            }
        }

        public IEnumerable<Marca> GetItemsOrdered(IEnumerable<Marca> marcas, ResourceLocation resourceLocation)
        {
            return marcas.Select(x => new Marca()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                Lanzamiento = x.Lanzamiento,
                Estatus = x.Estatus,
                Foto = $"{resourceLocation.Scheme}://{resourceLocation.Host}{resourceLocation.PathBase}/wwwroot/Resources/Marcas/{x.Foto}"
            });
        }
    }
}