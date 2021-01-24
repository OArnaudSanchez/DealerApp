using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class ModeloValidator : AbstractValidator<ModeloDTO>
    {
        public ModeloValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().Length(3, 30);
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().Length(10, 130);
        }
    }
}