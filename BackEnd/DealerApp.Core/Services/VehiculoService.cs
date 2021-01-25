using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;
using Microsoft.Extensions.Options;

namespace DealerApp.Core.Services
{
    public class VehiculoService : IVehiculoService, IOrderItems<Vehiculo>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Vehiculo> _pagedGenerator;

        public VehiculoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions, IPagedGenerator<Vehiculo> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Vehiculo>> GetVehiculos(VehiculoQueryFilter filters, ResourceLocation resourceLocation)
        {
            var vehiculos = await _unitOfWork.VehiculoRepository.GetAll();
            vehiculos = filters.Matricula != null ? vehiculos.Where(x => x.Matricula == filters.Matricula) : vehiculos;
            vehiculos = filters.Placa != null ? vehiculos.Where(x => x.Placa == filters.Placa) : vehiculos;
            vehiculos = filters.Lanzamiento != null ? vehiculos.Where(x => x.Lanzamiento == filters.Lanzamiento.ToString()) : vehiculos;
            return _pagedGenerator.GeneratePagedList(GetItemsOrdered(vehiculos, resourceLocation), filters);
        }
        public async Task<Vehiculo> GetVehiculo(int id)
        {
            var currentVehiculo = await _unitOfWork.VehiculoRepository.GetById(id);
            return currentVehiculo ?? throw new BussinessException("El vehiculo no existe", 404);
        }

        public async Task InsertVehiculo(Vehiculo vehiculo)
        {
            await VehiculoValidation(vehiculo);
            vehiculo.Id = 0;
            vehiculo.Estatus = true;
            await _unitOfWork.VehiculoRepository.Add(vehiculo);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateVehiculo(Vehiculo vehiculo)
        {
            var currentVehiculo = await GetVehiculo(vehiculo.Id);
            currentVehiculo.Precio = vehiculo.Precio;
            currentVehiculo.Descripcion = vehiculo.Descripcion;
            currentVehiculo.Foto = vehiculo.Foto;
            currentVehiculo.Estatus = vehiculo.Estatus ?? true;
            _unitOfWork.VehiculoRepository.Update(currentVehiculo);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehiculo(int id)
        {
            var currentVehiculo = await GetVehiculo(id);
            await _unitOfWork.VehiculoRepository.Delete(currentVehiculo.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task VehiculoValidation(Vehiculo vehiculo)
        {
            var vehiculos = await _unitOfWork.VehiculoRepository.GetAll();
            if (vehiculos.Where(x => x.Matricula == vehiculo.Matricula).Any())
            {
                throw new BussinessException("La matricula ya existe", 400);
            }

            if (vehiculos.Where(x => x.Placa == vehiculo.Placa).Any())
            {
                throw new BussinessException("La Placa ya existe", 400);
            }

            if (await _unitOfWork.MarcaRepository.GetById(vehiculo.IdMarca) == null)
            {
                throw new BussinessException("La marca no existe", 400);
            }

            if (await _unitOfWork.ModeloRepository.GetById(vehiculo.IdModelo) == null)
            {
                throw new BussinessException("El modelo no existe", 400);
            }

            if (await _unitOfWork.CombustibleRepository.GetById(vehiculo.IdCombustible) == null)
            {
                throw new BussinessException("El combustible no existe", 400);
            }

            if (await _unitOfWork.ColorRepository.GetById(vehiculo.IdColor) == null)
            {
                throw new BussinessException("El color no existe", 400);
            }
        }

        public IEnumerable<Vehiculo> GetItemsOrdered(IEnumerable<Vehiculo> vehiculos, ResourceLocation resourceLocation)
        {
            return vehiculos.Select(x => new Vehiculo()
            {
                Id = x.Id,
                Matricula = x.Matricula,
                Placa = x.Placa,
                Precio = x.Precio,
                Puertas = x.Puertas,
                Lanzamiento = x.Lanzamiento,
                Descripcion = x.Descripcion,
                Estatus = x.Estatus,
                IdMarca = x.IdMarca,
                IdModelo = x.IdModelo,
                IdCombustible = x.IdCombustible,
                IdColor = x.IdColor,
                Foto = $"{resourceLocation.Scheme}://{resourceLocation.Host}{resourceLocation.PathBase}/Resources/Vehiculos/{x.Foto}"
            });
        }
    }
}