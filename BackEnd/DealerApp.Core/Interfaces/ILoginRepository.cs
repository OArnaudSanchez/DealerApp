using System.Threading.Tasks;
using DealerApp.Core.Entities;

namespace DealerApp.Core.Interfaces
{
    public interface ILoginRepository : IRepository<Usuario>
    {
        Task<Usuario> GetLoginByCredentials(UserLogin userLogin);
    }
}