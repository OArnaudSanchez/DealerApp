using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class CombustibleValidator : AbstractValidator<CombustibleDTO>
    {
        public CombustibleValidator()
        {
            RuleFor(x => x.TipoCombustible).NotEmpty().NotNull().IsInEnum();
            RuleFor(x => x.Descripcion).NotEmpty().NotNull().Length(5, 150);
        }
    }
}