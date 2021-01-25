using DealerApp.Core.Entities;
using FluentValidation;

namespace DealerApp.Infrastructure.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().Length(10, 30);
            RuleFor(x => x.Password).NotNull().NotEmpty().Length(8, 15);
        }
    }
}