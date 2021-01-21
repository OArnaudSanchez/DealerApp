using System.Collections.Generic;
using System.Threading.Tasks;
using DealerApp.Core.Common;
using DealerApp.Core.Interfaces;
using DealerApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DealerApp.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DealerContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(DealerContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Add(T Entity)
        {
            await _entities.AddAsync(Entity);
        }

        public bool Update(T Entity)
        {
            _entities.Update(Entity);
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            T currentEntity = await GetById(id);
            _entities.Remove(currentEntity);
            return true;
        }
    }
}