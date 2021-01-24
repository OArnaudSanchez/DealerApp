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
    public class SangreClienteService : ISangreClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<SangreCliente> _pagedGenerator;
        public SangreClienteService(IUnitOfWork unitOfWork, IPagedGenerator<SangreCliente> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<SangreCliente>> GetSangreClientes(SangreClienteQueryFilter filters)
        {
            var sangresListado = await _unitOfWork.SangreClienteRepository.GetAll();
            sangresListado = filters.Tipo != null ? sangresListado.Where(x => x.TipoSangre.ToLower().Contains(filters.Tipo.ToLower())) : sangresListado;
            sangresListado = filters.Descripcion != null ? sangresListado.Where(x => x.Descripcion.ToLower().Contains(filters.Descripcion.ToLower())) : sangresListado;
            sangresListado = filters.Estatus != null ? sangresListado.Where(x => x.Estatus == filters.Estatus) : sangresListado;
            return _pagedGenerator.GeneratePagedList(sangresListado, filters);
        }

        public async Task<SangreCliente> GetSangreCliente(int id)
        {
            var currentSangre = await _unitOfWork.SangreClienteRepository.GetById(id);
            return currentSangre ?? throw new BussinessException("La sangre no existe", 404);
        }

        public async Task InsertSangreCliente(SangreCliente sangreCliente)
        {
            await SangreClienteValidation(sangreCliente);
            sangreCliente.Id = 0;
            sangreCliente.TipoSangre = sangreCliente.TipoSangre.Trim().ToUpper();
            sangreCliente.Estatus = true;
            await _unitOfWork.SangreClienteRepository.Add(sangreCliente);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateSangreCliente(SangreCliente sangreCliente)
        {
            var currentSangre = await GetSangreCliente(sangreCliente.Id);
            currentSangre.Descripcion = sangreCliente.Descripcion;
            currentSangre.Estatus = sangreCliente.Estatus ?? true;
            _unitOfWork.SangreClienteRepository.Update(currentSangre);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSangreCliente(int id)
        {
            var currentSangre = await GetSangreCliente(id);
            await _unitOfWork.SangreClienteRepository.Delete(currentSangre.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task SangreClienteValidation(SangreCliente sangre)
        {
            var sangres = await _unitOfWork.SangreClienteRepository.GetAll();
            if (sangres.Where(x => x.TipoSangre.ToLower().Trim() == sangre.TipoSangre.ToLower().Trim()).Any())
            {
                throw new BussinessException("La Sangre ya existe", 400);
            }

            var tiposSangres = new List<string> {"A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"};
            if(!tiposSangres.Contains(sangre.TipoSangre.ToUpper()))
            {
                throw new BussinessException("Solo se aceptan tipos: A+, A-, B+, B-, AB+, AB-, O+, O-", 400);
            }
        }
    }
}