using System.Threading.Tasks;

namespace DealerApp.Core.Interfaces
{
    public interface IRolValidation
    {
        Task<bool> ValidateRol(int idRol);
    }
}