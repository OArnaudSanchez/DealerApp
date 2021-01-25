using System;
using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class VehiculoValidator : AbstractValidator<VehiculoDTO>
    {
        public VehiculoValidator()
        {
            RuleFor(x => x.Matricula).NotNull().NotEmpty().Length(5, 15);
            RuleFor(x => x.Placa).NotNull().NotEmpty().Length(5, 15);
            RuleFor(x => x.Precio).NotNull().NotEmpty().GreaterThan(1000);
            RuleFor(x => x.Puertas).NotNull().NotEmpty().Length(1,1).InclusiveBetween(2.ToString(),6.ToString());
            RuleFor(x => x.Lanzamiento).NotNull().NotEmpty().Length(4, 4).LessThanOrEqualTo(DateTime.Now.Year.ToString());
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().Length(10, 150);
            RuleFor(x => x.IdMarca).NotNull().NotEmpty();
            RuleFor(x => x.IdModelo).NotNull().NotEmpty();
            RuleFor(x => x.IdCombustible).NotNull().NotEmpty();
            RuleFor(x => x.IdColor).NotNull().NotEmpty();
            RuleFor(x => x.Image).NotNull().NotEmpty();
        }
    }
}