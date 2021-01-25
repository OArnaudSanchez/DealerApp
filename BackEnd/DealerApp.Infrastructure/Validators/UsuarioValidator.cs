using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().Length(3, 45);
            RuleFor(x => x.Apellidos).NotNull().NotEmpty().Length(5, 45);
            RuleFor(x => x.Email).NotNull().NotEmpty().Length(10, 30).EmailAddress();
            RuleFor(x => x.Contrasena).NotNull().NotEmpty().Length(8, 15);
            RuleFor(x => x.Image).NotNull().NotEmpty();
            RuleFor(x => x.IdSangre).NotNull().NotEmpty();
            RuleFor(x => x.IdRol).NotNull().NotEmpty();
        }
    }
}