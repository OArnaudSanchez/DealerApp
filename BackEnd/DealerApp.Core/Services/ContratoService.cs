using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Services
{
    public class ContratoService : IContratoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Contrato> _pagedGenerator;
        public ContratoService(IUnitOfWork unitOfWork, IPagedGenerator<Contrato> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Contrato>> GetContratos(ContratoQueryFilter filters)
        {
            var contratos = await _unitOfWork.ContratoRepository.GetAll();
            contratos = filters.Concepto != null ? contratos.Where(x=>x.Concepto.ToLower().Contains(filters.Concepto.ToLower())) : contratos;
            contratos = filters.Descripcion != null ? contratos.Where(x=>x.Descripcion.ToLower().Contains(filters.Descripcion.ToLower())) : contratos;
            contratos = filters.Estatus != null ? contratos.Where(x=>x.Estatus == filters.Estatus) : contratos;
            contratos = filters.Cliente != null ? contratos.Where(x=>x.IdCliente == filters.Cliente) : contratos;
            contratos = filters.Usuario != null ? contratos.Where(x=>x.IdUsuario == filters.Usuario) : contratos;
            contratos = filters.Vehiculo != null ? contratos.Where(x=>x.IdVehiculo == filters.Vehiculo) : contratos;
            return _pagedGenerator.GeneratePagedList(contratos, filters);
        }
        public async Task<Contrato> GetContrato(int id)
        {
            var currentContrato = await _unitOfWork.ContratoRepository.GetById(id);
            return currentContrato ?? throw new BussinessException("El contrato no fue encontrado", 404);
        }

        public async Task InsertContrato(Contrato contrato)
        {
            await ContratoValidation(contrato);
            contrato.Id = 0;
            await _unitOfWork.ContratoRepository.Add(contrato);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateContrato(Contrato contrato)
        {
            var currentContrato = await GetContrato(contrato.Id);
            currentContrato.Descripcion = contrato.Descripcion;
            currentContrato.Estatus = contrato.Estatus;
            _unitOfWork.ContratoRepository.Update(currentContrato);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteContrato(int id)
        {
            var currentContrato = await GetContrato(id);
            await _unitOfWork.ContratoRepository.Delete(currentContrato.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task ContratoValidation(Contrato contrato)
        {
            if (await _unitOfWork.ClienteRepository.GetById(contrato.IdCliente) == null)
            {
                throw new BussinessException("El Cliente no existe", 404);
            }

            if (await _unitOfWork.VehiculoRepository.GetById(contrato.IdVehiculo) == null)
            {
                throw new BussinessException("El Vehiculo no existe", 404);
            }

            if (await _unitOfWork.UsuarioRepository.GetById(contrato.IdUsuario) == null)
            {
                throw new BussinessException("El Usuario no existe", 404);
            }

            var contratos = await _unitOfWork.ContratoRepository.GetAll();
            if(contratos.Where(x=>x.IdVehiculo == contrato.IdVehiculo).Any())
            {
                throw new BussinessException("El vehiculo ya esta asignado a un contrato", 400);
            }
        }
    }
}