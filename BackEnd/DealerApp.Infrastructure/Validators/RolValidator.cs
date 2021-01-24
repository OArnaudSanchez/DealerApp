using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class RolValidator : AbstractValidator<RolDTO>
    {
        public RolValidator()
        {
            RuleFor(x => x.TipoRol).NotNull().NotEmpty().IsInEnum();
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().Length(10, 50);
        }
    }
}