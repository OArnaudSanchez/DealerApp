using System;
using DealerApp.Core.Exceptions;
using DealerApp.Core.Interfaces;

namespace DealerApp.Core.Validations
{
    public class FechaValidation : IFechaValidation
    {
        public bool ValidateFecha(string fecha)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParse(fecha, out dateTime);
            return isValid ? true : throw new BussinessException("El formato de la fecha no es valido", 400);
        }
    }
}