using System.Threading.Tasks;
using DealerApp.Core.Interfaces;

namespace DealerApp.Core.Validations
{
    public class RolValidation : IRolValidation
    {
        private readonly IRolService _rolService;
        public RolValidation(IRolService rolService)
        {
            _rolService = rolService;
        }
        public async Task<bool> ValidateRol(int idRol)
        {
            return await _rolService.GetRol(idRol) != null ? true : false;
        }
    }
}