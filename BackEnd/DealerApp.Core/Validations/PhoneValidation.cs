using System.Text.RegularExpressions;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;

namespace DealerApp.Core.Validations
{
    public class PhoneValidation : IPhoneNumberValidation
    {
        public bool ValidatePhoneNumber(string phoneNumber)
        {
            var test = Regex.IsMatch(phoneNumber, "^[01]?[- .]?(\\([2-9]\\d{2}\\)|[2-9]\\d{2})[- .]?\\d{3}[- .]?\\d{4}$");
            return test ? true : throw new BussinessException("El telefono no tiene un formato valido", 400);
        }
    }
}