using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class SangreClienteValidator : AbstractValidator<SangreClienteDTO>
    {
        public SangreClienteValidator()
        {
            RuleFor(x => x.TipoSangre).NotEmpty().NotNull().Length(2, 3);
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().Length(10, 50);
        }
    }
}