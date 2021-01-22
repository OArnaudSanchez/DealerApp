using System;
using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.QueryFilters;
using System.Collections.Generic;
using DealerApp.Core.Enumerations;

namespace DealerApp.Core.Services
{
    public class ClienteService : IClienteService, IOrderItems<Cliente>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Cliente> _pagedGenerator;
        private readonly IEmailValidation _validateEmail;
        private readonly IDniValidation _validateDni;
        private readonly IPhoneNumberValidation _phoneNumberValidation;
        private readonly IFechaValidation _fechaValidation;
        private readonly ISangreValidation _sangreValidation;
        private readonly IRolValidation _rolValidation;

        public ClienteService(IUnitOfWork unitOfWork, IPagedGenerator<Cliente> pagedGenerator, IEmailValidation validateEmail,
                            IDniValidation validateDni, IPhoneNumberValidation phoneNumberValidation, IFechaValidation fechaValidation, ISangreValidation sangreValidation, IRolValidation rolValidation)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
            _validateEmail = validateEmail;
            _validateDni = validateDni;
            _phoneNumberValidation = phoneNumberValidation;
            _fechaValidation = fechaValidation;
            _sangreValidation = sangreValidation;
            _rolValidation = rolValidation;
        }
        public async Task<PagedList<Cliente>> GetClientes(ClienteQueryFilter filters, ResourceLocation resourceLocation)
        {
            var clientes = await _unitOfWork.ClienteRepository.GetAll();

            clientes = filters.Dni != null ? clientes = clientes.Where(x => x.Dni == filters.Dni) : clientes;
            clientes = filters.Nombre != null ? clientes = clientes.Where(x => x.Nombre.ToLower() == filters.Nombre.ToLower()) : clientes;
            clientes = filters.Apellidos != null ? clientes = clientes.Where(x => x.Apellidos.ToLower() == filters.Apellidos.ToLower()) : clientes;
            clientes = filters.Email != null ? clientes = clientes.Where(x => x.Email.ToLower().Contains(filters.Email.ToLower())) : clientes;
            clientes = filters.Telefono != null ? clientes = clientes.Where(x => x.Telefono == filters.Telefono) : clientes;
            clientes = filters.Direccion != null ? clientes = clientes.Where(x => x.Direccion.ToLower().Contains(filters.Direccion.ToLower())) : clientes;
            clientes = filters.Nacimiento != null ? clientes = clientes.Where(x => DateTime.Parse(x.Nacimiento).Year == filters.Nacimiento) : clientes;
            clientes = GetItemsOrdered(clientes, resourceLocation);
            return _pagedGenerator.GeneratePagedList(clientes, filters);
        }

        public async Task<Cliente> GetCliente(int id)
        {
            var cliente = await _unitOfWork.ClienteRepository.GetById(id);
            return cliente ?? throw new BussinessException("Cliente no Encontrado", 404);
        }

        public async Task InsertCliente(Cliente cliente)
        {
            await ClienteValidation(cliente);
            cliente.Id = 0;
            cliente.IdRol = RolType.Cliente;
            cliente.Estatus = true;
            await _unitOfWork.ClienteRepository.Add(cliente);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            var currentCliente = await GetCliente(cliente.Id);
            currentCliente.Foto = cliente.Foto;
            currentCliente.Estatus = cliente.Estatus ?? true;
            _unitOfWork.ClienteRepository.Update(currentCliente);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCliente(int id)
        {
            var currentCliente = await GetCliente(id);
            await _unitOfWork.ClienteRepository.Delete(currentCliente.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task ClienteValidation(Cliente cliente)
        {
            var currentClientes = await _unitOfWork.ClienteRepository.GetAll();
            _phoneNumberValidation.ValidatePhoneNumber(cliente.Telefono);
            _validateEmail.ValidateEmailProveedor(cliente.Email);
            _validateDni.ValidateDNI(cliente.Dni);
            _fechaValidation.ValidateFecha(cliente.Nacimiento);
            await _sangreValidation.ValidateSangre(cliente.IdSangre);

            if (currentClientes.Where(x => x.Dni == cliente.Dni).Any())
            {
                throw new BussinessException("El DNI Ya existe", 400);
            }

            if (currentClientes.Where(x => x.Email.ToLower() == cliente.Email.ToLower()).Any())
            {
                throw new BussinessException("El Email Ya existe", 400);
            }

            if (currentClientes.Where(x => x.Telefono == cliente.Telefono).Any())
            {
                throw new BussinessException("Ya existe el telefono", 400);
            }

            if (DateTime.Parse(cliente.Nacimiento).Year > (DateTime.Now.Year - 18))
            {
                throw new BussinessException("Usted no tiene la edad requerida", 400);
            }
        }

        public IEnumerable<Cliente> GetItemsOrdered(IEnumerable<Cliente> clientes, ResourceLocation resourceLocation)
        {
            return clientes.Select(x => new Cliente()
            {
                Id = x.Id,
                Dni = x.Dni,
                Nombre = x.Nombre,
                Apellidos = x.Apellidos,
                Sexo = x.Sexo,
                Nacimiento = x.Nacimiento,
                Direccion = x.Direccion,
                Email = x.Email,
                Telefono = x.Telefono,
                Estatus = x.Estatus,
                IdRol = x.IdRol,
                IdSangre = x.IdSangre,
                Foto = $"{resourceLocation.Scheme}://{resourceLocation.Host}{resourceLocation.PathBase}/Resources/Clientes/{x.Foto}"
            });
        }
    }
}