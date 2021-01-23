using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class ContratoValidator : AbstractValidator<ContratoDTO>
    {
        public ContratoValidator()
        {
            RuleFor(x => x.Concepto).NotNull().NotEmpty().Length(10, 500);
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().Length(10, 250);
            RuleFor(x => x.IdVehiculo).NotNull().NotEmpty();
            RuleFor(x => x.IdCliente).NotNull().NotEmpty();
            RuleFor(x => x.IdUsuario).NotNull().NotEmpty();
            RuleFor(x => x.Estatus).NotNull().NotEmpty().IsInEnum();
        }
    }
}