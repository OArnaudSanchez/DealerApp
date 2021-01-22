using System.Threading.Tasks;

namespace DealerApp.Core.Interfaces
{
    public interface ISangreValidation
    {
        Task<bool> ValidateSangre(int idSangre);
    }
}