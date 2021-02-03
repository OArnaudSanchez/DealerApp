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
    
    public class UsuarioService : IUsuarioService, IOrderItems<Usuario>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPagedGenerator<Usuario> _pagedGenerator;
        public UsuarioService(IUnitOfWork unitOfWork, IPagedGenerator<Usuario> pagedGenerator)
        {
            _unitOfWork = unitOfWork;
            _pagedGenerator = pagedGenerator;
        }
        public async Task<PagedList<Usuario>> GetUsuarios(UsuarioQueryFilter filters, ResourceLocation resourceLocation)
        {
            var usuarios = await _unitOfWork.UsuarioRepository.GetAll();
            usuarios = filters.Nombre != null ? usuarios.Where(x => x.Nombre.ToLower() == filters.Nombre.ToLower()) : usuarios;
            usuarios = filters.Apellidos != null ? usuarios.Where(x => x.Apellidos.ToLower() == filters.Apellidos.ToLower()) : usuarios;
            usuarios = filters.Email != null ? usuarios.Where(x => x.Email.Contains(filters.Email.ToLower())) : usuarios;
            usuarios = filters.Creacion != null ? usuarios.Where(x => x.Creacion == filters.Creacion) : usuarios;
            return _pagedGenerator.GeneratePagedList(GetItemsOrdered(usuarios, resourceLocation), filters);
        }

        public async Task<Usuario> GetUsuario(int id)
        {
            var currentUsuario = await _unitOfWork.UsuarioRepository.GetById(id);
            return currentUsuario ?? throw new BussinessException("Usuario no Encontrado", 404);
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            var currentUsuario = await _unitOfWork.UsuarioRepository.GetById(id);
            await _unitOfWork.UsuarioRepository.Delete(currentUsuario.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Usuario> GetItemsOrdered(IEnumerable<Usuario> usuarios, ResourceLocation resourceLocation)
        {
            return usuarios.Select(x => new Usuario()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellidos = x.Apellidos,
                Email = x.Email,
                Creacion = x.Creacion,
                Estatus = x.Estatus,
                IdSangre = x.IdSangre,
                IdRol = x.IdRol,
                Foto = $"{resourceLocation.Scheme}://{resourceLocation.Host}{resourceLocation.PathBase}/wwwroot/Resources/Usuarios/{x.Foto}"
            });
        }

    }
}