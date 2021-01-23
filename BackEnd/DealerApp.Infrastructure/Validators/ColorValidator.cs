using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class ColorValidator : AbstractValidator<ColorDTO>
    {
        public ColorValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().Length(4, 9);
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().Length(15, 50);
        }
    }
}