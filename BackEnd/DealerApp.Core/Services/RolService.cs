using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;

namespace DealerApp.Core.Services
{
    public class RolService : IRolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Rol> _pagedGenerator;
        public RolService(IUnitOfWork unitOfWork, IPagedGenerator<Rol> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Rol>> GetRoles(RolQueryFilter filters)
        {
            var roles = await _unitOfWork.RolRepository.GetAll();
            roles = filters.TipoRol != null ? roles.Where(x => x.TipoRol == filters.TipoRol) : roles;
            roles = filters.Descripcion != null ? roles.Where(x => x.Descripcion.ToLower().Contains(filters.Descripcion.ToLower())) : roles;
            return _pagedGenerator.GeneratePagedList(roles, filters);
        }

        public async Task<Rol> GetRol(int id)
        {
            var currentRol = await _unitOfWork.RolRepository.GetById(id);
            return currentRol ?? throw new BussinessException("El Rol no existe", 404);
        }

        public async Task InsertRol(Rol rol)
        {
            await RolValidation(rol);
            rol.Id = 0;
            rol.Estatus = true;
            await _unitOfWork.RolRepository.Add(rol);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateRol(Rol rol)
        {
            var currentRol = await GetRol(rol.Id);
            currentRol.Descripcion = rol.Descripcion;
            currentRol.Estatus = rol.Estatus ?? true;
            _unitOfWork.RolRepository.Update(currentRol);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRol(int id)
        {
            var currentRol = await GetRol(id);
            await _unitOfWork.RolRepository.Delete(currentRol.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task RolValidation(Rol rol)
        {
            var roles = await _unitOfWork.RolRepository.GetAll();
            if (roles.Where(x => x.TipoRol == rol.TipoRol).Any())
            {
                throw new BussinessException("El rol ya existe", 400);
            }
        }
    }
}