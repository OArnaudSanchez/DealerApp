using System.Text.RegularExpressions;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;

namespace DealerApp.Core.Services
{
    public class DniValidation : IDniValidation
    {
        public bool ValidateDNI(string dni)
        {
            var test = Regex.IsMatch(dni, "^[0-9]{11,11}$");
            return test ? true : throw new BussinessException("El formato del DNI no es valido", 400);
        }
    }
}