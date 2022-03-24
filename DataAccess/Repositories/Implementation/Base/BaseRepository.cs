using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Base;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implementation.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly BlogContext _context;
        protected readonly ILogger _logger;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(BlogContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync((int) id);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task CreateAsync(T entity) => await _dbSet.AddAsync(entity);

        public virtual async Task DeleteAsync(T entity)
        {
            var exist = await _dbSet.FindAsync(entity);
            if (exist != null)
                _dbSet.Remove(exist);
        }

        public virtual async Task UpdateAsync(int id, T entity)
        {
            var exist = await _dbSet.FindAsync(id);
            if (exist != null)
                _dbSet.Update(exist);
        }
    }
}