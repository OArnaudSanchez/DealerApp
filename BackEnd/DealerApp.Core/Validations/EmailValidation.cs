using System;
using System.Text.RegularExpressions;
using DealerApp.Core.Enumerations;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;

namespace DealerApp.Core.Services
{
    public class EmailValidation : IEmailValidation
    {
        public bool ValidateEmailProveedor(string email)
        {
            var serviciosCorreos = Enum.GetNames(typeof(EmailType));
            var result = email.ToLower().Contains(serviciosCorreos[0].ToLower()) || email.ToLower().Contains(serviciosCorreos[1].ToLower());
            return result ? true : throw new BussinessException("Solo se permiten correos de Gmail o Outlook", 400);
        }
    }
}