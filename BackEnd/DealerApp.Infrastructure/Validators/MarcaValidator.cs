using System;
using DealerApp.Core.DTOs;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class MarcaValidator : AbstractValidator<MarcaDTO>
    {
        public MarcaValidator()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().Length(3, 30);
            RuleFor(x => x.Lanzamiento).NotNull().NotEmpty().Length(4, 4).LessThan(DateTime.Now.Year.ToString());
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().Length(5, 150);
            RuleFor(x => x.Image).NotNull().NotEmpty();

        }
    }
}