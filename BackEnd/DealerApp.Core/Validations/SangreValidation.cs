using System.Threading.Tasks;
using DealerApp.Core.Interfaces;

namespace DealerApp.Core.Validations
{
    public class SangreValidation : ISangreValidation
    {
        private readonly ISangreClienteService _sangreCliente;

        public SangreValidation(ISangreClienteService sangreCliente)
        {
            _sangreCliente = sangreCliente;
        }
        public async Task<bool> ValidateSangre(int idSangre)
        {
            return await _sangreCliente.GetSangreCliente(idSangre) != null ? true : false;
        }
    }
}