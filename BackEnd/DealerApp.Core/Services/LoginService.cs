using System;
using System.Linq;
using System.Threading.Tasks;
using DealerApp.Core.Entities;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;

namespace DealerApp.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRolValidation _rolValidation;
        private readonly ISangreValidation _sangreValidation;
        private readonly IEmailValidation _emailValidation;
        public LoginService(IUnitOfWork unitOfWork, IEmailValidation emailValidation, IRolValidation rolValidation, ISangreValidation sangreValidation)
        {
            _unitOfWork = unitOfWork;
            _rolValidation = rolValidation;
            _sangreValidation = sangreValidation;
            _emailValidation = emailValidation;
        }
        public async Task<Usuario> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _unitOfWork.LoginRepository.GetLoginByCredentials(userLogin);
        }

        public async Task RegisterUser(Usuario usuario)
        {
            await UsuarioValidation(usuario);
            usuario.Id = 0;
            usuario.Creacion = DateTime.Now.ToShortDateString();
            usuario.Estatus = true;
            await _unitOfWork.UsuarioRepository.Add(usuario);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UsuarioValidation(Usuario usuario)
        {
            _emailValidation.ValidateEmailProveedor(usuario.Email);
            await _rolValidation.ValidateRol((int)usuario.IdRol);
            await _sangreValidation.ValidateSangre(usuario.IdSangre);
            var usuarios = await _unitOfWork.UsuarioRepository.GetAll();
            if (usuarios.Where(x => x.Email.ToLower() == usuario.Email.ToLower()).Any())
            {
                throw new BussinessException("El usuario ya existe", 400);
            }
        }
    }
}