using FluentValidation;
using DealerApp.Core.DTOs;

namespace DealerApp.Infrastructure.Validators
{
    public class ClienteValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Dni).NotNull().NotEmpty().Length(11,11);
            RuleFor(x => x.Nombre).NotNull().NotEmpty().Length(3, 45);
            RuleFor(x => x.Apellidos).NotNull().NotEmpty().Length(5, 45);
            RuleFor(x => x.Sexo).NotNull().NotEmpty().IsInEnum();
            RuleFor(x => x.Nacimiento).NotNull().NotEmpty();
            RuleFor(x => x.Direccion).NotNull().NotEmpty().Length(5, 150);
            RuleFor(x => x.Email).NotNull().NotEmpty().Length(10, 30).EmailAddress();
            RuleFor(x => x.Telefono).NotNull().NotEmpty().Length(10, 10);
            RuleFor(x => x.Image).NotNull().NotEmpty();
            RuleFor(x => x.IdSangre).NotNull().NotEmpty();
        }
    }
}