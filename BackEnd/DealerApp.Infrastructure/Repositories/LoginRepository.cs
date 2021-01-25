using System.Threading.Tasks;
using DealerApp.Core.Entities;
using DealerApp.Core.Interfaces;
using DealerApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DealerApp.Infrastructure.Repositories
{
    public class LoginRepository : BaseRepository<Usuario>, ILoginRepository
    {
        public LoginRepository(DealerContext context) : base(context) { }
        public async Task<Usuario> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email == userLogin.Email);
        }
    }
}