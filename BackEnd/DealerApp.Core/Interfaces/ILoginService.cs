using System.Threading.Tasks;
using DealerApp.Core.Entities;

namespace DealerApp.Core.Interfaces
{
    public interface ILoginService
    {
        Task<Usuario> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Usuario usuario);
    }
}